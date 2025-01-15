using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIGenerator : MonoBehaviour
{
    [SerializeField] GameObject playerUI;
    Canvas canvas;
    // Start is called before the first frame update
    void Awake()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        //UIÇê∂ê¨
        GameObject instance = Instantiate(playerUI, this.transform.position, Quaternion.identity, canvas.transform);
        instance.GetComponent<PlayerUIManager>().SetInstance(this.GetComponent<PlayerController>());

        GetComponent<PlayerController>().SetUI(instance.GetComponent<PlayerUIManager>());
    }
}
