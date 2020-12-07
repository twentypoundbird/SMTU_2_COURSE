using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlPanel : MonoBehaviour
{
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

    [Header("Colors")]
    public Color standartColor;
    public Color ifColor;
    public Color conditionColor;
    public Color operatorColor;


    [Header("Transforms")]
    public Transform content;
    public Transform buttonContent;


    Stack <bool> moveTapped = new Stack<bool>(),
        ifCondition = new Stack<bool>(),
        conditionParamTapped = new Stack<bool>(),
        exitFromParam = new Stack<bool>(),
        logicOperatorTapped = new Stack<bool>(),
        isEndCondition = new Stack<bool>();


    private int clearIndex = 7, notClearIndex = 8,
        andIndex = 9, orIndex = 10, toIndex = 11,
        endIndex = 12;

    private int numberOfIf = 0;

    private void ConditionDelete()
    {
        ifCondition.Pop();
        moveTapped.Pop();
        conditionParamTapped.Pop();
        exitFromParam.Pop();
        logicOperatorTapped.Pop();
        isEndCondition.Pop();
    }
    private void equalLastInStack(Stack<bool> stack, bool newBool)
    {
        stack.Pop();
        stack.Push(newBool);
    }
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
    private void Start()
    {
        allStacksPosition(false);
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
    }
    public void IsTappedForward()
    {
        Instantiate(forwardLabel,content);
        forwardLabel.GetComponent<Image>().color = standartColor;
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedBack()
    {
        Instantiate(backLabel, content);
        backLabel.GetComponent<Image>().color = standartColor;
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedTurnLeft()
    {
        Instantiate(turnToLeftLabel, content);
        turnToLeftLabel.GetComponent<Image>().color = standartColor;
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedTurnRight()
    {
        Instantiate(turnToRightLabel, content);
        turnToRightLabel.GetComponent<Image>().color = standartColor;
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedUp()
    {
        Instantiate(upLabel, content);
        upLabel.GetComponent<Image>().color = standartColor;
        equalLastInStack(moveTapped, true);
        equalLastInStack(conditionParamTapped, false);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedDown()
    {
        Instantiate(downLabel, content);
        downLabel.GetComponent<Image>().color = standartColor;
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
        Instantiate(clearLabel, content);
        clearLabel.GetComponent<Image>().color = conditionColor;

        equalLastInStack(conditionParamTapped,true);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedNotClear()
    {
        Instantiate(notClearLabel, content);
        notClearLabel.GetComponent<Image>().color = conditionColor;

        equalLastInStack(conditionParamTapped, true);
        equalLastInStack(logicOperatorTapped, false);
    }
    public void IsTappedAnd()
    {
        Instantiate(andLabel, content);
        andLabel.GetComponent<Image>().color = operatorColor;
        equalLastInStack(logicOperatorTapped, true);
        equalLastInStack(moveTapped, false);
        equalLastInStack(conditionParamTapped, false);
    }
    public void IsTappedOr()
    {
        Instantiate(orLabel, content);
        orLabel.GetComponent<Image>().color = operatorColor;
        equalLastInStack(logicOperatorTapped, true);
        equalLastInStack(moveTapped, false);
        equalLastInStack(conditionParamTapped, false);
    }
    public void IsTappedTo()
    {
        Instantiate(toLabel, content);
        toLabel.GetComponent<Image>().color = ifColor;
        equalLastInStack(exitFromParam, true);
    }
    public void IsTappedIf()
    {
        equalLastInStack(moveTapped, false);
        allStacksPosition(false);
        equalLastInStack(ifCondition, true);
        Instantiate(ifLabel, content);
        ifLabel.GetComponent<Image>().color = ifColor;
        StartCoroutine(IfCondition(numberOfIf));
    }
    public void IsTappedEndCondition()
    {
        Instantiate(endOfConditionLabel, content);
        endOfConditionLabel.GetComponent<Image>().color = ifColor;
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
