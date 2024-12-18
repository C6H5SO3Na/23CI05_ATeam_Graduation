using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    void Awake()
    {
        GetComponent<PlayerController>().SetInstance(gameManager);
    }
}
