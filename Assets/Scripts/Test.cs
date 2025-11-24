using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class TestMove
{
    public string name;
    public string direction;
}

public class Test : MonoBehaviour
{
    public GameObject go;

    private bool flag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!flag)
        {   
            if (collision.gameObject.name == "Player")
            {
                flag = true;
                go.SetActive(true);
            }
        }
    }
}
