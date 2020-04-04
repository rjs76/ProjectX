using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit_controller : MonoBehaviour
{
	
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col)
	{
		if(col.CompareTag("Player2"))
		{
			//Debug.Log("Hit");
		}
		
	}
}
