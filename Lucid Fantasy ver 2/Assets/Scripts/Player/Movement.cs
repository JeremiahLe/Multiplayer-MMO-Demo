using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System;

public class Movement : MonoBehaviourPun
{
    public static bool playerFacingRight = true;

    public Rigidbody2D rb;

    Vector2 movement;

    public GameObject inputField;

    bool canMove = true;

    public Animator animator;

    int scaler = 1;

    void Start()
    {
       
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            TakeInput();
        }

        /*
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (canMove == true)
            {
                canMove = false;
            }
            else
                canMove = true;
        }
        */
    }

    private void TakeInput()
    {
        if (canMove)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("horizontal", movement.x);
            animator.SetFloat("vertical", movement.y);
            animator.SetFloat("speed", movement.sqrMagnitude);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * Stats.playerWalkSpeed * Time.fixedDeltaTime);

        if (playerFacingRight == false && movement.x > 0 && AttackScript.shooting != true)
        {
            Flip();
        }
        else if (playerFacingRight == true && movement.x < 0 && AttackScript.shooting != true)
        {
            Flip();
        }
    }

    // flipping the character's sprite
    public void Flip()
    {
        playerFacingRight = !playerFacingRight;
        //Vector3 Scaler = transform.localScale;
        //Scaler.x *= -1;
        //transform.localScale = Scaler;
        animator.SetFloat("horizontal", scaler *-1);
        
    }
    
}
