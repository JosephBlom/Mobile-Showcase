using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBrain : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] GameObject player;
    [SerializeField] GameObject heart;
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] float damage;

    Vector3 direction;
    KunaiManager kunaiManager;
    EnemySpawning enemySpawning;

    void Start()
    {
        enemySpawning = FindObjectOfType<EnemySpawning>();
        kunaiManager = FindObjectOfType<KunaiManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        direction = player.transform.position - transform.position;
        direction.Normalize();
        rb2d.velocity = direction * moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Damage")
        {
            health -= kunaiManager.Damage;
            if(health <= 0)
            {
                dropHealth();
                enemySpawning.aliveEnemies.RemoveAt(0);
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("Bomb"))
        {
            enemySpawning.aliveEnemies.RemoveAt(0);
        }
    }

    void dropHealth()
    {
        int num = Random.Range(1, 100);
        if(num <= 20)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }
    }
}
