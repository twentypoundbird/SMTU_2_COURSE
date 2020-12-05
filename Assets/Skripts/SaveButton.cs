using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveButton : MonoBehaviour
{
    public void IsTapped()
    {
        string nameOfSaveFile = "/test.txt",
            nameDirectiry = "/testDir";
        if (!Directory.Exists(Directory.GetCurrentDirectory() + nameDirectiry))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + nameDirectiry);
        }
        if (!File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile))
        {
            File.Create(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile);
        }
        if(File.Exists(Directory.GetCurrentDirectory() + nameDirectiry + nameOfSaveFile))
        {
            
        }
    }
}
