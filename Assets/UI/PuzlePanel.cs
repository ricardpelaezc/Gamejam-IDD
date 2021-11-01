using UnityEngine;
using UnityEngine.UI;

public class PuzlePanel : MonoBehaviour
{
    public int currentPuzzle = 0;
    public Button Button;
    public GameObject PuzzleSlide;
    public GameObject PuzzlePaint;
    public GameObject PuzzlePaintParent;

    public bool PuzzleActive;

    private Animator puzzleSlideAnim;
    private Animator puzzlePaintAnim;



    public Animator inv;
    // public Player p;

    static PuzlePanel PuzzlePanel;

    public bool GetPuzzleActive() => PuzzleActive;
    private void Awake()
    {
        PuzzlePanel = this;
    }

    static public PuzlePanel GetPuzzlePanel() => PuzzlePanel;
    private void Start()
    {
        puzzleSlideAnim = PuzzleSlide.GetComponent<Animator>();
        puzzlePaintAnim = PuzzlePaint.GetComponent<Animator>();
    }
    public void ResetPanel()
    {
        PuzzleActive = false;
        PuzzleSlide.gameObject.SetActive(false);
        PuzzlePaint.gameObject.SetActive(false);
        Button.gameObject.SetActive(false);
    }
    public void MakePuzzle()
    {
        inv.SetBool("Show", false);
        Player.GetPlayer().RemoveInventory();
        if (currentPuzzle == 0)
        {
            PuzzleActive = true;
            PuzzleSlide.gameObject.SetActive(true);
            Button.gameObject.SetActive(true);
            puzzleSlideAnim.SetBool("Show", true);
        }


        if (currentPuzzle == 1)
        {
            PuzzleActive = true;
            PuzzlePaintParent.gameObject.SetActive(true);
            PuzzlePaint.gameObject.SetActive(true);
        }
             //tr   SetActive(true);
        //if(currentPuzzle == 2)
        //    Puzzle
    }
}
