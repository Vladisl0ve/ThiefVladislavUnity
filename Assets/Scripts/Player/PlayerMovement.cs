using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float wallResponsePower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private Health playerHealth;
    private float walljumpCD; //cooldown
    private float horInput;

    public float gravityScale;


    private void Awake()
    {
        //Grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        horInput = Input.GetAxis("Horizontal");


        //Player flipping while moving
        if (horInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator parameters
        anim.SetBool("running", horInput != 0);
        anim.SetBool("grounded", IsGrounded());

        //Wall-Jump logic
        if (walljumpCD > 0.2f)
        {
            body.velocity = new Vector2(horInput * speed, body.velocity.y);

            if (IsOnWall() && !IsGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = gravityScale;

            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        else
            walljumpCD += Time.deltaTime;

    }

    private void Jump()
    {
        if (IsGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            anim.SetTrigger("jump");
        }
        else if (IsOnWall() && !IsGrounded())
        {
            if (horInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * gravityScale * wallResponsePower, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * gravityScale, wallResponsePower);

            walljumpCD = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsOnWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool CanAttack()
    {
        return !playerHealth.IsDead;
    }
}
