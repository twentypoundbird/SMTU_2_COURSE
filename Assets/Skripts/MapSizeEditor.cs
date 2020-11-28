using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeEditor : MonoBehaviour
{
    public Text sizeWidth, sizeLongth, sizeDepth;
    public GameObject widthSlider, longthSlider, depthSlider;
    public static int sizeX = 0, sizeY = 0, sizeZ = 0;

    private void Start()
    {
        widthSlider.GetComponent<Slider>().value = sizeX;
        longthSlider.GetComponent<Slider>().value = sizeY;
        depthSlider.GetComponent<Slider>().value = sizeZ;
        sizeWidth.text ="" + widthSlider.GetComponent<Slider>().value;
        sizeLongth.text = "" + longthSlider.GetComponent<Slider>().value;
        sizeDepth.text = "" + depthSlider.GetComponent<Slider>().value;
    }

    public void ValueInputWidth(float value)
    {
        sizeX = (int)(value * 10f);
        sizeWidth.text = "" + (int)(value);
    }
    public void ValueInputLongth(float value)
    {
        sizeY = (int)(value * 10f);
        sizeLongth.text = "" + (int)(value);
    }
    public void ValueInputDepth(float value)
    {
        sizeZ = (int)(value * 10f);
        sizeDepth.text = "" + (int)(value);
    }
}
