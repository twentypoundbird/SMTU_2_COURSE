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

    public static IEnumerator MoveObject(GameObject @object, string direction, int timeOfAnimation, int smoothCoeficient)
    {
        Vector3 target = new Vector3();
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
                @object.transform.Rotate(0, -90, 0);
                target = @object.transform.position + @object.transform.forward * MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.forward * MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                break;
            case "Turn right":
                @object.transform.Rotate(0, 90, 0);
                target = @object.transform.position + @object.transform.forward * MapSizeEditor.step;
                while (@object.transform.position != target)
                {
                    @object.transform.position += @object.transform.forward * MapSizeEditor.step / smoothCoeficient;
                    yield return new WaitForSeconds(timeOfAnimation / smoothCoeficient);
                }
                break;
        }
        
        yield return 0;

    }



    // Start is called before the first frame update
    //void Start()
    //{
    //    StartCoroutine(StartMove());
    //    StartCoroutine(DELETE_StartInsert());
    //}
    //private void Update()
    //{
    //    if (PlayerIsMoving)
    //    {
    //        float mainXYZ = 0;
    //        float targetXYZ = 0;
    //        float startXYZ = 0;

    //        if (movetype == (byte)Movementtype.onX) { mainXYZ = this.transform.position.x; targetXYZ = targetPoint.x; startXYZ = startPoint.x; }
    //        else if (movetype == (byte)Movementtype.onY) { mainXYZ = this.transform.position.y; targetXYZ = targetPoint.y; startXYZ = startPoint.y; }
    //        else if (movetype == (byte)Movementtype.onZ) { mainXYZ = this.transform.position.z; targetXYZ = targetPoint.z; startXYZ = startPoint.z; }

    //        float x = Mathf.Abs(startXYZ - mainXYZ);
    //        if(x < 5)
    //        {
    //            x = (-x + 10) * (0.01f * x);
    //            speed = x;
    //        }
    //        else
    //        {
    //            x = Mathf.Abs(targetXYZ - mainXYZ);
    //            if(x<5)
    //            {
    //                x = (-x + 10) * (0.01f * x);
    //                speed = x;
    //            }
    //        }
    //        if (speed < 0.004f)
    //        {
    //            speed = 0.004f;
    //        }

    //        if (targetXYZ == mainXYZ) PlayerIsMoving = false;

    //        this.transform.position = Vector3.MoveTowards(transform.position, targetPoint, (speed + 0.01f));
    //        Debug.Log("Speed = " + speed + ";");
    //    }
    //}
    //IEnumerator StartMove()
    //{
    //    startPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
    //    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
    //    while(true)
    //    {
    //        MoveStart = false;
    //        yield return new WaitUntil(() => MoveStart);
    //        MoveStart = false;
    //        int j; // сколько элементов пропущено за 1 цикл
    //        for (int i = 0; i < MoveList.Length; i += j)
    //        {
    //            yield return new WaitUntil(() => !PlayerIsMoving);
    //            yield return new WaitForSeconds(0.42f);
    //            startPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));

    //            for (j = 1; j+i < MoveList.Length; j++) if(MoveList[i + j] != MoveList[i]) break;
    //            speed = 0.1f;

    //            switch (MoveList[i])
    //            {
    //                case 0: // FORWARD
    //                    movetype = (byte)Movementtype.onX;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1 + 10 * j), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
    //                    break;
    //                case 1: // BACK
    //                    movetype = (byte)Movementtype.onX;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1 - 10 * j), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
    //                    break;
    //                case 2: // LEFT
    //                    movetype = (byte)Movementtype.onZ;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1 - 10 * j));
    //                    break;
    //                case 3: // RIGHT
    //                    movetype = (byte)Movementtype.onZ;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1 + 10 * j));
    //                    break;
    //                case 4: // UP
    //                    movetype = (byte)Movementtype.onY;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1 + 10 * j), (int)(this.transform.position.z + 0.1));
    //                    break;
    //                case 5: // DOWN
    //                    movetype = (byte)Movementtype.onY;
    //                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1 - 10 * j), (int)(this.transform.position.z + 0.1));
    //                    break;
    //                default:
    //                    j = 1;
    //                    break;
    //            }
    //            PlayerIsMoving = true;
    //        }
    //    }
    //}
    //IEnumerator DELETE_StartInsert()
    //{
    //    while(true)
    //    {
    //        DELETE_InsertLogicMove = false;
    //        yield return new WaitUntil(() => DELETE_InsertLogicMove);
    //        InsertLogicMove(DELETE_text);
    //    }
    //}

}
