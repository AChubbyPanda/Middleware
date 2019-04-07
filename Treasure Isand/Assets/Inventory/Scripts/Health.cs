using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public playerController thePlayer;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;

        thePlayer = FindObjectOfType<playerController>();
    }


    // Update is called once per frame
    void Update()
    {

    }

    public void Hurt(int damage, Vector3 direction)
    {
        currentHealth -= damage;

        //thePlayer.knockback(direction);
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }
}
