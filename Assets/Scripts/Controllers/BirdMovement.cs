using UnityEngine;

public class BirdMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float timeLimit = 3f;
    private string correctKey;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeLimit)
        {
            CheckInput();
            timer = 0; // Reset timer
        }
    }

    void CheckInput()
    {
        correctKey = RandomKey(); // Get random key (W, A, D, etc.)

        if (Input.GetKeyDown(correctKey))
        {
            MoveForward();
        }
        else
        {
            BirdFalls();
        }
    }

    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        Debug.Log("Moving forward!");
    }

    void BirdFalls()
    {
        Debug.Log("Bird falls! Try again.");
        // Handle bird falling logic here.
    }

    string RandomKey()
    {
        string[] keys = { "w", "a", "d", "left", "right" };
        return keys[Random.Range(0, keys.Length)];
    }
}
