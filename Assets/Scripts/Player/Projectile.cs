using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float dir; //direction
    private bool IsHit;
    [SerializeField] private float maxLifetime;
    private float lifetime;

    private Animator anim;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (IsHit)
            return;

        float movespeed = Time.deltaTime * speed * dir;
        transform.Translate(movespeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > maxLifetime)
            gameObject.SetActive(false);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsHit = true;
        boxCollider.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float direction)
    {
        lifetime = 0;
        dir = direction;
        gameObject.SetActive(true);
        IsHit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
