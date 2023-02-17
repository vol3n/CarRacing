using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public Vector3[] positions;
    public CinemachineVirtualCamera cam;
    int activeposition = 0;

    void changeCameraPosition(int position)
    {
        cam.GetCinemachineComponent<CinemachineTransposer>().m_FollowOffset = positions[position];
    }

    private void Start()
    {
        if (positions.Length == 0) return;
        changeCameraPosition(activeposition);

    }

    private void Update()
    {
        if(positions.Length ==0) return;
        if (Input.GetKeyDown(KeyCode.T))
        {
            activeposition++;
            activeposition = activeposition % positions.Length;
            changeCameraPosition(activeposition);

        }
    }

    public void SetCameraPropertis(GameObject car)
    {
        cam.Follow = car.GetComponent<DrivingScript>().rb.transform;
        cam.LookAt = car.GetComponent<DrivingScript>().CameraTarget.transform;
    }

}
