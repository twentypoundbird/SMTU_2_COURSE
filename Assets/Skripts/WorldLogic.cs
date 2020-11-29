using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldLogic : MonoBehaviour
{
    private static int step = 1000;

    public Camera camera;

    public Material generalMaterial, invisibleMaterial,
        sandMaterial;
    public Mesh generalMesh;
    
    public static Material materialForMiniCube;

    private GameObject miniCube;
    private BoxCollider boxOfMinicube;

    float posX, posY, posZ;

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(MapSizeEditor.sizeX, MapSizeEditor.sizeY, MapSizeEditor.sizeZ);


        for(int x = 0; x < MapSizeEditor.sizeX / step; x++)
        {
            for (int y = 0; /*y < MapSizeEditor.sizeY / step*/ y<1; y++)
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
                    boxOfMinicube = miniCube.AddComponent<BoxCollider>();


                    #region generalMesh

                    miniCube.AddComponent<MeshRenderer>().material = generalMaterial;
                    miniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                    #endregion
                }
            }
        }


        //for (int x = 0; x < MapSizeEditor.sizeX / step; x++)
        //{
        //    for (int y = 1; y < MapSizeEditor.sizeY / step; y++)
        //    {
        //        for (int z = 0; z < MapSizeEditor.sizeZ / step; z++)
        //        {
        //            if (y != 0 ? cubeEnable[x, y - 1, z] : false ||
        //                x != 0 ? cubeEnable[x - 1, y, z] : false ||
        //                z != 0 ? cubeEnable[x, y, z - 1] : false ||
        //                y != MapSizeEditor.sizeY / step - 1 ? cubeEnable[x, y + 1, z] : false ||
        //                x != MapSizeEditor.sizeX / step - 1 ? cubeEnable[x + 1, y, z]  : false ||
        //                z != MapSizeEditor.sizeZ / step - 1 ? cubeEnable[x, y, z + 1] : false)
        //            {
        //                if(!cubeEnable[x,y,z])
        //                {
        //                    int xCoord = x * step - MapSizeEditor.sizeX / 2 + step / 2;
        //                    int yCoord = y * step - MapSizeEditor.sizeY / 2 + step / 2;
        //                    int zCoord = z * step - MapSizeEditor.sizeZ / 2 + step / 2;

        //                    miniCube = new GameObject();
        //                    miniCube.transform.position = new Vector3(xCoord, yCoord, zCoord);
        //                    miniCube.transform.localScale = new Vector3(step, step, step);
        //                    miniCube.transform.parent = transform;

        //                    boxOfMinicube = miniCube.AddComponent<BoxCollider>();
        //                    miniCube.AddComponent<miniCubeLogic>().isInvis = true;

        //                    #region generalMesh

        //                    miniCube.AddComponent<MeshRenderer>().material = invisibleMaterial;
        //                    miniCube.AddComponent<MeshFilter>().mesh = generalMesh;

        //                    #endregion
        //                }
        //            }
        //        }
        //    }
        //}

        materialForMiniCube = sandMaterial;

        //miniCube = new GameObject();
        //miniCube.transform.localScale = new Vector3(step, step, step);
        //miniCube.transform.parent = transform;
        //#region generalMesh

        //miniCube.AddComponent<MeshRenderer>().material = generalMaterial;
        //miniCube.AddComponent<MeshFilter>().mesh = generalMesh;

        //#endregion
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 99999999))
            {
                // hit.collider.gameObject.GetComponent<MeshRenderer>().material = sandMaterial;
                int differenceX = Mathf.Abs((int)(hit.point.x - hit.collider.gameObject.transform.position.x)) < step / 2 ? 0 : (int)(hit.point.x - hit.collider.gameObject.transform.position.x);
                int differenceY = Mathf.Abs((int)(hit.point.y - hit.collider.gameObject.transform.position.y)) < step / 2 ? 0 : (int)(hit.point.y - hit.collider.gameObject.transform.position.y);
                int differenceZ = Mathf.Abs((int)(hit.point.z - hit.collider.gameObject.transform.position.z)) < step / 2 ? 0 : (int)(hit.point.z - hit.collider.gameObject.transform.position.z);
                int positionX = (int)(hit.collider.gameObject.transform.position.x) + differenceX * 2;
                int positionY = (int)(hit.collider.gameObject.transform.position.y) + differenceY * 2;
                int positionZ = (int)(hit.collider.gameObject.transform.position.z) + differenceZ * 2;
                miniCube = new GameObject();
                miniCube.transform.position = new Vector3(positionX,positionY,positionZ);
                miniCube.transform.localScale = new Vector3(step, step, step);
                miniCube.transform.parent = transform;
                boxOfMinicube = miniCube.AddComponent<BoxCollider>();
                miniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                #region generalMesh

                miniCube.AddComponent<MeshRenderer>().material = generalMaterial;

                #endregion
            }
        }

        //posX = (int)(Input.mousePosition.x);
        //posY = (int)(Input.mousePosition.y / step);
        //posZ = (int)(Input.mousePosition.z / step);
        //miniCube.transform.localPosition = new Vector3(posX,posY,posZ);
    }
}
