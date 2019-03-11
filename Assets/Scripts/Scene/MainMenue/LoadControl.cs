using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadControl : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.C))
            SceneManager.LoadScene("Control", LoadSceneMode.Single);
    }
}