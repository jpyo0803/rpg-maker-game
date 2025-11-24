using UnityEngine;

public class LightController : MonoBehaviour
{
    private PlayerManager thePlayer;
    private Vector2 direction;

    private Quaternion rotation; // 회전 각도, 4D 벡터 (x, y, z, w)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, this.transform.position.z);
        
        direction.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));

        if (direction.x == 1f)
        {
            rotation = Quaternion.Euler(0f, 0f, 90f);
            this.transform.rotation = rotation;
        }
        else if (direction.x == -1f)
        {
            rotation = Quaternion.Euler(0f, 0f, 270f);
            this.transform.rotation = rotation;
        }
        else if (direction.y == 1f)
        {
            rotation = Quaternion.Euler(0f, 0f, 180f);
            this.transform.rotation = rotation;
        }
        else if (direction.y == -1f)
        {
            rotation = Quaternion.Euler(0f, 0f, 0f);
            this.transform.rotation = rotation;
        }
    }
}
