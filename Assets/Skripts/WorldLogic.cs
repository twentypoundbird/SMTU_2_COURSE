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

    private static int step = MapSizeEditor.step;

    public Camera camera;

    public Material generalMaterial, invisibleMaterial,
        sandMaterial;
    public Mesh generalMesh;

    public static Material materialForMiniCube;

    private GameObject newMiniCube;
    private BoxCollider boxOfMinicube;

    GameObject[,,] TypeOfObjectOnMap;

    float mouseScrollValue;
    float posX, posY, posZ;

    private float positionX, positionY, positionZ, differenceX, differenceY, differenceZ;
    int xCoord, yCoord, zCoord;
    int tempxCoord = 0, tempzCoord = 0;
    bool Boolfas;

    WorldLogic()
    {
        TypeOfObjectOnMap = new GameObject[MapSizeEditor.countX, MapSizeEditor.countY, MapSizeEditor.countZ];
    }

    public void IsTapped(int num)
    {
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
                    newMiniCube = new GameObject();
                    newMiniCube.transform.position = new Vector3(xCoord, yCoord, zCoord);
                    newMiniCube.transform.localScale = new Vector3(step, step, step);
                    newMiniCube.transform.parent = transform;
                    newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);


                    #region generalMesh

                    newMiniCube.AddComponent<MeshRenderer>().material = generalMaterial;
                    newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                    TypeOfObjectOnMap[x, y, z] = newMiniCube;
                    #endregion
                }
            }
        }
        newMiniCube = null;
        xCoord = 0;
        yCoord = 0; 
        zCoord = 0;
        materialForMiniCube = sandMaterial;
    }

    /// <summary> Метод, отвечающий за инициализацию 
    /// <see cref="UnityEngine.GameObject"/> 
    ///  на сцене
    ///  <para> Является методом класса <seealso cref="WorldLogic"/></para>
    /// </summary>
    void SpawnerControl(GameObject model3D)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (newMiniCube == null)
            {
                if (model3D != null)
                {
                    //newMiniCube = new GameObject;
                    newMiniCube = new GameObject();
                    newMiniCube.transform.localScale = new Vector3(step + 1, step +1 , step + 1);
                    newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;

                    if (newMiniCube == model3D) Debug.Log("Всё заебись");
                    #region generalMesh

                    //newMiniCube.AddComponent<MeshRenderer>().material = generalMaterial;

                    #endregion

                    //Instantiate(model3D, newMiniCube.transform);
                    Instantiate(model3D, newMiniCube.transform);
                    if(model3D == mine)
                    {
                        newMiniCube.tag = "mine";
                    }

                }
                else
                {
                    Debug.Log("нет модели");
                }
            }
            else
            {
                if (xCoord >= 0 && xCoord < MapSizeEditor.countX)
                {
                    if (yCoord >= 0 && yCoord < MapSizeEditor.countY)
                    {
                        if (zCoord >= 0 && zCoord < MapSizeEditor.countZ)
                        {
                            if (TypeOfObjectOnMap[xCoord, yCoord, zCoord] == null)
                            {
                                newMiniCube.transform.localScale = new Vector3(step , step , step );
                                newMiniCube.transform.parent = transform;
                                newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                                TypeOfObjectOnMap[xCoord, yCoord, zCoord] = newMiniCube;

                                if(model3D == mine)
                                {
                                    for (int i = yCoord; i>= 1; i--)
                                    {
                                        if (TypeOfObjectOnMap[xCoord, i - 1, zCoord] != null)
                                        {
                                            if (TypeOfObjectOnMap[xCoord, i - 1, zCoord].tag == "mine")
                                            {
                                                Debug.LogWarning("Нажал2");
                                                Destroy(TypeOfObjectOnMap[xCoord, i - 1, zCoord]);
                                                TypeOfObjectOnMap[xCoord, i - 1, zCoord] = null;
                                            }
                                        }
                                        if (TypeOfObjectOnMap[xCoord, i - 1, zCoord] != null)
                                        {
                                            if (TypeOfObjectOnMap[xCoord, i - 1, zCoord].tag != "chain")
                                            {
                                                if (TypeOfObjectOnMap[xCoord, i, zCoord].tag != "mine")
                                                {
                                                    // 
                                                    newMiniCube = new GameObject();
                                                    newMiniCube.transform.localScale = new Vector3(step, step, step);
                                                    newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;
                                                    newMiniCube.transform.position = new Vector3(xCoord * step, i * step, zCoord * step);
                                                    newMiniCube.transform.parent = transform;
                                                    newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                                                    Destroy(TypeOfObjectOnMap[xCoord, i, zCoord]);
                                                    Instantiate(fixOnTheGround, newMiniCube.transform);
                                                    Instantiate(chainLink, newMiniCube.transform);
                                                    newMiniCube.tag = "chain";
                                                    TypeOfObjectOnMap[xCoord, i, zCoord] = newMiniCube;
                                                    newMiniCube = null;
                                                    break;
                                                }
                                                else
                                                {
                                                    Instantiate(fixOnTheGround, newMiniCube.transform);
                                                    TypeOfObjectOnMap[xCoord, i, zCoord] = newMiniCube;
                                                    newMiniCube = null;
                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                break;
                                            }
                                            
                                        }
                                        yCoord = i - 1;
                                        xCoord = tempxCoord;
                                        zCoord = tempzCoord;
                                        newMiniCube = new GameObject();
                                        newMiniCube.transform.localScale = new Vector3(step, step, step);
                                        newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;
                                        newMiniCube.transform.position = new Vector3(xCoord * step, yCoord * step, zCoord * step);
                                        newMiniCube.transform.parent = transform;
                                        newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                                        Instantiate(chainLink, newMiniCube.transform);
                                        newMiniCube.tag = "chain";
                                        TypeOfObjectOnMap[xCoord, yCoord, zCoord] = newMiniCube;

                                    }

                                }

                                newMiniCube = null;
                                mouseScrollValue = 0;
                            }
                            else
                            {
                                Debug.LogWarning("Место занято другим объектом!");
                            }
                        }
                    }
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
            if (Physics.Raycast(ray, out hit, 1000 * step))
            {
                differenceX = Mathf.Abs(hit.point.x - hit.collider.gameObject.transform.position.x) < step / 2 - 0.0001f ? 0 : hit.point.x - hit.collider.gameObject.transform.position.x;
                differenceY = Mathf.Abs(hit.point.y - hit.collider.gameObject.transform.position.y) < step / 2 - 0.0001f ? 0 : hit.point.y - hit.collider.gameObject.transform.position.y;
                differenceZ = Mathf.Abs(hit.point.z - hit.collider.gameObject.transform.position.z) < step / 2 - 0.0001f ? 0 : hit.point.z - hit.collider.gameObject.transform.position.z;

                positionX = hit.collider.gameObject.transform.position.x + differenceX * 2;
                positionY = hit.collider.gameObject.transform.position.y + differenceY * 2 + mouseScrollValue * step;
                positionZ = hit.collider.gameObject.transform.position.z + differenceZ * 2;




                if (positionY >= 0 && positionY / step + 0.1 < MapSizeEditor.countY)
                {
                    yCoord = (int)(positionY / step + 0.1);
                    if (positionX >= 0 && positionX / step + 0.1 < MapSizeEditor.countX)
                    {
                        tempxCoord = (int)(positionX / step + 0.1);
                    }
                    if (positionZ >= 0 && positionZ/ step + 0.1 < MapSizeEditor.countZ)
                    {
                        tempzCoord = (int)(positionZ / step + 0.1);
                    }
                    //if (model3D == mine) { }
                }
                else
                {
                    mouseScrollValue = 0;
                }
                Boolfas = false;
                for (int i = yCoord; i < MapSizeEditor.countY; i++)
                {
                    if (TypeOfObjectOnMap[tempxCoord, i, tempzCoord] == null)
                    {
                        xCoord = tempxCoord;
                        yCoord = i;
                        zCoord = tempzCoord;
                        Boolfas = true;
                        break;
                    }
                }
                if(!Boolfas)
                { 
                    for (int i = MapSizeEditor.countY-1; i >= 0; i--)
                    {
                        if (TypeOfObjectOnMap[tempxCoord, i, tempzCoord] == null)
                        {
                            xCoord = tempxCoord;
                            yCoord = i;
                            zCoord = tempzCoord;
                            break;
                        }
                    }
                }
                if (TypeOfObjectOnMap[xCoord, yCoord, zCoord] == null) newMiniCube.transform.position = new Vector3(xCoord * step, yCoord * step, zCoord * step);
                Debug.Log("Cord{" + positionX + ":" + positionY + ":" + positionZ + "}");
                Debug.Log("RealCord{" + xCoord + ":" + yCoord + ":" + zCoord + "}");
            }
        }
    }


    /// <summary>
    /// Метод, который удаляет <see cref="UnityEngine.GameObject"/> со сцены 
    /// </summary>
    /// <remarks><see cref="UnityEngine.GameObject"/> выбирается с помощью <see cref="UnityEngine.Physics.Raycast(Ray, out RaycastHit, float)"/>
    /// </remarks>
    void DeleteControl()
    {
        if (Input.GetMouseButtonDown(0) && Input.GetAxis("Shift")>0)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100 * step)){
                Destroy(hit.collider.gameObject);
                if(hit.collider.gameObject.tag == "mine")
                {
                    for(int i = (int)(hit.collider.gameObject.transform.position.y / step + 0.1); i >=0; i--)
                    {
                        Debug.LogWarning((int)hit.collider.gameObject.transform.position.x / step);
                        if(TypeOfObjectOnMap[(int)(hit.collider.gameObject.transform.position.x / step + 0.1),i, (int)(hit.collider.gameObject.transform.position.z / step + 0.1)].tag == "chain")
                        {
                            Destroy(TypeOfObjectOnMap[(int)(hit.collider.gameObject.transform.position.x / step + 0.1), i, (int)(hit.collider.gameObject.transform.position.z / step + 0.1)]);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetAxis("Shift") <= 0)
        {
            SpawnerControl(model3D);
        }
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            DeleteControl();
        }

    }
}
