using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    public float speed;
    private bool isGrounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(speed * horizontalInput, body.velocity.y);

        //changing the orientation of the character according to the direction
        //he is moving to
        if(horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(2,2,2);
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-2,2,2);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            jump();
        }


        //=================================================================================== ANIMATION BOOLEANS =================================================================

        //should have the same name for argument as the name of the variable
        //created in the animetor. Instead of explixitly declearing a bool value
        // we put a statement that checks wethather the character is in motion
        anim.SetBool("run", horizontalInput != 0);
        //setting the boolean in the engine to the current state
        anim.SetBool("isGrounded", isGrounded);
    }

    //=================================================================================== FUNCTIONS =================================================================
    private void jump()
    {
        //keeping the orientation and speed of the character the same as
        //before pressing the jump button
        body.velocity = new Vector2(body.velocity.x, speed);
        //changing the state of the character to not grounded
        isGrounded = false;
    }

    private void onCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
