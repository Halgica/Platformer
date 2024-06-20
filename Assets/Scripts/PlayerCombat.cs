using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{

    //components
    private Animator myAnimator;
    private Health playerHealth;

    //variables
    private float attackCooldown = 0.3f;
    private float health = 1;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            myAnimator.SetBool("isDead", true);
        }

        attackCooldown -= Time.deltaTime;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && attackCooldown < 0f)
        {
            myAnimator.SetTrigger("Attack");
            attackCooldown = 1f;
            
        }
    }
}
