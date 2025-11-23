using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    static public DatabaseManager instance;

    /* 
      DB 목적
      1. 씬 이동 시 정보 유지
      2. 세이브와 로드
      3. 아이템의 오브젝트 풀링
    */

    public string[] var_name;
    public float[] var;

    public string[] switch_name;
    public bool[] switches;

    private void Awake()
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
        
    }
}
