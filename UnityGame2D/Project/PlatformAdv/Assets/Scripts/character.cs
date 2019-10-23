using UnityEngine;

public class character : MonoBehaviour {

    public float speed = 8f;
    public float jump_impulse = 14f;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform cirTarget;
    public float radCir = 0.3f;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (Input.GetButtonDown("Jump"))
                Jump();

        if (Input.GetKey(KeyCode.S))
            anim.SetInteger("ch_anim", 2);
        else
        {
            if(Input.GetButtonDown("Jump") && isGround())
                anim.SetInteger("ch_anim", 2);
            else
            {
                anim.SetInteger("ch_anim", 1);
            }
        }
	}

    void FixedUpdate()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * 12f, rb.velocity.y);
        Flip();
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") > 0)
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (Input.GetAxis("Horizontal") < 0)
            transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    void Jump()
    {
        if(isGround())
            rb.AddForce(transform.up * jump_impulse, ForceMode2D.Impulse);
    }

    bool isGround()
    {
       Collider2D[] col = Physics2D.OverlapCircleAll(cirTarget.position, radCir);
        int j = 0;
        for(int i=0; i<col.Length; i++)
            if (col[i].gameObject != gameObject)
                j++;
        if (j > 0)
            return true;
        else
            return false;
    }
}
