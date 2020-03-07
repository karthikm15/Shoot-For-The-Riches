using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    public float sensitivity;
    private float xRot = 0.0f;
    private float yRot = 0.0f;
    [HideInInspector] public float currentX;
    [HideInInspector] public float currentY;
    [HideInInspector] public float velocityX;
    [HideInInspector] public float velocityY;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        xRot -= sensitivity * Input.GetAxis("Mouse Y");
        yRot += sensitivity * Input.GetAxis("Mouse X");
        xRot = Mathf.Clamp(xRot, -90, 90);
        currentX = Mathf.SmoothDamp(currentX, xRot, ref velocityX, 0.1f);
        currentY = Mathf.SmoothDamp(currentY, yRot, ref velocityY, 0.1f);
        transform.rotation = Quaternion.Euler(currentX, currentY, 0.0f);

    }
}
