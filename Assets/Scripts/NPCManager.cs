using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NPCMove
{
    [Tooltip("NPCMove를 체크하면 NPC가 움직입니다.")] // 툴팁은 에디터에서 마우스를 올렸을 때 설명이 나옵니다.
    public bool NPCmove;

    public string[] direction; // npc가 움직일 방향 설정 

    [Range(1, 5)] [Tooltip("1 = 천천히, 2 = 조금 천천히, 3 = 보통, 4 = 빠르게, 5 = 연속적으로")] // inspector에서 슬라이더로 설정 가능
    public int frequency; // npc가 움직일 방향으로 얼마나 빠른 속도로 움직일지 설정
}
public class NPCManager : MovingObject
{
    [SerializeField]
    public NPCMove npcMove;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        queue = new Queue<string>();
        // StartCoroutine(MoveCoroutine());
    }

    public void SetMove()
    {

    }

    public void SetNotMove()
    {
        StopAllCoroutines();
    }
    
    IEnumerator MoveCoroutine()
    {
        if (npcMove.direction.Length != 0)
        {
            for (int i = 0; i < npcMove.direction.Length; i++)
            {
                switch (npcMove.frequency)
                {
                    case 1:
                        yield return new WaitForSeconds(4.0f);
                        break;
                    case 2:
                        yield return new WaitForSeconds(3.0f);
                        break;
                    case 3:
                        yield return new WaitForSeconds(2.0f);
                        break;
                    case 4:
                        yield return new WaitForSeconds(1.0f);
                        break;
                    case 5:
                        break;
                }

                yield return new WaitUntil(() => queue.Count < 2);

                base.Move(npcMove.direction[i], npcMove.frequency);

                if (i == npcMove.direction.Length - 1)
                {
                    i = -1; // 처음부터 다시 반복
                }
            }
        }
    }
}
