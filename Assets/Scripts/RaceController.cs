using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RaceController : MonoBehaviour
{
    public static bool racing = false;
    public static int totalLaps = 1;
    public int timer = 3;

    public Text startText;
    AudioSource audioSource;
    public AudioClip count;
    public AudioClip start;
    public GameObject endPanel;

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

    private void Start()
    {
        endPanel.SetActive(false);
        startText.gameObject.SetActive(false);
        InvokeRepeating("CountDown", 3, 1);
        audioSource = GetComponent<AudioSource>();


        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        carsController = new CheckPointController[cars.Length];

        for (int i = 0; i < cars.Length; i++)
        {
            carsController[i] = cars[i].GetComponent<CheckPointController>();
        }
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

            }


        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
