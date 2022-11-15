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

    private void Update()
    {
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");
        if (!RaceController.racing)
        {
            accel = 0;
            brake = 1;
        }
        drivingScript.Drive(accel, brake, steer);
    }
}
