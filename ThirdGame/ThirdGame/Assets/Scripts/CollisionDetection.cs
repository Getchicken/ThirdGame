using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    public WeaponController wc;
    public GameObject Particle;

    private float currentHealth;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && wc.IsAttacking && currentHealth >= 1f)
        {
            //Debug.Log(other.name);
            other.GetComponent<Animator>().SetTrigger("isHurt");

            // blood particles after hit
            Instantiate(Particle, transform.position, Quaternion.identity);  
            //Rigidbody rb = Instantiate(Particle, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        }
        // new Vector3(other.transform.position.x, transform.position.y, transform.position.z),other.transform.rotation;
    }
}