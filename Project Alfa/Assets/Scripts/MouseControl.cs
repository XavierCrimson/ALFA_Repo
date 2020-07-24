using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    [SerializeField] private float mouseSensitivity = 100.0f;
    [SerializeField] [Range(-90, 0)] private int clampVertMin;
    [SerializeField] [Range (0, 90)] private int clampVertMax;
    [SerializeField] private Transform playerBody = null;
    [SerializeField] private Texture2D mouseCursorTexture = null;
    [SerializeField] private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 _hotSpot = Vector2.zero;

    private float xRotation = 0.0f;

    void Start()
    {
        Cursor.SetCursor(mouseCursorTexture, _hotSpot, cursorMode);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -clampVertMax, -clampVertMin);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
