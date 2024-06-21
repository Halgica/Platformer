using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle behaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    [Header("Enemy animator")]
    [SerializeField]private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    void OnDisable()
    {
        anim.SetBool("move", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
            {
                moveInDirection(-1);
            }
            else
            {
                directionChange();
            }
        }
        else
        {
            if(enemy.position.x <= rightEdge.position.x)
            {
                moveInDirection(1);
            }
            else
            {
                directionChange();
            }
        }
    }

    private void directionChange()
    {
        anim.SetBool("move", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
    }

    private void moveInDirection(int direction)
    {
        idleTimer = 0;
        anim.SetBool("move",true);
        //make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, initScale.y, initScale.z);

        //move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
