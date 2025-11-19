using System.Collections;
using UnityEngine;

public class Event1 : MonoBehaviour
{
    public Dialogue dialogue_1;
    public Dialogue dialogue_2;

    private DialogueManager theDM;
    private OrderManager theOrder;
    private PlayerManager thePlayer;

    private bool flag; // false
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theDM = DialogueManager.instance;
        theOrder = OrderManager.instance;
        thePlayer = PlayerManager.instance;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (!flag && Input.GetKey(KeyCode.Z) && thePlayer.animator.GetFloat("DirY") == 1)
        {
            flag = true;
            StartCoroutine(EventCoroutine());
        }
    }

    IEnumerator EventCoroutine()
    {
        theOrder.PreLoadCharacters();

        theOrder.NotMove();

        theDM.ShowDialogue(dialogue_1);

        yield return new WaitUntil(() => theDM.talking == false);

        theOrder.Move("player", "Right");
        theOrder.Move("player", "Right");
        theOrder.Move("player", "Up");

        yield return new WaitUntil(() => thePlayer.queue.Count == 0);

        theDM.ShowDialogue(dialogue_2);

        yield return new WaitUntil(() => theDM.talking == false);

        theOrder.Move();
    }
}
