using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵 이름

    public Transform target;
    public BoxCollider2D targetBound;

    private PlayerManager thePlayer;
    private CameraManager theCamera;

    private FadeManager theFade;
    private OrderManager theOrder;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // theCamera, thePlayer는 Awake에서 이미 초기화 되었으므로 바로 할당
        theCamera = CameraManager.instance;
        thePlayer = PlayerManager.instance;
        theFade = FadeManager.instance;
        theOrder = OrderManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(TransferCoroutine());
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.NotMove(); // 이동 금지
        theFade.FadeOut();
        yield return new WaitForSeconds(1f);

        thePlayer.currentMapName = transferMapName; // 이동할 맵 이름 저장
        Debug.Log("Transfer to " + transferMapName);

        // 카메라 및 플레이어 위치 이동
        theCamera.transform.position = new Vector3(target.position.x, target.position.y, theCamera.transform.position.z);
        theCamera.SetBound(targetBound);

        thePlayer.transform.position = target.position;

        theFade.FadeIn();
        yield return new WaitForSeconds(0.5f);
        theOrder.Move(); // 이동 허용
    }
}
