using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    void Awake()
    {
        var playerController = GetComponent<PlayerController>();
        var stageClear = GetComponent<StageClear>();
        if (playerController != null)
        {
            GetComponent<PlayerController>().SetInstance(gameManager);
        }
        if (stageClear != null)
        {
            GetComponent<StageClear>().SetInstance(gameManager);
        }
    }
}
