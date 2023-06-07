using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MageUp : MonoBehaviour
{

    [SerializeField] GameObject bulletParent;
    [SerializeField] Settings settings;
    GameManager gameManager;
   
    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    private void OnMouseDown()
    {
        if (gameManager.CurrentState == State.Mage)
        {
            transform.DOLocalMoveX(settings.MagazineUpSpeed, 0.5f);
            gameManager.CurrentState++;
            Invoke("GetParent", 1f);
        }
    }

    private void GetParent()
    {
        bulletParent.transform.parent = gameObject.transform;
    }

}
