using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] Camera PlayerCamera;

    [SerializeField] float MovingTurnSpeed = 360;
    [SerializeField] float StationaryTurnSpeed = 180;
    [SerializeField] float MoveSpeedMultiplier = 10f;
    private Vector3 MoveDirections;
    Rigidbody Rigidbody;
    float TurnAmount;
    float ForwardAmount;
    CapsuleCollider Capsule;

    public float ActionRange { get { return actionRange; } }
    float actionRange = 10f;
    IEnumerator Cor;

    RaycastHit hit;
    GameObject hitObject;

    public Ressource ressource;
    public Tile tile;
    public Building building;

    bool moveForAction = false;
    public bool constructMode = false;

    int layerMask = 1 << 6;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        
        layerMask = ~layerMask;
    }

    private void Update()
    {
        GetKeyboardInput();
        if(!IsMouseOverUI()) GetMouseInput();
    }

    private void GetKeyboardInput()
    {
        float h = Input.GetAxis("Horizontal") * MoveSpeedMultiplier * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * MoveSpeedMultiplier * Time.deltaTime;
        if (v != 0 || h != 0)
        {
            StopActionCoroutine();
            MoveDirections = v * Vector3.forward + h * Vector3.right;
            MoveFromKeyboard(MoveDirections);
        }
        else StopKeyboard();
    }

    public void MoveFromKeyboard(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, new Vector3());
        TurnAmount = Mathf.Atan2(move.x, move.z);
        ForwardAmount = move.z;
        transform.Translate(move);
        ApplyExtraTurnRotation();
    }

    private void GetMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopActionCoroutine();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                switch (hit.transform.tag)
                {
                    case "Ground":
                        MouseClickOnGround();
                        break;

                    case "Ressources":
                        MouseClickOnRessource();
                        break;

                    case "Building":
                        MouseClickOnBuilding();
                        break;

                    default: break;
                }
            }
        }
    }

    private void MouseClickOnGround()
    {
        tile = hit.transform.gameObject.GetComponent<Tile>();
        if (constructMode)
        {
            if (tile.InRange())
            {
                tile.ConstructBuilding();
            }
            else
            {
                moveForAction = true;
                Cor = MoveFromMouse();
                StartCoroutine(Cor);
            }
        }
        else if (tile.transform.childCount == 0)
        {
            Cor = MoveFromMouse();
            StartCoroutine(Cor);
        }
    }
    
    private void MouseClickOnRessource()
    {
        hitObject = hit.transform.gameObject;
        ressource = hitObject.gameObject.GetComponentInParent<Ressource>();
        if (ressource.InRange())
        {
            Cor = ressource.Harvest();
            StartCoroutine(Cor);
        }
        else
        {
            moveForAction = true;
            Cor = MoveFromMouse();
            StartCoroutine(Cor);
        }
    }

    private void MouseClickOnBuilding()
    {
        hitObject = hit.transform.gameObject;
        building = hitObject.gameObject.GetComponentInParent<Building>();
        building.SelectedBuilding();
        
    }

    public IEnumerator MoveFromMouse()
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(hit.point.x,transform.position.y, hit.point.z);
        float travelPercent = 0;

        transform.LookAt(endPosition);

        float d = Vector3.Distance(transform.position, hit.point);

        if(moveForAction)
        {
            if(ressource is not null)
            {
                while (travelPercent < 1f && !ressource.InRange())
                {
                    travelPercent += Time.deltaTime * (MoveSpeedMultiplier / d);
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
                moveForAction = false;
                Cor = ressource.Harvest();
                StartCoroutine(Cor);
            }
            else if (tile is not null){
                while (travelPercent < 1f && !tile.InRange())
                {
                    travelPercent += Time.deltaTime * (MoveSpeedMultiplier / d);
                    transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                    yield return new WaitForEndOfFrame();
                }
                moveForAction = false;
                tile.ConstructBuilding();
            }
            
        }
        else
        {
            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * (MoveSpeedMultiplier/d);
                transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    private void StopKeyboard()
    {
        Rigidbody.velocity = new Vector3();
    }
    private void StopActionCoroutine()
    {
        if (ressource is not null)ressource.isUsed = false;
        if (Cor is not null)
        {
            StopCoroutine(Cor);
            Cor = null;
        }
        ressource = null;
    }

    private bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
