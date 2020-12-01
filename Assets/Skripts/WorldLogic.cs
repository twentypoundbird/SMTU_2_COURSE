using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WorldLogic : MonoBehaviour
{
    public GameObject submarine, mine;


    private GameObject model3D;

    private static int step = MapSizeEditor.step;

    public Camera camera;

    public Material generalMaterial, invisibleMaterial,
        sandMaterial;
    public Mesh generalMesh;
    
    public static Material materialForMiniCube;

    private GameObject miniCube, newMiniCube;
    private BoxCollider boxOfMinicube;


    float mouseScrollValue;
    float posX, posY, posZ;

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
                model3D = null;
                break;
            case 4:
                model3D = null;
                break;
            case 5:
                model3D = null;
                break;
            case 6:
                model3D = null;
                break;
            case 7:
                model3D = null;
                break;
            default:
                model3D = null;
                break;

        }
    }

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
                    newMiniCube.transform.localScale = new Vector3(step, step, step);
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
                newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
                newMiniCube = null;
                mouseScrollValue = 0;
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
                float positionX, positionY, positionZ, differenceX, differenceY, differenceZ;
                differenceX = Mathf.Abs(hit.point.x - hit.collider.gameObject.transform.position.x) < step / 2 - 0.0001f ? 0 : hit.point.x - hit.collider.gameObject.transform.position.x;
                differenceY = Mathf.Abs(hit.point.y - hit.collider.gameObject.transform.position.y) < step / 2 - 0.0001f ? 0 : hit.point.y - hit.collider.gameObject.transform.position.y;
                differenceZ = Mathf.Abs(hit.point.z - hit.collider.gameObject.transform.position.z) < step / 2 - 0.0001f ? 0 : hit.point.z - hit.collider.gameObject.transform.position.z;

                positionX = hit.collider.gameObject.transform.position.x + differenceX * 2;
                positionY = hit.collider.gameObject.transform.position.y + differenceY * 2 + mouseScrollValue * step;
                positionZ = hit.collider.gameObject.transform.position.z + differenceZ * 2;


                newMiniCube.transform.position = new Vector3(positionX, positionY, positionZ);
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
