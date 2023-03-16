using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health, maxHealth = 3f;

    private Shoot _playerShoot;

    private void Start()
    {
        health = maxHealth;
        _playerShoot = GetComponent<Shoot>();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        var player = FindObjectOfType<InputsReceiver>();

        var direction = player.transform.position - transform.position;
        _playerShoot.SetDirection(direction);
        if(player != null)
        {
            _playerShoot.IsShooting = true;
            _playerShoot.Shooting();
        }
    }
}