using UnityEngine;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    public static FadeManager instance;
    public SpriteRenderer white;
    public SpriteRenderer black;

    private Color color;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
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

    public void FadeOut(float _fadeSpeed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine(_fadeSpeed));
    }

    IEnumerator FadeOutCoroutine(float _fadeSpeed)
    {
        color = black.color;
        while (color.a < 1f)
        {
            color.a += _fadeSpeed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void FadeIn(float _fadeSpeed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FadeInCoroutine(_fadeSpeed));
    }

    IEnumerator FadeInCoroutine(float _fadeSpeed)
    {
        color = black.color;
        while (color.a > 0f)
        {
            color.a -= _fadeSpeed;
            black.color = color;
            yield return waitTime;
        }
    }

    public void Flash(float _fadeSpeed = 0.1f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashCoroutine(_fadeSpeed));
    }

    IEnumerator FlashCoroutine(float _fadeSpeed)
    {
        // Flash Out
        color = white.color;
        while (color.a < 1f)
        {
            color.a += _fadeSpeed;
            white.color = color;
            yield return waitTime;
        }

        // Flash In
        color = white.color;
        while (color.a > 0f)
        {
            color.a -= _fadeSpeed;
            white.color = color;
            yield return waitTime;
        }
    }

    public void FlashOut(float _fadeSpeed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashOutCoroutine(_fadeSpeed));
    }

    IEnumerator FlashOutCoroutine(float _fadeSpeed)
    {
        color = white.color;
        while (color.a < 1f)
        {
            color.a += _fadeSpeed;
            white.color = color;
            yield return waitTime;
        }
    }

    public void FlashIn(float _fadeSpeed = 0.02f)
    {
        StopAllCoroutines();
        StartCoroutine(FlashInCoroutine(_fadeSpeed));
    }

    IEnumerator FlashInCoroutine(float _fadeSpeed)
    {
        color = white.color;
        while (color.a > 0f)
        {
            color.a -= _fadeSpeed;
            white.color = color;
            yield return waitTime;
        }
    }
}