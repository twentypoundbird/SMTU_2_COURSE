using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MoveLogic : MonoBehaviour
{
    public static GameObject deathScreen;
    public static GameObject winScreen;
    public static AudioSource winSound;
    public static AudioSource loseSound;

    //[SerializeField]
    public bool MoveStart = false;
    public static int speed = 100;
    private enum Movementtype : byte
    {
        onX,
        onY,
        onZ
    }

    public string DELETE_text;
    public bool DELETE_InsertLogicMove = false;



    private void Start()
    {
        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);
        winScreen = GameObject.Find("WinScreen");
        winScreen.SetActive(false);
        winSound = GameObject.Find("WinSound").GetComponent<AudioSource>();
        loseSound = GameObject.Find("LoseSound").GetComponent<AudioSource>();
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="object"></param>
    /// <returns>0 - ничего / 1 - проигрыш / 2 - выигрыш</returns>
    public static int BoatLossORWinСheck(GameObject @object)
    {
        int cX, cY, cZ;
        int x = cX = (int)(@object.transform.position.x / MapSizeEditor.step);
        int y = cY = (int)(@object.transform.position.y / MapSizeEditor.step);
        int z = cZ = (int)(@object.transform.position.z / MapSizeEditor.step);

        if (GameWorldLogic.TypeOfObjectOnMapInt[x, y, z] == 3) return 2;
        if (GameWorldLogic.TypeOfObjectOnMapInt[x, y, z] > 1) return 1;

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
                        return 1;
                    }
                }
            }
        }
        return 0;
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
        if (BoatLossORWinСheck(@object) == 1)
        {
            deathScreen.SetActive(true);
            loseSound.Play();
            yield return DeathScreen();
        }
        if (BoatLossORWinСheck(@object) == 2)
        {
            winScreen.SetActive(true);
            winSound.Play(); 
            yield return DeathScreen();
        }
        yield return 0;
    }

    public static IEnumerator DeathScreen()
    {
        bool tap = false;
        while (!tap)
        {
            if (Input.GetMouseButton(0))
            {
                tap = true;
                Debug.LogWarning(tap);
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitUntil(() => tap);
        SceneManager.LoadScene("GameScene");
    }
}
