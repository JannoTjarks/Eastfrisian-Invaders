using UnityEngine;
using UnityEngine.SceneManagement;

public class Stats : MonoBehaviour
{
    private static string _time = string.Empty;

    public static string Time
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
        }
    }

    public void Reset()
    {
        _time = string.Empty;
    }
}
