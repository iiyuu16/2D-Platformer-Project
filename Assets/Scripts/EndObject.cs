using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndObject : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] GameManager gameManager;
    [SerializeField] TutorialManager tutorialManager;
    [SerializeField] private bool tutorialMode;
    private void OnEnable()
    {
        key = "Key";
        if (!tutorialMode)
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        else
            tutorialManager = GameObject.Find("TutorialManager").GetComponent<TutorialManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (key == collision.gameObject.GetComponent<PlayerInventory>().CheckItem())
            {
                if (!tutorialMode)
                    gameManager.doorUnlocked = true;
                else
                    tutorialManager.doorUnlocked = true;
                    
            }
        }
    }
}
