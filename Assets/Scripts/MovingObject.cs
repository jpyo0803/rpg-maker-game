using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    // 공통 
    public float speed;
    protected Vector3 direction;
    public int walkCount;
    protected int currentWalkCount;

    protected bool npcCanMove = true;

    public Animator animator;
    public BoxCollider2D boxCollider;

    // NOTE(jpyo0803): 어떤 Layer와 충돌했는지 알기 위해 필요
    public LayerMask layerMask;

    protected void Move(string _dir, int _frequency)
    {
        StartCoroutine(MoveCoroutine(_dir, _frequency));
    }
    
    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        npcCanMove = false;
        direction.Set(0, 0, direction.z);
        switch (_dir)
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

        currentWalkCount = 0;
        while (currentWalkCount < walkCount)
        {
            transform.Translate(direction.x * speed, direction.y * speed, 0);
            currentWalkCount++;
            yield return new WaitForSeconds(0.01f);
        }

        if (_frequency != 5)
        {
            animator.SetBool("Walking", false);
        }
        npcCanMove = true;
    }
}
