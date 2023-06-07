using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Levels", menuName = "ScriptableObject/Levels", order =1)]
public class LevelScriptable : ScriptableObject
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject targetObject;
    [SerializeField] private int bulletCount;
    [SerializeField] private int targetCount;
    [SerializeField] private Transform cameraNewPos;
    [SerializeField] private Transform gunShootNewPos;
    public GameObject BulletPrefab { get => bulletPrefab; }
    public GameObject TargetObject { get => targetObject; }
    public int BulletCount { get => bulletCount;  }
    public int TargetCount { get => targetCount; }
    public Transform CameraNewPos { get => cameraNewPos; }
    public Transform GunShootNewPos { get => gunShootNewPos; }
}
