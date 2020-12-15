using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveLogic : MonoBehaviour
{
    //[SerializeField]
    private byte[] MoveList;
    public bool MoveStart = false;
    private bool PlayerIsMoving = false;
    public static int speed = 100;
    private byte movetype = 0;
    private enum Movementtype : byte
    {
        onX,
        onY,
        onZ
    }

    private Vector3 targetPoint;
    private Vector3 startPoint;

    public string DELETE_text;
    public bool DELETE_InsertLogicMove = false;

    void InsertLogicMove(string text)
    {
        if (!MoveStart)
        { 
            MoveList = new byte[text.Length];
            byte outValue;
            for (int i=0;i<text.Length;i++)
            {
                if (byte.TryParse(text[i].ToString(), out outValue))
                {
                    MoveList[i] = outValue;
                }
            }
        }
    }


    public static void MoveObject(GameObject @object, string directoin)
    {
         
        switch (directoin)
        {
            case "Forward":
                @object.transform.position += @object.transform.forward * MapSizeEditor.step / speed; 
                break;
            case "Back":
                @object.transform.position += @object.transform.forward * -MapSizeEditor.step / speed;
                break;
            case "Up":
                @object.transform.position += @object.transform.up * MapSizeEditor.step / speed;
                break;
            case "Down":
                @object.transform.position += @object.transform.up * -MapSizeEditor.step / speed;
                break;
            case "Turn left":
                @object.transform.Rotate(0, -90, 0);
                @object.transform.position += @object.transform.forward * MapSizeEditor.step / speed;
                break;
            case "Turn right":
                @object.transform.Rotate(0, 90, 0);
                @object.transform.position += @object.transform.forward * MapSizeEditor.step / speed;
                break;
        }

    }



    private static bool BoatLossСheck(GameObject @object)
    {
        int cX, cY, cZ;
        int x = cX = (int)(@object.transform.position.x / MapSizeEditor.step);
        int y = cY = (int)(@object.transform.position.y / MapSizeEditor.step);
        int z = cZ = (int)(@object.transform.position.z / MapSizeEditor.step);

        if (GameWorldLogic.TypeOfObjectOnMapInt[x, y, z] > 1) return true;

        if (x + 1 < MapSizeEditor.countX) cX++;
        if (y + 1 < MapSizeEditor.countX) cY++;
        if (z + 1 < MapSizeEditor.countX) cZ++;
        if (--x < 0) x = 0;
        if (--y < 0) y = 0;
        if (--z < 0) z = 0;

        for (int xx=x; xx <= cX; xx++)
        {
            for (int yy=y; yy <= cY; yy++)
            {
                for (int zz=z; zz <= cZ; zz++)
                {
                    if (GameWorldLogic.TypeOfObjectOnMapInt[xx, yy, zz] == 2)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static IEnumerator MoveObject(GameObject @object, string direction, int timeOfAnimation, int smoothCoeficient)
    {
        Vector3 target = new Vector3();
        int rotationAngle;
        switch (direction)
        {
            case "Forward":
                target = @object.transform.position + @object.transform.forward * MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.forward * MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation/smoothCoeficient);
                }
                break;
            case "Back":
                target = @object.transform.position + @object.transform.forward * -MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.forward * -MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                break;
            case "Up":
                target = @object.transform.position + @object.transform.up * MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.up * MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                break;
            case "Down":
                target = @object.transform.position + @object.transform.up * -MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.up * -MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                break;
            case "Turn left":
                rotationAngle = -90 + (int)@object.transform.rotation.eulerAngles.y;
                Debug.LogWarning("rotationAngle = " + rotationAngle);
                while (((int)@object.transform.eulerAngles.y - rotationAngle)%360 != 0)
                {
                    @object.transform.Rotate(0, -90f / smoothCoeficient, 0);
                    Debug.LogWarning("rotation.y = " + @object.transform.eulerAngles.y);
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                @object.transform.rotation = Quaternion.Euler(@object.transform.eulerAngles.x, rotationAngle, @object.transform.eulerAngles.z);
                
                break;
            case "Turn right":
                rotationAngle = 90 + (int)@object.transform.rotation.eulerAngles.y;
                Debug.LogWarning("rotationAngle = " + rotationAngle);
                while (((int)@object.transform.eulerAngles.y - rotationAngle) % 360 != 0)
                {
                    @object.transform.Rotate(0, 90f / smoothCoeficient, 0);
                    Debug.LogWarning("rotation.y = " + @object.transform.eulerAngles.y);
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                @object.transform.rotation = Quaternion.Euler(@object.transform.eulerAngles.x, rotationAngle, @object.transform.eulerAngles.z);
                
                break;
        }
        if (BoatLossСheck(@object)) Debug.LogError("Лодка проигралась");
        yield return 0;
    }
}
