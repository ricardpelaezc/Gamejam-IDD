using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float _rotationAmount;
    int _roomID = 1;
    bool _appearing;
    bool _lockControls;
    int _unlockedRooms;
    bool _unlockAnimation;
    float _unlockAnimationInitialRotationY;
    float _unlockAnimationTime = 1;
    float _unlockAnimationTimer;
    bool _skyBoxFading;
    float _skyboxFadeTimer;
    Color _initialSkyboxColor;
    Color _finalSkyboxColor;
    float _roomRotationalSpeed = 0;
    bool _dragging = true;
    bool _draggingPickedItem;
    Vector3 _pickedItemLeavePosition;
    Quaternion _pickedItemLeaveRotation;
    bool _returnPickedItem;
    private float _leaveTimer;
    bool _reset;

    public Camera Camera;
    KeyCode Drag = KeyCode.Mouse0;
    KeyCode Interact = KeyCode.Mouse0;
    public GameObject RoomContainer;
    public List<GameObject> Rooms;
    public Material SkyboxMaterial;
    public List<Color> SkyboxColors;
    public Pickable PickedItem;
    public GameObject PickedInstance;
    public bool Picked;
    public GameObject PickedItemTargetItem;
    public Transform Inventory;
    public LayerMask DragIgnoreLayers;
    public float LeaveTime = 0;
    public GameObject InventoryRemover;
    [SerializeField] float _roomRotationalAcceleration = 5;
    [SerializeField] float _roomRotationalDeceleration = 5;
    [SerializeField] float _skyboxFadeTime = 1;

    static Player _Player;

    [SerializeField] private Animator hudAnim;

    private void Awake()
    {
        _Player = this;
    }

    static public Player GetPlayer() => _Player;
    internal int GetRoomID()
    {
        return _unlockedRooms;
    }
    private void Start()
    {
        for (int i = 0; i < Rooms.Count; i++)
        {
            Rooms[i].SetActive(false);
        }
        RenderSettings.skybox.SetColor("_Tint", SkyboxColors[0]);
        UnlockRoom();
    }
    private void Update()
    {
        if (!_lockControls)
        {
            if (_draggingPickedItem)
            {

                if (Input.GetKey(Interact))
                {
                    RaycastHit hit;
                    Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, DragIgnoreLayers))
                    {
                        hudAnim.SetBool("SemiHide", true);
                        Vector3 position = Camera.transform.InverseTransformPoint(hit.point);
                        position = new Vector3(position.x / 2, position.y / 2, position.z / 2);
                        position = Camera.transform.TransformPoint(position);

                        PickedInstance.transform.position = position;
                    }
                }
                else if (Input.GetKeyUp(Interact))
                {
                    hudAnim.SetBool("SemiHide", false);
                    RaycastHit hit;
                    Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Interactable")
                        {

                            if (hit.collider.transform.position == PickedItemTargetItem.transform.position)
                            {
                                hit.collider.GetComponent<Interactable>().Match();
                                Destroy(PickedItem.gameObject);
                                Destroy(PickedInstance);
                                Picked = false;
                                _draggingPickedItem = false;
                                InventoryRemover.SetActive(false);
                            }
                            else
                            {
                                _draggingPickedItem = false;
                                PickedInstance.SetActive(true);
                                _lockControls = true;
                                _returnPickedItem = true;
                                _pickedItemLeavePosition = PickedInstance.transform.position;
                                _pickedItemLeaveRotation = PickedInstance.transform.rotation;
                                InventoryRemover.SetActive(false);
                            }
                        }
                        else if (hit.collider.tag == "InventoryRemover")
                        {
                            RemoveInventory();
                            InventoryRemover.SetActive(false);
                            hudAnim.SetBool("Show", false);
                        }
                        else
                        {
                            _draggingPickedItem = false;
                            PickedInstance.SetActive(true);
                            _lockControls = true;
                            _returnPickedItem = true;
                            _pickedItemLeavePosition = PickedInstance.transform.position;
                            _pickedItemLeaveRotation = PickedInstance.transform.rotation;
                            InventoryRemover.SetActive(false);
                        }
                    }
                    else
                    {
                        _draggingPickedItem = false;
                        PickedInstance.SetActive(true);
                        _lockControls = true;
                        _returnPickedItem = true;
                        _pickedItemLeavePosition = PickedInstance.transform.position;
                        _pickedItemLeaveRotation = PickedInstance.transform.rotation;
                        InventoryRemover.SetActive(false);
                        hudAnim.SetBool("Show", false);
                    }
                }
            }
            else
            {
                if (Input.GetKey(Drag))
                {
                    _roomRotationalSpeed -= Input.GetAxis("Mouse X") * _roomRotationalAcceleration * Time.deltaTime;
                    _dragging = true;
                }
                else
                {
                    _dragging = false;
                }
                if (Input.GetKeyDown(Interact))
                {
                    RaycastHit hit;
                    Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.tag == "Interactable" || hit.collider.tag == "InteractableObj")
                        {
                            Interactable interactable = hit.transform.GetComponent<Interactable>();
                            interactable.Interact();

                            if (hit.collider.tag != "InteractableObj" && !hit.collider.GetComponent<Water>() && !hit.collider.GetComponent<OneClick>() && !hit.collider.GetComponent<Lock>())
                            {
                                hudAnim.SetBool("Show", true);
                            }


                            print("pickedup");
                        }
                        else if (hit.collider.tag == "Inventory" && Picked)
                        {
                            _draggingPickedItem = true;
                            InventoryRemover.SetActive(true);
                            //PickedInstance.SetActive(false);
                        }
                    }
                }
            }
            if (_unlockedRooms != 4)
            {
                if (_rotationAmount >= AngleLimit(_unlockedRooms))
                {
                    float correction = AngleLimit(_unlockedRooms) - _rotationAmount;
                    RoomContainer.transform.Rotate(0.0f, correction, 0.0f);
                    _rotationAmount = AngleLimit(_unlockedRooms);
                    _roomRotationalSpeed = -_roomRotationalSpeed / 2;
                }
                else if (_rotationAmount <= AngleLimit(0))
                {
                    float correction = AngleLimit(0) - _rotationAmount;
                    RoomContainer.transform.Rotate(0.0f, correction, 0.0f);
                    _rotationAmount = AngleLimit(0);
                    _roomRotationalSpeed = -_roomRotationalSpeed / 2;
                }
            }
        }
        if (_returnPickedItem)
        {
            Vector3 movement = Vector3.Lerp(_pickedItemLeavePosition, Inventory.transform.position, _leaveTimer / LeaveTime);
            Quaternion rotationLerp = Quaternion.Lerp(_pickedItemLeaveRotation, Inventory.transform.rotation, _leaveTimer / LeaveTime);
            PickedInstance.transform.position = movement;
            PickedInstance.transform.rotation = rotationLerp; 
            if (_leaveTimer > LeaveTime)
            {
                PickedInstance.transform.position = Inventory.transform.position;
                _leaveTimer = 0;
                _returnPickedItem = false;
                _lockControls = false;
            }
            _leaveTimer += Time.deltaTime;
        }
        if (_roomRotationalSpeed != 0)
        {
            _roomRotationalSpeed += _roomRotationalDeceleration * Time.deltaTime * ((_roomRotationalSpeed < 0) ? 1 : -1) * ((_dragging) ? 0f : 1);

            if (Input.GetMouseButtonDown(0))
            {
                _roomRotationalSpeed = 0;
            }
        }
        else if (Mathf.Abs(_roomRotationalSpeed) < 1)
        {
            _roomRotationalSpeed = 0;
        }
        float rotation = _roomRotationalSpeed * Time.deltaTime;
        RoomContainer.transform.Rotate(0.0f, rotation, 0.0f);
        _rotationAmount += rotation;
        if (_unlockedRooms == Rooms.Count)
        {
            if (_rotationAmount > 360)
            {
                _rotationAmount = 0;
                _reset = true;
                _roomID = 1;
                _initialSkyboxColor = SkyboxColors[Rooms.Count - 1];
                _finalSkyboxColor = SkyboxColors[0];
                _skyBoxFading = true;
                _skyboxFadeTimer = 0;

            }
            if (_rotationAmount < 0)
            {
                _rotationAmount += 360;
                _reset = true;
                _roomID = Rooms.Count;
                _initialSkyboxColor = SkyboxColors[0];
                _finalSkyboxColor = SkyboxColors[Rooms.Count - 1];
                _skyBoxFading = true;
                _skyboxFadeTimer = 0;

            }
        }
        if ((_roomID < _unlockedRooms || _unlockedRooms == Rooms.Count) && !_reset)
        {
            if (_rotationAmount > AngleLimit(_roomID))
            {
                //print(_roomID);
                _initialSkyboxColor = SkyboxColors[_roomID - 1];
                _roomID++;
                _finalSkyboxColor = SkyboxColors[_roomID - 1];
                _skyBoxFading = true;
                _skyboxFadeTimer = 0;

            }
        }
        if ((_roomID > 1 || _unlockedRooms == Rooms.Count) && !_reset)
        {
            if (_rotationAmount < AngleLimit(_roomID - 1))
            {
                _initialSkyboxColor = SkyboxColors[_roomID - 1];
                _roomID--;
                print(_roomID);
                _finalSkyboxColor = SkyboxColors[_roomID - 1];
                _skyBoxFading = true;
                _skyboxFadeTimer = 0;

            }
        }
        if (_skyBoxFading)
        {
            _skyboxFadeTimer += Time.deltaTime;
            Color outputColor = Color.Lerp(_initialSkyboxColor, _finalSkyboxColor, _skyboxFadeTimer / _skyboxFadeTime);
            if (_skyboxFadeTimer > _skyboxFadeTime)
            {
                outputColor = _finalSkyboxColor;
                _skyBoxFading = false;
                _reset = false;
            }
            RenderSettings.skybox.SetColor("_Tint", outputColor);
        }
        if (_unlockAnimation)
        {
            float animationRotation = Mathf.Lerp(_unlockAnimationInitialRotationY, AngleLimit(_unlockedRooms) - 45, _unlockAnimationTimer / _unlockAnimationTime);
            _unlockAnimationTimer += Time.deltaTime;
            if (_unlockAnimationTimer > _unlockAnimationTime)
            {
                animationRotation = Mathf.Lerp(_unlockAnimationInitialRotationY, AngleLimit(_unlockedRooms) - 45, 1);
                _unlockAnimation = false;
                _rotationAmount = animationRotation;

                Rooms[_unlockedRooms - 1].SetActive(true);
                _appearing = true;

            }
            RoomContainer.transform.rotation = Quaternion.Euler(0.0f, animationRotation, 0.0f);
        }
        if (_appearing)
        {
            _lockControls = false;
        }
    }

    public void UnlockRoom()
    {
        _lockControls = true;
        _unlockAnimation = true;
        _unlockAnimationInitialRotationY = RoomContainer.transform.rotation.eulerAngles.y;
        _unlockAnimationTimer = 0;
        _unlockedRooms++;
    }
    private float AngleLimit(int id)
    {
        return 90 * id;
    }
    public void RemoveInventory()
    {
        if (PickedItem)
            PickedItem.ReturnPickedItem();

        _draggingPickedItem = false;
    }
}
