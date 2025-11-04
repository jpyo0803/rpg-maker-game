using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name; // 사운드 이름
    public AudioClip clip; // 사운드 파일
    private AudioSource source; // 사운드 플레이어

    public float volume;
    public bool loop;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
    }

    public void SetVolume(float _volume)
    {
        source.volume = _volume;
    }

    public void Play()
    {
        source.Play();
    }

    public void Stop()
    {
        source.Stop();
    }

    public void SetLoop()
    {
        source.loop = true;
    }

    public void SetLoopCancel()
    {
        source.loop = false;
    }
}

public class AudioManager : MonoBehaviour
{
    static public AudioManager instance;

    [SerializeField]
    public Sound[] sounds;

    void Awake()
    {
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
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("사운드 파일 이름 : " + i + " : " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform); // AudioManager의 자식 객체로 설정
        }
    }

    public void SetVolume(string _name, float _volume)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].SetVolume(_volume);
                return;
            }
        }
        // Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }
        // Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Stop();
                return;
            }
        }
        // Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
        // Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }

    public void SetLoopCancel(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
        // Debug.LogWarning("AudioManager: Sound not found in list, " + _name);
    }
}
