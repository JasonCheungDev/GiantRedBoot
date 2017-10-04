using UnityEngine;

public class MovementController : MonoBehaviour {

    public float speed = 5.0f;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float xVel = Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime;
        float yVel = Input.GetAxisRaw("Vertical") * speed * Time.deltaTime;

        if (xVel > 0) //-x = left
        {
            anim.Play("WalkRight");
        }else if(xVel < 0)
        {
            anim.Play("WalkLeft");
        }
        else
        {
            anim.Play("girlIdle");
        }
        
        transform.Translate(xVel, yVel, 0);

        if (Input.GetButtonDown("Fire1"))
        {
            ClickTransform();
        }
    }

    void ClickTransform()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
       if (hit.collider != null)
       {
            print(hit.collider.name);
            //print(mousePos.x + " y: " + mousePos.y);
            //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
        }
    }
}