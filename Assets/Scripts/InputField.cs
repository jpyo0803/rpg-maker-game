using TMPro;
using UnityEngine;

public class InputField : MonoBehaviour
{
    private PlayerManager thePlayer;

    public TextMeshProUGUI text;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        thePlayer = PlayerManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Debug.Log(text.text);
            thePlayer.characterName = text.text;
            Destroy(this.gameObject);
        }
    }
}
