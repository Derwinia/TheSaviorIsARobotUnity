using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class PlayerControl : MonoBehaviour
{

    [SerializeField] float MovingTurnSpeed = 360;
    [SerializeField] float StationaryTurnSpeed = 180;
    [SerializeField] float MoveSpeedMultiplier = 10f;

    private Vector3 MoveDirections;

    Rigidbody Rigidbody;
    float TurnAmount;
    float ForwardAmount;
    Vector3 GroundNormal;
    CapsuleCollider Capsule;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Capsule = GetComponent<CapsuleCollider>();
        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal")*MoveSpeedMultiplier * Time.deltaTime;
        float v = Input.GetAxis("Vertical") * MoveSpeedMultiplier * Time.deltaTime;
        if (v != 0 || h != 0)
        {
            MoveDirections = v * Vector3.forward + h * Vector3.right;
            Move(MoveDirections);
        }
        else Stop();
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

    void ApplyExtraTurnRotation()
    {
        // help the character turn faster (this is in addition to root rotation in the animation)
        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    void Stop()
    {
        Rigidbody.velocity = new Vector3();
    }
}
