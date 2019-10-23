using UnityEngine;

public class Menu_bttns : MonoBehaviour {

    public Sprite layer_blue, layer_red;
    public GameObject canvas;

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().sprite = layer_red;
    }

    private void OnMouseUpAsButton()
    {
        switch (gameObject.name)
        {
            case "Play":
                {
                    GetComponent<SpriteRenderer>().sprite = layer_blue;
                    canvas.SetActive(!canvas.activeSelf);
                    break;
                }
            case "Settings":
                {
                    GetComponent<SpriteRenderer>().sprite = layer_blue;
                    canvas.SetActive(!canvas.activeSelf);
                    break;
                }
            case "Exit":
                {
                    GetComponent<SpriteRenderer>().sprite = layer_blue;
                    canvas.SetActive(!canvas.activeSelf);
                    Application.Quit();
                    break;
                }
        }
    }
}
