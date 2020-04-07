using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenManager : MonoBehaviour
{
    public PlayerPrefs prefs;
    string activeScreen;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Cursor[] cursors; 
    // Start is called before the first frame update
    void Start()
    {
        activeScreen = "Character Select";
        Cursor.activeScreen = activeScreen;

        cursors[2].selection_current = GameObject.Find("Stage 2").GetComponent<Button>();
        cursors[2].cursor_stage = GameObject.FindWithTag("Stage").GetComponent<Cursor>();

        cursors[0].inuse = true;
        cursors[1].inuse = true;
        cursors[2].inuse = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cursors[0].confirmed && cursors[1].confirmed && activeScreen == "Character Select")
        {
            cam.transform.position = new Vector3(100, 0, -10);
            activeScreen = "Stage Select";
            cursors[0].inuse = false;
            cursors[1].inuse = false;
            cursors[2].inuse = true;

            Cursor.activeScreen = activeScreen;
        }
        else if (!cursors[0].confirmed && !cursors[1].confirmed && activeScreen == "Stage Select") { 
            cam.transform.position = new Vector3(0, 0, -10);
            activeScreen = "Character Select";
            cursors[0].inuse = true;
            cursors[1].inuse = true;
            cursors[2].inuse = false;

            Cursor.activeScreen = activeScreen;
        }

        if (activeScreen == "Stage Select")
        {
            if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Backspace))
            {
                cursors[0].confirmed = false;
                cursors[0].cursor_highlighter.color = new Color(1, 0, 0);
                cursors[1].confirmed = false;
                cursors[1].cursor_highlighter.color = new Color(0, 0, 1);
            }

            if (cursors[2].confirmed) SceneManager.LoadScene("Movement", LoadSceneMode.Single);
        }
    }
}
