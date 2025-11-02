using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    public string transferMapName;

    public Transform target;

    private MovingObject thePlayer;
    private CameraManager theCamera;

    public bool flag;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // FindAnyObjectByType: Hierarchy에 있는 모든 객체의 <>컴포넌트를 검색해서 리턴
        // GetComponent: 해당 스크립트가 속해있는 객체의 <>컴포넌트를 검색해서 리턴 
        if (flag == false)
        {
            theCamera = CameraManager.instance;
        }
        thePlayer = MovingObject.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (flag == true)
            {
                thePlayer.currentMapName = transferMapName;
                SceneManager.LoadScene(transferMapName);
            }
            else
            {
                theCamera.transform.position = new Vector3(target.position.x, target.position.y, theCamera.transform.position.z);
                thePlayer.transform.position = target.position;
            }
        }
    }
}
