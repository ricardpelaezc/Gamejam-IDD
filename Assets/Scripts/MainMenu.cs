using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public CanvasGroup options;

    private void Start()
    {
        CloseOptions();
    }
    public void StartGame()
    {
      SceneManager.LoadScene(firstLevel);
    }

    public void OpenOptions()
    {
        options.alpha = 1f;
        options.blocksRaycasts = true;
        options.interactable = true;
    }

    public void CloseOptions()
    {
        options.alpha = 0f;
        options.blocksRaycasts = false;
        options.interactable = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
        
}
