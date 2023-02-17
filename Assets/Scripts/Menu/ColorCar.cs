using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCar : MonoBehaviour
{
    public Renderer rend;

    public Slider redSlider;
    public Slider greenSlider;
    public Slider blueSlider;

    public Text redSliderText;
    public Text greenSliderText;
    public Text blueSliderText;

    public Color col;

    public static Color IntToColor(int red, int green, int blue)
    {
        float r = (float)red / 255;
        float g = (float)green / 255;
        float b = (float)blue / 255;

        return new Color(r, g, b);

    }

    void SetCarColor(int red, int green, int blue)
    {
        rend.material.color = IntToColor(red, green, blue);

        PlayerPrefs.SetInt("Red", red);
        PlayerPrefs.SetInt("Green", green);
        PlayerPrefs.SetInt("Blue", blue);

    }

    private void Start()
    {
        col = IntToColor(
            PlayerPrefs.GetInt("Red"),
            PlayerPrefs.GetInt("Green"),
            PlayerPrefs.GetInt("Blue"));

        rend.material.color = col;

        redSlider.value = (int)(col.r * 255);
        greenSlider.value = (int)(col.g * 255);
        blueSlider.value = (int)(col.b * 255);

    }

    private void Update()
    {
        SetCarColor(
            (int)redSlider.value,
            (int)greenSlider.value,
            (int)blueSlider.value
            );

        redSliderText.text = redSlider.value.ToString();
        greenSliderText.text = greenSlider.value.ToString();
        blueSliderText.text = blueSlider.value.ToString();

    }



}
