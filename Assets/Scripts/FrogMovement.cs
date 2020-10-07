using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    [SerializeField]
    float leapForce = 5;
    bool IsFacingRight = true;
    float rayDistance = 100;

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rB.AddForce(leapArc, ForceMode2D.Impulse);
        }

        RaycastHit2D wallInfo = Physics2D.Raycast(wallDetection.position, rayDirection, rayDistance,
            1 << LayerMask.NameToLayer("Walls"));

        if (wallInfo.distance < 0.05)
        {
            if(IsFacingRight == true)
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
        Debug.Log(wallInfo.distance);
    }
}
