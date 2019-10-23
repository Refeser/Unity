using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;

public class Game : MonoBehaviour, IRewardedVideoAdListener
{
    [Header("Текст, отвечающий за отображение энергии")]
    public Text scoreText;
    [Header("Текст, отвечающий за отображение денег")]
    public Text moneyText;
    [Header("Магазин")]
    public List<Item> shopItems = new List<Item>();
    [Header("Текст на кнопках товаров")]
    public Text[] shopItemsText;
    [Header("Кнопки товаров")]
    public Button[] shopBttns;

    private long score = 0; //энергия
    private long cash = 0; //бабосики
    private int scoreIncrease = 1; //Бонус при клике
    private long sokr, sokrd, so, sok, s, kofff, qwerty;
    private long qwe; // для перевода koef в лонг
    private string qwes; // для вывоа сокращения qwe
    private double koef = 0.5; // Коэф умножения
    private Save sv = new Save();
    private const string APP_KEY = "9f2b4d6ef7e147a4becc1555db2a5ad5a505f019168593f4";

    //------------------------------------------------------------------------------------------------------------------

    private void Awake()
    {
        if (PlayerPrefs.HasKey("SV"))
        {
            long totalBonusPS = 0;
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("SV"));
            score = sv.score;
            cash = sv.cash;
            scoreIncrease = sv.scoreIncrease;
            for (int i = 0; i < shopItems.Count; i++)
            {
                shopItems[i].levelOfItem = sv.levelOfItem[i];
                shopItems[i].bonusCounter = sv.bonusCounter[i];
                shopItems[i].obmenCount = sv.obmenCount[i];
                shopItems[i].cost = sv.costs[i];
                totalBonusPS += shopItems[i].bonusPerSec * shopItems[i].bonusCounter;
            }
            DateTime dt = new DateTime(sv.date[0], sv.date[1], sv.date[2], sv.date[3], sv.date[4], sv.date[5]);
            TimeSpan ts = DateTime.Now - dt;
            long offlineBonus = (long)ts.TotalSeconds * totalBonusPS;
            score += offlineBonus;
            print("Вы отсутствовали: " + ts.Days + "Д. " + ts.Hours + "Ч. " + ts.Minutes + "М. " + ts.Seconds + "S.");
            print("Ваши рабочие заработали: " + offlineBonus + "$");
            updateCosts();
        }
    }

    //------------------------------------------------------------------------------------------------------------------

    private void Start()
    {
        updateCosts(); //Обновить текст с ценами
        StartCoroutine(BonusPerSec()); //Запустить просчёт бонуса в секунду
        InitAds();
        Appodeal.show(Appodeal.BANNER_BOTTOM);
    }

    //------------------------------------------------------------------------------------------------------------------

    private void InitAds()
    {
        Appodeal.disableLocationPermissionCheck();
        Appodeal.initialize(APP_KEY, Appodeal.BANNER | Appodeal.REWARDED_VIDEO);
        Appodeal.setRewardedVideoCallbacks(this);
    }

    //------------------------------------------------------------------------------------------------------------------

    private void Update()
    {
        if (score < 1000) scoreText.text = score + " "; //Отображаем энергию
        else if ((score >= 1000) && (score < 1000000)) { sokr = score / 1000; scoreText.text = sokr + "K"; }
        else if ((score >= 1000000) && (score < 1000000000)) { sokr = score / 1000000; scoreText.text = sokr + "M"; }
        else if ((score >= 1000000000) && (score < 1000000000000)) { sokr = score / 1000000000; scoreText.text = sokr + "B"; }
        else if ((score >= 1000000000000) && (score < 1000000000000000)) { sokr = score / 1000000000000; scoreText.text = sokr + "T"; }
        else if ((score >= 1000000000000000) && (score < 1000000000000000000)) { sokr = score / 1000000000000000; scoreText.text = sokr + "Qd"; }
        else if ((score >= 1000000000000000000) && (score < 9223372036854775807)) { sokr = score / 1000000000000000000; scoreText.text = sokr + "Qn"; }

        if (cash < 1000) moneyText.text = cash + " $"; //Отображаем деньги
        else if ((cash >= 1000) && (cash < 1000000)) { sokrd = cash / 1000; moneyText.text = sokrd + "K $"; }
        else if ((cash >= 1000000) && (cash < 1000000000)) { sokrd = cash / 1000000; moneyText.text = sokrd + "M $"; }
        else if ((cash >= 1000000000) && (cash < 1000000000000)) { sokrd = cash / 1000000000; moneyText.text = sokrd + "B $"; }
        else if ((cash >= 1000000000000) && (cash < 1000000000000000)) { sokrd = cash / 1000000000000; moneyText.text = sokrd + "T $"; }
        else if ((cash >= 1000000000000000) && (cash < 1000000000000000000)) { sokrd = cash / 1000000000000000; moneyText.text = sokrd + "Qd $"; }
        else if ((cash >= 1000000000000000000) && (cash < 9223372036854775807)) { sokrd = cash / 1000000000000000000; moneyText.text = sokrd + "Qn $"; }
    }

    //------------------------------------------------------------------------------------------------------------------

    public void BuyBttn(int i) //Метод при нажатии на кнопку покупки товара (индекс товара)
    {
        if (shopItems[i].reward && cash >= 0)
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
            Appodeal.getRewardParameters("Rewards");
            score += (shopItems[i].bonusCounter * shopItems[i].bonusPerSec * 120) + scoreIncrease * 100;

        }
        else
        if (shopItems[i].upCash && cash >= shopItems[i].cost) 
        {
            cash -= shopItems[i].cost;
            koef *= 2;
            if (koef == 1)
                shopItems[i].cost = 25000;
            else
                if (koef == 2)
                    shopItems[i].cost = 250000;
            else
                if (koef > 2)
                    shopItems[i].cost = shopItems[i].cost * 2;
        }
        else
        if (shopItems[i].itsObmen && score >= shopItems[i].obmenCount) //Если товар нажатой кнопки - обмен валют и денег >= цены(е)
        {
                score -= shopItems[i].obmenCount; // - энергия
                kofff = (long)(shopItems[i].obmenCount * koef); // расчитываем сколько прибавить
                cash += kofff; // + бабосик
        }
        else
        if (cash >= shopItems[i].cost) // Иначе, если товар нажатой кнопки - это не то, что выше, и денег >= цена
        {
            if (shopItems[i].itsItemPerSec)
            {
                cash -= shopItems[i].cost;
                double fif = shopItems[i].cost / 3;
                shopItems[i].bonusCounter++; // Если нанимаем рабочего (к примеру), то прибавляем кол-во рабочих
                shopItems[i].cost += (long)fif;
            }
            else
            {
                cash -= shopItems[i].cost;
                if (scoreIncrease <= 4)
                    scoreIncrease += 1; // Иначе бонусу при клике добавляем бонус товара
                else
                    scoreIncrease *= 2;
                shopItems[i].cost *= 2;
            }
            shopItems[i].levelOfItem++; // Поднимаем уровень предмета на 1
        }
        else print("Не хватает денег!"); // Иначе если 2 проверки равны false, то выводим в консоль текст.
        updateCosts(); //Обновить текст с ценами
    }

    //------------------------------------------------------------------------------------------------------------------

    private void updateCosts() // Метод для обновления текста с ценами
    {
        for (int i = 0; i < shopItems.Count; i++) // Цикл выполняется, пока переменная i < кол-ва товаров
        {
            if (shopItems[i].reward)
            {
                qwerty = (shopItems[i].bonusCounter * shopItems[i].bonusPerSec * 120) + scoreIncrease * 100;
                if (qwerty < 10000) shopItemsText[i].text = "Watch ad - " + qwerty + " energy";
                else if (qwerty >= 10000 && qwerty < 1000000) shopItemsText[i].text = "Watch ad - " + qwerty / 1000 + "K energy";
                else if (qwerty >= 1000000 && qwerty < 1000000000) shopItemsText[i].text = "Watch ad - " + qwerty / 1000000 + "M energy";
                else if (qwerty >= 1000000000 && qwerty < 1000000000000) shopItemsText[i].text = "Watch ad - " + qwerty / 1000000000 + "B energy";
                else if (qwerty >= 1000000000000 && qwerty < 1000000000000000) shopItemsText[i].text = "Watch ad - " + qwerty / 1000000000000 + "T energy";
                else if (qwerty >= 1000000000000000 && qwerty < 1000000000000000000) shopItemsText[i].text = "Watch ad - " + qwerty / 1000000000000000 + "Qd energy";
                else if (qwerty >= 1000000000000000000 && qwerty < 9223372036854775807) shopItemsText[i].text = "Watch ad - " + qwerty / 1000000000000000000 + "Qn energy";
            }
            else
            if (shopItems[i].upCash)
                shopItemsText[i].text = "More expensive energy (x2) - " + shopItems[i].cost + " $";
            else
            if (shopItems[i].itsObmen)
            {
                qwe = (long)(shopItems[i].obmenCount * koef);
                if (qwe < 1000) qwes = qwe + " ";
                else if ((qwe >= 1000) && (qwe < 1000000)) qwes = qwe / 1000 + "K";
                else if ((qwe >= 1000000) && (qwe < 1000000000)) qwes = qwe / 1000000 + "M";
                else if ((qwe >= 1000000000) && (qwe < 1000000000000)) qwes = qwe / 1000000000 + "B";
                else if ((qwe >= 1000000000000) && (qwe < 1000000000000000)) qwes = qwe / 1000000000000 + "T";
                else if ((qwe >= 1000000000000000) && (qwe < 1000000000000000000)) qwes = qwe / 1000000000000000 + "Qd";
                else if ((qwe >= 1000000000000000000) && (qwe < 9223372036854775807)) qwes = qwe / 1000000000000000000 + "Qn";

                if (shopItems[i].obmenCount < 1000) shopItemsText[i].text = "Exchange " + shopItems[i].obmenCount + " energy for " + qwes + "$";
                else if ((shopItems[i].obmenCount >= 1000) && (shopItems[i].obmenCount < 1000000)) { s = shopItems[i].obmenCount / 1000; shopItemsText[i].text = "Exchange " + s + "K energy for " + qwes + " $"; }
                else if ((shopItems[i].obmenCount >= 1000000) && (shopItems[i].obmenCount < 1000000000)) { s = shopItems[i].obmenCount / 1000000; shopItemsText[i].text = "Exchange " + s + "M energy for " + qwes + " $"; }
                else if ((shopItems[i].obmenCount >= 1000000000) && (shopItems[i].obmenCount < 1000000000000)) { s = shopItems[i].obmenCount / 1000000000; shopItemsText[i].text = "Exchange " + s + "B energy for " + qwes + " $"; }
                else if ((shopItems[i].obmenCount >= 1000000000000) && (shopItems[i].obmenCount < 1000000000000000)) { s = shopItems[i].obmenCount / 1000000000000; shopItemsText[i].text = "Exchange " + s + "T energy for " + qwes + " $"; }
                else if ((shopItems[i].obmenCount >= 1000000000000000) && (shopItems[i].obmenCount < 1000000000000000000)) { s = shopItems[i].obmenCount / 1000000000000000; shopItemsText[i].text = "Exchange " + s + "Qd energy for " + qwes + " $"; }
                else if ((shopItems[i].obmenCount >= 1000000000000000000) && (shopItems[i].obmenCount < 9223372036854775807)) { s = shopItems[i].obmenCount / 1000000000000000000; shopItemsText[i].text = "Exchange " + s + "Qn energy for " + qwes + " $"; }
            }
                else
                        {
                         if (shopItems[i].cost < 1000) shopItemsText[i].text = shopItems[i].name + "\n" + shopItems[i].cost + "$";
                         else if ((shopItems[i].cost >= 1000) && (shopItems[i].cost < 1000000)) { sok = shopItems[i].cost / 1000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "K $"; }
                         else if ((shopItems[i].cost >= 1000000) && (shopItems[i].cost < 1000000000)) { sok = shopItems[i].cost / 1000000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "M $"; }
                         else if ((shopItems[i].cost >= 1000000000) && (shopItems[i].cost < 1000000000000)) { sok = shopItems[i].cost / 1000000000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "B $"; }
                         else if ((shopItems[i].cost >= 1000000000000) && (shopItems[i].cost < 1000000000000000)) { sok = shopItems[i].cost / 1000000000000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "T $"; }
                         else if ((shopItems[i].cost >= 1000000000000000) && (shopItems[i].cost < 1000000000000000000)) { sok = shopItems[i].cost / 1000000000000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "Qd $"; }
                         else if ((shopItems[i].cost >= 1000000000000000000) && (shopItems[i].cost < 9223372036854775807)) { sok = shopItems[i].cost / 1000000000000; shopItemsText[i].text = shopItems[i].name + "\n" + sok + "Qn $"; }
                        }
        }
    }

    //------------------------------------------------------------------------------------------------------------------

    IEnumerator BonusPerSec() // Бонус в секунду
    {
        while (true) // Бесконечный цикл
        {
            if (score < 9223372036854775807)
            for (int i = 0; i < shopItems.Count; i++)
                score += (shopItems[i].bonusCounter * shopItems[i].bonusPerSec); // Добавляем к игровой валюте бонус рабочих (к примеру)
            yield return new WaitForSeconds(1); // Делаем задержку в 1 секунду
        }
    }

    //------------------------------------------------------------------------------------------------------------------

#if UNITY_ANDROID && !UNITY_EDITOR

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            sv.score = score;
            sv.cash = cash;
            sv.scoreIncrease = scoreIncrease;
            sv.koef = koef;
            sv.levelOfItem = new int[shopItems.Count];
            sv.bonusCounter = new int[shopItems.Count];
            sv.obmenCount = new long[shopItems.Count];
            for (int i = 0; i < shopItems.Count; i++)
            {
                sv.levelOfItem[i] = shopItems[i].levelOfItem;
                sv.bonusCounter[i] = shopItems[i].bonusCounter;
                sv.obmenCount[i] = shopItems[i].obmenCount;
            }
            sv.date[0] = DateTime.Now.Year;
            sv.date[1] = DateTime.Now.Month;
            sv.date[2] = DateTime.Now.Day;
            sv.date[3] = DateTime.Now.Hour;
            sv.date[4] = DateTime.Now.Minute;
            sv.date[5] = DateTime.Now.Second;
            PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
        }
    }

#else

    private void OnApplicationQuit()
    {
        sv.score = score;
        sv.cash = cash;
        sv.scoreIncrease = scoreIncrease;
        sv.koef = koef;
        sv.levelOfItem = new int[shopItems.Count];
        sv.bonusCounter = new int[shopItems.Count];
        sv.obmenCount = new long[shopItems.Count];
        sv.costs = new long[shopItems.Count];
        for (int i = 0; i < shopItems.Count; i++)
        {
            sv.levelOfItem[i] = shopItems[i].levelOfItem;
            sv.bonusCounter[i] = shopItems[i].bonusCounter;
            sv.obmenCount[i] = shopItems[i].obmenCount;
            sv.costs[i] = shopItems[i].cost;
        }
        sv.date[0] = DateTime.Now.Year;
        sv.date[1] = DateTime.Now.Month;
        sv.date[2] = DateTime.Now.Day;
        sv.date[3] = DateTime.Now.Hour;
        sv.date[4] = DateTime.Now.Minute;
        sv.date[5] = DateTime.Now.Second;
        PlayerPrefs.SetString("SV", JsonUtility.ToJson(sv));
    }

#endif

    #region Rewarded Video callback handlers
    public void onRewardedVideoLoaded() { print("Video loaded"); }
    public void onRewardedVideoFailedToLoad() { print("Video failed"); }
    public void onRewardedVideoShown() { print("Video shown"); }
    public void onRewardedVideoClosed(bool finished) { print("Video closed"); }
    public void onRewardedVideoFinished(int amount, string name) { print("Reward: " + amount + " " + name); }
    #endregion

    //------------------------------------------------------------------------------------------------------------------
    public void OnClick()
    {
        if (score < 9223372036854775807)
            score += scoreIncrease; // К игровой валюте прибавляем бонус при клике
    }

    //------------------------------------------------------------------------------------------------------------------

}
[Serializable]
public class Item // Класс товара
{
    [Tooltip("Название используется на кнопках")]
    public string name;
    [Tooltip("Цена товара")]
    public long cost;
    [HideInInspector]
    public int levelOfItem; // Уровень товара
    [Space]
    [Tooltip("Этот товар даёт бонус в секунду?")]
    public bool itsItemPerSec;
    [Tooltip("Бонус, который даётся в секунду")]
    public long bonusPerSec;
    [HideInInspector]
    public int bonusCounter; // Кол-во рабочих (к примеру)
    [Space]
    [Tooltip("Это обмен?")]
    public bool itsObmen;
    [Tooltip("Кол-во?")]
    public long obmenCount;
    [Space]
    [Tooltip("это улучшение продаж?")]
    public bool upCash;
    [Space]
    [Tooltip("это реклама?")]
    public bool reward;
}

//------------------------------------------------------------------------------------------------------------------

[Serializable]
public class Save
{
    public long score;
    public long cash;
    public int scoreIncrease;
    public double koef;
    public long[] costs;
    public long[] obmenCount;
    public int[] levelOfItem;
    public int[] bonusCounter;
    public int[] date = new int[6];
}