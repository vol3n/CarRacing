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

    void Start()
    {
        nameText.SetText(playerName);
        carRenderer.material.color = carColor;
        nameText.color = carColor;
        
    }

    private void Update()
    {
        
    }






}
