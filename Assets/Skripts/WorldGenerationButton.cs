using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldGenerationButton : MonoBehaviour
{
    public void IsTapped()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
