using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLogic : MonoBehaviour
{
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(MapSizeEditor.sizeX, MapSizeEditor.sizeY, MapSizeEditor.sizeZ);
    }
}
