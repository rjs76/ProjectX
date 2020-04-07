using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public class Web : MonoBehaviour 
{
    public Text ResponseText;
	
	void Start(){
		
	}
	
	public void loginUser(string username,string password){
		string url = "https://web.njit.edu/~rp553/get_user.php";

        WWWForm formDate = new WWWForm ();
        formDate.AddField ("username", username);
        formDate.AddField ("password", password);

        UnityWebRequest www = UnityWebRequest.Post(url, formDate);

        StartCoroutine (request (www));
		
	}
	
	IEnumerator request (UnityWebRequest www) 
    {
		Debug.Log("request");
        yield return www.SendWebRequest();
		if(www.isNetworkError || www.isHttpError){
			Debug.Log(www.error);
		}
		else{
			Debug.Log(www.downloadHandler.text);
        ResponseText.text = www.downloadHandler.text;
		
		}
		if(ResponseText.text!="Error, invalid credentials"){
		Main.Instance.player = JsonUtility.FromJson<PlayerClass>(ResponseText.text);	
		}
		//set player object to response data
		
    }
}

