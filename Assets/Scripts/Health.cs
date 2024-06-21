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
            disableMovement();

            anim.SetTrigger("Death");
        }
    }

    public void takeDamage(int damageTaken)
    {
        startingHealth -= damageTaken;
        anim.SetTrigger("Hurt");
        Debug.Log("Damage taken");
    }

    private void disableMovement() //disable movement for enemy/player
    {
            if(GetComponent<PlayerMovement>() != null)
                GetComponent<PlayerMovement>().enabled = false;

            if(GetComponentInParent<EnemyPatrol>() != null)
                GetComponentInParent<EnemyPatrol>().enabled = false;

            if(GetComponent<MeleeEnemy>() != null)
                GetComponent<MeleeEnemy>().enabled = false;
    }
}
