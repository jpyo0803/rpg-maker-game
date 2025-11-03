using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵 이름

    public Transform target;
    public BoxCollider2D targetBound;

    private MovingObject thePlayer;
    private CameraManager theCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // theCamera, thePlayer는 Awake에서 이미 초기화 되었으므로 바로 할당
        theCamera = CameraManager.instance;
        thePlayer = MovingObject.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            thePlayer.currentMapName = transferMapName; // 이동할 맵 이름 저장

            // 카메라 및 플레이어 위치 이동
            theCamera.transform.position = new Vector3(target.position.x, target.position.y, theCamera.transform.position.z);
            theCamera.SetBound(targetBound);

            thePlayer.transform.position = target.position;
        }
    }
}
