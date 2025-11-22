using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    static public WeatherManager instance;

    private AudioManager theAudio;

    public ParticleSystem rain;
    public string rainSound;

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
        theAudio = AudioManager.instance;
    }

    public void RainStart()
    {
        rain.Play();
        theAudio.Play(rainSound);
    }

    public void RainStop()
    {
        rain.Stop();
        theAudio.Stop(rainSound);
    }

    // public void RainDrop()
    // {
    //     rain.Emit(10);
    // }
}
