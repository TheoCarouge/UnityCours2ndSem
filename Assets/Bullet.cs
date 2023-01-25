using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private float bulletSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb.velocity = transform.up * bulletSpeed;
    }
}