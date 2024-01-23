using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] GameObject player;
    [SerializeField] FixedJoystick joystick;
    [SerializeField] Canvas mobileCanvas;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] float moveSpeed;

    public bool keyboard;
    public float moveMultiplier;
    public int maxHealth = 20;
    public int currentHealth;

    float inputX;
    float inputY;
    EnemySpawning enemySpawning;

    void Start()
    {
        enemySpawning = FindObjectOfType<EnemySpawning>();
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        if (keyboard)
        {
            mobileCanvas.enabled = false;
        }
    }

    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);
        if (keyboard)
        {
            inputX = Input.GetAxis("Horizontal");
            inputY = Input.GetAxis("Vertical");
            move = new Vector3(inputX * moveSpeed * (moveMultiplier + 1), inputY * moveSpeed * (moveMultiplier + 1), 0);
        }
        else
        {
            move = new Vector3(joystick.Horizontal * moveSpeed * (moveMultiplier + 1), joystick.Vertical * moveSpeed * (moveMultiplier + 1), 0);
        }
        rb2d.velocity = move;
        Vector3 mouseScreen = Input.mousePosition;
        Vector3 mouse = Camera.main.ScreenToWorldPoint(mouseScreen);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float force = 5000;
        if (collision.gameObject.CompareTag("Bomb"))
        {
            currentHealth -= 15;
            healthText.text = "Health: " + currentHealth;
            if (currentHealth <= 0)
            {
                Die();
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Healthy"))
        {
            currentHealth -= 5;
            healthText.text = "Health: " + currentHealth;
            if (currentHealth <= 0)
            {
                Die();
            }
            //Adds force to the player after being hit.
            Vector3 dir = new Vector3 (collision.contacts[0].point.x - transform.position.x , collision.contacts[0].point.y - transform.position.y, 0f);
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir*force);
        }
        else if (collision.gameObject.CompareTag("Fast"))
        {
            //Updates health.
            currentHealth -= 3;
            healthText.text = "Health: " + currentHealth;
            if (currentHealth <= 0)
            {
                Die();
            }
            //Adds force to the player after being hit.
            Vector3 dir = new Vector3(collision.contacts[0].point.x - transform.position.x, collision.contacts[0].point.y - transform.position.y, 0f);
            dir = -dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
        else if (collision.gameObject.CompareTag("Heart"))
        {
            currentHealth += 5;
            if(currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            healthText.text = "Health: " + currentHealth;
            Destroy(collision.gameObject);
        }
    }

    private void Die()
    {
        Time.timeScale = 0;
        Debug.Log("You died.");
    }
}
