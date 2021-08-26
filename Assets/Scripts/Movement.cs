using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector3 horizontal;
    Vector3 vertical;
    Vector3 prevDirection;
    // Start is called before the first frame update
    void Start()
    {
        prevDirection = new Vector3();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, 0.0f);
        vertical = new Vector3(0.0f, Input.GetAxisRaw("Vertical"), 0.0f);
        if (horizontal != new Vector3() && vertical == new Vector3())
        {
            transform.position = transform.position + horizontal * Time.deltaTime;
            prevDirection = horizontal;
        }
        else if (horizontal == new Vector3() && vertical != new Vector3())
        {
            transform.position = transform.position + vertical * Time.deltaTime;
            prevDirection = vertical;
        }
        else
        {
            if (prevDirection == vertical)
            {
                transform.position = transform.position + horizontal * Time.deltaTime;
            }
            else if (prevDirection == horizontal)
            {
                transform.position = transform.position + vertical * Time.deltaTime;
            }
        }
        
    }
}
