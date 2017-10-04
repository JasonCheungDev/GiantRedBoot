using UnityEngine;

public class MovementController : MonoBehaviour {

    public float speed = 5.0f;

    void Update()
    {
        transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, 0);
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

//float xVel = Input.GetAxisRaw("Horizontal");
//float yVel = Input.GetAxisRaw("Vertical");