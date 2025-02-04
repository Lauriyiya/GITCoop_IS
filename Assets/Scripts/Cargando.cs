using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Cargando : MonoBehaviour
{
    public Text texto;

    private void Start()
    {
        string nivelACargar = CambioNivel.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(string nivel)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        //no activa la siguiente escena hasta que no le des a un boton
        operacion.allowSceneActivation = false;

        while (!operacion.isDone)
        {
            if (operacion.progress >= 0.9f)
            {
                texto.text = "Presiona cualquier tecla para continuar";
                if (Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }

            //se reprograma la corrutina
            yield return null;
        }
    }
}
