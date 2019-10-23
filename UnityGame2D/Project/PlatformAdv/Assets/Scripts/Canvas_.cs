using UnityEngine;

public class Canvas_ : MonoBehaviour {

    public GameObject canvas, bttn1, bttn2, bttn3;
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            canvas.SetActive(!canvas.activeSelf);
        }
    }
}
