using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    public int EnemysAlive;
    public int SceneIndex;

    void FixedUpdate()
    {
        if (EnemysAlive == 0)
            SceneManager.LoadScene(SceneIndex+1, LoadSceneMode.Single);
    }
}