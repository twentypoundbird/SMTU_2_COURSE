using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//public class CommandReader : MonoBehaviour
//{
//    public static GameObject submarine;

//    public Transform content;

//    private int step = MapSizeEditor.step,
//        usedEndIndex = 0, usedIfIndex = 0;
//    private bool isClear = false;



//    private Stack<int> lastIfIndex = new Stack<int>();
//    private Stack<int> lastEndIndex = new Stack<int>();

//    private Queue<int> firstIfIndex = new Queue<int>();
//    private Queue<int> firstEndIndex = new Queue<int>();


//    delegate bool CashMethod(string name);
//    private Stack<CashMethod>[] cashMethodsStack; //массив стэков, содержащие в себе методы проверки соседних кубов (один стэк на каждую пару if-end)
//    bool CheckForward(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.forward * step;
//        Debug.LogWarning((int)(cash.x / step + 0.1) + " " + (int)(cash.y / step + 0.1) + " " + (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }
//    bool CheckBack(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.forward * -step;
//        Debug.LogWarning((int)(cash.x / step + 0.1)+" "+ (int)(cash.y / step + 0.1)+" "+ (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }
//    bool CheckUp(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.up * step;
//        Debug.LogWarning((int)(cash.x / step + 0.1) + " " + (int)(cash.y / step + 0.1) + " " + (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }
//    bool CheckDown(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.up * -step;
//        Debug.LogWarning((int)(cash.x / step + 0.1) + " " + (int)(cash.y / step + 0.1) + " " + (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }
//    bool CheckRight(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.right * step;
//        Debug.LogWarning((int)(cash.x / step + 0.1) + " " + (int)(cash.y / step + 0.1) + " " + (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }
//    bool CheckLeft(string name)
//    {
//        Vector3 cash = submarine.transform.position;
//        cash = cash + submarine.transform.right * -step;
//        Debug.LogWarning((int)(cash.x / step + 0.1) + " " + (int)(cash.y / step + 0.1) + " " + (int)(cash.z / step + 0.1));
//        int n = GameWorldLogic.TypeOfObjectOnMapInt[(int)(cash.x / step + 0.1), (int)(cash.y / step + 0.1), (int)(cash.z / step + 0.1)];
//        if ((n != 0 && name == "Clear") || (n == 0 && name == "Not clear"))
//        {
//            return false;
//        }
//        else return true;
//    }

//    struct conditionCash
//    {
//        public CashMethod method;
//        public string name;
//    }


//    private Queue<conditionCash>[] queOfConds; //массив очередей, содержащие в себе структуры conditionCash (одна очереди на каждую пару if-end)

//    private Queue<string>[] queueOfNamesOfLabels;
//    string nameOfLabel(Transform transform)
//    {
//        return transform.GetComponentInChildren<Text>().text;
//    }

//    void LabelChecking(string name, ref CashMethod cashMethod)
//    {
//        switch (name)
//        {
//            case "Forward":
//                cashMethod = CheckForward;
//                break;
//            case "Back":
//                cashMethod = CheckBack;
//                break;
//            case "Up":
//                cashMethod = CheckUp;
//                break;
//            case "Down":
//                cashMethod = CheckDown;
//                break;
//            case "Turn right":
//                cashMethod = CheckRight;
//                break;
//            case "Turn left":
//                cashMethod = CheckLeft;
//                break;
//            default:
//                break;
//        }
//    }


//    public void IsTappedPlay()
//    {
//        int[] cashIfIndex;
//        int couples;
//        CashMethod cashMethod = null;


//        for(int i = 0; i < content.childCount; i++)
//        {
//            if(content.GetChild(i).GetComponentInChildren<Text>().text == "If")
//            {
//                lastIfIndex.Push(i);
//            }
//        }
//        for(int i = content.childCount - 1; i >= 0; i--)
//        {
//            if (content.GetChild(i).GetComponentInChildren<Text>().text == "End")
//            {
//                lastEndIndex.Push(i);
//            }
//        }
//        couples = lastIfIndex.Count;
//        queOfConds = new Queue<conditionCash>[lastIfIndex.Count];

//        cashIfIndex = new int[lastIfIndex.Count];
//        for (int j = 0; j < lastIfIndex.Count; j++)
//        {
//            int n = 0;
//            while(lastEndIndex.Peek() > lastIfIndex.Peek()) { 
//                if (lastEndIndex.Peek() < lastIfIndex.Peek())
//                {
//                    cashIfIndex[n++] = lastIfIndex.Pop();
//                }
//            }
//            string labelname = "";
//            for (int i = lastIfIndex.Peek(); i <=lastEndIndex.Peek(); i++) // пробегаемся по парам if-end
//            {
//                if (usedIfIndex <= i && i <= usedEndIndex) //игнорируем другие пары if-end, которые могут быть внутри данной пары
//                {

//                }
//                else
//                {
//                    if (content.GetChild(i).GetComponentInChildren<Text>().text != "End" && content.GetChild(i).GetComponentInChildren<Text>().text != "To") // условия внутри if
//                    {
//                        string clearOrNotClear = content.GetChild(i).GetComponentInChildren<Text>().text;
//                        if (clearOrNotClear == "Clear" || clearOrNotClear == "Not clear")
//                        {
//                            LabelChecking(labelname, ref cashMethod);
//                            conditionCash condition = new conditionCash();
//                            condition.method = cashMethod;
//                            condition.name = clearOrNotClear;
//                            queOfConds[j].Enqueue(condition);
//                        }

//                        labelname = content.GetChild(i).GetComponentInChildren<Text>().text;
//                    }
//                    else if(content.GetChild(i).GetComponentInChildren<Text>().text == "To") //здесь все, что после to и перед end, исключая if
//                    {

//                    }
//                    else
//                    {
//                        usedIfIndex = lastIfIndex.Peek();
//                        usedEndIndex = i;
//                        break;
//                    }
//                }
//            }
//            lastIfIndex.Pop();
//            lastEndIndex.Pop();
//            if (cashIfIndex.Length > 0)
//            {
//                while (--n >= 0)
//                {
//                    lastIfIndex.Push(cashIfIndex[n]);
//                }
//                cashIfIndex = null;
//            }
//        }




//        lastIfIndex.Clear();
//        lastEndIndex.Clear();
//        for (int i = 0; i < content.childCount; i++)
//        {
//            if (content.GetChild(i).GetComponentInChildren<Text>().text == "If")
//            {
//                firstIfIndex.Enqueue(i);
//                lastIfIndex.Push(i);
//            }
//        }
//        for (int i = content.childCount - 1; i >= 0; i--)
//        {
//            if (content.GetChild(i).GetComponentInChildren<Text>().text == "End")
//            {
//                firstEndIndex.Enqueue(i);
//                lastEndIndex.Push(i);
//            }
//        }

//        for(int j = 0; j < queOfConds.Length; j++)
//        {
//            int[] tempFirstIfIndex = new int[firstIfIndex.Count];
//            int[] tempLastEndIndex = new int[lastEndIndex.Count];
//            int number = 0;
//            while (firstIfIndex.Peek() < lastEndIndex.Peek())
//            {
//                tempFirstIfIndex[number] = firstIfIndex.Dequeue();
//                number++;
//            }
//            for (int i = --number; i <= 0; i--)
//            {
//                firstIfIndex.Enqueue(tempFirstIfIndex[i]);
//                tempLastEndIndex[i] = lastEndIndex.Pop();
//            }

//            for(int i = firstIfIndex.Peek(); i <= lastEndIndex.Peek(); i++)
//            {

//            }









//            lastEndIndex.Pop();
//            firstIfIndex.Dequeue();
//            if(number > 0)
//            {
//                for(int i = 0; i <= number; i++)
//                {
//                    lastEndIndex.Push(tempLastEndIndex[i]);
//                }
//            }
//        }
//    }

//}

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
                yield return StartCoroutine(MoveLogic.MoveObject(submarine, content.transform.GetChild(step).GetComponentInChildren<Text>().text, 1, 20));
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

    IEnumerator MoveAnimation(GameObject @object,string direction)
    {
        int finishAnim = 0; 
        while (finishAnim++ != MoveLogic.speed)
        {
            MoveLogic.MoveObject(@object, direction);
            yield return new WaitForSeconds(0.01f);
        }
    }
    
}

