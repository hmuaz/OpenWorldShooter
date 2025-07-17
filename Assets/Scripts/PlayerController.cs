using UnityEngine;

public sealed class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 5f;

    [SerializeField]
    private float _mouseSensitivity = 2f;

    [SerializeField]
    private Transform _cameraPivot;

    private float _xRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        // Mouse look
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

        transform.Rotate(0f, mouseX, 0f);

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -80f, 80f);

        _cameraPivot.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        // Movement
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 move = transform.right * horizontal + transform.forward * vertical;
        transform.position += move.normalized * _moveSpeed * Time.deltaTime;

        // Shoot
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Ateş edildi!");
        }
    }
}