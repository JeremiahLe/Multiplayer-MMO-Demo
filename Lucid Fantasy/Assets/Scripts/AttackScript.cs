using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    public GameObject attackProjectile;
    public Transform target;
    private Vector3 targetShoot;

    public Vector2 projectileVelocity;

    public Camera cam;

    public float resetAttackTimer = Stats.playerAtkSpeed;

    public float playerProjectileSpeed = 6.5f;
    public float playerProjectileLifespan = .9f;
    public float playerAttack = 5f;

    public bool canAttack = true;
    public static bool shooting = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetShoot = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        Vector3 difference = targetShoot - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        if (canAttack)
        {
            if (Input.GetButton("Fire1"))
            {
                shooting = true;
                float distance = difference.magnitude;
                Vector2 direction = difference / distance;
                direction.Normalize();
                FireProjectile(direction, rotationZ);
                canAttack = false;
            }
            else
            {
                shooting = false;
            }
        }
        else
        {
            resetAttackTimer -= Time.deltaTime;
            if (resetAttackTimer <= 0)
            {
                canAttack = true;
                resetAttackTimer = Stats.playerAtkSpeed;
            }

        }
    }

    void FireProjectile(Vector2 direction, float rotationZ)
    {
        GameObject projectile = Instantiate(attackProjectile) as GameObject;

        projectile.transform.position = target.transform.position;
        projectile.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        projectile.GetComponent<Rigidbody2D>().velocity = direction * playerProjectileSpeed * (playerProjectileSpeed/2);
        projectileVelocity = projectile.GetComponent<Rigidbody2D>().velocity;

        projectile.GetComponent<ProjectileScript>().projectileLifespan = playerProjectileLifespan;
        projectile.GetComponent<ProjectileScript>().damage = playerAttack;

        bool orientation = Movement.playerFacingRight;

        // if facing right and projectile shooting left, flip orientation left
        if (orientation == true && projectileVelocity.x <= 0)
        {
            Movement movementScript = GetComponent<Movement>();
            movementScript.Flip();
        }
        // if facing left and projectile shooting right, flip orientation right
        else if (orientation == false && projectileVelocity.x >= 0)
        {
            Movement movementScript = GetComponent<Movement>();
            movementScript.Flip();
        }
    }

}
