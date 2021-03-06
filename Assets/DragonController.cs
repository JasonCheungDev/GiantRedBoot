﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {

    private Animator anim;
    private Transform animatedHead;
    private Transform animatedBody;
    private Transform animatedLeftArm;
    private Transform animatedRightArm;
    private Transform freeLimbs, head, body, leftArm, rightArm;

    public GameObject FireProjectilePrefab;


	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        animatedHead = transform.Find("Dragon Head");
        animatedBody = transform.Find("Dragon Body");
        animatedLeftArm = transform.Find("Dragon Left Arm");
        animatedRightArm = transform.Find("Dragon Right Arm");

        freeLimbs = transform.Find("LimbsCopy");
        head = freeLimbs.Find("Dragon Head");
        body = freeLimbs.Find("Dragon Body");
        leftArm = freeLimbs.Find("Dragon Left Arm");
        rightArm = freeLimbs.Find("Dragon Right Arm");

        anim.SetBool("LeftSwipe", true);
    }

    // Update is called once per frame
    void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetBool("LeftSwipe", true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(FireBreathAttack());
        }
	}

    public void AnimEvent_ShakeCamera()
    {
        var shaker = Camera.main.GetComponent<Shaker>();
        shaker.ShakeObject(2f, 0.5f);
    }

    public void AnimEvent_FireBreath()
    {
        StartCoroutine(FireBreathAttack());
    }


    IEnumerator FireBreathAttack()
    {
        // Disable animated limbs 
        ShowAnimated(false);

        // Show free head and arm only.  
        CopyFrame();
        head.gameObject.SetActive(true);
        leftArm.gameObject.SetActive(true);

        // Calculate head position and show 
        var headLoc = GetRandomHeadPos();
        var offMapPos = headLoc.position + headLoc.direction * 2f;
        var finalPos = headLoc.position + headLoc.direction * -1f;
        head.position = offMapPos;
        head.rotation = headLoc.rotation;
        head.gameObject.SetActive(true);

        // Move head towards position
        float elapsedTime = 0;
        float time = 1f;        // overall duration
        while (elapsedTime < time)
        {
            head.position = Vector3.Slerp(offMapPos, finalPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // pause for player to hide 
        yield return new WaitForSeconds(2f);

        // BLAST em 
        float fireRate = 0.2f;
        float fireDuration = 3f;
        elapsedTime = 0f;
        while (elapsedTime < fireDuration)
        {
            CastRowOfFire(finalPos, -headLoc.direction, headLoc.rotation);
            elapsedTime += fireRate;
            yield return new WaitForSeconds(fireRate);
        }

        // Move head off map
        elapsedTime = 0f;
        while (elapsedTime < time)
        {
            head.position = Vector3.Slerp(finalPos, offMapPos, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // finish animation
        ShowCopy(false);
        ShowAnimated(true);
        anim.SetBool("LeftSwipe", false);
    }

    void CastRowOfFire(Vector2 origin, Vector2 direction, Quaternion bulletRot)
    {
        var perpendicular = new Vector2(direction.y, -direction.x).normalized;

        for (float i = -10; i < 10; i += 1f)
            Instantiate(FireProjectilePrefab, origin + perpendicular * i, bulletRot); 
    }

    void CopyFrame()
    {
        // Copies the current location of the animated limbs to the free limbs 
        head.Copy(animatedHead);
        body.Copy(animatedBody);
        leftArm.Copy(animatedLeftArm);
        rightArm.Copy(animatedRightArm);
    }

    void ShowCopy(bool visible)
    {
        head.gameObject.SetActive(visible);
        body.gameObject.SetActive(visible);
        leftArm.gameObject.SetActive(visible);
        rightArm.gameObject.SetActive(visible);
    }

    void ShowAnimated(bool visible)
    {
        animatedHead.gameObject.SetActive(visible);
        animatedBody.gameObject.SetActive(visible);
        animatedLeftArm.gameObject.SetActive(visible);
        animatedRightArm.gameObject.SetActive(visible);
    }

    PositionRotation GetRandomHeadPos()
    {
        // Get bounds of visible map
        var bounds = Camera.main.OrthographicBounds();

        // get random direction from center of map 
        Vector3 dir = Quaternion.Euler(0, 0, Random.Range(0f, 180f)) * Vector3.right;
        Debug.Log(dir);
        Ray ray = new Ray(Vector3.zero, dir);

        // get intersection point on bounds 
        float intersectDist = 0f;
        bounds.IntersectRay(ray, out intersectDist);    // distance is negative if inside bounds (guessing) 

        return new PositionRotation(dir * -intersectDist, dir); 
    }
}

public static class CameraExtensions
{
    public static Bounds OrthographicBounds(this Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            Vector3.zero,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}

public static class TransformExtensions
{
    public static void Copy(this Transform transform, Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        transform.localScale = target.localScale;
    }
}

public struct PositionRotation
{
    public Vector2 direction;
    public Vector2 position;
    // lol 
    // http://answers.unity3d.com/questions/654222/make-sprite-look-at-vector2-in-unity-2d-1.html
    public Quaternion rotation
    {
        get
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        }
    }

    public PositionRotation(Vector2 position, Vector2 direction)
    {
        this.position = position;
        this.direction = direction;
    }
}