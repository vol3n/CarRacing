using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportDriveScript : MonoBehaviour
{
    Rigidbody rigidbodyOfCar;
    float lastTimeChecked;
    [Header("0 - lewe ko³o, 1 - prawe ko³o")]
    public WheelCollider[] frontWheels = new WheelCollider[2];
    public WheelCollider[] rearWheels = new WheelCollider[2];
    public float antiRoll = 5000.0f;

    private void Start()
    {
        rigidbodyOfCar = GetComponent<Rigidbody>();
    }

    void TurnBackCar()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }

    private void Update()
    {
        if(transform.up.y > 0.5f)
            lastTimeChecked = Time.time;

        if(Time.time > lastTimeChecked + 3)
            TurnBackCar();
    }

    private void FixedUpdate()
    {
        HoldWheelsOnGround(frontWheels);
        HoldWheelsOnGround(rearWheels);
    }

    void HoldWheelsOnGround(WheelCollider[] wheels)
    {
        WheelHit hit;
        float leftRiding = 1.0f; //1 - pe³ne rozci¹gniêcie, 0 - pe³ne œciœniêcie zawieszenia
        float rightRiding = 1.0f;

        bool groundedL = wheels[0].GetGroundHit(out hit);
        if (groundedL) leftRiding = (-wheels[0].transform.InverseTransformPoint(hit.point).y -
                 wheels[0].radius) / wheels[0].suspensionDistance;
        bool groundedR = wheels[1].GetGroundHit(out hit);
        if (groundedR) rightRiding = (-wheels[1].transform.InverseTransformPoint(hit.point).y -
                 wheels[1].radius) / wheels[1].suspensionDistance;

        float antiRollForce = (leftRiding - rightRiding) * antiRoll;

        if (groundedL) rigidbodyOfCar.AddForceAtPosition(wheels[0].transform.up * -antiRollForce,
             wheels[0].transform.position);
        if (groundedR) rigidbodyOfCar.AddForceAtPosition(wheels[1].transform.up * antiRollForce,
            wheels[1].transform.position);
    }
}
