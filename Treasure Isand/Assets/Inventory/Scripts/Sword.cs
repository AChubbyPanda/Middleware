using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{

    public GameObject player;
    public GameObject weapon;

    public float damage = 1f;

    // Use this for initialization
    void Start()
    {

    }


    void Update()
    {

    }

    void DealWeaponDamge()
    {
        Vector3 center = transform.position + transform.forward + transform.up;
        Vector3 halfExtentes = new Vector3(0.5f, 1.0f, 0.5f);
        Collider[] hits = Physics.OverlapBox(center, halfExtentes, transform.rotation);

        foreach (Collider hit in hits)
        {
 

            
        }
    }
}

   

