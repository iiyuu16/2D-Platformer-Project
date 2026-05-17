using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [SerializeField] int damagePerCollision = 1;
    [SerializeField] float currentDirection = 1.0f;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float wallCheckDistance = 0.25f;

    [SerializeField] float attackRange = 1.0f;
    [SerializeField] float launchForce = 20.0f;
    [SerializeField] float attackCooldown = 3.0f;
    public LayerMask wallLayer;

    public int bossHealth = 5;

    private Transform playerTransform;
    private float lastAttackTime = -Mathf.Infinity;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            playerTransform = playerObject.transform;
    }

    void Update()
    {
        if (playerTransform == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            LaunchAtPlayer();
            lastAttackTime = Time.time;
        }

    }

    void FixedUpdate()
    {
        sprite.flipX = currentDirection < 0.0f;
        rb.AddForce(Vector2.right * currentDirection * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);

        Vector2 position = transform.position + Vector3.up;
        Vector2 direction = Vector2.right;

        RaycastHit2D hit = Physics2D.Raycast(position, direction * currentDirection, wallCheckDistance, wallLayer);
        currentDirection = hit ? currentDirection * -1.0f : currentDirection;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerManager>().IncrementLives(-damagePerCollision);
    }

    private void LaunchAtPlayer()
    {
        if (playerTransform == null)
            return;

        Vector2 direction = (playerTransform.position - transform.position).normalized;
        rb.velocity = Vector2.zero;
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, Vector2.right * wallCheckDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void TakeDamage(int amount = 1)
    {
        bossHealth -= amount;
        Debug.Log($"Boss '{name}' took {amount} damage. Remaining HP: {bossHealth}");
        CheckBossHP();
    }

    public void CheckBossHP()
    {
        if (bossHealth <= 0)
        {
            Debug.Log($"Boss '{name}' defeated.");
            gameObject.SetActive(false);
        }
    }
}
