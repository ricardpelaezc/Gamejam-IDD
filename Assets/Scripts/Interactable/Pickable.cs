using UnityEngine;
using UnityEngine.UI;
public class Pickable : Interactable
{
    Vector3 _intialPosition;
    Quaternion _intialRotation;
    Vector3 _finalPosition;
    Quaternion _finalRotation;

    bool _pick;
    float _pickTimer;
    GameObject _instance;
    public Player PlayerScript;
    public GameObject InventoryPoint;
    public float PickTime;
    public GameObject TargetGameObject;

    public override void Interact()
    {
        if (!PlayerScript.Picked)
        {
            _intialPosition = transform.position;
            _finalPosition = InventoryPoint.transform.position;
            _intialRotation = transform.rotation;
            _finalRotation = InventoryPoint.transform.rotation;
            _instance = Instantiate(gameObject, null);
            if (_instance.GetComponent<MeshRenderer>())
                _instance.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            foreach (MeshRenderer meshRenderer in _instance.GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            }
            _instance.GetComponent<Collider>().enabled = false;
            EnableRenderers(false);

            _pick = true;
            PlayerScript.Picked = true;
            PlayerScript.PickedItemTargetItem = TargetGameObject;
            PlayerScript.PickedInstance = _instance;
        }
    }
    private void Update()
    {
        if (_pick)
        {
            Vector3 movement = Vector3.Lerp(_intialPosition, _finalPosition, _pickTimer / PickTime);
            Quaternion rotation = Quaternion.Lerp(_intialRotation, _finalRotation, _pickTimer / PickTime);
            _instance.transform.position = movement;
            _instance.transform.rotation = rotation;
            if (_pickTimer > PickTime)
            {
                _instance.transform.position = _finalPosition;
                PlayerScript.PickedItem = this;
                _pickTimer = 0;
                _pick = false;
            }
            _pickTimer += Time.deltaTime;
        }
    }
    public void ReturnPickedItem()
    {
        EnableRenderers(true);
        PlayerScript.Picked = false;
        Destroy(_instance);
    }
    public override void Match()
    {
    }
    private void EnableRenderers(bool b)
    {
        if (transform.GetComponent<Renderer>())
            transform.GetComponent<Renderer>().enabled = b;
        foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
        {
            renderer.enabled = b;
        }
    }
}
