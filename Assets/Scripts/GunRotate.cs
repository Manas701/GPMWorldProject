using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Ensure the mouse position is on the same z-plane as the object

        // Calculate the direction from the object to the mouse position
        Vector3 direction = mousePosition - transform.position;


        // Flip the object's scale on the x-axis based on mouse position
        if (direction.x > 0)
        {
            // Mouse is to the right of the object
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            // transform.position = new Vector3(Mathf.Abs(transform.position.x), transform.position.y, transform.position.z);
        }
        else
        {
            // Mouse is to the left of the object
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            // transform.localPosition = new Vector3(-Mathf.Abs(transform.position.x), transform.position.y, transform.position.z);
        }

        // Rotate the object to face the mouse position
        transform.up = direction.normalized;
    }
}
