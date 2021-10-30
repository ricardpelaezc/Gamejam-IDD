using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FragmentsController : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField] private string currentNameRow;

    private float maxDistance = 50f;
    private Sprite currentSprite;
    public Sprite EmptySprite;

    public LayerMask m_LayerMask;

    [Header("Currents Rows")]
    public List<SpriteRenderer> FirstRow;
    public List<SpriteRenderer> SecondRow;
    public List<SpriteRenderer> ThirdRow;

    [Header("Pieces Controller")]
    public List<Sprite> Solution;
    [SerializeField]private List<Sprite> ResetList = new List<Sprite>();
    [SerializeField]private List<SpriteRenderer> AllPieces = new List<SpriteRenderer>();

    private Color emptyColor = new Color(0, 0, 0, 0);
    private Color activeColor = new Color(1, 1, 1, 1);

    Camera m_Camera => Cameras.GetCameras().OrtographicCamera;
    public PuzlePanel m_PuzzlePanel;

    private bool helping = false;
    public Sprite helpSprite;
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AllPieces.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
            ResetList.Add(AllPieces[i].sprite);
        }
       ThirdRow[2].GetComponent<SpriteRenderer>().color = emptyColor;
    }
    private void Update()
    {
        RaycastHit hit;
        Ray ray = m_Camera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, maxDistance, m_LayerMask))
            {
                if(hit.collider.GetComponent<SpriteRenderer>().color != emptyColor)
                {
                    FindRowAndIndex(hit.collider.gameObject.GetComponent<SpriteRenderer>());
                    currentSprite = hit.collider.GetComponent<SpriteRenderer>().sprite;

                    if (!helping)
                        MovePiece(currentNameRow);
                    else
                    {
                        print("Doing help");
                        if (hit.collider != blank.gameObject)
                            Help(currentNameRow);
                    }
                }
            }
        }

        if (CheckCombination()) 
        {
            Cameras.GetCameras().Perspective();
            m_PuzzlePanel.currentPuzzle++;

            m_PuzzlePanel.ResetPanel();
            gameObject.transform.parent.gameObject.SetActive(false);
            Player.GetPlayer().UnlockRoom();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Cameras.GetCameras().Perspective();
            m_PuzzlePanel.currentPuzzle++;
            m_PuzzlePanel.ResetPanel();
            gameObject.transform.parent.gameObject.SetActive(false);
            Player.GetPlayer().UnlockRoom();
        }

    }
    private bool CheckCombination()
    {
        for (int i = 0; i < AllPieces.Count; i++)
        {
            if (AllPieces[i].sprite != Solution[i])
                return false;
        }
        return true;
    }

    private void MovePiece(string name)
    {
        switch (name)
        {
            case "First":
                if(index == 0)
                {

                    if(FirstRow[index+1].color == emptyColor) //esquina izquierda
                    {
                        FirstRow[index + 1].color = activeColor;
                        FirstRow[index + 1].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;

                        FirstRow[index].sprite = EmptySprite;
                    }
                    if (SecondRow[index].color == emptyColor) //abajo
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                }else if(index == 1)//en medio
                {
                    if (FirstRow[index + 1].color == emptyColor) //derecha
                    {
                        FirstRow[index + 1].color = activeColor;
                        FirstRow[index + 1].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                    else if (FirstRow[index - 1].color == emptyColor) //izquierda
                    {
                        FirstRow[index - 1].color = activeColor;
                        FirstRow[index - 1].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index].color == emptyColor) //abajo
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                }
                else
                {
                    if (FirstRow[index - 1].color == emptyColor) //izquierda
                    {
                        FirstRow[index - 1].color = activeColor;
                        FirstRow[index - 1].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index].color == emptyColor) //abajo
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        FirstRow[index].color = emptyColor;
                        FirstRow[index].sprite = EmptySprite;
                    }
                }

                break;
            case "Second":
                if(index == 0)
                {
                    if (FirstRow[index].color == emptyColor) //arriba
                    {
                        FirstRow[index].color = activeColor;
                        FirstRow[index].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index + 1].color == emptyColor) //derecha
                    {
                        SecondRow[index + 1].color = activeColor;
                        SecondRow[index + 1].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if(ThirdRow[index].color == emptyColor)
                    {
                        ThirdRow[index].color = activeColor;
                        ThirdRow[index].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                }
                else if(index == 1)
                {
                    if (FirstRow[index].color == emptyColor) //arriba
                    {
                        FirstRow[index].color = activeColor;
                        FirstRow[index].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index + 1].color == emptyColor) //derecha
                    {
                        SecondRow[index + 1].color = activeColor;
                        SecondRow[index + 1].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index - 1].color == emptyColor) //Izquierda
                    {
                        SecondRow[index - 1].color = activeColor;
                        SecondRow[index - 1].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if (ThirdRow[index].color == emptyColor)
                    {
                        ThirdRow[index].color = activeColor;
                        ThirdRow[index].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                }else if (index == 2)
                {
                    if (FirstRow[index].color == emptyColor) //arriba
                    {
                        FirstRow[index].color = activeColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if (SecondRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor) //Izquierda
                    {
                        SecondRow[index - 1].color = activeColor;
                        SecondRow[index - 1].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                    else if(ThirdRow[index].color == emptyColor)
                    {
                        ThirdRow[index].color = activeColor;
                        ThirdRow[index].sprite = currentSprite;

                        SecondRow[index].color = emptyColor;
                        SecondRow[index].sprite = EmptySprite;
                    }
                }
                break;
            case "Third":
                if(index == 0)
                {
                    if (SecondRow[index].color == emptyColor) //arriba
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                    else if(ThirdRow[index+1].color == emptyColor) //derecha
                    {
                        ThirdRow[index+1].color = activeColor;
                        ThirdRow[index+1].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }

                }else if (index == 1)
                {
                    if (SecondRow[index].color == emptyColor) //arriba
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                    else if (ThirdRow[index + 1].color == emptyColor)//derecha
                    {
                        ThirdRow[index + 1].color = activeColor;
                        ThirdRow[index + 1].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                    else if (ThirdRow[index - 1].color == emptyColor)//izquierda
                    {
                        ThirdRow[index - 1].color = activeColor;
                        ThirdRow[index - 1].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                }
                else if (index == 2)
                {
                    
                    if (SecondRow[index].color == emptyColor) //arriba
                    {
                        SecondRow[index].color = activeColor;
                        SecondRow[index].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                    else if (ThirdRow[index - 1].GetComponent<SpriteRenderer>().color==emptyColor)//izquierda
                    {
                        ThirdRow[index - 1].color = activeColor;
                        ThirdRow[index - 1].sprite = currentSprite;

                        ThirdRow[index].color = emptyColor;
                        ThirdRow[index].sprite = EmptySprite;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void FindRowAndIndex(SpriteRenderer selected)
    {
        if (FirstRow.Contains(selected))
        {
            for (int i = 0; i < FirstRow.Count; i++)
            {
                if (selected == FirstRow[i])
                    index = i;
                
                currentNameRow = "First";
            }
        }
        else if (SecondRow.Contains(selected))
        {
            for (int i = 0; i < SecondRow.Count; i++)
            {
                if (selected == SecondRow[i])
                    index = i;

                currentNameRow = "Second";
            }
        }
        else if (ThirdRow.Contains(selected))
        {
            for (int i = 0; i < ThirdRow.Count; i++)
            {
                if (selected == ThirdRow[i])
                    index = i;

                currentNameRow = "Third";
            } 
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < AllPieces.Count; i++)
        {
            AllPieces[i].sprite = ResetList[i];
            AllPieces[i].color = activeColor;
        }
        ThirdRow[2].color = emptyColor;
    }


    SpriteRenderer blank;
    private void Help(string name)
    {
        blank.color = activeColor;
        blank.sprite = currentSprite;

        switch (name)
        {
            case "First":
                FirstRow[index].color = emptyColor;
                FirstRow[index].sprite = EmptySprite;
                break;

            case "Second":
                SecondRow[index].color = emptyColor;
                SecondRow[index].sprite = EmptySprite;
                break;

            case "Third":
                ThirdRow[index].color = emptyColor;
                ThirdRow[index].sprite = EmptySprite;
                break;
        }
        helping = false;
    }

    private SpriteRenderer FindBlanckItem()
    {
        for (int i = 0; i < AllPieces.Count; i++)
        {
            if (AllPieces[i].color == emptyColor)
                return AllPieces[i];
        }
        return null;
    }
    public void ClickHelp()
    {
        blank = FindBlanckItem();

        blank.sprite = helpSprite;
        blank.color = new Color(1, 1, 1, 1);

        helping = true;
    }
}
