using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * Holds data and handles the process for changing sccenes between buttons. 
 * @author Riviere Seguie
 */

public class scenebutton : MonoBehaviour
{
    /** 
 * Will take you to the specified unity scene
 */
    public void Startmenu()
    {
        SceneManager.LoadScene("Startmenu");
    }
    /** 
    * Will take you to the specified unity scene
    */
    public void secondmenu()
    {
        SceneManager.LoadScene("secondmenu");
    }
    /** 
    * Will take you to the specified unity scene
    */
    public void thirdmenu()
    {
        SceneManager.LoadScene("thirdmenu");
    }
    /** 
    * Will take you to the specified unity scene
    */
    public void loginmenu()
    {
        SceneManager.LoadScene("loginmenu");
    }
    /** 
    * Will take you to the specified unity scene
    */
    public void statsmenu()
    {
        SceneManager.LoadScene("statsmenu");
    }
    /** 
    * Will quit the application or game when used
    */
    public void QuitNOW()
    {
        Application.Quit();
    }
    /** 
    * Will take you to the specified unity scene
    */
    public void Matchup()
    {
        SceneManager.LoadScene("Match");
    }
}
