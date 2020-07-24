using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform groundCheck = null;
    [Tooltip ("Lower value = quicker fall, keep it < 0")]
    [SerializeField] private float gravity;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float sprintSpeed;
    [Tooltip ("Change this for the 'anti fall damage boot' thing : 0.3 = no thing, the higher the value the longer the thing is")]
    [SerializeField] private float groundDistance;
    [SerializeField] private float initialFOV;
    [SerializeField] private float sprintingFOV;
    [Tooltip ("Lower value = quicker lerp")]
    [SerializeField] [Range(10.0f, 20.0f)] private float fovSpeed;
    [SerializeField] private float fovCoefficientToOriginal;


    public LayerMask groundMask;

    private CharacterController controller;
    private Transform _playerTransform;
    private Vector3 _velocity;
    private bool _isGrounded;
    private bool _isCrouching;
    private float _speed = 0.0f;
    private Camera _camera;
    private bool _isSprinting = false;
    private float _fovValue = 0.0f;
    [SerializeField] private List<AudioClip> audioClip = new List<AudioClip>();
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        _playerTransform = transform;
        _speed = walkingSpeed;
        _camera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        _fovValue = fovSpeed * Time.deltaTime;
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2.0f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            _isSprinting = true;
            _speed = sprintSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _isSprinting = false;
            _speed = walkingSpeed;
        }

        Vector3 move = _playerTransform.right * x + _playerTransform.forward * z;
        controller.Move(move * (_speed * Time.deltaTime));

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        if (Input.GetKeyDown(KeyCode.LeftControl) && _isGrounded)
        {
            _isCrouching = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftControl) && _isGrounded)
        {
            _isCrouching = false;
        }

        _velocity.y += gravity * Time.deltaTime;

        controller.Move(_velocity * Time.deltaTime);
    }

    private void LateUpdate()
    {
        if (_isSprinting)
        {
            if (_camera.fieldOfView < sprintingFOV)
                _camera.fieldOfView = sprintingFOV;
            else if (_camera.fieldOfView > sprintingFOV)
                _camera.fieldOfView -= _fovValue;
        }
        else
        {
            if (_camera.fieldOfView > initialFOV)
            {
                ResetFOV();
            }
            else if (_camera.fieldOfView < initialFOV)
            {
                fovCoefficientToOriginal = 4.0f;
                _camera.fieldOfView += _fovValue * fovCoefficientToOriginal;
            }
        }
    }

    public bool IsGrounded()
    {
        return _isGrounded;
    }
    public void ResetFOV()
    {
        _camera.fieldOfView = initialFOV;
    }

}
