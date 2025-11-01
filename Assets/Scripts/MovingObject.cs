using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    // NOTE(jpyo0803): 어떤 Layer와 충돌했는지 알기 위해 필요
    public LayerMask layerMask;

    public float speed;

    private Vector3 direction;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = true;

    public int walkCount;
    private int currentWalkCount;

    private bool canMove = true;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    
    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                applyRunSpeed = runSpeed;
                applyRunFlag = true;
            }
            else
            {
                applyRunSpeed = 0;
                applyRunFlag = false;
            }

            direction.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (direction.x != 0)
            {
                direction.y = 0;
            }

            animator.SetFloat("DirX", direction.x);
            animator.SetFloat("DirY", direction.y);

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

            if (allowedWalkCount < 1f)
            {
                animator.SetBool("Walking", false);
                canMove = true;
                yield break; // MoveCoroutine 완전히 종료
            }
            
            currentWalkCount = 0;
            while (currentWalkCount < allowedWalkCount)
            {
                if (direction.x != 0)
                {
                    transform.Translate(direction.x * (speed + applyRunSpeed), 0, 0);
                }
                else if (direction.y != 0)
                {
                    transform.Translate(0, direction.y * (speed + applyRunSpeed), 0);
                }
                if (applyRunFlag)
                    currentWalkCount++;
                currentWalkCount++;
                yield return new WaitForSeconds(0.01f);
            }

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
