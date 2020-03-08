using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class postMessage : MonoBehaviour 
{
    public Text ResponseText;
    // Use this for initialization
    void Start ()
    {
        string url = "website.php";

		//query data "ex.com/site.php?username=example+password=example
        WWWForm formDate = new WWWForm ();
        formDate.AddField ("username", "example");
        formDate.AddField ("password", "example");

        WWW www = new WWW (url, formDate);

        StartCoroutine (request (www));
    }
    
    // Update is called once per frame
    IEnumerator request (WWW www) 
    {
        yield return www;

        ResponseText.text = www.text;
    }
}