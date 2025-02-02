using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float delayDeTiempo = 1f;  // Tiempo de espera antes de cargar la escena

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que colisiona tiene el tag "win"
        if (collision.CompareTag("Win"))
        {
            Debug.Log("Ganaste");
            StartCoroutine(CargarEscenaWinConRetraso());
        }
    }

    IEnumerator CargarEscenaWinConRetraso()
    {
        yield return new WaitForSecondsRealtime(delayDeTiempo);  // Espera 3 segundos en tiempo real
        Time.timeScale = 1f;  // Restaurar la escala de tiempo antes de cargar la escena
        SceneManager.LoadScene("1 Win");
    }
}
