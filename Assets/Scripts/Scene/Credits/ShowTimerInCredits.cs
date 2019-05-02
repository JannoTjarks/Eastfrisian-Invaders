using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowTimerInCredits : MonoBehaviour
{
    Text text = null;

    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<Text>();
        if (Stats.Time != string.Empty)
        {
            text.text += "TIME: " + Stats.Time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
