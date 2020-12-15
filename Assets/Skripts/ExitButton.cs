using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitButton : MonoBehaviour
{
    public void IsTapped()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("EditScene");
    }
}
