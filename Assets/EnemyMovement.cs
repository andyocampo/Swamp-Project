using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 3;
    float rayDistance = 100;    //distance of raycast wall detector
    bool IsFacingRight = true; //if the enemy is facing left/right

    Vector2 rayDirection;
    Rigidbody2D rB;

    [SerializeField] Transform wallDetection;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Movement();
        CheckWallCollision();
        CheckGroundCollision();
    }

    private void Movement()
    {
        Vector2 velocity = rB.velocity;
        velocity.x = enemySpeed;
        rB.velocity = velocity;
    }

    private void CheckGroundCollision()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(wallDetection.position, Vector2.down, 2,
            1 << LayerMask.NameToLayer("Ground"));

        if (groundInfo.collider == false)
        {
            if (IsFacingRight == true)
            {

                transform.eulerAngles = new Vector3(0, -180, 0);
                enemySpeed *= -1;
                IsFacingRight = false;

            }
            else
            {

                transform.eulerAngles = new Vector3(0, 0, 0);
                enemySpeed *= -1;
                IsFacingRight = true;

            }
        }
        Debug.DrawRay(wallDetection.position, Vector2.down);
        //Debug.Log(groundInfo.distance);
    }

    private void CheckWallCollision()
    {
        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, rayDirection, rayDistance,
            1 << LayerMask.NameToLayer("Walls"));

        if (wallInfo.distance < 0.05)
        {
            if (IsFacingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                rayDirection = new Vector2(-1, 0);
                enemySpeed *= -1;
                IsFacingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                rayDirection = new Vector2(1, 0);
                enemySpeed *= -1;
                IsFacingRight = true;
            }
        }
        Debug.DrawRay(wallDetection.position, rayDirection);
        //Debug.Log(wallInfo.distance);
    }
}
