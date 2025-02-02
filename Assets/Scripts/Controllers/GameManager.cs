using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Nivel1");
    }
    public void FinalizeActions()
    {
        Debug.Log("Finalizando todas las acciones.");
        // Aquí va el código para manejar lo que ocurra al final de la escena
    }

    public void MenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void Exit()
    {
        Debug.Log("Saliste");
        Application.Quit();
    }

    public void IntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}

