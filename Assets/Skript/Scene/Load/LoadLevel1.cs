using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel1 : MonoBehaviour
{

    // Use this for initialization
	void Start ()
    {

    }
    
    // Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Submit"))
            SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }
}