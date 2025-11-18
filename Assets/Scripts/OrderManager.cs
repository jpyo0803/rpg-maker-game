using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager instance;

    private PlayerManager thePlayer; // 이벤트 도중에 키입력 처리 방지
    private List<MovingObject> characters; // 모든 캐릭터의 움직임 제어

    void Awake()
    {
        if (instance == null)
        {
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
        thePlayer = PlayerManager.instance;
    }

    public void PreLoadCharacters()
    {
        characters = ToList();
    }

    public List<MovingObject> ToList()
    {
        List<MovingObject> tempList = new List<MovingObject>();
        MovingObject[] temp = FindObjectsOfType<MovingObject>();

        Debug.Log("Found " + temp.Length + " MovingObject instances.");
        for (int i = 0; i < temp.Length; i++)
        {
            tempList.Add(temp[i]);
        }
        return tempList;
    }

    public void NotMove()
    {
        thePlayer.notMove = true;
    }

    public void Move()
    {
        thePlayer.notMove = false;
    }

    public void UnsetCollider(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].boxCollider.enabled = false;
            }
        }
    }

    public void SetCollider(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].boxCollider.enabled = true;
            }
        }
    }

    public void UnsetActive(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].gameObject.SetActive(false);
            }
        }
    } 

    public void SetActive(string _name)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].gameObject.SetActive(true);
            }
        }
    }
    
    public void Move(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].Move(_dir); // frequency 기본값 5 사용
            }
        }
    }

    public void Turn(string _name, string _dir)
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].characterName == _name)
            {
                characters[i].animator.SetFloat("DirX", 0);
                characters[i].animator.SetFloat("DirY", 0);
                
                switch (_dir)
                {
                    case "Up":
                        characters[i].animator.SetFloat("DirY", 1);
                        break;
                    case "Down":
                        characters[i].animator.SetFloat("DirY", -1);
                        break;
                    case "Left":
                        characters[i].animator.SetFloat("DirX", -1);
                        break;
                    case "Right":
                        characters[i].animator.SetFloat("DirX", 1);
                        break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
