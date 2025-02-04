using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageImageGenerator : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] Sprite[] images;

    //ステージ画像の生成
    void Start()
    {
        //親(キャンバス)
        Transform canvas = transform.parent;
        for (int i = 0; i < y; ++i)
        {
            for (int j = 0; j < x; ++j)
            {
                GameObject stageImage = Instantiate(imagePrefab, canvas);

                //ステージ画像が割り当てられていたらステージ画像に差し替え
                if (images[i * 4 + j] != null)
                {
                    stageImage.GetComponent<Image>().sprite = images[i * 4 + j];
                }
                stageImage.transform.SetParent(canvas, false);
                stageImage.transform.localPosition = new Vector3(450 * j - 675, 200 - 400 * i);

                //ステージ番号を割り当て
                stageImage.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{i + 1}-{j + 1}";
            }
        }
    }
}
