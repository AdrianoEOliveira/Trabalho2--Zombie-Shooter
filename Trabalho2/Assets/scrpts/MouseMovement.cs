using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float xRotation = 0f;
    [SerializeField] private float yRotation = 0f;

    [SerializeField] private float maxLookAngle = -90f;
    [SerializeField] private float minLookAngle = 90f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Travar e esconder o cursor
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, maxLookAngle, minLookAngle);

        yRotation += mouseX;

        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
    }
}
