using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CanvasGroup _canvasGroup;
    public static bool Pause = false;
    private bool _show;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause)
            {
                ShowPauseMenu(true);
            }
            else
            {
                ShowPauseMenu(_show);
            }

        }


    }

    public void ShowPauseMenu(bool show)
    {
        if (show)
        {
            Pause = false;
            Time.timeScale = 0;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _show = false;
        }
        else
        {
            Pause = true;
            Time.timeScale = 1;
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _show = true;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
