using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private InputsReceiver _inputsReceiver;

    [SerializeField] private Transform _shootingPoint;
    [SerializeField] private Transform _pivotRotation;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Rigidbody2D _rigidbody2DBullet;
    [SerializeField] private SpriteRenderer _weaponRenderer;
    public Transform pivotRotation => _pivotRotation;

    [SerializeField] private float resetReload = .1f;
    private float speedBullet;
    private float reloadTime;

    private bool _isShooting;

    [SerializeField] Vector2 _directionRotation;

    public bool IsShooting
    {
        get { return _isShooting; }
        set { _isShooting = value; }
    }

    private void Awake()
    {
        speedBullet = 100f;
        _rigidbody2DBullet = _bulletPrefab.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _weaponRenderer.flipY = Mathf.Abs(_pivotRotation.localEulerAngles.z) > 90f && Mathf.Abs(_pivotRotation.localEulerAngles.z) < 270f;
        Shooting();
    }

    public void Shooting()
    {
        reloadTime -= Time.deltaTime;
        if (_isShooting && reloadTime < 0)
        {
            Instantiate(_bulletPrefab, _shootingPoint.position, _shootingPoint.rotation);
            reloadTime = resetReload;
        }
    }

    public void MousePosition(InputAction.CallbackContext context)
    {
        if (!_inputsReceiver.IsUsingGamepad)
        {
            _directionRotation = (context.ReadValue<Vector2>() - (Vector2)Camera.main.WorldToScreenPoint(pivotRotation.position)).normalized;
            SetRotationToPivot();
        }
    }

    private void SetRotationToPivot()
    {
        float angle = Mathf.Atan2(_directionRotation.y, _directionRotation.x);
        SetPivotRotation(new Vector3(0, 0, Mathf.Rad2Deg * angle));
    }

    public void SetPivotRotation(Vector3 rotationEuler)
    {
        _pivotRotation.eulerAngles = rotationEuler;
    }

    public void SetDirection(Vector3 direction)
    {
        _pivotRotation.right = direction;
    }
}