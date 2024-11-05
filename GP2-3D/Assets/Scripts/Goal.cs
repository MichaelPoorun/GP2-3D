using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Player")
        {
            Debug.Log("Player collided");

            SceneManager.LoadScene("Win");
        }
    }
}