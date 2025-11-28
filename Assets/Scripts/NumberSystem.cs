using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class NumberSystem : MonoBehaviour
{
    static public NumberSystem instance;
    public AudioManager theAudio;
    public string key_sound;
    public string enter_sound; // 결정키 사운드
    public string cancel_sound; // 오답 && 취소키 사운드
    public string correct_sound; // 정답 사운드

    private int count; // 배열의 크기, 자릿수 
    private int selectedTextBox; // 현재 선택된 텍스트 박스
    private int result; // 사용자가 입력한 숫자
    private int correctNumber; // 정답 숫자

    private string tempNumber; // 임시로 숫자 저장

    public GameObject superObject; // 자릿수 마다 화면 가운데 정렬을 위해 필요
    public GameObject[] panel;
    public TextMeshProUGUI[] Number_Text;

    public Animator anim;

    public bool activated; // waitUntil 용도 
    private bool keyInput; // 키 입력 가능 여부
    private bool correctFlag; // 정답 여부

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
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
        theAudio = AudioManager.instance;
        
    }

    public void ShowNumber(int _correctNumber)
    {
        correctNumber = _correctNumber;
        activated = true;
        correctFlag = false;

        string temp = correctNumber.ToString(); // 자릿수 구하기 위해 문자열로 변환
        for (int i = 0; i < temp.Length; i++)
        {
            count = i;
            panel[i].SetActive(true);
            Number_Text[i].text = "0";
        }

        superObject.transform.position = new Vector3(superObject.transform.position.x + 30 * count, superObject.transform.position.y, superObject.transform.position.z);
        selectedTextBox = 0;
        result = 0;
        SetColor();

        anim.SetBool("Appear", true);
        keyInput = true;
    }

    public bool GetResult()
    {
        return correctFlag;
    }

    public void SetNumber(string _arrow)
    {
        int temp = int.Parse(Number_Text[selectedTextBox].text);
        if (_arrow == "Down")
        {
           if (temp == 0)
            {
                temp = 9;
            }
            else
            {
                temp -= 1;
            }
        }
        else if (_arrow == "Up")
        {
            if (temp == 9)
            {
                temp = 0;
            }
            else
            {
                temp += 1;
            }
        }
        Number_Text[selectedTextBox].text = temp.ToString();
    }

    public void SetColor()
    {
        Color color = Number_Text[0].color;
        color.a = 0.3f;
        for (int i = 0; i <= count; i++)
        {
            Number_Text[i].color = color;
        }
        color.a = 1f;
        Number_Text[selectedTextBox].color = color;
    }

    // Update is called once per frame
    void Update()
    {
        if (keyInput)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                theAudio.Play(key_sound);       
                SetNumber("Down");
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                theAudio.Play(key_sound);         
                SetNumber("Up");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox < count)
                {
                    selectedTextBox++;
                }
                else
                {
                    selectedTextBox = 0;
                }
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                theAudio.Play(key_sound);
                if (selectedTextBox > 0)
                {
                    selectedTextBox--;
                }
                else
                {
                    selectedTextBox = count;
                }
                SetColor();
            }
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                theAudio.Play(enter_sound);
                keyInput = false;
                StartCoroutine(OXCoroutine());
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                theAudio.Play(cancel_sound);
                keyInput = false;
                StartCoroutine(ExitCoroutine());
            }
        }
    }
    IEnumerator OXCoroutine()
    {
        Color color = Number_Text[0].color;
        color.a = 1f;

        for (int i = count; i >= 0; i--)
        {
            Number_Text[i].color = color;
            tempNumber += Number_Text[i].text;
        }

        yield return new WaitForSeconds(1f);

        result = int.Parse(tempNumber);

        if (result == correctNumber)
        {
            theAudio.Play(correct_sound);
            correctFlag = true;
            // 정답 처리
        }
        else
        {
            theAudio.Play(cancel_sound);
            correctFlag = false;
            // 오답 처리
        }

        Debug.Log("Our answer = " + result.ToString() + ", Correct answer = " + correctNumber.ToString());
        StartCoroutine(ExitCoroutine());
    }

    IEnumerator ExitCoroutine()
    {
        result = 0;
        tempNumber = "";
        anim.SetBool("Appear", false);
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i <= count; i++)
        {
            panel[i].SetActive(false);
        }
        superObject.transform.position = new Vector3(superObject.transform.position.x - 30 * count, superObject.transform.position.y, superObject.transform.position.z);
    
        activated = false;
    }
}



