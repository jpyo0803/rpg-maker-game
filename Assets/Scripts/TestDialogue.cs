using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField]
    public Dialogue dialogue;

    private DialogueManager theDM;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theDM = DialogueManager.instance;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            theDM.ShowDialogue(dialogue);
        }
    }
}
