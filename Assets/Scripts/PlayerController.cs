using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript drivingScript;
    CheckPointController checkPointController;
    float lastTimeMoving = 0;

    private void Start()
    {
        drivingScript = GetComponent<DrivingScript>();
        checkPointController = drivingScript.rb.GetComponent<CheckPointController>();
    }

    void ResetLayer()
    {
        drivingScript.rb.gameObject.layer = 0;
    }

    void checkIfSafe()
    {
        if (drivingScript.rb.velocity.magnitude > 1 || !RaceController.racing)
        {
            lastTimeMoving = Time.time;
        }
        if (Time.time > lastTimeMoving + 4 || drivingScript.rb.gameObject.transform.position.y < -5)
        {
            drivingScript.rb.transform.position = checkPointController.lastPoint.transform.position;

            drivingScript.rb.transform.rotation = checkPointController.lastPoint.transform.rotation;

            drivingScript.rb.gameObject.layer = 6;

            Invoke("ResetLayer", 2);
        }

    }

    private void Update()
    {
        if (checkPointController.lap == RaceController.totalLaps + 1) return;
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        checkIfSafe();

        if (!RaceController.racing)
        {
            accel = 0;
            brake = 1;
        }
        drivingScript.Drive(accel, brake, steer);
    }
}
