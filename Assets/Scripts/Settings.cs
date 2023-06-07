using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Settings/Settings")]
public class Settings : ScriptableObject
{
    [SerializeField] private float shoottingTime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float _aimSpeed;

    [SerializeField] private float _aimIntervalYmin;
    [SerializeField] private float _aimIntervalYmax;
    [SerializeField] private float _aimIntervalZmin;
    [SerializeField] private float _aimIntervalZmax;
    [SerializeField] private float _magazineUpSpeed;


    public float ShoottingTime { get => shoottingTime;}
    public float BulletSpeed { get => bulletSpeed; }
    public float AimSpeed { get => _aimSpeed; }
    public float AimIntervalYmin { get => _aimIntervalYmin; }
    public float AimIntervalYmax { get => _aimIntervalYmax; }
    public float AimIntervalZmin { get => _aimIntervalZmin; }
    public float AimIntervalZmax { get => _aimIntervalZmax; }
    public float MagazineUpSpeed { get => _magazineUpSpeed; }
}
