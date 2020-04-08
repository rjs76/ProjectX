using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_controller : MonoBehaviour
{
	[SerializeField] private LayerMask platformLayerMask;
	public static Player2_controller player2;
	public Rigidbody2D myRigidBody;
	//public Game_Controller gameController;
	private Animator anim;
	private BoxCollider2D boxCollider2d;
	private bool isAttacking,isBlocking,isDucking,jumpTrigger,isGrounded,fLeft,fRight;
	private bool touchingHitbox=false;
	public bool defeated;
	public bool canMove;
	public GameObject target;
	public float health;
	public MeterScript healthbar,energybar;
	public Vector2 startPosition;
	float horizontal;
	float knockbackTime;
	int knockbackDir;
	int flip;
    // Start is called before the first frame update
    void Start()
    {
		player2=this;
		anim = transform.GetComponent<Animator>();
        myRigidBody=transform.GetComponent<Rigidbody2D>();
		boxCollider2d=transform.GetComponent<BoxCollider2D>();
		//target = GameObject.Find("Player2");
		fRight=true;
		canMove=true;
		defeated=false;
		startPosition=myRigidBody.position;
    }

    // Update is called once per frame
    void Update()
    {
		if(defeated==true||canMove==false)
		{
			if(GroundCheck())
			{
			if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Victory"))
			{
				anim.SetBool("isAttacking",false);
				anim.SetBool("isRunning",false);
				anim.SetBool("isDucking",false);
				anim.SetBool("isBlocking",false);
				anim.SetBool("isJumping",false);
				anim.SetTrigger("victory");
				return;
			}
			myRigidBody.velocity = Vector2.zero;
			return;
			}		
		}
		
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Victory")&&Game_Controller.gameController.roundOver==false){
			anim.SetTrigger("idle");
		}
		
			horizontal = Input.GetAxis("P2Horizontal");
			
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
			if(GroundCheck()){
			myRigidBody.velocity = Vector2.zero;
			anim.SetTrigger("duck");
			anim.SetBool("isDucking",true);
			isDucking=true;
			}
		}
		else{
			anim.SetBool("isDucking",false);
		}
		if(Input.GetKey(KeyCode.J)){
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
		if(Input.GetKey(KeyCode.K)){
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
		
		if(Input.GetKey(KeyCode.M)){
			if(isGrounded==true){
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
		}
		else
		{
			anim.SetBool("isBlocking",false);
			isBlocking=false;
		}
		
		if(Input.GetKey(KeyCode.UpArrow)){
			if(isAttacking==false&&isBlocking==false&&isDucking==false){
				anim.SetTrigger("jump");
				anim.SetBool("isJumping",true);
			}
		}
		if(GroundCheck()){
			isGrounded=true;
			if(anim.GetBool("isJumping")==true){
				anim.SetBool("isJumping",false);
				anim.ResetTrigger("punch");
				anim.ResetTrigger("kick");
		}
		}
		if (transform.position.x > target.transform.position.x) {
			if(fLeft==false){
				fLeft=true;
				fRight=false;
			flip=-1;
			Vector3 temp=transform.localScale;
			temp.x*=flip;
			transform.localScale=temp;
		}
		}
		else {
			if(fRight==false){
				fRight=true;
				fLeft=false;
			flip=-1;
			Vector3 temp=transform.localScale;
			temp.x*=flip;
			transform.localScale=temp;
			}
		}
		
	 if(health<=0f){
		 if(defeated==false)
		 {
			defeated=true;
			anim.SetTrigger("knockdown");
			myRigidBody.velocity= new Vector2(knockbackDir*20,myRigidBody.velocity.y);
		 }
		 
	 }
    }
	
	void FixedUpdate(){
		if(defeated==false&&canMove==true){
		 Movement(horizontal);	
		 Jump();
		}
	}
	
	private void Movement(float horizontal)
	{
		if(knockbackTime<=0){
		if(isAttacking == false&&isBlocking==false&&isDucking==false&&defeated==false){
			myRigidBody.velocity= new Vector2(horizontal*20,myRigidBody.velocity.y);
		}
		}
		else{			
			myRigidBody.velocity= new Vector2(knockbackDir*20,myRigidBody.velocity.y);
			knockbackTime-=Time.deltaTime;
		}
	}
	
	private void Jump(){
		float jumpVelocity=100f;
		if(Input.GetKey(KeyCode.UpArrow)){
			if(GroundCheck()&&isAttacking == false&&isBlocking==false&&isDucking==false){
				myRigidBody.velocity+=Vector2.up*jumpVelocity;
			}
		}
	}
	
	public void Victory(){
		canMove=false;
		//play victory animation
		//stop movement
		
	}
	
	public void setAnimTrigger(string trigger){
		anim.SetTrigger(trigger);
	}
	
	private bool GroundCheck(){
		RaycastHit2D raycastHit2d=Physics2D.BoxCast(boxCollider2d.bounds.center,boxCollider2d.bounds.size,0f,Vector2.down,.1f,platformLayerMask);
		//Debug.Log(raycastHit2d.collider);
		return raycastHit2d.collider !=null;
	}
	
	 void OnTriggerEnter2D(Collider2D other) {
         if (other.CompareTag("HitboxP1")&&touchingHitbox==false&&defeated==false) {
             touchingHitbox = true;
			 Debug.Log("Hit");
			 FindObjectOfType<HitStop>().Stop(0.1f);
			 knockbackTime=.1f;
			 if(other.transform.position.x<myRigidBody.position.x)
				 knockbackDir=1;
			 else
				 knockbackDir=-1;
			 if(isBlocking==false){
			 anim.SetTrigger("hit");
			 anim.SetBool("isHit",true);
			 health-=45;
			 healthbar.ChangeEnergy(-45);
			 }
         }
     }
     
     void OnTriggerExit2D(Collider2D other) {
         if (other.CompareTag("HitboxP1")) {
             touchingHitbox = false;
         }
     }
}
