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
    public Animator anim;

    [SerializeField]
    Image cursor_highlighter;

    [SerializeField]
    private Grid grid_highlighted;

    [SerializeField]
    float ddd;

    [SerializeField]
    Selectable selection_current;
    [SerializeField]
    Selectable selection_next;

    public bool confirmed;
    Cursor P1, P2, cursor_stage;
    GameObject stage, characterSelect;

    private string player_tag;

    

    // Start is called before the first frame update
    void Start()
    {
        //grid_highlighted = grid.gameObject;
        confirmed = false;
        selection_current = grid_highlighted.GetComponent<Button>();
        player_tag = this.tag;
        P1 = GameObject.FindWithTag("P1").GetComponent<Cursor>();
        P2 = GameObject.FindWithTag("P2").GetComponent<Cursor>();
        cursor_stage = GameObject.FindWithTag("Stage").GetComponent<Cursor>();
        P1.anim.SetInteger("Character", 1);
        P2.anim.SetInteger("Character", 2);
        characterSelect = GameObject.Find("Character Select");
        stage = GameObject.Find("Stage Select");
        stage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.tag == "P1")
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                P1.confirmed = true;
                P1.cursor_highlighter.color = new Color(0.66f, 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                P1.confirmed = false;
                P1.cursor_highlighter.color = new Color(1, 0, 0);
            }
        }

        if (this.tag == "P2")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                P2.confirmed = true;
                P2.cursor_highlighter.color = new Color(0, 0, 0.66f);
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                P2.confirmed = false;
                P2.cursor_highlighter.color = new Color(0, 0, 1);
            }
        }

        if (this.tag != "Stage")
        {
            if (P1.confirmed && P2.confirmed)
            {
                stage.SetActive(true);
                characterSelect.SetActive(false);
                selection_current = GameObject.Find("Stage 2").GetComponent<Button>();
                cursor_stage = GameObject.FindWithTag("Stage").GetComponent<Cursor>();
            }
        }

        if (this.tag == "Stage")
        {
                if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.Backspace))
                {
                    characterSelect.SetActive(true);
                    stage.SetActive(false);
                    P1.confirmed = false;
                    P1.cursor_highlighter.color = new Color(1, 0, 0);
                    P2.confirmed = false;
                    P2.cursor_highlighter.color = new Color(0, 0, 1);
                }
        }

        if (this.tag == "Stage"){
            cursor_move_stage();
        }
        else { 
            cursor_move();
            cursor_update();
        }
    }

    void cursor_update()
    {
        if (selection_current != selection_next)
        {
            cursor_name.text = grid_highlighted.grid_name;
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

            if (grid_highlighted.grid_name == "Random") anim.SetInteger("Character", 0);
            else if (grid_highlighted.grid_name == "unknown") anim.SetInteger("Character", 0);
            else if (grid_highlighted.grid_name == "Generic") anim.SetInteger("Character", 1);
            else if (grid_highlighted.grid_name == "Chris") anim.SetInteger("Character", 2);
            selection_next = null;
            this.anim.StopPlayback();
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
                selection_next = selection_current.FindSelectableOnRight();
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                selection_next = selection_current.FindSelectableOnDown();
            }

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                selection_next = selection_current.FindSelectableOnUp();
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
