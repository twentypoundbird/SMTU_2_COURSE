using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Reflection;
using System;

public class GameWorldLogic : MonoBehaviour
{
    public GameObject submarine, mine, chainLink, fixOnTheGround, lighthouse;

    private static readonly int step = MapSizeEditor.step;

    public Camera camera;

    public Material generalMaterial, invisibleMaterial,
        sandMaterial;
    public Mesh generalMesh;

    public static Material materialForMiniCube;

    private GameObject newMiniCube;

    public byte[,,] TypeOfObjectOnMapInt;


    private void Start()
    {
        string nameOfSaveFile = "/test.txt", nameDirectiry = "/testDir";
        string[] readedLines = File.ReadAllLines(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile); // чтение файла
        string sepLine = readedLines[0];
        if (byte.TryParse(sepLine, out byte outValue)) MapSizeEditor.countX = outValue;
        sepLine = readedLines[1];
        if (byte.TryParse(sepLine, out outValue)) MapSizeEditor.countY = outValue;
        sepLine = readedLines[2];
        if (byte.TryParse(sepLine, out outValue)) MapSizeEditor.countZ = outValue;
        sepLine = readedLines[3];
        int count = 0;
        TypeOfObjectOnMapInt = new byte[MapSizeEditor.countX, MapSizeEditor.countY, MapSizeEditor.countZ];
        for (byte x = 0; x < MapSizeEditor.countX; x++)
        {
            for (byte y = 0; y < MapSizeEditor.countY; y++)
            {
                for (byte z = 0; z < MapSizeEditor.countZ; z++)
                {
                    if (byte.TryParse(sepLine[count++].ToString(), out outValue))
                    {
                        Debug.Log("M[" + x + ":" + y + ":" + z + "] = " + outValue);
                        TypeOfObjectOnMapInt[x, y, z] = outValue;
                        CreateObjectType(x, y, z, outValue);
                    }
                }
            }
        }
    }

    void CreateObjectType(byte x, byte y, byte z, byte type)
    {
        if(type != 0)
        { 
            newMiniCube = new GameObject();
            newMiniCube.transform.localScale = new Vector3(step, step, step);
            newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;
            newMiniCube.transform.position = new Vector3(x * step, y * step, z * step);
            newMiniCube.transform.parent = transform;
            newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            switch(type)
            {
                case 1:
                    Instantiate(submarine, newMiniCube.transform);
                    newMiniCube.AddComponent<MoveLogic>();
                    break;
                case 2:
                    Instantiate(mine, newMiniCube.transform);
                    if (TypeOfObjectOnMapInt[x, y - 1, z] != 8)
                    {
                        Instantiate(fixOnTheGround, newMiniCube.transform);
                    }
                    break;
                case 3:
                    Instantiate(lighthouse, newMiniCube.transform);
                    break;
                case 8:
                    Instantiate(chainLink, newMiniCube.transform);
                    if(y!=0)
                    {
                        if (TypeOfObjectOnMapInt[x, y - 1, z] != 8)
                        {
                            Instantiate(fixOnTheGround, newMiniCube.transform);
                        }
                    }
                    break;
                case 9:
                    newMiniCube.AddComponent<MeshRenderer>().material = generalMaterial;
                    break;
                default:
                    break;
            }
        }
    }
}
