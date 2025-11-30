using System;
using System.Collections;
using NUnit.Framework.Constraints;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private OrderManager theOrder;
    // private NumberSystem theNumber;

    private DialogueManager theDM;    

    public bool flag = false;
    public string[] texts;

    // public int correctNumber = 345;

    void Start()
    {
        theOrder = OrderManager.instance;
        theDM = DialogueManager.instance;
        // theNumber = NumberSystem.instance;
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
        // theNumber.ShowNumber(correctNumber);
        theDM.ShowText(texts);
        yield return new WaitUntil(() => theDM.talking == false);
        // yield return new WaitUntil(() => theNumber.activated == false);
        theOrder.Move();
    }
}
