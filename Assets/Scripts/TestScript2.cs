using System.Collections;
using UnityEngine;

public class TestScript2 : MonoBehaviour
{
    BGMManager bgmManager;

    public int playMusicIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bgmManager = BGMManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(abc());
    }

    IEnumerator abc()
    {
        bgmManager.FadeOutMusic();
        yield return null;
    }
}

