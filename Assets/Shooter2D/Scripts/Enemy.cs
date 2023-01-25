using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script d'enemy basic j'ai pas eu le temps de test
public class Enemy : MonoBehaviour
{
    [SerializeField] private float health = 40;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody2D rb;
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }
}
