using System.IO;
using UnityEngine;

public class GameWorldLogic : MonoBehaviour
{
    public GameObject submarine, mine, chainLink, fixOnTheGround, lighthouse; // 3d модели

    private static int step = MapSizeEditor.step; // step - размер одной клетки. Пр. 1 кл = 10 у.е

    public Camera camera; // камера на сцене, через которую смотрит игрок. Нужна для использования Raycast

    // Материалы для 3d моделей
    public Material generalMaterial, invisibleMaterial,sandMaterial;
    public Mesh generalMesh;
    public static Material materialForMiniCube;

    private GameObject newMiniCube; // временный объект для размещения его на карте
    private GameObject landscape; // главный объект для иерархии ландшафта

    public static byte[,,] TypeOfObjectOnMapInt; // массив с ID объектов

    private void Start()
    {
        // загружаем игровое пространство
        landscape = new GameObject(); // создаём родительский объект, куда будем записывать все объекты, типа 9(поверхность куб)
        landscape.name = "Landscape";
        landscape.transform.parent = transform;
        string nameOfSaveFile = "/save1.txt", nameDirectiry = "/saves";
        string[] readedLines = File.ReadAllLines(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile); // чтение файла
        string sepLine = readedLines[0]; // читаем первую линию из файла
        if (byte.TryParse(sepLine, out byte outValue)) MapSizeEditor.countX = outValue; // она отвечает за размер карты по X
        sepLine = readedLines[1]; // читаем вторую линию из файла
        if (byte.TryParse(sepLine, out outValue)) MapSizeEditor.countY = outValue; // она отвечает за размер карты по Y
        sepLine = readedLines[2]; // читаем третью линию из файла
        if (byte.TryParse(sepLine, out outValue)) MapSizeEditor.countZ = outValue; // она отвечает за размер карты по Z
        sepLine = readedLines[3]; // читаем четвёртую линию из файла, которая хранит типы объектов расположенных подряд
        int count = 0;
        TypeOfObjectOnMapInt = new byte[MapSizeEditor.countX, MapSizeEditor.countY, MapSizeEditor.countZ]; // инициализируем массив с размерами, указанными в файле
        for (byte x = 0; x < MapSizeEditor.countX; x++)
        {
            for (byte y = 0; y < MapSizeEditor.countY; y++)
            {
                for (byte z = 0; z < MapSizeEditor.countZ; z++)
                {
                    if (byte.TryParse(sepLine[count++].ToString(), out outValue)) // конвертируем символ в число
                    {
                        TypeOfObjectOnMapInt[x, y, z] = outValue;
                        if (outValue != 1) CreateObjectType(x, y, z, outValue);
                        else
                        {
                            byte.TryParse(sepLine[count++].ToString(), out byte outValue2);
                            CreateObjectType(x, y, z, outValue,outValue2);
                        }
                    }
                }
            }
        }
    }
    void CreateObjectType(byte x, byte y, byte z, byte type, byte rotation = 0)
    {
        if(type != 0)
        { 
            newMiniCube = new GameObject();
            newMiniCube.transform.localScale = new Vector3(step, step, step);
            newMiniCube.AddComponent<MeshFilter>().mesh = generalMesh;
            newMiniCube.transform.position = new Vector3(x * step, y * step, z * step);
            newMiniCube.AddComponent<BoxCollider>().center = new Vector3(0, 0, 0);
            if(type != 9) newMiniCube.transform.parent = transform;
            switch (type)
            {
                case 1:
                    Instantiate(submarine, newMiniCube.transform);
                    newMiniCube.AddComponent<MoveLogic>();
                    newMiniCube.name = "MainPodLODKA";
                    CommandReader.submarine = newMiniCube;
                    if(rotation != 0)
                    {
                        newMiniCube.transform.Rotate(0, 90*(rotation), 0);
                    }
                    break;
                case 2:
                    Instantiate(mine, newMiniCube.transform);
                    if (TypeOfObjectOnMapInt[x, y - 1, z] != 8) // если под бомбой снизу нет цепи, то создаём объект крепёж
                    {
                        Instantiate(fixOnTheGround, newMiniCube.transform);
                    }
                    newMiniCube.name = "Bomb";
                    break;
                case 3:
                    Instantiate(lighthouse, newMiniCube.transform);
                    newMiniCube.name = "LightHouse";
                    break;
                case 8:
                    Instantiate(chainLink, newMiniCube.transform);
                    if(y!=0)
                    {
                        if (TypeOfObjectOnMapInt[x, y - 1, z] != 8) // если под цепью снизу нет цепи, то создаём объект крепёж
                        {
                            Instantiate(fixOnTheGround, newMiniCube.transform);
                        }
                    }
                    newMiniCube.name = "Chain";
                    break;
                case 9:
                    newMiniCube.AddComponent<MeshRenderer>().material = generalMaterial;
                    newMiniCube.name = "Box["+x+":"+y+":"+z+"]";
                    newMiniCube.transform.parent = landscape.transform;
                    break;
                default:
                    break;
            }
        }
    }
}