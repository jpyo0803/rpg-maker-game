using System.Collections;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;

    private Vector3 direction;

    public float runSpeed;
    private float applyRunSpeed;
    private bool applyRunFlag = true;

    public int walkCount;
    private int currentWalkCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private bool canMove = true;

    void Start()
    {

    }
    
    IEnumerator MoveCoroutine()
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

        currentWalkCount = 0;
        while (currentWalkCount < walkCount)
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
