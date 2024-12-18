using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    //ƒ‰ƒCƒt
    [SerializeField] GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            GameObject life = Instantiate(heart, transform.parent);
            life.transform.localPosition = new Vector3(-900 + i * 100, 480);
        }
    }

    public void DecreaseHP()
    {
        Destroy(transform.parent.GetChild(transform.parent.childCount - 1).gameObject);
    }
}
