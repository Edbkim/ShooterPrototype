using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;
    [SerializeField]
    private float _gravity = 5;
    [SerializeField]
    private float _jumpHeight = 5;

    [SerializeField]
    private float _sensitivityX = 1;
    [SerializeField]
    private float _sensitivityY = 1;


    private Vector3 _direction;
    private Vector3 _velocity;

    private Camera _mainCamera;

    private CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _mainCamera = Camera.main;

        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera is NULL");
        }

        //lock cursor & hide it
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        MouseLook();

        //if escape key
        //unlock cursor

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           Cursor.lockState = CursorLockMode.Locked;
        }

    }

    private void CalculateMovement()
    {
        if (_controller.isGrounded == true)
        {
            float h = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

             _direction = new Vector3(h, 0, z);
            _velocity = _direction * _speed;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _velocity.y += _jumpHeight;
        }


        _velocity.y -= _gravity * Time.deltaTime;

        _velocity = transform.TransformDirection(_velocity);

        _controller.Move(_velocity * Time.deltaTime);
    }

    private void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.y += mouseX * _sensitivityX;
        transform.localRotation = Quaternion.AngleAxis(currentRotation.y, Vector3.up);

        //apply mouse Y to maincamera rotation value X

        Vector3 currentCameraRotation = _mainCamera.gameObject.transform.localEulerAngles;
        currentCameraRotation.x -= mouseY * _sensitivityY;

        _mainCamera.gameObject.transform.localRotation = Quaternion.AngleAxis(currentCameraRotation.x, Vector3.right);
    }

}
