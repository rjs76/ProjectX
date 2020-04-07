using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionData : MonoBehaviour
{
    [SerializeField]
    Text P1, P2;
    // Start is called before the first frame update
    void Start()
    {
        P1.text = PlayerPrefs.GetString("P1 Character");
        P2.text = PlayerPrefs.GetString("P2 Character");
    }
    /*private void OnDestroy()
    {
        PlayerPrefs.DeleteAll();
    }*/
}
