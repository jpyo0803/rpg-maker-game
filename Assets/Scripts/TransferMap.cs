using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class TransferMap : MonoBehaviour
{
    public string transferMapName; // 이동할 맵 이름

    public Transform target;
    public BoxCollider2D targetBound;

    public Animator anim_1;
    public Animator anim_2;

    public int door_count;

    [Tooltip("Up, Down, Left, Right")]
    public string direction; // 플레이어가 바라볼 방향
    private Vector2 vector; // getfloat("dirX)

    [Tooltip("문이 있으면: true, 문이 없다: false")]
    public bool door; // 문이 있는지 여부

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
        if (door == false)
        {
            if (collision.gameObject.name == "Player")
            {
                StartCoroutine(TransferCoroutine());
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (door)
        {
            if (collision.gameObject.name == "Player")
            {
                Debug.Log("Player is in door trigger area.");
                if (Input.GetKeyDown(KeyCode.Z))
                {
                    Debug.Log("Player Direction Vector: " + vector);
                    vector.Set(thePlayer.animator.GetFloat("DirX"), thePlayer.animator.GetFloat("DirY"));
                    switch (direction)
                    {
                        case "Up":
                            if (vector.y == 1f)
                            {
                                StartCoroutine(TransferCoroutine());
                            }
                            break;
                        case "Down":
                            if (vector.y == -1f)
                            {
                                StartCoroutine(TransferCoroutine());
                            }
                            break;
                        case "Left":
                            if (vector.x == -1f)
                            {
                                StartCoroutine(TransferCoroutine());
                            }
                            break;
                        case "Right":
                            if (vector.x == 1f)
                            {
                                StartCoroutine(TransferCoroutine());
                            }
                            break;
                        default:
                            StartCoroutine(TransferCoroutine());
                            break;
                    }
                }
            }
        }
    }

    IEnumerator TransferCoroutine()
    {
        theOrder.PreLoadCharacters(); // 캐릭터들 미리 로드
        theOrder.NotMove(); // 이동 금지
        theFade.FadeOut();

        if (door)
        {
            anim_1.SetBool("Open", true);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", true);
            }
        }
        yield return new WaitForSeconds(0.5f);
 
        theOrder.SetTransparent("player");

        if (door)
        {
            anim_1.SetBool("Open", false);
            if (door_count == 2)
            {
                anim_2.SetBool("Open", false);
            }
        }

        theOrder.UnsetTransparent("player");

        yield return new WaitForSeconds(0.5f);

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
