using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Controller : MonoBehaviour
{
	public static Game_Controller gameController;
	public Player_controller player1;
	public Player2_controller player2;
	public TimerScript time;
	public GameObject p1win,p2win,tie,p1wctr,p2wctr;
	public bool matchOver;
	public bool roundOver;
	public string winner;
	public int roundNumber;
	public int p1WinCounter,p2WinCounter;
    // Start is called before the first frame update
    void Start()
    {
		gameController=this;
        matchOver=false;
		roundOver=false;
		roundNumber=1;
		p1WinCounter=0;
		p2WinCounter=0;
		p1win.SetActive(false);
		p2win.SetActive(false);
		tie.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		
		if(roundOver==false){
			if(player1.health<=0&&player2.health<=0){
				//tie game
				time.slowTime();
				tie.SetActive(true);
				Debug.Log("Tie game");
				roundOver=true;
				time.toggleTimer(false);
				roundNumber++;
				InvokeRepeating("ResetMatch", 5.0f, 0.0f);
			}
        else if(player1.health<=0){
			time.slowTime();
			player2.Victory();
			p2win.SetActive(true);
			Debug.Log("Player 2 wins");
			roundOver=true;
			time.toggleTimer(false);
			p2WinCounter++;
			p2wctr.GetComponent<Text>().text+="w";
			roundNumber++;
			if(p2WinCounter!=2)
				InvokeRepeating("ResetMatch", 5.0f, 0.0f);
		}
		else if(player2.health<=0){
			time.slowTime();
			player1.Victory();
			p1win.SetActive(true);
			Debug.Log("Player 1 wins");
			roundOver=true;
			time.toggleTimer(false);
			p1WinCounter++;
			p1wctr.GetComponent<Text>().text+="w";
			roundNumber++;
			if(p1WinCounter!=2)
				InvokeRepeating("ResetMatch", 5.0f, 0.0f);
		}
		if(p1WinCounter==2||p2WinCounter==2){
		if(matchOver==false){
				matchOver=true;
			//end match
				InvokeRepeating("BackToMenu", 6.0f, 0.0f);
			}
		}
		}
    }
	void ResetMatch(){
		p1win.SetActive(false);
		p2win.SetActive(false);
		tie.SetActive(false);
		roundOver=false;
		time.resetTimer();
		time.toggleTimer(true);
		player1.health=100;
		player1.healthbar.SetEnergy(100);
		player1.myRigidBody.position=player1.startPosition;
		player1.canMove=true;
		player1.defeated=false;
		player1.setAnimTrigger("idle");
		player2.health=100;
		player2.healthbar.SetEnergy(100);		
		player2.myRigidBody.position=player2.startPosition;
		player2.defeated=false;
		player2.canMove=true;		
		player2.setAnimTrigger("idle");
	}
	void BackToMenu(){
		SceneManager.LoadScene("thirdmenu");
	}
}
