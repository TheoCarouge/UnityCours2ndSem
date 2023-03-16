using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] InputsReceiver _inputsReceiver;
    private bool _isJumping;

    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private float _jumpingPower = 8f;

    public bool IsJumping
    {
        get { return _isJumping; }
        set { _isJumping = value; }
    }

    private void Awake()
    {
        _groundLayer = LayerMask.GetMask("Ground");
    }

    public void Jumping(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpingPower);
        }
        if (context.canceled && _rigidbody2D.velocity.y > 0f)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y * 0.5f);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }
}