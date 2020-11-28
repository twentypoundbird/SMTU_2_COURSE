using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeEditor : MonoBehaviour
{
    public Text sizeWidth, sizeLongth, sizeDepth;
    public GameObject widthSlider, longthSlider, depthSlider;
    public static int sizeX = 10 * 1000, sizeY = 10 * 1000, sizeZ = 10 * 1000;

    private void Start()
    {
        widthSlider.GetComponent<Slider>().value = sizeX / 1000;
        longthSlider.GetComponent<Slider>().value = sizeY / 1000;
        depthSlider.GetComponent<Slider>().value = sizeZ / 1000;
        sizeWidth.text ="" + widthSlider.GetComponent<Slider>().value;
        sizeLongth.text = "" + longthSlider.GetComponent<Slider>().value;
        sizeDepth.text = "" + depthSlider.GetComponent<Slider>().value;
    }

    public void ValueInputWidth(float value)
    {
        sizeX = (int)(value) * 1000;
        sizeWidth.text = "" + (int)(value);
    }
    public void ValueInputLongth(float value)
    {
        sizeY = (int)(value) * 1000;
        sizeLongth.text = "" + (int)(value);
    }
    public void ValueInputDepth(float value)
    {
        sizeZ = (int)(value) * 1000;
        sizeDepth.text = "" + (int)(value);
    }
}
