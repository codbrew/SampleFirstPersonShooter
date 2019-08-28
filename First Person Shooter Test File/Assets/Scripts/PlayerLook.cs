using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    //refrence to the player body since we are going to rotate the body which with then move the camera's rotate
    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        //upon awake set the mouse clamp to zero, we don't want to restrict it right away
        xAxisClamp = 0.0f;
        //lock cursor at the start of the level
        LockCursor();
    }
    
    private void CameraRotation()
    {
        //this allows us to get the input from our mouse up and down to look; mouseSensitivity is a float we can alter
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;
        //these restrict the camera from going beyond 90/-90 degrees; we can alter this for our character later on
        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            //the degree at which we are looking when looking directly up
            ClampXAxisToSetValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            //the degree at which we are looking when looking directly down
            ClampXAxisToSetValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    
    private void Update()
    {
        CameraRotation();
    }

    //locks cursor to the center of the screen
    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    //function to help with the fact that even with the clamp restriction the camera can overshoot our clamps
    private void ClampXAxisToSetValue(float value)
    {
        //no idea what eulerRotation and Angles are
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
