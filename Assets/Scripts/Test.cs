using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    private OrderManager theOrder;
    private NumberSystem theNumber;

    public bool flag = false;

    public int correctNumber = 345;

    void Start()
    {
        theOrder = OrderManager.instance;
        theNumber = NumberSystem.instance;
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
        theNumber.ShowNumber(correctNumber);
        yield return new WaitUntil(() => theNumber.activated == false);
        theOrder.Move();
    }
}
