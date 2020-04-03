using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_controller : MonoBehaviour
{
	[SerializeField] private LayerMask platformLayerMask;
	private Animator anim;
	private Rigidbody2D myRigidBody;
	private BoxCollider2D boxCollider2d;
	private bool isAttacking,isBlocking,isDucking,jumpTrigger,isGrounded;
	float xDir;
	float horizontal;
	
    // Start is called before the first frame update
    void Start()
    {
		anim = GetComponent<Animator>();
        myRigidBody=transform.GetComponent<Rigidbody2D>();
		boxCollider2d=transform.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
			horizontal = Input.GetAxis("Horizontal");
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Kick")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Kick")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Standing_Punch")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Punch")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Punch")||!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Kick")){
				anim.SetBool("isAttacking",false);
				
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")||anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch")||anim.GetCurrentAnimatorStateInfo(0).IsName("Walk")){
				isAttacking=false;				
			}
			if(anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
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
			if(isAttacking==false){
			if(anim.GetBool("isJumping") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Punch")){
					anim.SetTrigger("punch");
					anim.SetBool("isAttacking",true);
					//isAttacking=true;
			}
			}
			else if(anim.GetBool("isDucking") == true){
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
		}
		if(Input.GetKey(KeyCode.S)){
			if(isAttacking==false){
			if(anim.GetBool("isJumping") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Jump_Kick")){
					anim.SetTrigger("kick");
					anim.SetBool("isAttacking",true);
					//isAttacking=true;
			}
			}
			else if(anim.GetBool("isDucking") == true){
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
		}
		
		if(Input.GetKey(KeyCode.D)){
			if(anim.GetBool("isDucking") == true){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Crouch_Block")){
					myRigidBody.velocity = Vector2.zero;
					anim.SetTrigger("block");
					anim.SetBool("isBlocking",true);
					isBlocking=true;
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
			if(isAttacking==false&&isBlocking==false&&isDucking==false){
				anim.SetTrigger("jump");
				anim.SetBool("isJumping",true);
			}
		}
		if(GroundCheck()&&anim.GetBool("isJumping")==true){
			anim.SetBool("isJumping",false);
		}
		
		
       Debug.Log(myRigidBody.velocity);
    }
	
	void FixedUpdate(){
		 Movement(horizontal);	
		 Jump();
	}
	
	private void Movement(float horizontal)
	{

		if(isAttacking == false&&isBlocking==false&&isDucking==false){
			myRigidBody.velocity= new Vector2(horizontal*20,myRigidBody.velocity.y);
		}
	}
	
	private void Jump(){
		float jumpVelocity=100f;
		if(Input.GetKey(KeyCode.Space)){
			if(GroundCheck()&&isAttacking == false&&isBlocking==false&&isDucking==false){
				myRigidBody.velocity+=Vector2.up*jumpVelocity;
			}
		}
	}
	
	private bool GroundCheck(){
		RaycastHit2D raycastHit2d=Physics2D.BoxCast(boxCollider2d.bounds.center,boxCollider2d.bounds.size,0f,Vector2.down,.1f,platformLayerMask);
		//Debug.Log(raycastHit2d.collider);
		return raycastHit2d.collider !=null;
	}
}
