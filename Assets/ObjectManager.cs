using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField]
    private Camera cameraObject;
    

    private static ObjectManager instance = null;

    public static ObjectManager Instance { get => instance; set => instance = value; }
    public Camera CameraObject { get => cameraObject; set => cameraObject = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

}
