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
    // [SerializeField]
    // public TestMove[] testMove;

    public string direction;
    private OrderManager theOrder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theOrder = OrderManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theOrder.PreLoadCharacters();
            // for (int i = 0; i < testMove.Length; i++)
            // {
            //     theOrder.Move(testMove[i].name, testMove[i].direction);
            // }
            theOrder.Turn("npc1", direction);
        }
    }
}
