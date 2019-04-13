using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    public GameObject selectedObject;

    private RectTransform _rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rectTransform.position.y >= 350)
        {
            selectedObject.SetActive(true);
        }       
    }
}
