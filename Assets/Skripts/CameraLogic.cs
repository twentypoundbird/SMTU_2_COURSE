using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraLogic : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.position = new Vector3(MapSizeEditor.sizeX, 0, 0);
    }
    private void Update()
    {
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Water")
        {
            gameObject.GetComponent<PostProcessVolume>().enabled = true;
            Debug.Log("enable");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Water")
        {
            gameObject.GetComponent<PostProcessVolume>().enabled = false;
            Debug.Log("unenable");
        }

    }
}
