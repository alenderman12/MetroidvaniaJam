using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyType enemyData;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "AttackZone")
        {
            print("Ouch");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerMovement>().Knockback(transform.position, enemyData.knockbackForce);
        }
    }
}
