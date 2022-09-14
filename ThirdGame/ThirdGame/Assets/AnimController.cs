using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    public GameObject Trails;
    public GameObject Glow;
    public GameObject Particle;
    public GameObject Hand;

    public void ShowTrailsOnAttack()
    {
        Trails.SetActive(true);
        Glow.SetActive(true);
        Particle.SetActive(true);
        IsAttacking();
    }

    public void HideTrailsOnAnimEnd()
    {
        Trails.SetActive(false);
        Glow.SetActive(false);
        Particle.SetActive(false);
        IsNotAttacking();
    }

    public void IsAttacking()
    {
        Hand.GetComponent<WeaponController>().IsAttacking = true;
    }

    public void IsNotAttacking()
    {
        Hand.GetComponent<WeaponController>().IsAttacking = false;
    }
}
