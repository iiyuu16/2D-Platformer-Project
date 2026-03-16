using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PlayerAttackHitbox : MonoBehaviour
{
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private List<GameObject> enemiesInHitbox = new List<GameObject>();

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        ToggleSprite(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            enemiesInHitbox.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") == false)
            return;
            
        if (enemiesInHitbox.Contains(collision.gameObject))
            enemiesInHitbox.Remove(collision.gameObject);
    }

    public void AttackOnHitbox()
    {
        List<GameObject> enemiesToKill = new List<GameObject>();
        enemiesToKill.AddRange(enemiesInHitbox);

        foreach (var enemy in enemiesToKill)
        {
            enemiesInHitbox.Remove(enemy);
            enemy.GetComponent<Enemy>().KillEnemy();
        }
    }

    public void ToggleSprite(bool visibility)
    {
        sprite.enabled = visibility;
    }

    public void ToggleSpriteFlip(bool flip)
    {
        sprite.flipX = flip;
    }
}
