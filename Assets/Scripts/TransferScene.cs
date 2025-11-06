using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferScene : MonoBehaviour
{
    public string transferMapName; // 이동할 맵 이름

    private PlayerManager thePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // thePlayer는 Awake에서 이미 초기화 되었으므로 바로 할당
        thePlayer = PlayerManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName;
            SceneManager.LoadScene(transferMapName);
        }
    }
}
