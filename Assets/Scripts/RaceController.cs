using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
public class RaceController : MonoBehaviourPunCallbacks
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;
    public GameObject endPanel;

    public GameObject carPrefab;
    public Transform[] spawnPos;
    public int playerCount = 2;

    public RawImage mirror;

    public GameObject startButton;
    public GameObject waitingText;

    public GameObject placePanel;
    public Text[] places;

    public CheckPointController[] carsController;
    void CountDown()
    {
        startText.gameObject.SetActive(true);
        if (timer != 0)
        {
            startText.text = timer.ToString();
            if(count!= null) audioSource.PlayOneShot(count);
            timer--;
        }else
        {
            startText.text = "START!!!";
            if(start != null) audioSource.PlayOneShot(start);
            racing = true;
            CancelInvoke("CountDown");
            Invoke("HideStartText", 1);
        }
    }

    private void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }

    void CarCreate()
    {
        for (int i = 0; i < playerCount; i++)
        {
            GameObject car = Instantiate(carPrefab);

            car.transform.position = spawnPos[i].position;
            car.transform.rotation = spawnPos[i].rotation;
            car.GetComponent<CarApperance>().playerNumber = i;

            if (i == 0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                FindObjectOfType<CameraController>().SetCameraPropertis(car);
            }

        }
    }

    [PunRPC]
    public void StartGame()
    {
        startButton.SetActive(false);
        waitingText.SetActive(false);


        InvokeRepeating("CountDown", 3, 1);
        audioSource = GetComponent<AudioSource>();
        //CarCreate();

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
    }

    public void BeginGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("StartGame", RpcTarget.All, null);
        }
    }

    void CreateNetworkCar()
    {
        Vector3 startPos = spawnPos[0].position;
        Quaternion startRot = spawnPos[0].rotation;
        GameObject playerCar = null;

        object[] instanceData = new object[4];
        instanceData[0] = (string)PlayerPrefs.GetString("PlayerName");
        instanceData[1] = PlayerPrefs.GetString("Red");
        instanceData[2] = PlayerPrefs.GetString("Green");
        instanceData[3] = PlayerPrefs.GetString("Blue");

        if (OnlinePlayer.LocalPlayerInstance == null)
        {
            startPos = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount -1].position;
            startRot = spawnPos[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation;

            playerCar = PhotonNetwork.Instantiate(carPrefab.name,startPos,startRot, 0,instanceData);
            playerCar.GetComponent<CarApperance>().SetLocalPlayer();

            playerCar.GetComponent<DrivingScript>().enabled = true;
            playerCar.GetComponent<PlayerController>().enabled = true;

        }
    }

    private void Start()
    {
        endPanel.SetActive(false);
        startButton.SetActive(false);
        waitingText.SetActive(false);

        if (PhotonNetwork.IsConnected)
        {
            CreateNetworkCar();
            if (PhotonNetwork.IsMasterClient)
            {
                startButton.SetActive(true);
            }
            else
            {
                waitingText.SetActive(true);
            }
        }

        //GetComponent<OnlinePlayer>().SetNameAndColor(playerName, playerColor);
    }



    private void LateUpdate()
    {
        int finishedLap = 0;
        foreach (CheckPointController controller in carsController)
        {
            if (controller.lap == totalLaps + 1) finishedLap += 1;

            if (finishedLap == carsController.Length && racing)
            {
                endPanel.SetActive(true);
                racing = false;
                placePanel.SetActive(true);

                GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
                carsController = new CheckPointController[cars.Length];

                for (int i = 0; i < cars.Length; i++)
                {
                    //places[i] = 
                    //carsController[i] = cars[i].GetComponent<CheckPointController>();
                }

            }


        }
    }


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void SetMirror(Camera backCamera)
    {
        mirror.texture = backCamera.targetTexture;
    }
}
