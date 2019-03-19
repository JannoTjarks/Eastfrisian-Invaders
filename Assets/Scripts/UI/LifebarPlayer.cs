using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarPlayer : MonoBehaviour
{
    public Sprite LifeBar3;
    public Sprite LifeBar2;
    public Sprite LifeBar1;

    private int _life;
    private static LifebarPlayer instance = null;

    public static LifebarPlayer Instance
    {
        get
        {
            return instance;
        }
    }

    public int Life
    {
        set
        {
            this._life = value;
            CheckLifebar();
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Image>().sprite = LifeBar3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckLifebar()
    {
        if (_life == 2)
        {
            this.gameObject.GetComponent<Image>().sprite = LifeBar2;
        }

        if (_life == 1)
        {
            this.gameObject.GetComponent<Image>().sprite = LifeBar1;
        }
    }    
}
