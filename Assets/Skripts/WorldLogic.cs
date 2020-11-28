using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLogic : MonoBehaviour
{
    private int step = 1000;

    public Material material;
    public Mesh mesh;
    private void Start()
    {
        gameObject.transform.localScale = new Vector3(MapSizeEditor.sizeX, MapSizeEditor.sizeY, MapSizeEditor.sizeZ);

        GameObject miniCube;
        for(int x = 0; x < MapSizeEditor.sizeX / step; x++)
        {
            for (int y = 0; y < MapSizeEditor.sizeY / step; y++)
            {
                for (int z = 0; z < MapSizeEditor.sizeZ / step; z++)
                {
                    int xCoord = x * step - MapSizeEditor.sizeX / 2 + step / 2;
                    int yCoord = y * step - MapSizeEditor.sizeY / 2 + step / 2;
                    int zCoord = z * step - MapSizeEditor.sizeZ / 2 + step / 2;
                    miniCube = new GameObject();
                    miniCube.transform.position = new Vector3(xCoord, yCoord, zCoord);
                    miniCube.transform.localScale = new Vector3(step, step, step);
                    miniCube.transform.parent = transform;
                    #region mesh

                    miniCube.AddComponent<MeshRenderer>().material = material;
                    miniCube.AddComponent<MeshFilter>().mesh = mesh;

                    #endregion
                }
            }
        }
    }


}
