using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureScript : MonoBehaviour
{
    // Enumerators for statuses - Type of NPC, Elemental Affinity, Status, States
    enum NPCType {docileNPC, neutralNPC, enemyNPC }
    enum Element { Fire, Water, Wind, Earth, Lightning, Dark, Light }
    enum Status { Poisoned, Burning, Slowed, Stunned, Cursed, Immune }
    enum State { Wander, Patrol, Attack }

    [SerializeField]
    private State currentState;

    [SerializeField]
    // Combat Stats
    public float health;
    public int attack;
    public int physicalDefense;
    public int magicalDefense;

    // Speed of movement and time between movements in Patrol/Wander State
    public float speed;
    private float waitTime;
    public float startWaitTime;

    // Patrol State Array
    public Transform[] moveSpots;

    // Wander State iterator
    private int randomSpot;

    // Range of Movement for Wander state
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    // Transform of player if following
    public Transform targetPlayer;

    // Orientation for flipping Sprite
    public bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        // Set default state - Can be done in the inspector
        //currentState = State.Patrol;

        // Set the next position in the array to move to
        randomSpot = Random.Range(0, moveSpots.Length);

        //SpriteRenderer spriteColor = GetComponent<SpriteRenderer>();

        //moveSpots.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    // Update is called once per frame
    void Update()
    {
        // Check if dead
        CheckHealth();

        
        // Switch statement for checking current NPC state
       switch (currentState)
       {
            case State.Patrol:
                PatrolState();
                break;

            case State.Attack:
                AttackState();
                break;

            case State.Wander:
                WanderState();
                break;
       }
    }

    // Flip Function for changing sprite orientation
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    // Collision checking with Player Projectile
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8) // Projectile
        {
            GameObject projectile = collision.gameObject;
            health -= projectile.GetComponent<ProjectileScript>().damage;
            //spriteColor.color = Color.red;
            Invoke("ChangeColor", 2);
        }
        else
        return;
    }

    // Check Health to make sure if Dead
    void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Change color script for when damaged
    void ChangeColor()
    {
        //spriteColor.color = Color.white;
    }

    // Enemy States
    void PatrolState()
    {
        // Patrol between different preset locations in the editor at random

        // In Patrol/Wander State, move towards target position in moveSpots array
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                Vector3 target = moveSpots[randomSpot].position;

                waitTime = startWaitTime;

                // Move right, face right
                if (transform.position.x <= target.x && facingRight != true)
                {
                    Flip();
                }
                // Move left, face left
                else if (transform.position.x >= target.x && facingRight == true)
                {
                    Flip();
                }
            }
            else
            {
                // Reset timer till next movement
                waitTime -= Time.deltaTime;
            }
        }

    }

    void WanderState()
    {
        // randomly wander around within a set bounds
    }

    void AttackState()
    {
        // Follow player if in aggro radius
    }

} // Script End

