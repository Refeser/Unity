using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainBttns : MonoBehaviour
{
    public Sprite layer_blue, layer_red;
    public GameObject s_on, s_off;

    private void Start()
    {
        switch (gameObject.name)
        {
            case "Sound":
                if (PlayerPrefs.GetString("Music") == "Off")
                {
                    s_on.SetActive(false);
                    s_off.SetActive(true);
                }
                else
                {
                    s_on.SetActive(true);
                    s_off.SetActive(false);
                }
                break;
        }
    }

    void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = layer_red;
    }

    void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().sprite = layer_blue;
    }

    void OnMouseUpAsButton()
    {
        if (PlayerPrefs.GetString("Sound") != "Off")
           GameObject.Find ("Click Audio").GetComponent<AudioSource>().Play();
        switch (gameObject.name)
        {
            case "Play":
                SceneManager.LoadScene("game");
                break;
            case "Rate":
                Application.OpenURL("http://google.com");
                break;
            case "Sound":
                if (PlayerPrefs.GetString("Sound") != "Off")
                {
                    PlayerPrefs.SetString("Sound", "Off");
                    s_on.SetActive(false);
                    s_off.SetActive(true);
                }
                else
                {
                    PlayerPrefs.SetString("Sound", "On");
                    s_on.SetActive(true);
                    s_off.SetActive(false);
                }
                break;
        }
    }
}
