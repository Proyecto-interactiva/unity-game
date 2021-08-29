using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Camera cam;
    public float padding = 1;
    public float stiffness = 1;
    public float baseCorrectionSpeed = 1;

    float marginLeft;
    float marginRight;
    float marginBottom;
    float marginTop;

    // Start is called before the first frame update
    void Start()
    {
        marginLeft = cam.pixelWidth * padding / 2;
        marginRight = cam.pixelWidth - marginLeft;
        marginBottom = cam.pixelHeight * padding / 2;
        marginTop = cam.pixelHeight - marginBottom;

        //Debug.Log($"ml: {marginLeft}, mr: {marginRight}, mb:{marginBottom}, mt:{marginTop}");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerScreenPos = cam.WorldToScreenPoint(player.position);
        float horizontalCorrection = 0f;
        float verticalCorrection = 0f;

        if (playerScreenPos.x < marginLeft)
        {
            horizontalCorrection = playerScreenPos.x - marginLeft;
            
        }
        else if (playerScreenPos.x > marginRight)
        {
            horizontalCorrection = playerScreenPos.x - marginRight;
        }
        if (playerScreenPos.y < marginBottom)
        {
            verticalCorrection = playerScreenPos.y - marginBottom;
        }
        else if (playerScreenPos.y > marginTop)
        {
            verticalCorrection = playerScreenPos.y - marginTop;
        }

        verticalCorrection = verticalCorrection / cam.pixelHeight * stiffness;
        horizontalCorrection = horizontalCorrection / cam.pixelWidth * stiffness;


        if (verticalCorrection != 0f)
        {
            verticalCorrection = verticalCorrection + Mathf.Sign(verticalCorrection) * baseCorrectionSpeed;
        }
        if (horizontalCorrection != 0f)
        {
            horizontalCorrection = horizontalCorrection + Mathf.Sign(horizontalCorrection) * baseCorrectionSpeed;
        }
        

        //Debug.Log($"hc: {horizontalCorrection}, vc: {verticalCorrection}, cp: ({playerScreenPos.x},{playerScreenPos.y})");

        if (horizontalCorrection != 0f || verticalCorrection != 0f)
        {
            transform.position = transform.position + new Vector3(horizontalCorrection, verticalCorrection, 0);
        }

    }
}
