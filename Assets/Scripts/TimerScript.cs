using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public float startTime = 180f;
    private float displayedTime;
    public Text timer;
	private bool toStart;
    // Start is called before the first frame update
    void Start()
    {
        displayedTime = startTime;
		toStart=true;
    }

    // Update is called once per frame
    void Update()
    {
		if(toStart==true){
        displayedTime -= Time.deltaTime;
        timer.text = displayedTime.ToString("F2"); //Alves: Shows time rounded to the nearest two decimal places.
		}
    }
	public void toggleTimer(bool tog)
	{
		toStart=tog;
	}
	public void resetTimer(){
		displayedTime = startTime;
	}
	public void slowTime(){
		Time.timeScale=.1f;
		InvokeRepeating("resetTime", 5.0f, 0.0f);
	}
	public void resetTime(){
		Time.timeScale=1;
	}
}
