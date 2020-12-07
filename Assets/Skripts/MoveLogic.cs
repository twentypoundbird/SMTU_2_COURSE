using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoveLogic : MonoBehaviour
{
    public  byte[] MoveList;
    public  bool MoveStart = false;

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
    
    IEnumerator StartMove()
    {
        yield return new WaitUntil(() => MoveStart);
        for (int i = 0; i < MoveList.Length; i++)
        {
            switch (MoveList[i])
            {
            }
            Debug.Log("MoveList[" + i + "] = " + MoveList[i]);
        }
    }
    IEnumerator DELETE_StartInsert()
    {
        yield return new WaitUntil(() => DELETE_InsertLogicMove);
        InsertLogicMove(DELETE_text);
    }

}
