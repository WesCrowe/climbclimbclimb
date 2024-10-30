using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CameraScript : MonoBehaviour
{
    public Transform player;
    private Vector3 offset;
    private Vector3 zoomOut = new Vector3(0,0,6);
    public float zoom;
    public float maxZoom = 20f;
    public float minZoom = 0f;

    public float rotationAngle = 90f;
    public float rotationSmoothTime = .5f;
    public float rotationSpeed = 20f;
    private float targetRotationY;
    private float currentRotationY;
    private float targetRotationX;
    public float smoothTime = 0.2f;
    public Vector3 currentVelocity = Vector3.zero;


    public void Awake()
    {
        offset = transform.position - player.position;
        currentRotationY = transform.eulerAngles.y;
        targetRotationY = transform.eulerAngles.y;  

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            targetRotationY += rotationAngle;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            targetRotationY -= rotationAngle;        
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) &&!(zoom <= 0f))
        {
            offset += zoomOut;
            zoom -= 1;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !(zoom >= maxZoom))
        {
            offset -= zoomOut;
            zoom += 1;
        }
    }
    private void LateUpdate()
    {
        currentRotationY = Mathf.LerpAngle(currentRotationY, targetRotationY, rotationSpeed );
        
        Quaternion targetRotation = Quaternion.Euler(0,currentRotationY, 0);
        //transform.position = player.position + targetRotation * offset;

        Vector3 targetPos = player.position + targetRotation * offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        
        transform.LookAt(player.position);  
    }


}
