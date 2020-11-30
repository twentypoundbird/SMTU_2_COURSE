using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeEditor : MonoBehaviour
{
    public Text sizeWidth, sizeLongth, sizeDepth;
    public GameObject widthSlider, longthSlider, depthSlider;
    public static int step = 10;
    public static int sizeX = 10 * step, sizeY = 10 * step, sizeZ = 10 * step;

    private void Start()
    {
        widthSlider.GetComponent<Slider>().value = sizeX / step;
        longthSlider.GetComponent<Slider>().value = sizeY / step;
        depthSlider.GetComponent<Slider>().value = sizeZ / step;
        sizeWidth.text ="" + widthSlider.GetComponent<Slider>().value;
        sizeLongth.text = "" + longthSlider.GetComponent<Slider>().value;
        sizeDepth.text = "" + depthSlider.GetComponent<Slider>().value;
    }

    public void ValueInputWidth(float value)
    {
        sizeX = (int)(value) * step;
        sizeWidth.text = "" + (int)(value);
    }
    public void ValueInputLongth(float value)
    {
        sizeY = (int)(value) * step;
        sizeLongth.text = "" + (int)(value);
    }
    public void ValueInputDepth(float value)
    {
        sizeZ = (int)(value) * step;
        sizeDepth.text = "" + (int)(value);
    }
}
