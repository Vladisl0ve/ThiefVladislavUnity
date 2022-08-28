using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] float startHealth;
    public float currentHealth { get; private set; }
    public bool IsDead;
    Animator anim;

    [Header("iFrames")]
    [SerializeField] float iFrameDuration;
    [SerializeField] int numerOfFlashes;
    SpriteRenderer spriteRend;

    private void Awake()
    {
        currentHealth = startHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float dmg)
    {
        currentHealth = Mathf.Clamp(currentHealth - dmg, 0, startHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
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

    IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        for (int i = 0; i < numerOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFrameDuration / (numerOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
    }
}
