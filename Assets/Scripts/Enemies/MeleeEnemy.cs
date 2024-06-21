using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header("Player layer")]
    [SerializeField]private LayerMask playerLayer;
    private float cooldownTimer;

    [Header("Attack parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private int damage;
    [SerializeField]private float range;

    [Header("Collider parameters")]
    [SerializeField]private float colliderDistance;
    [SerializeField]private BoxCollider2D boxCollider;

    //references
    private Animator anim;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown && playerInSight())
        {
            cooldownTimer = 0;
            anim.SetTrigger("meleeAttack");
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !playerInSight();
        }
    }

    private bool playerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        , 0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void damagePlayer()
    {
        if (playerInSight())
        {
            playerHealth.takeDamage(damage);
        }
    }
}
