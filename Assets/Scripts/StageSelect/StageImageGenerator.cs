using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageImageGenerator : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] int x;
    [SerializeField] int y;
    //ステージ画像の生成
    void Start()
    {
        Transform canvas = transform.parent;
        for(int i = 0; i < y; ++i)
        {
            for (int j = 0; j < x; ++j)
            {
                GameObject stageImage = Instantiate(imagePrefab, canvas);
                stageImage.transform.SetParent(canvas, false);
                stageImage.transform.localPosition = new Vector3(450 * j - 675, 260 - 200 * i);
                stageImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{i + 1}-{j + 1}";
            }
        }
    }
}
