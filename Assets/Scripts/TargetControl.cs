using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetControl : MonoBehaviour
{
    private int _bulletCount;
    private int _targetCount;

    private GameManager gameManager;

    private static TargetControl instance = null;
    public static TargetControl Instance { get => instance; set => instance = value; }
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    void Start()
    {
        LevelManager levelManager = GetComponent<LevelManager>();
        _targetCount = levelManager.TargetCount;
        _bulletCount = levelManager.BulletCount;

        gameManager = GameManager.Instance;

    }
    public void ReduceBullet()
    {
        _bulletCount--;
        if(_bulletCount < 1)
        {
            gameManager.CurrentState++;
        }
        Invoke("ResultInfo", 1f);
    }
    private void ResultInfo()
    {
        if (_bulletCount < 1 && _targetCount > 0)
        {
            Invoke("WaitForReload", 2f);
        }
    }
    private void WaitForReload()
    {
        SceneManager.LoadScene(Statics.SCENE_GAMESCENE);
    }
    public void ReduceTarget()
    {
        _targetCount--;
        if(_targetCount < 1 )
        {
            gameManager.CurrentState++;
            LevelControl.Instance.LevelUp();
        }
    }

}
