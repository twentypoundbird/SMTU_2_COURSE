using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CommandReader : MonoBehaviour
{
    public Transform content;


    public static GameObject submarine;

    private bool isCondition;

    Stack<bool> isConditionsDone = new Stack<bool>();
    void StackRerfreasher<T>(Stack<T> stack, T value)
    {
        stack.Pop();
        stack.Push(value);
    }

    bool Check(string condition, string direction)
    {
        if (condition == "Clear" || condition == "Not clear")
        {
            Vector3 cash = submarine.transform.position;
            switch (direction)
            {
                case "Forward":
                    cash = cash + submarine.transform.forward * MapSizeEditor.step;
                    break;
                case "Back":
                    cash = cash + submarine.transform.forward * -MapSizeEditor.step;
                    break;
                case "Up":
                    cash = cash + submarine.transform.up * MapSizeEditor.step;
                    break;
                case "Down":
                    cash = cash + submarine.transform.up * -MapSizeEditor.step;
                    break;
                case "Turn right":
                    cash = cash + submarine.transform.right * MapSizeEditor.step;
                    break;
                case "Turn left":
                    cash = cash + submarine.transform.right * -MapSizeEditor.step;
                    break;
            }
            //Debug.LogWarning((int)(cash.x / MapSizeEditor.step + 0.1) + " " + (int)(cash.y / MapSizeEditor.step + 0.1) + " " + (int)(cash.z / MapSizeEditor.step + 0.1));
            int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / MapSizeEditor.step + 0.1), (int)(cash.y / MapSizeEditor.step + 0.1), (int)(cash.z / MapSizeEditor.step + 0.1)];
            if ((n != 0 && condition == "Clear") || (n == 0 && condition == "Not clear"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false; 
        }
    }


    public void StartSwimming()
    {
        StartCoroutine(Swimming());
    }

    IEnumerator Swimming()
    {
        isConditionsDone.Push(true);
        int step = 0;
        while (step < content.transform.childCount)
        {
            if (content.transform.GetChild(step).tag == "MoveLabel" && isConditionsDone.Peek() && !isCondition)
            {
                yield return MoveLogic.MoveObject(submarine, content.transform.GetChild(step).GetComponentInChildren<Text>().text, 1, 20);
            }
            if (content.transform.GetChild(step).GetComponentInChildren<Text>().text == "If")
            {
                isConditionsDone.Push(true);
                isCondition = true;
            }
            if (isCondition && content.transform.GetChild(step).GetComponentInChildren<Text>().text != "If")
            {
                if (content.transform.GetChild(step).GetComponentInChildren<Text>().text == "&&" || content.transform.GetChild(step).GetComponentInChildren<Text>().text == "||")
                {
                    if (content.transform.GetChild(step).GetComponentInChildren<Text>().text == "&&" && isConditionsDone.Peek())
                    {
                        StackRerfreasher<bool>(isConditionsDone, true);
                    }
                    if (content.transform.GetChild(step).GetComponentInChildren<Text>().text == "||" && !isConditionsDone.Peek())
                    {
                        StackRerfreasher<bool>(isConditionsDone, true);
                    }
                }
                else if (content.transform.GetChild(step).GetComponentInChildren<Text>().text == "To")
                {
                    isCondition = false;
                }
                else if (content.transform.GetChild(step + 1).GetComponentInChildren<Text>().text == "Clear" || content.transform.GetChild(step + 1).GetComponentInChildren<Text>().text == "Not clear")
                {
                    if (isConditionsDone.Peek())
                    {
                        StackRerfreasher<bool>(isConditionsDone, Check(content.transform.GetChild(step + 1).GetComponentInChildren<Text>().text, content.transform.GetChild(step).GetComponentInChildren<Text>().text));
                        Debug.LogWarning(isConditionsDone.Peek());
                    }
                }
            }
            if (isCondition && content.transform.GetChild(step).GetComponentInChildren<Text>().text == "End")
            {
                isConditionsDone.Pop();
            }


            step++;
        }
        yield return 0;

    }
}
