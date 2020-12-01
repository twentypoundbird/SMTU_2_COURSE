using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;
using System;

public class WorldLogic : MonoBehaviour
{
    public GameObject submarine, mine, chainLink, fixOnTheGround;

    private GameObject model3D;
    private int modelID;

    private static int step = MapSizeEditor.step;

    public Camera camera;

    public Material generalMaterial, invisibleMaterial,
        sandMaterial;
    public Mesh generalMesh;

    public static Material materialForMiniCube;

    private GameObject miniCube, newMiniCube;
    private BoxCollider boxOfMinicube;

    GameObject[,,] TypeOfObjectOnMap;

    float mouseScrollValue;
    float posX, posY, posZ;

    public Color cl_warning;
    private Color cl_temp;
    private float positionX, positionY, positionZ, differenceX, differenceY, differenceZ;
    int xCoord,yCoord,zCoord;

    WorldLogic()
    {
        TypeOfObjectOnMap = new GameObject[MapSizeEditor.countX, MapSizeEditor.countY, MapSizeEditor.countZ];
    }
    public void IsTapped(int num)
    {
        modelID = num;
        switch (num)
        {
            case 1:
                model3D = submarine;
                break;
            case 2:
                model3D = mine;
                break;
            case 3:
                model3D = chainLink;
                break;
            case 4:
                model3D = fixOnTheGround;
                break;
            default:
                modelID = 0;
                model3D = null;
                break;
        }
    }

    private void Start()
    {
        gameObject.transform.localScale = new Vector3(MapSizeEditor.countX * step, MapSizeEditor.countY * step, MapSizeEditor.countZ * step);

        for(int x = 0; x < MapSizeEditor.countX; x++)
        {
            for (int y = 0; /*y < MapSizeEditor.sizeY / step*/ y<1; y++)
            {
                for (int z = 0; z < MapSizeEditor.countZ; z++)
                {
                    xCoord = (int)(step * (x/* - MapSizeEditor.countX / 2 + 0.5*/));
                    yCoord = (int)(step * (y/* - MapSizeEditor.countY / 2 + 0.5*/));
                    zCoord = (int)(step * (z/* - MapSizeEditor.countZ / 2 + 0.5*/));
                    miniCube = new GameObject();
                    miniCube.transform.position = new Vector3(xCoord, yCoord, zCoord);
                    miniCube.transform.localScale = new Vector3(step, step, step);
                    miniCube.transform.parent = transform;
                    miniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);


                    #region generalMesh

                    miniCube.AddComponent<MeshRenderer>().material = generalMaterial;
                    miniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                    #endregion
                }
            }
        }

        materialForMiniCube = sandMaterial;
    }

    void SpawnerControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (newMiniCube == null)
            {
                if (model3D != null)
                {
                    newMiniCube = new GameObject();
                    newMiniCube.transform.localScale = new Vector3(step + 1, step +1 , step + 1);
                    newMiniCube.transform.parent = transform;
                    newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                    #region generalMesh

                    //newMiniCube.AddComponent<MeshRenderer>().material = generalMaterial;

                    #endregion

                    Instantiate(model3D, newMiniCube.transform);

                }
                else
                {
                    Debug.Log("нет модели");
                }
            }
            else
            {
                if (TypeOfObjectOnMap[xCoord, yCoord, zCoord] == null)
                {
                    newMiniCube.transform.localScale = new Vector3(step / 100f, step / 100f, step / 100f);
                    newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                    TypeOfObjectOnMap[xCoord, yCoord, zCoord] = newMiniCube;
                    newMiniCube = null;
                    mouseScrollValue = 0;
                }
                else
                {
                    Debug.LogWarning("Место занято другим объектом!");
                }
            }
        }


        if (newMiniCube != null)
        {
            if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
            {
                mouseScrollValue += Input.mouseScrollDelta.y;
            }
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000))
            {
                differenceX = Mathf.Abs(hit.point.x - hit.collider.gameObject.transform.position.x) < step / 2 - 0.0001f ? 0 : hit.point.x - hit.collider.gameObject.transform.position.x;
                differenceY = Mathf.Abs(hit.point.y - hit.collider.gameObject.transform.position.y) < step / 2 - 0.0001f ? 0 : hit.point.y - hit.collider.gameObject.transform.position.y;
                differenceZ = Mathf.Abs(hit.point.z - hit.collider.gameObject.transform.position.z) < step / 2 - 0.0001f ? 0 : hit.point.z - hit.collider.gameObject.transform.position.z;

                positionX = hit.collider.gameObject.transform.position.x + differenceX * 2;
                positionY = hit.collider.gameObject.transform.position.y + differenceY * 2 + mouseScrollValue * step;
                positionZ = hit.collider.gameObject.transform.position.z + differenceZ * 2;

                xCoord = (int)(positionX / step + 0.1);
                yCoord = (int)(positionY / step + 0.1);
                zCoord = (int)(positionZ / step + 0.1);

                Debug.Log("Cord{" + positionX + ":" + positionY + ":" + positionZ + "}");
                Debug.Log("RealCord{" + xCoord + ":" + yCoord + ":" + zCoord + "}");


                if (positionX >= 0 && xCoord < MapSizeEditor.countX)
                {
                    if (positionY >= 0 && yCoord < MapSizeEditor.countY)
                    {
                        if (positionZ >= 0 && zCoord < MapSizeEditor.countZ)
                        {
                            newMiniCube.transform.position = new Vector3(positionX, positionY, positionZ);
                            if (TypeOfObjectOnMap[xCoord, yCoord, zCoord] == null)
                            {
                                if (cl_temp != newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color)
                                {
                                    if (cl_warning != newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color)
                                    {
                                        cl_temp = newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color;
                                    }
                                }
                                newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = cl_temp;
                            }
                            else
                            {
                                newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = cl_warning;
                                newMiniCube.GetComponentInChildren<MeshRenderer>().sharedMaterial.color = cl_warning;
                            }
                            if (model3D == mine)
                            {
                                RaycastHit hitUnderMine;
                                if (Physics.Raycast(newMiniCube.transform.position, -1 * newMiniCube.transform.up, out hitUnderMine, 100 * step))
                                {

                                }
                            }
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SpawnerControl();
        }
    }
}
