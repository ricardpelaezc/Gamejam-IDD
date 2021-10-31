using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAState : MonoBehaviour
{

    public enum States
    {
        IDLE=0, //CAMINAR POR LA HABITACIÓN
        WALK, //ACERCARSE A LA ACCIÓN
        EVENT,
        WAIT,
        DOOR,
    }

    public States WomanStates;

    private List<List<Transform>> RoomWaypoints = new List<List<Transform>>();
    public List<Transform> WayPointsFirstRoom;
    public List<Transform> WayPointsSecondRoom;
    public List<Transform> WayPointsThirdRoom;
    public List<Transform> WayPointsLastRoom;

    public Transform Hung;
    public Transform Picture;


    static IAState Woman;
    public int IndexWaypoint = 0;
    public float timerToNextWayPoint;
    private float maxTimeToNextWayPoint=2f;

    private Animator _Animator;

    private int currentDoor=-1;
    public List<Animator> Doors;
    public List<Transform> WayPointsDoor;

    public Animator Interrogante;
    int currentRoomID => Player.GetPlayer().GetRoomID() - 1;

    [SerializeField] private float speed = 0.7f;
    private void Update()
    {
        IAUpdate();

        timerToNextWayPoint += Time.deltaTime;
        print("EStado woman: "+WomanStates);
    }


    private void Awake()
    {
        Woman = this;
        _Animator = GetComponent<Animator>();
    }
    private void Start()
    {

        SetIdle();


        RoomWaypoints.Add(WayPointsFirstRoom);
        RoomWaypoints.Add(WayPointsSecondRoom);
        RoomWaypoints.Add(WayPointsThirdRoom);
        RoomWaypoints.Add(WayPointsLastRoom);
    }


    static public IAState GetIA() => Woman;
  
    public void IAUpdate()
    {
        switch (WomanStates)
        {
            case States.IDLE:
                StartCoroutine(IdleUpdate());
                break;
            case States.WALK:
                WalkUpdate();
                break;
            case States.EVENT:
                StartCoroutine(UpdateEvent());
                break;
            case States.WAIT:
                WaitUpdate();
                break;
            case States.DOOR:
                OpenDoor();
                break;
            default:
                break;
        }
    }

    public void SetIdle()
    {
        WomanStates = States.IDLE;
        _Animator.SetTrigger("Idle");
    }

    private void SetWalk()
    {
        WomanStates = States.WALK;
        _Animator.SetTrigger("Walk");
    }

    public void SetEvent()
    {
        WomanStates = States.EVENT;
        _Animator.SetTrigger("Walk");
    }

    public void SetWait()
    {
        WomanStates = States.WAIT;
        _Animator.SetTrigger("Idle");
    }

    public void SetDoor()
    {
        WomanStates = States.DOOR;
        currentDoor++;
    }
    private IEnumerator IdleUpdate()
    {
        yield return new WaitForSeconds(2f);
        SetWalk();
    }
    private void WalkUpdate()
    {

        if (IndexWaypoint >= RoomWaypoints[currentRoomID].Count)
            IndexWaypoint = 0;

        if (timerToNextWayPoint > maxTimeToNextWayPoint)
        {
            _Animator.SetTrigger("Walk");
            //rotation
            Vector3 dir = RoomWaypoints[currentRoomID][IndexWaypoint].position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = rotation;

            //movement
            Vector3 newPos = new Vector3(RoomWaypoints[currentRoomID][IndexWaypoint].position.x, transform.position.y, RoomWaypoints[currentRoomID][IndexWaypoint].position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);
        }else
            _Animator.SetTrigger("Idle");

    }

    public IEnumerator UpdateEvent()
    {

        _Animator.SetTrigger("Idle");
        Interrogante.SetBool("Dude", true);
        yield return new WaitForSeconds(1f);
        _Animator.SetTrigger("Walk");

        if (PuzlePanel.GetPuzzlePanel().currentPuzzle == 0)
        {
            Vector3 dir = Hung.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = rotation;

            Vector3 newPos = new Vector3(Hung.position.x, transform.position.y, Hung.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

           // print("DISTANCIA: "+Vector3.Distance(Hung.position, transform.position));
            if (Vector3.Distance(Hung.position, transform.position) < 0.9f)
            {
                SetWait();
                StartCoroutine(MakeHung());
                Interrogante.SetBool("Dude", false);
            }

            


        }
        else if (PuzlePanel.GetPuzzlePanel().currentPuzzle == 1)
        {
            Vector3 dir = Picture.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = rotation;

            Vector3 newPos = new Vector3(Picture.position.x, transform.position.y, Picture.position.z);
            transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

            if (Vector3.Distance(Picture.position, transform.position) < 0.5f)
            {
                SetWait();
                StartCoroutine(MakePicture());
                Interrogante.SetBool("Dude", false);
            }

            
            
        }

    }

    private void WaitUpdate()
    {
        _Animator.SetTrigger("Idle");
    }

    ///
    //////
    /*FUNCIONES DONDE SON TRIGGERS
     LA VIEJA SE MUEVE ALGÚN OBJETO CON
    EL QUE DEBA INTERACTUAR COMO CUADRO(PARA INICIAR PUZZLE)
    PINTURA (INICIAR)
    candado.....*/
    //////
    ///


    public IEnumerator MakeHung()
    {
        yield return new WaitForSeconds(1f);
        PuzlePanel.GetPuzzlePanel().MakePuzzle();
    }

    public IEnumerator MakePicture()
    {
        transform.position = transform.position;
        yield return new WaitForSeconds(1.5f);
        PuzlePanel.GetPuzzlePanel().MakePuzzle();
    }


    public void OpenDoor()
    {
        _Animator.SetTrigger("Walk");
        Vector3 dir = WayPointsDoor[currentDoor].position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(dir);
        rotation.z = 0;
        rotation.x = 0;
        transform.rotation = rotation;

        Vector3 newPos = new Vector3(WayPointsDoor[currentDoor].position.x, transform.position.y, WayPointsDoor[currentDoor].position.z);
        transform.position = Vector3.Lerp(transform.position, newPos, speed * Time.deltaTime);

        if (Vector3.Distance(WayPointsDoor[currentDoor].position, transform.position) < 0.5f)
        {
            Doors[currentDoor].SetBool("Open", true);
            SetIdle();
            IndexWaypoint = 0;
        }


    }
}
