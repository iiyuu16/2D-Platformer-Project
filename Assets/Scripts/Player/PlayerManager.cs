using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PlayerMovement movement;
    PlayerAttack attack;
    PlayerAnimation anims;

    [SerializeField] int lives = 3;
    [SerializeField] TextMeshProUGUI healthCounter;
    [SerializeField] GameObject gameOver;

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
            anims.SetAnimationTrigger("Hurt");

        lives += increment;
        lives = lives < 0 ? 0 : lives;

        healthCounter.text = lives.ToString();

        if (lives < 1)
        {
            KillPlayer();
        }
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }
}
