using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmasherScript : MonoBehaviour
{
    public Rigidbody2D rb;

    private float timer;
    private float smashTime = 2;
    private Vector2 origPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        origPosition = rb.position;
    }


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > smashTime)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - 1f);
            timer = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        while (rb.position.y < origPosition.y)
        {
            rb.velocity = new Vector2(rb.velocity.x, 1);
        }
    }
}
