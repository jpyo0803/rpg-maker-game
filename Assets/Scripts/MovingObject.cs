using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class MovingObject : MonoBehaviour
{
    public string characterName;
    // 공통 
    public float speed;
    protected Vector3 direction;

    public Queue<string> queue; // 이동 명령어 큐

    public int walkCount;
    protected int currentWalkCount;

    private bool coroutineDisabled = false;

    public Animator animator;
    public BoxCollider2D boxCollider;

    // NOTE(jpyo0803): 어떤 Layer와 충돌했는지 알기 위해 필요
    public LayerMask layerMask;

    public void Move(string _dir, int _frequency = 5)
    {
        queue.Enqueue(_dir);
        if (!coroutineDisabled)
        {
            coroutineDisabled = true;
            StartCoroutine(MoveCoroutine(_dir, _frequency));
        }
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while (queue.Count > 0)
        {
            string dir_str = queue.Dequeue();
            direction.Set(0, 0, direction.z);
            switch (dir_str)
            {
                case "Up":
                    direction.y = 1;
                    break;
                case "Down":
                    direction.y = -1;
                    break;
                case "Left":
                    direction.x = -1;
                    break;
                case "Right":
                    direction.x = 1;
                    break;
            }
            print("Dir: " + _dir);

            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);
            animator.SetBool("Walking", true);

            float allowedWalkCount = this.GetAllowedWalkCount();

            if (allowedWalkCount < 1f)
            {
                animator.SetBool("Walking", false);
                yield break; // MoveCoroutine 완전히 종료
            }

            currentWalkCount = 0;
            while (currentWalkCount < allowedWalkCount)
            {
                transform.Translate(direction.x * speed, direction.y * speed, 0);
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

            if (_frequency != 5)
            {
                animator.SetBool("Walking", false);
            }
        }

        animator.SetBool("Walking", false);
        coroutineDisabled = false;
    }
    
    protected int GetAllowedWalkCount()
    {
        RaycastHit2D hit;
        // A지점에서 B지점으로 레이저를 쏨
        // 만약 B지점까지 레이저가 도달하면 hit은 Null을 리턴
        // 만약 B지점까지 가는 길목에 방해물이 있다면 방해물을 리턴

        Vector2 start = transform.position; // A지점, 캐릭터의 현재 위치 값
        Vector2 end = start + new Vector2(direction.x * speed * walkCount, direction.y * speed * walkCount); // B지점, 캐릭터가 이동하고자 하는 위치 값

        hit = Physics2D.Linecast(start, end, layerMask);

        boxCollider.enabled = false; // 캐릭터 스스로의 box collider에 레이저 충돌 방지하기 위해 잠깐 꺼둠
        animator.SetBool("Walking", true);
        boxCollider.enabled = true; // 다시 킴 

        float allowedWalkCount = walkCount;
        if (hit.transform != null)
        {
            float totalDistance = Vector2.Distance(start, end);
            float ratio = hit.distance / totalDistance;
            ratio = Mathf.Max(0f, ratio - 0.2f);
            allowedWalkCount = Mathf.Floor(walkCount * ratio);
        }

        return (int)allowedWalkCount;
    }
}
