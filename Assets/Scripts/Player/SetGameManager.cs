using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGameManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<PlayerController>().SetInstance(gameManager);
    }
}
