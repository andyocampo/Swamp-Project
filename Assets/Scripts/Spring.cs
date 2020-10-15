using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    private float springForce = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Frog")
        {
            if(other.transform.eulerAngles.y == 180)
            {
                other.attachedRigidbody.AddForce(new Vector2(-springForce, springForce), ForceMode2D.Impulse);
            }
            else if (other.transform.eulerAngles.y == 0)
            {
                other.attachedRigidbody.AddForce(new Vector2(springForce, springForce), ForceMode2D.Impulse);
            }
        }
    }
}
