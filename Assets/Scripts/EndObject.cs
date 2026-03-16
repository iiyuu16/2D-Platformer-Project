using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] GameManager gameManager;
    [SerializeField] private bool tutorialMode;
    private void Start()
    {
        key = "Key";
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (key == collision.gameObject.GetComponent<PlayerInventory>().CheckItem())
            {
                gameManager.doorUnlocked = true;
            }
        }
    }
}
