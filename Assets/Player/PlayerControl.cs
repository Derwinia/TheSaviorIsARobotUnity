using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    Vector3 GroundNormal;
    CapsuleCollider Capsule;
    IEnumerator Cor;
    RaycastHit hit;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal")*MoveSpeedMultiplier * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * MoveSpeedMultiplier * Time.deltaTime;
        if (v != 0 || h != 0)
        {
            MoveDirections = v * Vector3.forward + h * Vector3.right;
            Move(MoveDirections);
        }
        else Stop();

        if (Input.GetMouseButtonDown(0))
        {
            MouseMove();

            if (Cor?.MoveNext() ?? false)
            {
                StopCoroutine(Cor);
                Cor = null;
            }
            Cor = ClickToMove(hit);
            StartCoroutine(Cor);
        }
    }

    public void MouseMove()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
             hit = new RaycastHit();
        }
    }

    public void Move(Vector3 move)
    {
        if (move.magnitude > 1f) move.Normalize();
        move = transform.InverseTransformDirection(move);
        move = Vector3.ProjectOnPlane(move, GroundNormal);
        TurnAmount = Mathf.Atan2(move.x, move.z);
        ForwardAmount = move.z;
        transform.Translate(move);
        ApplyExtraTurnRotation();
    }

    private void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    private void Stop()
    {
        Rigidbody.velocity = new Vector3();
    }

    public IEnumerator ClickToMove(RaycastHit destination)
    {
        Vector3 startPosition = transform.position;
        Vector3 endPosition = new Vector3(destination.point.x,transform.position.y, destination.point.z);
        float travelPercent = 0;

        transform.LookAt(endPosition);

        float d = Vector3.Distance(transform.position, destination.point);

        while (travelPercent < 1f)
        {
            travelPercent += Time.deltaTime * (MoveSpeedMultiplier/d);
            transform.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
            yield return new WaitForEndOfFrame();
        }
    }
}
