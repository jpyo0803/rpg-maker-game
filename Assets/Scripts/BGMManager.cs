using System.Collections;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public AudioClip[] clips; // 재생할 BGM 클립들

    private AudioSource audioSource; // BGM 재생을 위한 오디오 소스

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

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
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(int _index)
    {
        if (_index < 0 || _index >= clips.Length) return;

        audioSource.volume = 1.0f;
        audioSource.clip = clips[_index];
        audioSource.Play();
    }

    public void SetVolume(float _volume)
    {
        audioSource.volume = _volume;
    }

    public void Pause()
    {
        audioSource.Pause();
    }

    public void UnPause()
    {
        audioSource.UnPause();
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    // BGM이 점점 작아지면서 사라지게 하는 함수
    public void FadeOutMusic()
    {
        // FadeIn, FadeOut이 동시에 실행되지 않도록 모든 코루틴 정지
        StopAllCoroutines();
        StartCoroutine(FadeOutMusicCoroutine());
    }

    IEnumerator FadeOutMusicCoroutine()
    {
        for (float vol = 1.0f; vol >= 0; vol -= 0.01f)
        {
            audioSource.volume = vol;
            yield return waitTime;
        }
    }

    // BGM이 점점 커지면서 재생되게 하는 함수
    public void FadeInMusic()
    {
        // FadeIn, FadeOut이 동시에 실행되지 않도록 모든 코루틴 정지
        StopAllCoroutines();
        StartCoroutine(FadeInMusicCoroutine());
    }
    
    IEnumerator FadeInMusicCoroutine()
    {
        for (float vol = 0f; vol <= 1.0f; vol += 0.01f)
        {
            audioSource.volume = vol;
            yield return waitTime;
        }
    }
}
