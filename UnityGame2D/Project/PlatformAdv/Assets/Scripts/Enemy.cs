using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                anim.SetInteger("dead", 1);
            }
    }

    IEnumerator Die()
    {
        anim.SetInteger("dead", 2);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
