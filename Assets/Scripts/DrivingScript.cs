using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelScript[] wheels;
    public float torque = 200; 
    public float maxSteerAngle = 30; //skretnosc
    public float maxBrakeTorque = 500; //moc hamowania
    public float maxSpeed = 200; //maks pred
    public Rigidbody rb;
    public float currentSpeed;

    public GameObject backLights;
    public GameObject reversingLight;

    public GameObject CameraTarget;

    public void Drive(float accel, float brake, float steer)
    {
        accel = Mathf.Clamp(accel, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1) * maxSteerAngle;
        brake = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;
        if (accel < 0)
        {
            reversingLight.SetActive(true);
        }else
        {
            reversingLight.SetActive(false);
        }


        if (brake != 0)
        {
            backLights.SetActive(true);
        }else
        {
            backLights.SetActive(false);
        }


        float thrustTorque = 0;

        currentSpeed = rb.velocity.magnitude * 3;
        if (currentSpeed < maxSpeed)
        {
            thrustTorque = accel * torque;
        }

        foreach (WheelScript wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = thrustTorque;

            if (wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer;
            }
            else
            {
                wheel.wheelCollider.brakeTorque = brake;
            }

            Quaternion quat;
            Vector3 position;
            wheel.wheelCollider.GetWorldPose(out position, out quat);
            wheel.wheel.transform.position = position;
            wheel.wheel.transform.rotation = quat;

        }
    }

    public void XD()
    {

    }
    }
