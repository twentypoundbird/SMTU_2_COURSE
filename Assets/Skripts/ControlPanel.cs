using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
    //делаем ссылки на префабы блоков команд
    [Header("Labels")]
    public GameObject forwardLabel;
    public GameObject backLabel;
    public GameObject turnToLeftLabel;
    public GameObject turnToRightLabel;
    public GameObject upLabel;
    public GameObject downLabel;
    public GameObject ifLabel;
    public GameObject clearLabel;
    public GameObject notClearLabel;
    public GameObject andLabel;
    public GameObject orLabel;
    public GameObject toLabel;
    public GameObject endOfConditionLabel;

    //ссылки на цвета блоков
    [Header("Colors")]
    public Color standartColor;
    public Color ifColor;
    public Color conditionColor;
    public Color operatorColor;

    //ссылки на пространства, внутри которых будут меняться координаты объектов (родитель блоков - content, родитель кнопок - buttonContent)
    [Header("Transforms")]
    public Transform content;
    public Transform buttonContent;

    //создаем булевые стэки, отвечающие за нажатие тех или иных команд на данном уровне
    Stack <bool> moveTapped = new Stack<bool>(),
        ifCondition = new Stack<bool>(),
        conditionParamTapped = new Stack<bool>(),
        exitFromParam = new Stack<bool>(),
        logicOperatorTapped = new Stack<bool>(),
        isEndCondition = new Stack<bool>();

    //переменные, отвечающие за индекс позиций определенных кнопок в столбце команд
    private int clearIndex = 7, notClearIndex = 8,
        andIndex = 9, orIndex = 10, toIndex = 11,
        endIndex = 12;

    //кол-во условных операторов if
    private int numberOfIf = 0;

    //метод удаления последних эллементов в стэках
    private void ConditionDelete()
    {
        ifCondition.Pop();
        moveTapped.Pop();
        conditionParamTapped.Pop();
        exitFromParam.Pop();
        logicOperatorTapped.Pop();
        isEndCondition.Pop();
    }

    //метод замены последнего эл-та стэка
    private void equalLastInStack(Stack<bool> stack, bool newBool)
    {
        stack.Pop();
        stack.Push(newBool);
    }

    //метод, котрый либо полностью очищает стэки, либо пушит в стэки значение false
    private void allStacksPosition(bool delete)
    {
        if (delete)
        {
            moveTapped.Clear();
            ifCondition.Clear();
            conditionParamTapped.Clear();
            exitFromParam.Clear();
            logicOperatorTapped.Clear();
            isEndCondition.Clear();
        }
        else
        {
            moveTapped.Push(false);
            ifCondition.Push(false);
            conditionParamTapped.Push(false);
            exitFromParam.Push(false);
            logicOperatorTapped.Push(false);
            isEndCondition.Push(false);
        }
    }

    //метод Start вызывается перед первым фреймом на сцене
    private void Start()
    {
        allStacksPosition(false);
        for (int i = 0; i < buttonContent.childCount; i++) //делаем активными нужные нам кнопки
        {
            if (i < clearIndex)
            {
                buttonContent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                buttonContent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    
    public void IsTappedForward()
    {
        forwardLabel.GetComponent<Image>().color = standartColor;
        Instantiate(forwardLabel,content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedBack()
    {
        backLabel.GetComponent<Image>().color = standartColor;
        Instantiate(backLabel, content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedTurnLeft()
    {
        turnToLeftLabel.GetComponent<Image>().color = standartColor;
        Instantiate(turnToLeftLabel, content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedTurnRight()
    {
        turnToRightLabel.GetComponent<Image>().color = standartColor;
        Instantiate(turnToRightLabel, content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedUp()
    {
        upLabel.GetComponent<Image>().color = standartColor;
        Instantiate(upLabel, content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedDown()
    {
        downLabel.GetComponent<Image>().color = standartColor;
        Instantiate(downLabel, content);
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedDeleteLast()
    {
        if(content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "If")
        {

            Debug.LogWarning("delete if");
            StopAllCoroutines();
            for (int i = 0; i < buttonContent.childCount; i++)
            {
                if (i < clearIndex)
                {
                    buttonContent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    buttonContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            ConditionDelete();
        }
        if((content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Forward" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Back" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Up" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Down" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Turn left" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Turn right") &&
            ifCondition.Peek())
        {
            equalLastInStack(moveTapped, false);
            equalLastInStack(conditionParamTapped, true);
            equalLastInStack(logicOperatorTapped, true);
        }
        if((content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Clear" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "Not clear") && ifCondition.Peek())
        {
            equalLastInStack(moveTapped, true);
            equalLastInStack(logicOperatorTapped, true);
            equalLastInStack(conditionParamTapped, false);
        }
        if((content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "&&" ||
            content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "||") && ifCondition.Peek())
        {
            equalLastInStack(moveTapped, true);
            equalLastInStack(conditionParamTapped, true);
            equalLastInStack(logicOperatorTapped, false);
        }
        if (content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "To" && ifCondition.Peek())
        {
            StopCoroutine(IfCondition(numberOfIf));
            equalLastInStack(moveTapped, true);
            equalLastInStack(conditionParamTapped, true);
            equalLastInStack(exitFromParam, false);
            equalLastInStack(logicOperatorTapped, false);
            StartCoroutine(IfCondition(numberOfIf));
        }
        if (content.GetChild(content.childCount - 1).GetComponentInChildren<Text>().text == "End")
        {
            StopCoroutine(IfCondition(numberOfIf));
            allStacksPosition(false);
            equalLastInStack(exitFromParam, true);
            equalLastInStack(ifCondition, true);
            StartCoroutine(IfCondition(numberOfIf));
        }
        Destroy(content.GetChild(content.childCount - 1).gameObject);
    }
    public void IsTappedDeleteAll()
    {
        StopAllCoroutines();
        for (int i = 0; i < buttonContent.childCount; i++)
        {
            if (i < clearIndex)
            {
                buttonContent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                buttonContent.GetChild(i).gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        moveTapped.Clear();
        ifCondition.Clear();
        conditionParamTapped.Clear();
        exitFromParam.Clear();
        logicOperatorTapped.Clear();
        isEndCondition.Clear();



        allStacksPosition(false);
    }
    public void IsTappedClear()
    {
        clearLabel.GetComponent<Image>().color = conditionColor;
        Instantiate(clearLabel, content);

        equalLastInStack(conditionParamTapped,true);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedNotClear()
    {
        notClearLabel.GetComponent<Image>().color = conditionColor;
        Instantiate(notClearLabel, content);

        equalLastInStack(conditionParamTapped, true);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedAnd()
    {
        andLabel.GetComponent<Image>().color = operatorColor;
        Instantiate(andLabel, content);
        equalLastInStack(logicOperatorTapped, true);
        equalLastInStack(moveTapped, false);
        equalLastInStack(conditionParamTapped, false);
    }
    public void IsTappedOr()
    {
        orLabel.GetComponent<Image>().color = operatorColor;
        Instantiate(orLabel, content);
        equalLastInStack(logicOperatorTapped, true);
        equalLastInStack(moveTapped, false);
        equalLastInStack(conditionParamTapped, false);
    }
    public void IsTappedTo()
    {
        toLabel.GetComponent<Image>().color = ifColor;
        Instantiate(toLabel, content);
        equalLastInStack(exitFromParam, true);
    }
    public void IsTappedIf()
    {
        equalLastInStack(moveTapped, false);
        allStacksPosition(false);
        equalLastInStack(ifCondition, true);
        ifLabel.GetComponent<Image>().color = ifColor;
        Instantiate(ifLabel, content);
        StartCoroutine(IfCondition(numberOfIf));
    }
    public void IsTappedEndCondition()
    {
        endOfConditionLabel.GetComponent<Image>().color = ifColor;
        Instantiate(endOfConditionLabel, content);
        equalLastInStack(isEndCondition, true);
    }

    IEnumerator IfCondition(int n)
    {
        while (!exitFromParam.Peek())
        {
            for (int i = 0; i < buttonContent.childCount; i++)
            {
                if (i < 6)
                {
                    buttonContent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    buttonContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield return new WaitUntil(() => moveTapped.Peek());
            for (int i = 0; i < buttonContent.childCount; i++) // показываем кнопки clear и not clear
            {
                if (i == clearIndex || i == notClearIndex)
                {
                    buttonContent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    buttonContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield return new WaitUntil(() => conditionParamTapped.Peek());
            for(int i = 0; i< buttonContent.childCount; i++) // показывем кнопки &&, || и To
            {
                if(i == andIndex || i == orIndex || i == toIndex)
                {
                    buttonContent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    buttonContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield return new WaitUntil(() => logicOperatorTapped.Peek() || exitFromParam.Peek());
            equalLastInStack(logicOperatorTapped, false);
        }
        equalLastInStack(ifCondition, true);
        equalLastInStack(exitFromParam, true);
        while (!isEndCondition.Peek() && ifCondition.Peek())
        {
            for (int i = 0; i < buttonContent.childCount; i++) // показывем кнопки которыми можно воспользоваться внутри if
            {
                if (i < clearIndex || i == endIndex)
                {
                    buttonContent.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    buttonContent.GetChild(i).gameObject.SetActive(false);
                }
            }
            yield return new WaitUntil(() => isEndCondition.Peek()); // ждем кнопки end
            ConditionDelete();

        }
        for (int i = 0; i < buttonContent.childCount; i++) //показываем стандартный набор кнопок
        {
            if (i < clearIndex)
            {
                buttonContent.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                buttonContent.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
