﻿using UnityEngine;
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
    private Animator puzzleSlideAnim;
    private Animator puzzlePaintAnim;
    //public GameObject PuzzlePasser;

    public Animator inv;
    public Player p;
    private void Start()
    {
        puzzleSlideAnim = PuzzleSlide.GetComponent<Animator>();
        puzzlePaintAnim = PuzzlePaint.GetComponent<Animator>();
    }
    public void ResetPanel()
    {
        PuzzleSlide.gameObject.SetActive(false);
        PuzzlePaint.gameObject.SetActive(false);
        Button.gameObject.SetActive(false);
    }
    public void MakePuzzle()
    {
        inv.SetBool("Show", false);
        p.RemoveInventory();
        if (currentPuzzle == 0)
        {
            PuzzleSlide.gameObject.SetActive(true);
            Button.gameObject.SetActive(true);
            puzzleSlideAnim.SetBool("Show", true);
        }
            

        if (currentPuzzle == 1)
            PuzzlePaint.gameObject.SetActive(true);
        //if(currentPuzzle == 2)
        //    Puzzle
    }
}
