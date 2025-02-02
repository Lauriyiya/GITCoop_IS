using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;  // Necesario para reiniciar el nivel
using System.Collections.Generic;

public class ControlTecla : MonoBehaviour
{
    public GameObject[] prefabs;
    public GameObject objetoSeleccionado;
    public Vector2 direccionEmpuje;
    public float fuerzaEmpuje;
    public GameObject panelMensaje; // Panel que contiene la imagen y el texto
    public TextMeshProUGUI textoMensaje; // El texto dentro del panel
    public string[] mensajes;
    public string[] mensajesError;
    public string textoExtra;
    public float tiempoMaximoParaPulsar = 5f;
    public float tiempoMostrarMensaje = 2f;
    public int vidas = 3;
    public GameObject[] Vidas;

    public Canvas gameOverCanvas;  // El Canvas que se muestra al perder
    public float delayDeTiempo = 3f;  // Tiempo de espera antes de cargar la escena

    private string teclaSeleccionada;
    private Rigidbody2D rbHijo;
    private Rigidbody2D rbSeleccionado;
    private float tiempoRestante;
    public AudioController audioController;
    public AudioClip[] keyPressSounds;
    public AudioClip backgroundMusic;

    private Coroutine mensajeCoroutine;

    void Start()
    {
        SeleccionarTecla();
        if (objetoSeleccionado != null)
        {
            rbSeleccionado = objetoSeleccionado.GetComponent<Rigidbody2D>();
        }
        textoMensaje.gameObject.SetActive(false);
        tiempoRestante = tiempoMaximoParaPulsar;

        if (audioController != null && backgroundMusic != null)
        {
            audioController.PlayAudio(backgroundMusic, null);
        }

        gameOverCanvas.gameObject.SetActive(false);  // Asegurarse de que el Canvas est  oculto al inicio

        panelMensaje.SetActive(false);
    }

    void Update()
    {
        tiempoRestante -= Time.deltaTime;
        if (tiempoRestante <= 0)
        {
            PerderVidaPorTiempo();
        }

        if (Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
        {
            ReiniciarTemporizador();

            if (Input.GetKeyDown(teclaSeleccionada))
            {
                AplicarEmpuje();
                MostrarMensaje(mensajes);
                SeleccionarTecla();

                if (audioController != null && keyPressSounds.Length > 0)
                {
                    AudioClip clipAleatorio = keyPressSounds[Random.Range(0, keyPressSounds.Length)];
                    audioController.SetSoundVolume(0.5f);
                    audioController.PlayAudio(null, clipAleatorio);
                }
            }
            else
            {
                PerderVida();
                AplicarEmpujeHaciaAtras();
                MostrarMensaje(mensajesError);
            }
        }
    }

    void ReiniciarTemporizador()
    {
        tiempoRestante = tiempoMaximoParaPulsar;
    }

    void SeleccionarTecla()
    {
        int indice = Random.Range(0, 4);
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i].SetActive(i == indice);
        }

        switch (indice)
        {
            case 0: teclaSeleccionada = "w"; break;
            case 1: teclaSeleccionada = "a"; break;
            case 2: teclaSeleccionada = "s"; break;
            case 3: teclaSeleccionada = "d"; break;
        }

        rbHijo = prefabs[indice].GetComponent<Rigidbody2D>();
        tiempoRestante = tiempoMaximoParaPulsar;
    }

    void AplicarEmpuje()
    {
        if (rbSeleccionado != null)
        {
            rbSeleccionado.AddForce(direccionEmpuje.normalized * fuerzaEmpuje, ForceMode2D.Impulse);
        }
        else
        {
            rbHijo.AddForce(direccionEmpuje.normalized * fuerzaEmpuje, ForceMode2D.Impulse);
        }
    }

    void AplicarEmpujeHaciaAtras()
    {
        Vector2 direccionContraria = new Vector2(-direccionEmpuje.x, direccionEmpuje.y);

        if (rbSeleccionado != null)
        {
            rbSeleccionado.AddForce(direccionContraria * fuerzaEmpuje, ForceMode2D.Impulse);
        }
        else
        {
            rbHijo.AddForce(direccionContraria * fuerzaEmpuje, ForceMode2D.Impulse);
        }

        ReproducirAnimacion();

        if (audioController != null && keyPressSounds.Length > 0)
        {
            AudioClip clipAleatorio = keyPressSounds[Random.Range(0, keyPressSounds.Length)];

            // Reproduce el sonido con el volumen establecido
            audioController.PlayAudio(null, clipAleatorio);
        }
    }

    void ReproducirAnimacion()
    {
        Animator animator = objetoSeleccionado.GetComponentInChildren<Animator>();

        if (animator != null)
        {
            animator.SetTrigger("Damage");
        }
        else
        {
            Debug.LogWarning("No se encontr  un Animator en el objeto hijo.");
        }
    }

    void MostrarMensaje(string[] grupoMensajes)
    {
        // Verifica si el mensajeCoroutine est  activo y lo detiene
        if (mensajeCoroutine != null)
        {
            StopCoroutine(mensajeCoroutine);
        }

        // Activa el panel y el texto
        panelMensaje.SetActive(true);  // Aseg rate de que el panel est  activo
        textoMensaje.gameObject.SetActive(true);  // Activa el texto

        // Asigna un mensaje aleatorio
        textoMensaje.text = grupoMensajes[Random.Range(0, grupoMensajes.Length)];

        // Inicia la coroutine para ocultar el mensaje
        mensajeCoroutine = StartCoroutine(OcultarMensajeTemporalmente());
    }

    IEnumerator OcultarMensajeTemporalmente()
    {
        // Espera por el tiempo configurado para mostrar el mensaje
        yield return new WaitForSeconds(tiempoMostrarMensaje);

        // Verifica si el mensaje actual est  dentro de los mensajes de error
        bool esError = false;
        foreach (var mensajeError in mensajesError)
        {
            if (textoMensaje.text == mensajeError)
            {
                esError = true;
                break;
            }
        }

        // Si el mensaje era de error, muestra el texto extra por un tiempo adicional
        if (esError)
        {
            textoMensaje.text = textoExtra;
            yield return new WaitForSeconds(tiempoMostrarMensaje);
        }

        // Finalmente, oculta solo el texto y el panel
        textoMensaje.gameObject.SetActive(false);  // Oculta el texto
        panelMensaje.SetActive(false);  // Tambi n puedes ocultar el panel si lo deseas
    }

    void PerderVidaPorTiempo()
    {
        PerderVida();
        AplicarEmpujeHaciaAtras();
        MostrarMensaje(mensajesError);
        ReiniciarTemporizador();
    }

    void PerderVida()
    {
        if (vidas > 0)
        {
            vidas--;

            Vidas[vidas].SetActive(false);

            if (vidas <= 0)
            {
                PausarJuego();  // Llamar a PausarJuego en lugar de GameOver directamente
            }
        }
    }

    // M todo para pausar el juego, mostrar el canvas y cargar la escena de "GameOver"
    void PausarJuego()
    {
        Time.timeScale = 0f;  // Pausa el juego
        gameOverCanvas.gameObject.SetActive(true);  // Muestra el Canvas de Game Over
        StartCoroutine(CargarEscenaGameOverConRetraso());  // Llamar la coroutine con retraso
    }

    IEnumerator CargarEscenaGameOverConRetraso()
    {
        yield return new WaitForSecondsRealtime(delayDeTiempo);  // Espera 3 segundos en tiempo real
        Time.timeScale = 1f;  // Restaurar la escala de tiempo antes de cargar la escena
        SceneManager.LoadScene("1 GameOver");
    }



}








