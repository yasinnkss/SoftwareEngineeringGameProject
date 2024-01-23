using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Start is called before the first frame update
    public void ExitGame()
    {
        Application.Quit();

        //ditorApplication.ExitPlaymode();
    }
}
