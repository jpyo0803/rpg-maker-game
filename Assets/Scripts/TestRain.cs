using UnityEngine;

public class TestRain : MonoBehaviour
{
    WeatherManager theWeather;
    private bool rain;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theWeather = WeatherManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!rain)
            {
                // theWeather.RainStart();
                theWeather.RainDrop();
                rain = true;
            }
            else
            {
                theWeather.RainStop();
                rain = false;
            }
        }
    }
}
