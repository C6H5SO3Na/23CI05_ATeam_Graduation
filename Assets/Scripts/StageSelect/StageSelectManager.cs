using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ƒ^ƒCƒgƒ‹‚Ö–ß‚é
        if (Input.GetButtonDown("Jump_P1"))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}
