using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    private int _levelInfo;

    private static LevelControl instance = null;
    public static LevelControl Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        _levelInfo = PlayerPrefs.GetInt(Statics.PREF_LEVELINFO, 1);
    }
    public void LevelUp()
    {
        _levelInfo++;
        PlayerPrefs.SetInt(Statics.PREF_LEVELINFO, _levelInfo);
        Invoke("ReloadScene", 3f);
    }
    private void ReloadScene()
    {
        SceneManager.LoadScene(Statics.SCENE_GAMESCENE);
    }
}
