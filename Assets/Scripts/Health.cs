using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int startingHealth;

    public void takeDamage(int damageTaken)
    {
        startingHealth -= damageTaken;
        Debug.Log("Damage taken");
    }
}
