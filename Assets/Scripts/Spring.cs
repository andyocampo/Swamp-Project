using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] [Tooltip("The amount of force the spring adds to frog")] 
    Vector2 springForce;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Frog"))
        {
            if(other.transform.eulerAngles.y == 180)
            {
                other.attachedRigidbody.AddForce(new Vector2(-springForce.x, springForce.y), ForceMode2D.Impulse);
            }
            else if (other.transform.eulerAngles.y == 0)
            {
                other.attachedRigidbody.AddForce(new Vector2(springForce.x, springForce.y), ForceMode2D.Impulse);
            }
        }
    }
}
