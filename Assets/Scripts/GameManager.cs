using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Mage,
    Bullet,
    Base,
    Slide,
    Shoot,
    Result,
    End,
    
}
public class GameManager : MonoBehaviour
{

    private static GameManager instance = null;
    public static GameManager Instance { get => instance; set => instance = value; }

    private State currentState;
    public State CurrentState { get => currentState; set => currentState = value; }


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }


}
