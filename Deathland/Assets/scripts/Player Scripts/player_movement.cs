using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpPower;
    private float wallJumpCooldown = 1;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
       


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

        //wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            if (Input.GetKey(KeyCode.Space) && isGrounded())
            {
                jump();
            }

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 3;
            }
        }

        

        //=================================================================================== ANIMATION BOOLEANS =================================================================

        //should have the same name for argument as the name of the variable
        //created in the animetor. Instead of explixitly declearing a bool value
        // we put a statement that checks wethather the character is in motion
        anim.SetBool("run", horizontalInput != 0);
        //setting the boolean in the engine to the current state
        anim.SetBool("isGrounded", isGrounded());

        
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    //=================================================================================== FUNCTIONS =================================================================
    private void jump()
    {
        if (isGrounded())
        {

            //keeping the orientation and speed of the character the same as
            //before pressing the jump button
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            wallJumpCooldown = 0;
            body.velocity = new Vector2(-Mathf.Sign(transform.localPosition.x) * 3, jumpPower);
        }
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.01f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.01f, wallLayer);
        return raycastHit.collider != null;
    }

}
