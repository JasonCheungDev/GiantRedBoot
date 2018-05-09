using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragonController : MonoBehaviour {

    private Animator anim;
    private Transform animatedHead;
    private Transform animatedBody;
    private Transform animatedLeftArm;
    private Transform animatedRightArm;
    private Transform freeLimbs, head, body, leftArm, rightArm;

    public GameObject FireProjectilePrefab;
    public GameObject FirebreathPattern;        // shot pattern used in fire breath attack
    public GameObject LeftRightPattern;         // shot pattern used in head left to right attack
    public GameObject SwingPattern;             // shot pattern used in swing attack

    public AudioSource fireshotSfx;
    public AudioSource boomSfx; 

    private Coroutine firebreathCoroutine;
    private bool isFirebreathOn = false;

    ///////////////////////

    public ActionTimePair[] events;

    public int currentEventIndex;
    private float currentTime;
    private bool switchingShots;


    // Use this for initialization
    void Awake ()
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
    }

    void Start()
    {
        switchingShots = false;

        var leftRightAnimState = GetComponent<Animator>().GetBehaviour<SimpleStatemachineBehaviour>("DragonHeadLeftRightState");
        leftRightAnimState.OnStateEntered += () =>
        {
            transform.FindDeepChild("HeadLeftRightShotPattern").gameObject.SetActive(true);
        };
        leftRightAnimState.OnStateExited += () =>
        {
            transform.FindDeepChild("HeadLeftRightShotPattern").gameObject.SetActive(false);
        };

        currentEventIndex = -1;
    }

    void OnDisable()
    {
        anim.SetBool("LeftSwipe", false);
        anim.SetBool("Slam", false);
        anim.SetBool("LeftSwing", false);
        anim.SetBool("Death", false);
    }

    // Update is called once per frame
    void Update ()
    {
        if (currentTime <= 0.0f)
        {
            IncrementShotPattern();

            events[currentEventIndex].events.Invoke();

            currentTime = events[currentEventIndex].delay + currentTime;    // add current time to keep on a strict schedule
        }

        currentTime -= Time.deltaTime;
    }

    private void IncrementShotPattern()
    {
        if (currentEventIndex != events.Length - 1)
            currentEventIndex++;
        else
            currentEventIndex = 0;
    }

    public void SetEventIndex(int index)
    {
        currentEventIndex = index;
    }


    public void AnimEvent_ShakeCamera()
    {
        var shaker = Camera.main.GetComponent<Shaker>();
        shaker.ShakeObject(2f, 0.5f);
    }

    public void AnimEvent_MinorShakeCamera()
    {
        var shaker = Camera.main.GetComponent<Shaker>();
        shaker.ShakeObject(1f, 0.3f);
    }

    public void AnimEvent_PrepareFirebreath()
    {
        anim.SetFloat("HeadLeftRightRatio", UnityEngine.Random.Range(0f, 1f));
    }

    public void AnimEvent_ActivateObject(string objectName)
    {
        transform.FindDeepChild(objectName).gameObject.SetActive(true);
    }

    public void AnimEvent_DeactivateObject(string objectName)
    {
        transform.FindDeepChild(objectName).gameObject.SetActive(false);
    }

    public void AnimEvent_DisableLimb(string name)
    {
        switch (name)
        {
            case "head":
                animatedHead.gameObject.SetActive(false);
                break;
            case "body":
                animatedBody.gameObject.SetActive(false);
                break;
            case "left arm":
                animatedLeftArm.gameObject.SetActive(false);
                break;
            case "right arm":
                animatedRightArm.gameObject.SetActive(false);
                break;
        }
    }

    public void AnimEvent_EnableLimb(string name)
    {
        switch (name)
        {
            case "head":
                animatedHead.gameObject.SetActive(true);
                break;
            case "body":
                animatedBody.gameObject.SetActive(true);
                break;
            case "left arm":
                animatedLeftArm.gameObject.SetActive(true);
                break;
            case "right arm":
                animatedRightArm.gameObject.SetActive(true);
                break;
        }
    }

    public void AnimEvent_SmokeBreath(int on)
    {
        if (on == 0)
            animatedHead.GetComponentInChildren<ParticleSystem>().Stop();
        else
            animatedHead.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void Action_StartAnimation(string name)
    {
        anim.SetBool(name, true);
    }

    public void Action_EndAnimation(string name)
    {
        anim.SetBool(name, false);
    }

    public void Action_Flip()
    {
        var scale = transform.localScale;
        scale.x = -scale.x;
        transform.localScale = scale;
    }
}

//public static class CameraExtensions
//{
//    public static Bounds OrthographicBounds(this Camera camera)
//    {
//        float screenAspect = (float)Screen.width / (float)Screen.height;
//        float cameraHeight = camera.orthographicSize * 2;
//        Bounds bounds = new Bounds(
//            Vector3.zero,
//            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
//        return bounds;
//    }
//}

public static class TransformExtensions
{
    public static void Copy(this Transform transform, Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
        transform.localScale = target.localScale;
    }

    public static void FlipX(this Transform transform)
    {
        var scale = transform.localScale;   // get current scale
        scale.x = -scale.x;
        transform.localScale = scale;       // apply flipped scale (can't change x directly)
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

[Serializable]
public class ActionTimePair
{
    public UnityEvent events;
    public float delay;
}