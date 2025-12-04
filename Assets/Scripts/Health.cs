using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] string enemy;
    [SerializeField] Slider slider;
    public float currentHealth; 
    public GameObject healthUI;
    [SerializeField] UnityEvent onHealthDepleted;

    void Start()
    {
        if (slider == null)
        {
            return;
        }
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0)
        {
            if (onHealthDepleted != null)
            {
                onHealthDepleted.Invoke();
            }
        }
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
                if (slider != null)
                {
                    slider.value -= damage;

                    //xlamping so health never goes below zero
                    slider.value = Mathf.Clamp(slider.value, 0, maxHealth);
                    Debug.Log(slider.value);
                }
            }
            if (slider != null)
            {
                if (slider.value == 0)
                {
                    healthUI.SetActive(false);
                }
                else
                {
                    return;
                }
            }
        Debug.Log(currentHealth);
        }
    }

    public float GetCurrentHealth()
    {
        return slider.value; 
    }

    public float GetMaxHealth()
    {
        return maxHealth; 
    }

    public void IncreaseHealth(float amount)
    {
        currentHealth += amount;
        if (slider != null)
        {
            slider.value += amount;
            slider.value = Mathf.Clamp(slider.value, 0, maxHealth);
        }
    }
}

