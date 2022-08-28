using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead attributes")]
    [SerializeField] float speed;
    [SerializeField] float range;
    [SerializeField] float checkDelay;
    [SerializeField] LayerMask playerLayer;

    bool isAttacking;
    float checkTimer;
    Vector3 destination;
    Vector3[] directions = new Vector3[4];


    private void Update()
    {
        if (isAttacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
                CheckForPlayer();
        }
    }
    void CheckForPlayer()
    {
        CalculateDirections();
        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !isAttacking)
            {
                isAttacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    void CalculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }
    void Stop()
    {
        destination = transform.position;
        isAttacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        Stop();
    }
    private void OnEnable()
    {
        Stop();
    }
}
