using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveLogic : MonoBehaviour
{
    //[SerializeField]
    private byte[] MoveList;
    public bool MoveStart = false;
    private bool PlayerIsMoving = false;
    private float speed = 0;
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartMove());
        StartCoroutine(DELETE_StartInsert());
    }
    private void Update()
    {
        if (PlayerIsMoving)
        {
            float mainXYZ = 0;
            float targetXYZ = 0;
            float startXYZ = 0;
            
            if (movetype == (byte)Movementtype.onX) { mainXYZ = this.transform.position.x; targetXYZ = targetPoint.x; startXYZ = startPoint.x; }
            else if (movetype == (byte)Movementtype.onY) { mainXYZ = this.transform.position.y; targetXYZ = targetPoint.y; startXYZ = startPoint.y; }
            else if (movetype == (byte)Movementtype.onZ) { mainXYZ = this.transform.position.z; targetXYZ = targetPoint.z; startXYZ = startPoint.z; }

            float x = Mathf.Abs(startXYZ - mainXYZ);
            if(x < 5)
            {
                x = (-x + 10) * (0.01f * x);
                speed = x;
            }
            else
            {
                x = Mathf.Abs(targetXYZ - mainXYZ);
                if(x<5)
                {
                    x = (-x + 10) * (0.01f * x);
                    speed = x;
                }
            }
            if (speed < 0.004f)
            {
                speed = 0.004f;
            }
            
            if (targetXYZ == mainXYZ) PlayerIsMoving = false;

            this.transform.position = Vector3.MoveTowards(transform.position, targetPoint, (speed + 0.01f));
            Debug.Log("Speed = " + speed + ";");
        }
    }
    IEnumerator StartMove()
    {
        startPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
        targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
        yield return new WaitUntil(() => MoveStart);
        MoveStart = false;
        int j; // сколько элементов пропущено за 1 цикл
        for (int i = 0; i < MoveList.Length; i += j)
        {
            yield return new WaitUntil(() => !PlayerIsMoving);
            yield return new WaitForSeconds(0.5f);
            startPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));

            for (j = 1; j+i < MoveList.Length; j++) if(MoveList[i + j] != MoveList[i]) break;
            speed = 0.1f;

            switch (MoveList[i])
            {
                case 0: // FORWARD
                    movetype = (byte)Movementtype.onX;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1 + 10 * j), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
                    break;
                case 1: // BACK
                    movetype = (byte)Movementtype.onX;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1 - 10 * j), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1));
                    break;
                case 2: // LEFT
                    movetype = (byte)Movementtype.onZ;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1 - 10 * j));
                    break;
                case 3: // RIGHT
                    movetype = (byte)Movementtype.onZ;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1), (int)(this.transform.position.z + 0.1 + 10 * j));
                    break;
                case 4: // UP
                    movetype = (byte)Movementtype.onY;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1 + 10 * j), (int)(this.transform.position.z + 0.1));
                    break;
                case 5: // DOWN
                    movetype = (byte)Movementtype.onY;
                    targetPoint = new Vector3((int)(this.transform.position.x + 0.1), (int)(this.transform.position.y + 0.1 - 10 * j), (int)(this.transform.position.z + 0.1));
                    break;
                default:
                    j = 1;
                    break;
            }
            PlayerIsMoving = true;
            //Debug.Log("MoveList[" + i + "] = " + MoveList[i]);
        }
    }
    IEnumerator DELETE_StartInsert()
    {
        yield return new WaitUntil(() => DELETE_InsertLogicMove);
        InsertLogicMove(DELETE_text);
    }

}
