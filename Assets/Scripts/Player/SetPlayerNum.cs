using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPlayerNum : MonoBehaviour
{
    [SerializeField] int playerNum;
    void Awake()
    {
        GetComponent<PlayerController>().OriginalPlayerNum = playerNum;
    }
}
