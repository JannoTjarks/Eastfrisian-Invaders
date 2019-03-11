using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    public int EnemysAlive;

    void FixedUpdate()
    {
        if (EnemysAlive == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
}