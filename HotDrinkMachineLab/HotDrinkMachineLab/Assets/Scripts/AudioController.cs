using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public static AudioClip Bttn, Money, Kupyura, Sdachazvuk, Gotovka, Dzin;
    static AudioSource audioSrc;

	// Use this for initialization
	void Start () {
        Bttn = Resources.Load<AudioClip>("pressBttn");
        Money = Resources.Load<AudioClip>("vvodMoney");
        Kupyura = Resources.Load<AudioClip>("kupyuri");
        Sdachazvuk = Resources.Load<AudioClip>("zvukSdachi");
        Gotovka = Resources.Load<AudioClip>("gotovka");
        Dzin = Resources.Load<AudioClip>("dzin");

        audioSrc = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void PlaySound (string clip)
    {
        switch (clip)
        {
            case "gotovka":
                audioSrc.PlayOneShot(Gotovka);
                break;
            case "zvukSdachi":
                audioSrc.PlayOneShot(Sdachazvuk);
                break;
            case "kupyuri":
                audioSrc.PlayOneShot(Kupyura);
                break;
            case "vvodMoney":
                audioSrc.PlayOneShot(Money);
                break;
            case "pressBttn":
                audioSrc.PlayOneShot(Bttn);
                break;
            case "dzin":
                audioSrc.PlayOneShot(Dzin);
                break;
        }
    }
}
