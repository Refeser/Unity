using UnityEngine;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject canvas;

    void Update () {

        if (Input.GetButtonDown("Fire1") && !canvas.activeSelf)
            Shoot();
	}

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
