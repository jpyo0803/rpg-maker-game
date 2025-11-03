using UnityEngine;

public class CameraManager : MonoBehaviour
{
    static public CameraManager instance;
    public GameObject target; // 카메라가 따라갈 대상 
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 대상을 쫓을건지?
    private Vector3 targetPosition; // 대상의 현재 위치 값

    public BoxCollider2D bound; // 카메라가 움직일 수 있는 영역

    private Vector3 minBound; // 영역의 최소값
    private Vector3 maxBound; // 영역의 최대값

    private float halfWidth; // 카메라의 절반 너비
    private float halfHeight; // 카메라의 절반 높이

    private Camera theCamera; // 카메라의 절반 너비, 높이 값을 구하기 위해 카메라 컴포넌트 변수 선값

    void Awake()
    {
        // StartPoint와 TransferMap 스크립트에서 MovingObject.instance로 접근을 보장 
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject); // Scene 전환이 일어나도 해당 객체는 파괴 x 
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }    
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theCamera = GetComponent<Camera>();

        halfHeight = theCamera.orthographicSize;
        halfWidth = halfHeight * Screen.width / Screen.height; // 화면 비율에 따른 카메라 절반 너비 계산

        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }

    // Update is called once per frame
    void Update()
    {
        if (target.gameObject != null)
        {
            // 카메라의 z값이 대상의 z값보다 작아야 대상이 카메라에 잡힘
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // Time.deltaTime은 1초에 실행되는 프레임의 역시. 만약 초당 60프레임이면 Time.deltaTime = 1 / 60
            // Lerp는 첫번째 함수 인자 pose부터 두번째 함수 인자 pose까지 세번째 함수 인자 rate에 맞춰 이동시키는 것을 의미 
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime); // 1초에 moveSpeed만큼 이동 

            float clampedX = Mathf.Clamp(this.transform.position.x, minBound.x + halfWidth, maxBound.x - halfWidth);
            float clampedY = Mathf.Clamp(this.transform.position.y, minBound.y + halfHeight, maxBound.y - halfHeight);

            this.transform.position = new Vector3(clampedX, clampedY, this.transform.position.z);
        }
    }
    
    public void SetBound(BoxCollider2D newBound)
    {
        bound = newBound;

        minBound = bound.bounds.min;
        maxBound = bound.bounds.max;
    }
}
