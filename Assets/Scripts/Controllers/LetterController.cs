using UnityEngine;

public class LetterController : MonoBehaviour
{
    public GameObject[] letterPrefabs; // Prefabs para W, A, S, D
    public float displayTime = 3f; // Tiempo para mostrar cada letra
    public GameObject player; // Referencia al jugador (con Rigidbody2D)
    public float impulseX = 5f; // Impulso en el eje X
    public float impulseY = 10f; // Impulso en el eje Y

    private GameObject currentLetter;
    private KeyCode currentKey; // Tecla actual que se debe presionar
    private float timer;
    private bool keyPressed = false; // Para rastrear si se ha presionado una tecla
    private Rigidbody2D rb; // Referencia al Rigidbody2D del jugador

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>(); // Obtener el Rigidbody2D del jugador
        ShowRandomLetter(); // Mostrar la primera letra aleatoria
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Si el jugador no presiona la tecla correcta a tiempo, desencadena una caída
        if (timer >= displayTime && !keyPressed)
        {
            Debug.Log("Time's up! Player falls.");
            BirdFalls();
            timer = 0f; // Reiniciar temporizador después de fallar
            ShowRandomLetter(); // Mostrar una nueva letra después de que el tiempo se acabe
        }
        else
        {
            CheckInput(); // Seguir verificando la entrada del jugador
        }
    }

    void ShowRandomLetter()
    {
        // Reiniciar el temporizador y el seguimiento de la tecla presionada
        timer = 0f;
        keyPressed = false;

        // Desactivar todas las letras primero
        foreach (GameObject letter in letterPrefabs)
        {
            letter.SetActive(false);
        }

        // Escoger una letra aleatoria y activarla
        int index = Random.Range(0, letterPrefabs.Length);
        currentLetter = letterPrefabs[index];
        currentLetter.SetActive(true);

        // Asignar la tecla correspondiente en base a la letra mostrada
        currentKey = GetKeyFromLetter(currentLetter.name);
    }

    KeyCode GetKeyFromLetter(string letter)
    {
        switch (letter)
        {
            case "W": return KeyCode.W;
            case "A": return KeyCode.A;
            case "S": return KeyCode.S;
            case "D": return KeyCode.D;
            default: return KeyCode.None;
        }
    }

    void CheckInput()
    {
        if (Input.GetKeyDown(currentKey))
        {
            Debug.Log("Correct! Applied impulse.");
            keyPressed = true; // Marcar la tecla como presionada
            ApplyImpulse(); // Aplicar el impulso al jugador
            ShowRandomLetter(); // Mostrar una nueva letra
        }
        else if (Input.anyKeyDown) // Si se presiona una tecla incorrecta
        {
            Debug.Log("Incorrect! Player falls.");
            BirdFalls();
            keyPressed = true; // Marcar la tecla como presionada para evitar falla por tiempo
        }
    }

    void ApplyImpulse()
    {
        // Aplicar un impulso al jugador en el eje X e Y
        Vector2 force = new Vector2(impulseX, impulseY);
        rb.AddForce(force, ForceMode2D.Impulse); // Impulsar al jugador en 2D
    }

    void BirdFalls()
    {
        // Lógica para cuando el jugador cae (reiniciar la posición o la escena)
        Debug.Log("Resetting the scene.");
        // Aquí puedes implementar la lógica para reiniciar la posición del jugador
    }
}
