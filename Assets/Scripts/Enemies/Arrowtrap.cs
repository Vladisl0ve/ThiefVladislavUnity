using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    [SerializeField] float attackCD;
    [SerializeField] Transform firePoint;
    [SerializeField] GameObject[] arrows;
    float cdTimer;

    private void Attack()
    {
        cdTimer = 0;

        arrows[FindArrow()].transform.position = firePoint.position;
        arrows[FindArrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }
    private int FindArrow()
    {
        for (int i = 0; i < arrows.Length; i++)
        {
            if (!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cdTimer += Time.deltaTime;

        if (cdTimer >= attackCD)
        {
            Attack();
        }
    }
}
