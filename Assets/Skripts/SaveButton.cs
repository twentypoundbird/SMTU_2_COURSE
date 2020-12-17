using UnityEngine;
using System.IO;

public class SaveButton : MonoBehaviour
{
    string nameOfSaveFile = "/save1.txt", // имя файла
        nameDirectiry = "/saves"; // имя папки для этого файла
    private void Start()
    {
        if (!Directory.Exists(Directory.GetCurrentDirectory() + nameDirectiry)) // проверяем существует ли папка
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + nameDirectiry); // создаём папку
        }
        if (!File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile)) // проверяем существует ли файл
        {
            File.Create(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile); // создаём файл
        }
    }
    public void IsTapped()
    {
        if (File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile)) // проверяем существует ли файл
        {
            string sizeOfMas = MapSizeEditor.countX + "\n" + MapSizeEditor.countY + "\n" + MapSizeEditor.countZ + "\n"; // сохраняем размеры карты построчно
            string mass = "";
            for (int x = 0; x < MapSizeEditor.countX; x++)
            {
                for (int y = 0; y < MapSizeEditor.countY; y++)
                {
                    for (int z = 0; z < MapSizeEditor.countZ; z++)
                    {
                        mass += WorldLogic.TypeOfObjectOnMapInt[x, y, z].ToString(); // сохраняем типы всех объектов подряд
                        if (WorldLogic.TypeOfObjectOnMapInt[x, y, z] == 1) mass += WorldLogic.rotation.ToString();
                    }

                }

            }
            File.WriteAllText(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile, sizeOfMas + mass); // записываем string в файл
        }
    }
}