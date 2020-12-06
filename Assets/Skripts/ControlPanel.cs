using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanel : MonoBehaviour
{
    public GameObject forwardLabel;
    public GameObject backLabel;
    public GameObject turnToLeftLabel;
    public GameObject turnToRightLabel;

    public Transform content;
    private void Start()
    {
        content = gameObject.transform.parent.transform.GetChild(0).GetChild(0).GetChild(0);
    }
    public void IsTappedForward()
    {
        Instantiate(forwardLabel,content);
    }
    public void IsTappedBack()
    {
        Instantiate(backLabel, content);
    }
    public void IsTappedTurnLeft()
    {
        Instantiate(turnToLeftLabel, content);
    }
    public void IsTappedTurnRight()
    {
        Instantiate(turnToRightLabel, content);
    }
}
