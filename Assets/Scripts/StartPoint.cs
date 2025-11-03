using UnityEngine;

public class StartPoint : MonoBehaviour
{
    public string startPoint;
    private MovingObject thePlayer;
    private CameraManager theCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // FindAnyObjectByType 사용 x. 타이밍 이슈 발생 가능 (파괴될 수도 있는 객체를 참조할 수 있음)
        theCamera = CameraManager.instance;
        thePlayer = MovingObject.instance;
        
        Debug.Log("StartPoint: " + startPoint + ", CurrentMapName: " + thePlayer.currentMapName);
        if (startPoint == thePlayer.currentMapName)
        {
            theCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, theCamera.transform.position.z);
            thePlayer.transform.position = this.transform.position;
        }
    }
}
