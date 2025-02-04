using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{
    public static string siguienteNivel;
    public static void NivelCarga(string nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene("0 DS");
    }
}
