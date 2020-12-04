using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void IsTapped()
    {
        Debug.Log("EXIT");
        Application.Quit();
    }
}
