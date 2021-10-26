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

    public Camera Camera;
    public KeyCode Drag = KeyCode.Mouse0;
    public KeyCode Interact = KeyCode.Mouse1;
    public GameObject RoomContainer;
    public List<GameObject> Rooms;
    public Material SkyboxMaterial;
    public List<Color> SkyboxColors;
    [SerializeField] float _roomRotationalAcceleration = 5;
    [SerializeField] float _roomRotationalDeceleration = 5;
    [SerializeField] float _skyboxFadeTime = 1;

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
            if (Input.GetKey(Drag))
            {
                _roomRotationalSpeed -= Input.GetAxis("Mouse X") * _roomRotationalAcceleration * Time.deltaTime;
                _dragging = true;
            }
            if (Input.GetKeyDown(Interact))
            {
                RaycastHit hit;
                Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Interactable")
                    {
                        Interactable interactable = hit.transform.GetComponent<Interactable>();
                        interactable.Interact();
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
                    _roomRotationalSpeed = -_roomRotationalSpeed/2;
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
        if (_roomRotationalSpeed != 0)
        {
            _roomRotationalSpeed += _roomRotationalDeceleration * Time.deltaTime * ((_roomRotationalSpeed < 0) ? 1 : -1) * ((_dragging) ? 0.25f : 1);
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
            _rotationAmount = _rotationAmount % 360;
            if (_rotationAmount < 0)
            {
                _rotationAmount += 360;
            }
        }
        if (_roomID < _unlockedRooms || _unlockedRooms == Rooms.Count)
        {
            Debug.Log("CanRight");
            if (_rotationAmount > AngleLimit(_roomID))
            {
                _initialSkyboxColor = SkyboxColors[_roomID - 1];
                _roomID++;
                if (_roomID > Rooms.Count)
                {
                    _roomID = 1;
                }
                _finalSkyboxColor = SkyboxColors[_roomID - 1];
                _skyBoxFading = true;
                _skyboxFadeTimer = 0;
            }
        }
        if (_roomID > 1 || _unlockedRooms == Rooms.Count)
        {
            if (_rotationAmount < AngleLimit(_roomID - 1))
            {
                Debug.Log("CanLeft");
                _initialSkyboxColor = SkyboxColors[_roomID - 1];
                _roomID--;
                if (_roomID < 0)
                {
                    _roomID = Rooms.Count;
                }
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
}