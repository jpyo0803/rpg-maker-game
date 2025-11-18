using UnityEngine;

using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MovingObject
{
    static public PlayerManager instance;
    public string currentMapName; // transferMap 스크립트에 있는 transferMapName 변수의 값을 저장 


    public string walkSound_1;
    public string walkSound_2;
    public string walkSound_3;
    public string walkSound_4;

    private AudioManager theAudio;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = true;

    private bool canMove = true;

    public bool notMove = false;

    void Awake()
    {
        // StartPoint와 TransferMap 스크립트에서 MovingObject.instance로 접근을 보장 
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // Scene 전환이 일어나도 해당 객체는 파괴 x 
            boxCollider = GetComponent<BoxCollider2D>();
            animator = GetComponent<Animator>();
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        queue = new Queue<string>();
        theAudio = AudioManager.instance;
    }

    IEnumerator MoveCoroutine()
    {
        while (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0 && !notMove)
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

            float allowedWalkCount =  base.GetAllowedWalkCount();
            if (allowedWalkCount < 1f)
            {
                animator.SetBool("Walking", false);
                canMove = true;
                yield break; // MoveCoroutine 완전히 종료
            }

            int temp = Random.Range(0, 4);
            switch (temp)
            {
                case 0:
                    theAudio.Play(walkSound_1);
                    break;
                case 1:
                    theAudio.Play(walkSound_2);
                    break;
                case 2:
                    theAudio.Play(walkSound_3);
                    break;
                case 3:
                    theAudio.Play(walkSound_4);
                    break;
            }

            boxCollider.offset = new Vector2(direction.x * 0.7f * speed * walkCount, direction.y * 0.7f * speed * walkCount);

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
                if (currentWalkCount == allowedWalkCount / 2)
                {
                    boxCollider.offset = Vector2.zero;
                }
                yield return new WaitForSeconds(0.01f);
            }

        }
        animator.SetBool("Walking", false);
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove && !notMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                canMove = false;
                StartCoroutine(MoveCoroutine());
            }
        }
    }
}
