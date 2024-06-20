using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth;

    [SerializeField] private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (startingHealth <= 0)
        {
            if(GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = false;

            if(GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = false;
            
            anim.SetTrigger("Death");
        }
    }

    public void takeDamage(int damageTaken)
    {
        startingHealth -= damageTaken;
        anim.SetTrigger("Hurt");
        Debug.Log("Damage taken");
    }
}
