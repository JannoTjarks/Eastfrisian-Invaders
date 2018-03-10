using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicChanger : MonoBehaviour
{
    private AudioSource _music;
    public AudioClip LevelSoundtrack;
    public AudioClip BossSoundtrack;
    public AudioClip CreditsSoundtrack;
    public AudioClip MenueSoundtrack;

    // Play Global
    private static MusicChanger instance = null;
    private static MusicChanger Instance
    {
        get { return instance; }
    }

    void Start()
    {
        // Movement
        _music = GetComponent<AudioSource>();
        MusicChange();
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

        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        MusicChange();
    }

    void MusicChange()
    {
        if (SceneManager.GetActiveScene().buildIndex < 3 && _music.clip != MenueSoundtrack)
        {
            _music.Stop();
            _music.clip = MenueSoundtrack;
            _music.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 9 && _music.clip != CreditsSoundtrack)
        {
            _music.Stop();
            _music.clip = CreditsSoundtrack;
            _music.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 8 && _music.clip != BossSoundtrack)
        {
            _music.Stop();
            _music.clip = BossSoundtrack;
            _music.Play();
        }
        else if (SceneManager.GetActiveScene().buildIndex > 2 && SceneManager.GetActiveScene().buildIndex < 8
            && _music.clip != LevelSoundtrack)
        {
            _music.Stop();
            _music.clip = LevelSoundtrack;
            _music.Play();
        }
    }
}