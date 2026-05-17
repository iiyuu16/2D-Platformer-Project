using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement movement;
    PlayerAttack attack;
    PlayerAnimation anims;

    [SerializeField] int lives = 5;
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] GameObject gameOver;

    [SerializeField] float enemyRepelRadius = 3.0f;
    [SerializeField] float enemyRepelForce = 30.0f;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        attack = GetComponent<PlayerAttack>();
        anims = GetComponent<PlayerAnimation>();
    }

    public void KillPlayer()
    {
        movement.enabled = false;
        attack.enabled = false;
        anims.SetAnimationTrigger("Died");
    }

    private void SpawnPlayer()
    {
        movement.enabled = true;
        attack.enabled = true;
        anims.SetSpriteVisibility(true);
    }

    public void IncrementLives(int increment)
    {
        if (increment < 0)
        {
            anims.SetAnimationTrigger("Hurt");
            RepelTaggedEnemies();
        }

        lives += increment;
        lives = lives < 0 ? 0 : lives;

        healthCounter.text = lives.ToString();

        if (lives < 1)
        {
            KillPlayer();
        }
    }

    private void RepelTaggedEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 playerPosition = transform.position;

        foreach (var enemy in enemies)
        {
            if (enemy == null || enemy.activeInHierarchy == false)
                continue;

            Vector2 enemyPosition = enemy.transform.position;
            Vector2 direction = enemyPosition - playerPosition;
            float distance = direction.magnitude;
            if (distance <= 0.0f || distance > enemyRepelRadius)
                continue;

            Rigidbody2D enemyRb = enemy.GetComponent<Rigidbody2D>();
            if (enemyRb == null)
                continue;

            direction /= distance;
            float strength = enemyRepelForce / Mathf.Max(distance, 0.1f);
            enemyRb.AddForce(direction * strength, ForceMode2D.Impulse);
            Debug.Log($"Repelled enemy '{enemy.name}' by shockwave. Distance={distance:F2}, Force={strength:F1}");
        }
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
}
