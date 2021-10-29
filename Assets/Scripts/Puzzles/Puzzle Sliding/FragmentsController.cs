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
    public List<GameObject> FirstRow;
    public List<GameObject> SecondRow;
    public List<GameObject> ThirdRow;

    [Header("Pieces Controller")]
    public List<Sprite> Solution;
    [SerializeField]private List<Sprite> ResetList = new List<Sprite>();
    [SerializeField]private List<SpriteRenderer> AllPieces = new List<SpriteRenderer>();

    private Color emptyColor = new Color(0, 0, 0, 0);
    private Color activeColor = new Color(1, 1, 1, 1);

    Camera m_Camera => Cameras.GetCameras().OrtographicCamera;
    public PuzlePanel m_PuzzlePanel;
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
                    FindRowAndIndex(hit.collider.gameObject);
                    currentSprite = hit.collider.GetComponent<SpriteRenderer>().sprite;
                    MovePiece(currentNameRow);
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

                    if(FirstRow[index+1].GetComponent<SpriteRenderer>().color == emptyColor) //esquina izquierda
                    {
                        FirstRow[index + 1].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index + 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;

                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //abajo
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }else if(index == 1)//en medio
                {
                    if (FirstRow[index + 1].GetComponent<SpriteRenderer>().color == emptyColor) //derecha
                    {
                        FirstRow[index + 1].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index + 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (FirstRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor) //izquierda
                    {
                        FirstRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //abajo
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }
                else
                {
                    if (FirstRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor) //izquierda
                    {
                        FirstRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //abajo
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        FirstRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }

                break;
            case "Second":
                if(index == 0)
                {
                    if (FirstRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        FirstRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index + 1].GetComponent<SpriteRenderer>().color == emptyColor) //derecha
                    {
                        SecondRow[index + 1].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index + 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if(ThirdRow[index].GetComponent<SpriteRenderer>().color == emptyColor)
                    {
                        ThirdRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }
                else if(index == 1)
                {
                    if (FirstRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        FirstRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index + 1].GetComponent<SpriteRenderer>().color == emptyColor) //derecha
                    {
                        SecondRow[index + 1].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index + 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor) //Izquierda
                    {
                        SecondRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (ThirdRow[index].GetComponent<SpriteRenderer>().color == emptyColor)
                    {
                        ThirdRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }else if (index == 2)
                {
                    if (FirstRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        FirstRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        FirstRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (SecondRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor) //Izquierda
                    {
                        SecondRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if(ThirdRow[index].GetComponent<SpriteRenderer>().color == emptyColor)
                    {
                        ThirdRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        SecondRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }
                break;
            case "Third":
                if(index == 0)
                {
                    if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if(ThirdRow[index+1].GetComponent<SpriteRenderer>().color == emptyColor) //derecha
                    {
                        ThirdRow[index+1].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index+1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }

                }else if (index == 1)
                {
                    if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (ThirdRow[index + 1].GetComponent<SpriteRenderer>().color == emptyColor)//derecha
                    {
                        ThirdRow[index + 1].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index + 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (ThirdRow[index - 1].GetComponent<SpriteRenderer>().color == emptyColor)//izquierda
                    {
                        ThirdRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }
                else if (index == 2)
                {
                    
                    if (SecondRow[index].GetComponent<SpriteRenderer>().color == emptyColor) //arriba
                    {
                        SecondRow[index].GetComponent<SpriteRenderer>().color = activeColor;
                        SecondRow[index].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                    else if (ThirdRow[index - 1].GetComponent<SpriteRenderer>().color==emptyColor)//izquierda
                    {
                        ThirdRow[index - 1].GetComponent<SpriteRenderer>().color = activeColor;
                        ThirdRow[index - 1].GetComponent<SpriteRenderer>().sprite = currentSprite;

                        ThirdRow[index].GetComponent<SpriteRenderer>().color = emptyColor;
                        ThirdRow[index].GetComponent<SpriteRenderer>().sprite = EmptySprite;
                    }
                }
                break;
            default:
                break;
        }
    }

    private void FindRowAndIndex(GameObject selected)
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
        ThirdRow[2].GetComponent<SpriteRenderer>().color = emptyColor;
    }
}
