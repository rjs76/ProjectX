using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour {
	public static Main Instance;
	
	public Web web;
	
	public PlayerClass player;
	
	void Start(){
		Instance=this;
		web = GetComponent<Web>();
		player= new PlayerClass();
	}
}