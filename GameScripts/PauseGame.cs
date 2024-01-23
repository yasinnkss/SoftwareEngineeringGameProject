using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    public GameObject pauseScreen;
    bool isPaused = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        PausingGame();

    }

    void PausingGame()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = true;
            Time.timeScale = 0.0f;
            pauseScreen.SetActive(true);
            Cursor.visible = true;
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = false;
            Time.timeScale = 1.0f;
            pauseScreen.SetActive(false);
            Cursor.visible = false;
        }

    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (isPaused)
        {
            Time.timeScale = 1.0f;
            isPaused = false;
        }
    }


}
