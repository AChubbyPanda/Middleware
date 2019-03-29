using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public Slider healthbar;

    public float maxHealth = 10f;
    float currentHealth;

	// Use this for initialization
	void Start ()
    {
        currentHealth = maxHealth;
	}

    //           how much      this much
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        healthbar.value = currentHealth / maxHealth;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
