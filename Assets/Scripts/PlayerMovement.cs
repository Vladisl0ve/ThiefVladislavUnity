using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horInput * speed, body.velocity.y);

        //Player flipping while moving
        if (horInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        if (Input.GetKey(KeyCode.Space) && grounded)
            Jump();

        //Set animator parameters
        anim.SetBool("running", horInput != 0);
        anim.SetBool("grounded", grounded);

    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, speed);
        anim.SetTrigger("jump");
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }
}
