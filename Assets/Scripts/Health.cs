using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] string enemy;
    public float currentHealth; 

    void Start()
    {
        currentHealth = maxHealth; 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check tag of other object, if labelled "hurt" then increase damage by its "hurt" value
        //Debug.Log("ccollide");
        if (other.gameObject.CompareTag(enemy))
        {
            //get hurt value
            Hurt hurtComponent = other.gameObject.GetComponent<Hurt>();
            if (hurtComponent != null)
            {
                float damage = hurtComponent.damageAmount;
                currentHealth -= damage;

                //xlamping so health never goes below zero
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            }
            else
            {
                return;
            }
        }
    }

    public float GetCurrentHealth()
    {
        return currentHealth; 
    }

    public float GetMaxHealth()
    {
        return maxHealth; 
    }
}

