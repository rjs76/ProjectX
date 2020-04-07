using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class Cursor : MonoBehaviour
{
    [SerializeField]
    Text cursor_name;

    [SerializeField]
    AnimatorController cursor_sprite;

    [SerializeField]
    private Image preview_sprite;

    [SerializeField]
    public Image cursor_highlighter;

    [SerializeField]
    private Grid grid_highlighted;

    [SerializeField]
    float ddd;

    [SerializeField]
    public Selectable selection_current;
    [SerializeField]
    public Selectable selection_next;

    public bool confirmed;
    public Cursor P1, P2, cursor_stage;
    GameObject stage, characterSelect;

    private string player_tag;

    public bool inuse;

    public static string activeScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        confirmed = false;
        selection_current = grid_highlighted.GetComponent<Button>();
        player_tag = this.tag;
        P1 = GameObject.FindWithTag("P1").GetComponent<Cursor>();
        P2 = GameObject.FindWithTag("P2").GetComponent<Cursor>();
        cursor_stage = GameObject.FindWithTag("Stage").GetComponent<Cursor>();
        characterSelect = GameObject.Find("Character Select");
        stage = GameObject.Find("Stage Select");

    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "P1")
        {
            if (Input.GetKeyDown(KeyCode.J) && !P1.confirmed)
            {
                P1.confirmed = true;
                PlayerPrefs.SetString("P1 Character", this.cursor_name.text);
                P1.cursor_highlighter.color = new Color(0.66f, 0, 0);
            }
        }

        if (this.tag == "P2")
        {
            if (Input.GetKeyDown(KeyCode.Return) && !P2.confirmed)
            {
                P2.confirmed = true;
                PlayerPrefs.SetString("P2 Character", this.cursor_name.text);
                P2.cursor_highlighter.color = new Color(0, 0, 0.66f);
            }
        }

        if (this.tag == "Stage" && this.inuse)
        {
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Return))
            {
                cursor_stage.confirmed = true;
                PlayerPrefs.SetString("Stage", this.cursor_name.text);
                cursor_stage.cursor_highlighter.color = new Color(0.557f, 0, 0.557f);
            }
        }

        if (activeScreen == "Stage Select" && this.inuse){
            cursor_move_stage();
        }
        else if(activeScreen == "Character Select" && this.inuse){ 
            cursor_move();
            cursor_update();
        }
    }

    void cursor_update()
    {
        if (selection_current != selection_next)
        {
            cursor_name.text = grid_highlighted.grid_name;
            preview_sprite.sprite = grid_highlighted.grid_sprite;
        }
    }
    void cursor_move()
    {
        if (!confirmed && selection_next == null)
        {

            if (this.tag == "P1")
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    P1.selection_next = P1.selection_current.navigation.selectOnLeft;//P1.selection_current.FindSelectableOnLeft();

                }

                if (Input.GetKeyDown(KeyCode.D))
                {
                    P1.selection_next = P1.selection_current.navigation.selectOnRight;//P1.selection_current.FindSelectableOnRight();
                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    P1.selection_next = P1.selection_current.navigation.selectOnDown;//P1.selection_current.FindSelectableOnDown();
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    P1.selection_next = P1.selection_current.navigation.selectOnUp;//P1.selection_current.FindSelectableOnUp();
                    Debug.Log(P1.selection_next);//current.navigation.selectOnUp);
                }
            }

            if (this.tag == "P2")
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    P2.selection_next = P2.selection_current.FindSelectableOnLeft();
                }

                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    P2.selection_next = P2.selection_current.FindSelectableOnRight();
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    P2.selection_next = P2.selection_current.FindSelectableOnDown();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    P2.selection_next = P2.selection_current.FindSelectableOnUp();
                }
            }
            
        }

        if (selection_next != null && selection_current != selection_next)
        {
            selection_current = selection_next;
            grid_highlighted = selection_current.GetComponent<Grid>();
            cursor_highlighter.transform.position = grid_highlighted.transform.position;
            selection_next = null;
        }
    }

    void cursor_move_stage()
    {
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
            cursor_stage.selection_next = cursor_stage.selection_current.FindSelectableOnLeft();
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
            cursor_stage.selection_next = selection_current.FindSelectableOnRight();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
            cursor_stage.selection_next = selection_current.FindSelectableOnDown();
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
            cursor_stage.selection_next = selection_current.FindSelectableOnUp();
            }
            if (selection_next != null && selection_current != selection_next)
            {
                cursor_stage.selection_current = cursor_stage.selection_next;
                cursor_stage.grid_highlighted = cursor_stage.selection_current.GetComponent<Grid>();
                cursor_stage.cursor_highlighter.transform.position = cursor_stage.grid_highlighted.transform.position;
                cursor_stage.cursor_name.text = cursor_stage.grid_highlighted.grid_name;
            }
    }
}
