using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 3;
    [SerializeField] string enemy;
    [SerializeField] Slider slider;
    public float currentHealth; 
    public GameObject healthUI;

    void Start()
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //check tag of other object, if labelled "hurt" then increase damage by its "hurt" value
        Debug.Log("ccollide");
        if (other.gameObject.CompareTag(enemy))
        {
            //get hurt value
            Hurt hurtComponent = other.gameObject.GetComponent<Hurt>();
            if (hurtComponent != null)
            {
                float damage = hurtComponent.damageAmount;
                slider.value -= damage;

                //xlamping so health never goes below zero
                slider.value = Mathf.Clamp(slider.value, 0, maxHealth);
                Debug.Log(slider.value);
            }
            if (slider.value == 0)
            {
                healthUI.SetActive(false);
            }
            else
            {
                return;
            }
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
}

