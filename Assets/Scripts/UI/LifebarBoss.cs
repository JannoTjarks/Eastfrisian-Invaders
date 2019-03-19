using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifebarBoss : MonoBehaviour
{
    Sprite[] sprites;
  
    private int _life;
    private int LIFEMAX;
    private static LifebarBoss instance = null;

    public static LifebarBoss Instance
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

    public int SetLIFEMAX
    {
        set
        {
            this.LIFEMAX = value;
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
        sprites = Resources.LoadAll<Sprite>("Bosses/SpritemapBoss1LifeBar");
        this.gameObject.GetComponent<Image>().sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckLifebar()
    {
        int lifeBarPosition = ((LIFEMAX - _life) / 2);
        if ((lifeBarPosition * 2) % 2 == 0 && _life < 60)
            this.gameObject.GetComponent<Image>().sprite = sprites[lifeBarPosition];        
    }
}
