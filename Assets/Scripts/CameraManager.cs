using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // 카메라가 따라갈 대상 
    public float moveSpeed; // 카메라가 얼마나 빠른 속도로 대상을 쫓을건지?
    private Vector3 targetPosition; // 대상의 현재 위치 값

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
        }        
    }
}
