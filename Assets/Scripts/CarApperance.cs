using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CarApperance : MonoBehaviour
{
    public string playerName;
    public Color carColor;
    public TextMeshProUGUI nameText;
    public Renderer carRenderer;
    public int playerNumber;
    public Camera backCamera;

    void Start()
    {

    }

    public void SetNameAndColor(string name, Color color)
    {
        nameText.text = name;
        carRenderer.material.color = color;
        nameText.color = color;
    }

    public void SetLocalPlayer()
    {
        FindObjectOfType<CameraController>().SetCameraPropertis(this.gameObject);
        playerName = PlayerPrefs.GetString("PlayerName");
        carColor = ColorCar.IntToColor(PlayerPrefs.GetInt("Red"), PlayerPrefs.GetInt("Green"), PlayerPrefs.GetInt("Blue"));
        nameText.text = playerName;
        carRenderer.material.color = carColor;
        nameText.color = carColor;
        RenderTexture rt = new RenderTexture(1024, 1024, 0);
        backCamera.targetTexture = rt;
        FindObjectOfType<RaceController>().SetMirror(backCamera);

    }

    private void Update()
    {
        
    }






}
