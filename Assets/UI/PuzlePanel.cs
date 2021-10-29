using UnityEngine;
using UnityEngine.UI;

public class PuzlePanel : MonoBehaviour
{
    public int currentPuzzle = 0;
    public Button Button;
  //  public GameObject SelectedItem;
    //CanvasGroup _canvasGroup;
    //private void Awake()
    //{
    //    _canvasGroup = GetComponent<CanvasGroup>();
    //}
    //public void ShowPanel()
    //{
    //    _canvasGroup.alpha = 1;
    //    _canvasGroup.interactable = true;
    //    _canvasGroup.blocksRaycasts = true;
    //}
    //public void HidePanel()
    //{
    //    _canvasGroup.alpha = 0;
    //    _canvasGroup.interactable = false;
    //    _canvasGroup.blocksRaycasts = false;
    //}
    public GameObject PuzzleSlide;
    public GameObject PuzzlePaint;

    //public GameObject PuzzlePasser;


    public void ResetPanel()
    {
        PuzzleSlide.gameObject.SetActive(false);
        PuzzlePaint.gameObject.SetActive(false);
        Button.gameObject.SetActive(false);
    }
    public void MakePuzzle()
    {
        Cameras.GetCameras().Ortographics();

        if (currentPuzzle == 0)
        {
            PuzzleSlide.gameObject.SetActive(true);
            Button.gameObject.SetActive(true);
        }
            

        if (currentPuzzle == 1)
            PuzzlePaint.gameObject.SetActive(true);
        //if(currentPuzzle == 2)
        //    Puzzle
    }
}
