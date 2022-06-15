using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject1 : MonoBehaviour
{
    Rigidbody2D rb;
    private float moveSpeed;

    private void Start()
    {
        moveSpeed = Screen.width / 180;
        
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        TouchMove();
        
    }
    
    void TouchMove() 
    {
        if (Input.GetMouseButton(0))
        {
     
            var mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(mousePos);

            if (touchPos.x < 0)
            {
                    rb.velocity = Vector2.left * moveSpeed;
                
            }
            else
            {
              
                    rb.velocity = Vector2.right * moveSpeed;
               
            }
            
        }
        else 
        {
            rb.velocity = Vector2.zero;
        }
    }
}
