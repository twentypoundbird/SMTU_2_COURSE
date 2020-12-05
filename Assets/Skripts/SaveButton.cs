using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveButton : MonoBehaviour
{
    string nameOfSaveFile = "/test.txt",
        nameDirectiry = "/testDir";
    private void Start()
    {
        if (!Directory.Exists(Directory.GetCurrentDirectory() + nameDirectiry))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + nameDirectiry);
        }
        if (!File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile))
        {
            File.Create(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile);
        }
    }
    public void IsTapped()
    {
        if(File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile))
        {
            string sizeOfMas = MapSizeEditor.countX + "\n" + MapSizeEditor.countY + "\n" + MapSizeEditor.countZ +"\n";
            string mass = "";
            for (int x = 0; x<MapSizeEditor.countX; x++)
            {
                for (int y = 0; y < MapSizeEditor.countY; y++)
                {
                    for (int z = 0; z < MapSizeEditor.countZ; z++)
                    {
                        mass += WorldLogic.TypeOfObjectOnMapInt[x, y, z].ToString();
                    }

                }

            }
            File.WriteAllText(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile, sizeOfMas + mass);
           // File.AppendAllText(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile, "123");
        }
    }
}
