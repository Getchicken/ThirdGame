using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController01 wc;
    public GameObject Particle1;

    private float currentHealth;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && wc.IsAttacking && currentHealth >= 1f)
        {
            //Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("isHurt");

            // blood particles after hit
            //Instantiate(Particle1, transform.position, Quaternion.identity);  
            Rigidbody rb = Instantiate(Particle1, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        }
        // new Vector3(other.transform.position.x, transform.position.y, transform.position.z),other.transform.rotation;
    }
}