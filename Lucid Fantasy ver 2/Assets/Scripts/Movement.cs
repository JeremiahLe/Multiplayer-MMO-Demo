using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static bool playerFacingRight = true;

    public Rigidbody2D rb;

    Vector2 movement;

    void Start()
    {
       
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
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
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
