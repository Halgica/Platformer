using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    //components
    private Animator myAnimator;
    private Health enemyHealth;

    //variables
    [Header("Enemy layer")]
    [SerializeField]private LayerMask enemyLayer;

    [Header("Attack parameters")]
    [SerializeField]private float attackCooldown;
    [SerializeField]private int damage;
    [SerializeField]private float range;

    [Header("Collider parameters")]
    [SerializeField]private float colliderDistance;
    [SerializeField]private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    private bool enemyInRange()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        , 0, Vector2.left, 0, enemyLayer);

        if (hit.collider != null)
        {
            enemyHealth = hit.transform.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed && attackCooldown < 0f)
        {
            myAnimator.SetTrigger("Attack");
            attackCooldown = 1f;
            damageEnemy();
        }
    }

    private void damageEnemy()
    {
        if (enemyInRange())
        {
            enemyHealth.takeDamage(damage);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
