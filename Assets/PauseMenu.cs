using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    private bool _show;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu(_show);
        }
    }

    public void ShowPauseMenu(bool show)
    {
        if (show)
        {
            Time.timeScale = 0;
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
            _show = false;
        }
        else
        {
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
}
