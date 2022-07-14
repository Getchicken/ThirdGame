using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    private Animator animator;
    private bool isGrounded;

    public LayerMask groundMask;
    public float groundDistance = 0.4f;
    public Transform groundCheck;
    

    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded)
        {
            animator.SetTrigger("Jump");
        }
        
    }
}
