using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeterScript : MonoBehaviour
{
    public Image meterImage; //The image of the changing bar itself.
    private Energy en; /*Alves: Creates an instance of the Energy class,
    which has the functions for energy management. */
    public string superKey; //Alves: The key the player needs to press to use the super.
    public string type; // type of meter, hp for health, mp for energy guage

    void Awake()
    {
        //meterImage = transform.Find("SuperMeter").GetComponent<Image>();

        en = new Energy(type);
    }

    private void Update()
    {
		en.type=type;
        en.Update();
        meterImage.fillAmount = en.ReturnEnergy();
        if (en.ReturnEnergy() >= 1f) //Alves: When the meter is full, it turns green.
        {
			if(type=="mp"){
				meterImage.color = Color.green;
			}
			else{
				meterImage.color = Color.yellow;
			}
            //if (Input.GetKeyDown(superKey)) en.EmptyEnergy(); //Alves: The "h" key fires the super.
        }

        else
        {
            meterImage.color = new Vector4(1f, 0f, en.ReturnEnergy(), 1f);
        }

        
    }
	public void ChangeEnergy(float amount){
		en.ChangeEnergy(amount);
	}
	public void SetEnergy(float amount){
		en.SetEnergy(amount);
	}
}

public class Energy {
    private float limit = 100f; /*Alves: This is how high
    the bar's limit is.*/

    private float energy; //Alves: How much health/energy is in the bar.
    private float regen; //Alves: Determines how fast the bar fills.
	public string type;
	
    public Energy() //Alves: Constructor function
    {
        energy = 0f;
        regen = 10f;
    }
	public Energy(string tp) //Alves: Constructor function
    {
        if(tp=="hp")
			energy = 100f;
		else
			energy=0f;
		
        regen = 10f;
		type=tp;
    }

    public void Update() //Alves: Fills up meter over time.
    {
		if(type=="mp")
			energy += regen * Time.deltaTime;
		
    }

    public void Blast() //Alves: Empties the bar if it's full.
    {
        if (energy >= limit) { energy = 0f; }
    }

    public float ReturnEnergy()
    {
        return energy / limit;
    }

    public void EmptyEnergy()
    {
        energy = 0f;
    }
	
	public void ChangeEnergy(float amount)
	{
		energy+=amount;
	}
	public void SetEnergy(float amount)
	{
		energy=amount;
	}
}
