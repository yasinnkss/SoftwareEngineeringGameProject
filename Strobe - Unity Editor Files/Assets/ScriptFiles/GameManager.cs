using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using TMPro;

public class GameManager : MonoBehaviour
{



    public GameObject GameOverUI;
    public GameObject deathScreen;
    public GameObject ssScreen;

    GameObject[] indicators;
    public GameObject player;
    GameObject[] enemies;
    int enemyCount;

    bool playerIsDead;
    public int score;
    public int bullets;
    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;
        indicators = GameObject.FindGameObjectsWithTag("Indicator");
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        playerIsDead = player.GetComponent<CharSpecs>().isDead;
        GameOver();

    }

    void LateUpdate()
    {
        GameObject.Find("Canvas/scoreText").GetComponent<TextMeshProUGUI>().text = score.ToString();
    }

    public void GameOver()
    {
        if (playerIsDead)
        {
            StartCoroutine("waitForDeath");
        }
        else if (!playerIsDead && (enemyCount == 0))
        {
            GameOverUI.SetActive(true);
            ssScreen.SetActive(true);
            indicators[0].SetActive(false);
            indicators[1].SetActive(false);
            Cursor.visible = true;
        }

    }


    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("restart");
    }

    public void mainMenu()
    {
        SceneManager.LoadScene("SampleScene");
        Debug.Log("mainMenu");
        Cursor.visible = true;
    }

    public void Quit()
    {
        Application.Quit();
        //EditorApplication.ExitPlaymode();
        Debug.Log("Quit");
    }

    public void AddPoint(int point)
    {
        score += point;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Cursor.visible = true;
    }

    void onDieGameOver()
    {

    }

    IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(4);
        GameOverUI.SetActive(true);
        deathScreen.SetActive(true);
        indicators[0].SetActive(false);
        indicators[1].SetActive(false);
        Time.timeScale = 0.0f;
        Cursor.visible = true;
    }
}
