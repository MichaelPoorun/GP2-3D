using UnityEngine;
using UnityEngine.SceneManagement; // To use SceneManager for loading scenes

public class Goal : MonoBehaviour
{
    // This method is called when a collision happens
    private void OnCollisionEnter(Collision collision)
    {
        // Debugging: Log the name and tag of the colliding object
        Debug.Log("Collided with: " + collision.gameObject.name + " (Tag: " + collision.gameObject.tag + ")");

        // Check if the colliding object is the "Player" by name
        if (collision.gameObject.name == "Player")
        {
            // Log to indicate we are about to load the scene
            Debug.Log("Player collided, loading 'Lose' scene.");

            // Load the "Lose" scene
            SceneManager.LoadScene("Win");
        }
        else
        {
            // Prevent destruction of the "floor" object by checking its tag
            if (collision.gameObject.CompareTag("Floor"))
            {
                // Log when the "floor" object collides
                Debug.Log("Floor object collided, not destroyed.");
            }
            else
            {
                // Log for non-floor collisions (this will destroy them)
                Debug.Log("Non-floor object collided: " + collision.gameObject.name);

                // Destroy any non-floor object that collides
                Destroy(collision.gameObject);
            }
        }
    }

    // Optional: If you want to use triggers instead of physical collisions:
    /*
    private void OnTriggerEnter(Collider other)
    {
        // Debugging: Log the name and tag of the colliding object
        Debug.Log("Triggered by: " + other.gameObject.name + " (Tag: " + other.gameObject.tag + ")");

        if (other.gameObject.name == "Player")
        {
            Debug.Log("Player triggered, loading 'Lose' scene.");
            SceneManager.LoadScene("Lose");
        }
        else
        {
            // Prevent destruction of the "floor" object by checking its tag
            if (other.gameObject.CompareTag("Floor"))
            {
                Debug.Log("Floor object triggered, not destroyed.");
            }
            else
            {
                Debug.Log("Non-floor object triggered: " + other.gameObject.name);
                Destroy(other.gameObject);
            }
        }
    }
    */
}