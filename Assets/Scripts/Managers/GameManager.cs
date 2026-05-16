using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject itemObject;
    [SerializeField] GameObject endObject;
    GameObject[] enemies;
    bool isGameActive = true;
    public bool doorUnlocked = false;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(enemies);
    }

    void Update()
    {
        if (isGameActive == false)
            return;

        foreach (var enemy in enemies)
        {
            if (enemy.activeInHierarchy == true)
                return;
        }

        if (LightManager.Instance != null)
            LightManager.Instance.SetLightIntensity(0f);

        itemObject.SetActive(true);
        endObject.SetActive(true);
        if (!doorUnlocked)
            return;

        winScreen.SetActive(true);
        isGameActive = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(2);
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
