using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;


    [SerializeField] int damagePerCollision = 1;
    [SerializeField] float currentDirection = 1.0f;
    [SerializeField] float speed = 3.0f;
    [SerializeField] float wallCheckDistance = 0.25f;
    [SerializeField] bool tutorialMode = false;
    public LayerMask wallLayer;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
        if (tutorialMode)
            return;
        if (collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<PlayerManager>().IncrementLives(-damagePerCollision);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position + Vector3.up, Vector2.right * wallCheckDistance);
    }

    public void KillEnemy()
    {
        gameObject.SetActive(false);
    }
}
