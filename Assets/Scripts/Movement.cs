using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Movement vectors
    Vector3 horizontal;
    Vector3 vertical;
    Vector3 currentDirection;
    Vector3 prevDirection;
    

    // Animation components
    public Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        prevDirection = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = new Vector3();
        horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, 0.0f);
        vertical = new Vector3(0.0f, Input.GetAxisRaw("Vertical"), 0.0f);

        // Direction logic
        if (horizontal != new Vector3() && vertical == new Vector3())
        {
            currentDirection = horizontal;
            prevDirection = horizontal;
        }
        else if (horizontal == new Vector3() && vertical != new Vector3())
        {
            currentDirection = vertical;
            prevDirection = vertical;
        }
        else
        {
            if (prevDirection == vertical)
            {
                currentDirection = horizontal;
            }
            else if (prevDirection == horizontal)
            {
                currentDirection = vertical;
            }
        }

        // Movement
        transform.position = transform.position + currentDirection * Time.deltaTime;

        // Animation logic
        animator.SetInteger("Horizontal", (int) currentDirection.x);
        animator.SetInteger("Vertical", (int) currentDirection.y);
        Debug.Log($"{(int) currentDirection.x}, {(int) currentDirection.y}");

    }
}
