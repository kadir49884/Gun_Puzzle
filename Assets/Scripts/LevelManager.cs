using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    LevelScriptable levelScriptable;

    [SerializeField]
    private GameObject gunParent;

    [SerializeField]
    private GameObject gunObject;

    private string _levelInfo;
    private int _bulletCount;
    private int _targetCount;

    public int TargetCount { get => _targetCount; }
    public int BulletCount { get => _bulletCount; }

    private void Awake()
    {
        GetLevelInfo();

        if (levelScriptable == null)//Stack over flow 
        {
            PlayerPrefs.SetInt(Statics.PREF_LEVELINFO, 1);
            //Awake();
            //return;
            GetLevelInfo();
        }
        Instantiate(levelScriptable.BulletPrefab, gunParent.transform);
        Instantiate(levelScriptable.CameraNewPos, gunObject.transform);
        Instantiate(levelScriptable.GunShootNewPos, gunObject.transform);
        
        Instantiate(levelScriptable.TargetObject);

        _bulletCount = levelScriptable.BulletCount;
        _targetCount = levelScriptable.TargetCount;

    }

    private void GetLevelInfo()
    {
        _levelInfo = Path.Combine(Statics.PREF_LEVELDATA, Statics.PREF_LEVELCONSTANT + PlayerPrefs.GetInt(Statics.PREF_LEVELINFO, 1));
        levelScriptable = Resources.Load<LevelScriptable>(_levelInfo);
    }
}
