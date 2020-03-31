using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour {
	
	public InputField UsernameInput;
	public InputField PasswordInput;
	public Button LoginButton;
	void Start(){
		LoginButton.onClick.AddListener(()=>{
			Main.Instance.web.loginUser(UsernameInput.text,PasswordInput.text);
		});
		
	}
}