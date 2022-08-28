using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [SerializeField] float damage;

    [Header("Fretrap Timers")]
    [SerializeField] float activationDelay;
    [SerializeField] float activeTime;
    Animator anim;
    SpriteRenderer spriteRend;

    bool isTriggered, isActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!isTriggered)
                StartCoroutine(ActivateFiretrap());


            if (isActive)
                collision.GetComponent<Health>().TakeDamage(damage);
        }
    }

    IEnumerator ActivateFiretrap()
    {
        //activating
        isTriggered = true;
        spriteRend.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;
        isActive = true;
        anim.SetBool("activated", true);

        //deactivating
        yield return new WaitForSeconds(activeTime);
        isActive = false;
        isTriggered = false;
        anim.SetBool("activated", false);
    }
}
