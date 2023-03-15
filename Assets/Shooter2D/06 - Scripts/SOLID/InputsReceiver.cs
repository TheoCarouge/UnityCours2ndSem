using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class InputsReceiver : MonoBehaviour
{
    [SerializeField] PlayerAim _playerAim;
    private Shoot _playerShoot;

    [SerializeField] Movement _playerMovement;
    // CONTROLLER
    [SerializeField] private Rigidbody2D _rigidbody2D;

    [SerializeField] Vector2 _directionRotation;

    [SerializeField] private PlayerControls _playerControls;
    [SerializeField] private PlayerControls.PlayerActions _playerControlsActions;

    // ACCESSEURS
    private float _horizontal;
    public float Horizontal => _horizontal;

    private float _vertical;
    public float Vertical => _vertical;

    // VARS
    private float _speed = 8f;
    private float _jumpingPower = 8f;

    private bool _isUsingGamepad = false;
    public bool IsUsingGamepad
    {
        get { return _isUsingGamepad; }
        set { _isUsingGamepad = value; }
    }

    private bool _isFacingRight = true;

    private void Awake()
    {
        _playerShoot = GetComponent<Shoot>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
        _playerControls.Player.Move.performed += SetHorizontal;
        _playerControls.Player.Move.canceled += SetHorizontal;
        _playerControls.Player.Shoot.performed += Shooting;
        _playerControls.Player.Shoot.canceled += CanceledShooting;
        _playerControls.Player.Joystick.performed += Aim;
        _playerControls.Player.Joystick.canceled += Aim;
    }
    private void OnDisable()
    {
        _playerControls.Disable();
        _playerControls.Player.Move.performed -= SetHorizontal;
        _playerControls.Player.Move.canceled -= SetHorizontal;
        _playerControls.Player.Shoot.performed -= Shooting;
        _playerControls.Player.Shoot.canceled -= CanceledShooting;
        _playerControls.Player.Joystick.performed -= Aim;
        _playerControls.Player.Joystick.canceled -= Aim;

    }

    private void Aim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerAim.lookInput = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _playerAim.lookInput = Vector2.zero;
        }
    }

    private void SetHorizontal(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    public void Shooting(InputAction.CallbackContext context)
    {
        _playerShoot.IsShooting = true;
    }

    public void CanceledShooting(InputAction.CallbackContext context)
    {
        _playerShoot.IsShooting = false;
    }

    void Update()
    {
        VelocityApplier();
        Shoot();

        //find the last Input Device used and set a bool.
        InputDevice lastUsedDevice = null;
        float lastEventTime = 0;
        foreach (var device in InputSystem.devices)
        {
            if (device.lastUpdateTime > lastEventTime)
            {
                lastUsedDevice = device;
                lastEventTime = (float)device.lastUpdateTime;
            }
        }

        if (lastUsedDevice is Gamepad)
        {
            _isUsingGamepad = true;
        }
        else
        {
            _isUsingGamepad = false;
        }
    }

    public void Shoot()
    {
        _playerShoot.Shooting();
    }

    public void VelocityApplier()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal * _speed, _rigidbody2D.velocity.y);
    }
}