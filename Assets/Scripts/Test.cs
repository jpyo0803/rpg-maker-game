using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    public Choice choice;
    private OrderManager theOrder;
    private ChoiceManager theChoice;

    public bool flag = false;

    void Start()
    {
        theOrder = OrderManager.instance;
        theChoice = ChoiceManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {   
            StartCoroutine(ACoroutine());
        }
    }

    IEnumerator ACoroutine()
    {
        flag = true;
        theOrder.NotMove();
        theChoice.ShowChoice(choice);
        yield return new WaitUntil(() => !theChoice.choiceIng);
        Debug.Log("Selected answer index: " + theChoice.GetResult());
        theOrder.Move();
        flag = false;
    }
}
