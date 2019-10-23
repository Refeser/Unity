using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetInteger("ch_anim", 2);
        }

        if(Input.GetButton("Crouch"))
        {
            crouch = true;
            anim.SetInteger("ch_anim", 2);
        } else //if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
            anim.SetInteger("ch_anim", 1);
        }
	}

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        anim.SetInteger("ch_anim", 1);
    }
}
