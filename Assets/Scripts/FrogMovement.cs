using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField] 
    float leapForce = 5;        //the force of the frogs jump

    float rayDistance = 100;    //distance of raycast wall detector
    bool IsFacingRight = true;  //is the frog facing right
    bool HasJumped = false;     //has the frog jumped 

    Vector2 leapArc;
    Vector2 rayDirection;
    Rigidbody2D rB;

    public Transform wallDetection;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        leapArc = new Vector2(leapForce, leapForce);
        rayDirection = new Vector2(1, 0);
    }


    void Update()
    {
        if(!HasJumped)
        {
            StartCoroutine(FrogJump());
        }

        CheckWallCollision();
    }

    //This coroutine makes the frog jump every 3 seconds.
    IEnumerator FrogJump()
    {
        HasJumped = true;
        yield return new WaitForSecondsRealtime(3f);
        rB.AddForce(leapArc, ForceMode2D.Impulse);
        HasJumped = false;
    }

    /// <summary>
    /// Using a raycast, this method checks whether a frog has hit a wall. If the raycast distance is lower than 0.05,
    /// the frog has hit a wall and the frog will turn around.
    /// </summary>
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
                leapArc = new Vector2(-leapForce, leapForce);
                IsFacingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                rayDirection = new Vector2(1, 0);
                leapArc = new Vector2(leapForce, leapForce);
                IsFacingRight = true;
            }
        }
        Debug.DrawRay(wallDetection.position, rayDirection);
        //Debug.Log(wallInfo.distance);
    }
}
