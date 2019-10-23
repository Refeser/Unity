using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

    public Collider2D c1, c2, c3, c4, c5, c6, c7, c8, sdcol;
    public Sprite button_off, button_on, sd_mon, sd_no_mon;
    public Text Money, Status, Openkr, SdachaText, Gotovitsa;
    public GameObject Tea, Latte, Espresso, Americano, Kapuchino, Tripplo, Macknato, Krishka, Coffe, Sdacha, SdachaPan, mani_in_da_sdacha, no_money_sdacha;
    int procent = 0;
    int money = 0;
    int densd = 0;
    //public bool open = false;
    public Vector3 target, target_back, trg, pos;

    private void Start()
    {
        c1.GetComponent<Collider2D>();
        c2.GetComponent<Collider2D>();
        c3.GetComponent<Collider2D>();
        c4.GetComponent<Collider2D>();
        c5.GetComponent<Collider2D>();
        c6.GetComponent<Collider2D>();
        c7.GetComponent<Collider2D>();
        c8.GetComponent<Collider2D>();
        sdcol.GetComponent<Collider2D>();
    }

    private void Update()
    {
        money = int.Parse(Money.text);
    }

    IEnumerator SdachaBttn()
    {
        GetComponent<SpriteRenderer>().sprite = button_on;
        yield return new WaitForSeconds(0.5f);
        GetComponent<SpriteRenderer>().sprite = button_off;
    }

    IEnumerator MakeADrink()
    {
        AudioController.PlaySound("pressBttn");
        c1.enabled = false; c2.enabled = false; c3.enabled = false; c4.enabled = false;
        c5.enabled = false; c6.enabled = false; c7.enabled = false; c8.enabled = false;
        AudioController.PlaySound("gotovka");
        Gotovitsa.text = "Процесс приготовления:";
        while (procent != 100)
        {
            procent++;
            yield return new WaitForSeconds(0.1f);
            Status.text = procent.ToString() + "%";
        }
        yield return new WaitForSeconds(0.5f);
        //StartCoroutine(Krisha());
        //Coffe.SetActive(Coffe.activeSelf);
        Status.text = "Готово!";
        Gotovitsa.text = " ";
        AudioController.PlaySound("dzin");
        Coffe.SetActive(!Coffe.activeSelf);
        GetComponent<SpriteRenderer>().sprite = button_off;
        c8.enabled = true;
    }

    IEnumerator OpenKr()
    {
        if (transform.position.y < 51.3)
        {
            target.y = 0.6f;
            while (transform.position.y < 51.3)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 9.9f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if (transform.position.y > -21.4)
        {
            target.y = -0.6f;
            while (transform.position.y > -21.4)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 9.9f);
                yield return new WaitForSeconds(0.1f);
            }
        }
        yield return new WaitForSeconds(0.1f);
        //Openkr.text = "1";
    }

    IEnumerator CloseKrishka()
    {
        while (target_back.y > -21.4)
        {
            transform.position = Vector3.MoveTowards(transform.position, target_back, Time.deltaTime * 9.9f);
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        //Openkr.text = "0";
        c1.enabled = true; c2.enabled = true; c3.enabled = true; c4.enabled = true;
        c5.enabled = true; c6.enabled = true; c7.enabled = true; c8.enabled = true;
    }

    IEnumerator Krisha()
    {
        while(Coffe.transform.position != pos )
        {
            Coffe.transform.position = Vector3.MoveTowards(transform.position, trg, Time.deltaTime * 9.9f);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Pl(int count)
    {
        if(count<50)
            AudioController.PlaySound("vvodMoney");
        else
            AudioController.PlaySound("kupyuri");
        money += count;
        Money.text = money.ToString();
    }

    void OnMouseUpAsButton()
    {
        bool open = false;
        switch (gameObject.name)
        {
            case "Tea":
                if (money >= 15)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 15;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;

            case "Latte":
                if (money >= 30)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 30;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;

            case "Espresso":
                if (money >= 25)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 25;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;
            case "Americano":
                if (money >= 20)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 20;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;
            case "Kapuchino":
                if (money >= 25)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 25;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;
            case "Tripplo":
                if (money >= 35)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 35;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    Status.text = "У вас не достаточно денег";
                }

                break;
            case "Macknato":
                if (money >= 35 && open == false)
                {
                    GetComponent<SpriteRenderer>().sprite = button_on;
                    procent = 0;
                    money -= 35;
                    Money.text = money.ToString();
                    StartCoroutine(MakeADrink());
                }
                else
                {
                    if(open == false)
                        Status.text = "У вас не достаточно денег";
                    else
                        Status.text = "Закройте крышку!";
                }

                break;

            case "Krishka":
                //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10f);
                //open = true;
                //if (Krishka.transform.position.y == 46.08)
                /* (Openkr.text == "1")//(open == true)
                {
                    StartCoroutine(CloseKrishka());
                    //transform.position = Vector3.MoveTowards(transform.position, target_back, Time.deltaTime * 10f);
                    Openkr.text = "0";
                    //open = false;
                }
                //if (Krishka.transform.position.y == -21.4)
                if (Openkr.text == "0")
                {
                    StartCoroutine(OpenKr());
                    //transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 10f);
                    Openkr.text = "1";
                    //open = true;
                }*/
                //StartCoroutine(OpenKr());
                if (transform.position.y < 51.3)
                {
                    target.y = 51.3f;
                    while (transform.position.y != target.y) //< 51.3)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 9.9f);
                    }
                }
                else if (transform.position.y > -21.4)
                {
                    target.y = -21.4f;
                    while (transform.position.y != target.y) //> -21.4)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * 9.9f);
                    }
                }
                break;

            case "Coffe":
                //StartCoroutine(CloseKrishka());
                Coffe.SetActive(!Coffe.activeSelf);
                //transform.position = pos;
                Status.text = "Выберите товар или внесите наличные";
                c1.enabled = true; c2.enabled = true; c3.enabled = true; c4.enabled = true;
                c5.enabled = true; c6.enabled = true; c7.enabled = true; c8.enabled = true;

                break;

            case "Sdacha":
                AudioController.PlaySound("zvukSdachi");
                //mani_in_da_sdacha.SetActive(!mani_in_da_sdacha.activeSelf);
                //no_money_sdacha.SetActive(!no_money_sdacha.activeSelf);
                SdachaPan.GetComponent<SpriteRenderer>().sprite = sd_mon;
                densd = int.Parse(SdachaText.text);
                densd += int.Parse(Money.text);
                SdachaText.text = densd.ToString();
                Money.text = "0";

                break;

            case "SdachaPan":
                AudioController.PlaySound("zvukSdachi");
                //mani_in_da_sdacha.SetActive(!mani_in_da_sdacha.activeSelf);
                //no_money_sdacha.SetActive(!no_money_sdacha.activeSelf);
                GetComponent<SpriteRenderer>().sprite = sd_no_mon;
                SdachaText.text = "0";
                break;

            default:
                break;
        }
    }

}
