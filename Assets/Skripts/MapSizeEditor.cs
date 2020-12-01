using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapSizeEditor : MonoBehaviour
{
    public Text sizeWidth, sizeLongth, sizeDepth;
    public GameObject widthSlider, longthSlider, depthSlider;
    public static int step = 10;
    public static int countX = 10, countY = 10, countZ = 10;

    private void Start()
    {
        widthSlider.GetComponent<Slider>().value = countX;
        longthSlider.GetComponent<Slider>().value = countY;
        depthSlider.GetComponent<Slider>().value = countZ;
        sizeWidth.text ="" + widthSlider.GetComponent<Slider>().value;
        sizeLongth.text = "" + longthSlider.GetComponent<Slider>().value;
        sizeDepth.text = "" + depthSlider.GetComponent<Slider>().value;
    }

    public void ValueInputWidth(float value)
    {
        countX = (int)(value);
        sizeWidth.text = "" + (int)(value);
    }
    public void ValueInputLongth(float value)
    {
        countY = (int)(value);
        sizeLongth.text = "" + (int)(value);
    }
    public void ValueInputDepth(float value)
    {
        countZ = (int)(value);
        sizeDepth.text = "" + (int)(value);
    }
}
