using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController01 : MonoBehaviour
{
    public GameObject Sword;
    public bool CanAttack = true;
    public float AttackCooldown = 2f;

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
        CanAttack = false;
        Animator anim = Sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");

        // sword swing sound
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);

        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(AttackCooldown);
        IsAttacking = false;
    }
}
