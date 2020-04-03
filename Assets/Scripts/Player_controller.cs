using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
	private Animator anim;
	private Rigidbody2D myRigidBody;
	private bool isAttacking,isBlocking,isDucking;
	float xDir;
	float horizontal;
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        myRigidBody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
			horizontal = Input.GetAxis("Horizontal");
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Kick")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Kick")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Punch")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Punch")){
				anim.SetBool("isAttacking",false);
				
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
				isAttacking=false;
				isDucking=false;
			}
		if(Input.GetKey(KeyCode.LeftArrow)||Input.GetKey(KeyCode.RightArrow)){
			if(isAttacking==false){
			anim.SetBool("isRunning",true);
			}
		}
		else{
			anim.SetBool("isRunning",false);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			myRigidBody.velocity = Vector2.zero;
			anim.SetTrigger("duck");
			anim.SetBool("isDucking",true);
			isDucking=true;
		}
		else{
			anim.SetBool("isDucking",false);
		}
		if(Input.GetKey(KeyCode.A)){
			if(anim.GetBool("isJumping") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Punch")){
					anim.SetTrigger("punch");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}
			if(anim.GetBool("isDucking") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Punch")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("punch");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}
			else{
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Punch")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("punch");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}
		}
		if(Input.GetKey(KeyCode.S)){
			if(anim.GetBool("isJumping") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Kick")){
					anim.SetTrigger("kick");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}
			if(anim.GetBool("isDucking") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Kick")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("kick");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}
			else{
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Kick")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("kick");
					anim.SetBool("isAttacking",true);
					isAttacking=true;
			}
			}	
		}
		
		
		if(Input.GetKey(KeyCode.D)){
			if(anim.GetBool("isDucking") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Block")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("block");
					anim.SetBool("isBlocking",true);
					isAttacking=true;
			}
			}
			else{
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Block")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("block");
					anim.SetBool("isBlocking",true);
					isBlocking=true;
			}
			}	
		}
		else
		{
			anim.SetBool("isBlocking",false);
			isBlocking=false;
		}
		
		if(Input.GetKey(KeyCode.Space)){
			anim.SetTrigger("jump");
		}
       
    }
	
	void FixedUpdate(){
		 Movement(horizontal);	
	}
	
	private void Movement(float horizontal)
	{
		if(isAttacking == false&&isBlocking==false&&isDucking==false){
			myRigidBody.velocity= new Vector2(horizontal*3,0.0f);
		}
	}
}
