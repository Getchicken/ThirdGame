using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController01 : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 2f;
    public float damageWindow = 0.3f;

    public bool IsAttacking = false;

    public AudioClip swordAttackSound;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(CanAttack == true)
            {
                SwordAttack();
            }
        }
    }

    public void SwordAttack()
    {
        //sword attack
        IsAttacking = true;
        Animator anim = Sword.GetComponent<Animator>();
        if(CanAttack == true)
            anim.SetTrigger("Attack");
        
        CanAttack = false;
        

        // sword swing sound
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);

        StartCoroutine(ResetAttackCooldown());
        StartCoroutine(ResetAttackBool());
    }

    IEnumerator ResetAttackCooldown()
    {
        //StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true; 
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(damageWindow);
        IsAttacking = false;
    }
}
