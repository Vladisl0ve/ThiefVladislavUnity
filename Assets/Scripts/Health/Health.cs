using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float startHealth;
    public float currentHealth { get; private set; }
    bool IsDead;
    Animator anim;

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!IsDead)
            {
                anim.SetTrigger("die");
                GetComponent<PlayerMovement>().enabled = false;
                IsDead = true;
            }

        }
    }

    public void AddHealth(float toAdd)
    {
        currentHealth = Mathf.Clamp(currentHealth + toAdd, 0, startHealth);
    }
}
