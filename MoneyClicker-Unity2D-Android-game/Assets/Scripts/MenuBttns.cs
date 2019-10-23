using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBttns : MonoBehaviour {

    public Sprite layer_blue, layer_red;
    public GameObject Panel, Click, ClickPan, shop, Menu, MoneyText, EnergyText, coin_shop, adv, top;

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
        {
            GameObject.Find("Click Audio").GetComponent<AudioSource>().Play();
        }
        switch (gameObject.name)
        {
            case "Menu":
                SceneManager.LoadScene("main");
                break;
            case "shop":
                Panel.SetActive(!Panel.activeSelf);
                Click.SetActive(!Click.activeSelf);
                Menu.SetActive(!Menu.activeSelf);
                MoneyText.SetActive(!MoneyText.activeSelf);
                EnergyText.SetActive(!EnergyText.activeSelf);
                ClickPan.SetActive(!ClickPan.activeSelf);
                coin_shop.SetActive(!coin_shop.activeSelf);
                adv.SetActive(!adv.activeSelf);
                top.SetActive(!top.activeSelf);
                break;
            case "coin_shop":
                Panel.SetActive(!Panel.activeSelf);
                Click.SetActive(!Click.activeSelf);
                Menu.SetActive(!Menu.activeSelf);
                MoneyText.SetActive(!MoneyText.activeSelf);
                EnergyText.SetActive(!EnergyText.activeSelf);
                shop.SetActive(!shop.activeSelf);
                adv.SetActive(!adv.activeSelf);
                top.SetActive(!top.activeSelf);
                ClickPan.SetActive(!ClickPan.activeSelf);
                break;
            case "adv":
                Panel.SetActive(!Panel.activeSelf);
                Click.SetActive(!Click.activeSelf);
                Menu.SetActive(!Menu.activeSelf);
                MoneyText.SetActive(!MoneyText.activeSelf);
                EnergyText.SetActive(!EnergyText.activeSelf);
                coin_shop.SetActive(!coin_shop.activeSelf);
                shop.SetActive(!shop.activeSelf);
                top.SetActive(!top.activeSelf);
                ClickPan.SetActive(!ClickPan.activeSelf);
                break;
            case "top":
                Panel.SetActive(!Panel.activeSelf);
                Click.SetActive(!Click.activeSelf);
                Menu.SetActive(!Menu.activeSelf);
                MoneyText.SetActive(!MoneyText.activeSelf);
                EnergyText.SetActive(!EnergyText.activeSelf);
                coin_shop.SetActive(!coin_shop.activeSelf);
                shop.SetActive(!shop.activeSelf);
                adv.SetActive(!adv.activeSelf);
                ClickPan.SetActive(!ClickPan.activeSelf);
                break;
        }
    }
}
