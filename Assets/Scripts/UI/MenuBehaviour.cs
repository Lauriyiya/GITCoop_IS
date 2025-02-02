using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    
    public void ExitButton()
    {
        Application.Quit();
        Debug.Log("is working");
    }

    public void TitleButton()
    {
        SceneManager.LoadScene("0 Main Title");
    }

    public void MainCinematicButton()
    {
        SceneManager.LoadScene("1 Cinematic 2");
    }


    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }
}
