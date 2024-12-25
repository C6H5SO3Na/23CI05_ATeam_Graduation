using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject heart;//ライフ
    [SerializeField] GameObject clearPrefab;//クリア画面
    [SerializeField] GameObject gameOverPrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 3; ++i)
        {
            //Canvas上に生成
            GameObject life = Instantiate(heart, transform.parent);
            life.transform.localPosition = new Vector3(-900 + i * 100, 480);
        }
    }

    public void DecreaseHP(int damageAmount)
    {
        int tmpLife = gameManager.PlayersHP;

        for (int i = 0; i < damageAmount; ++i)
        {
            --tmpLife;
            if(tmpLife < 0) { break; }
            Destroy(transform.parent.GetChild(transform.parent.childCount - 1 - i).gameObject);
        }

    }

    public void ShowClear()
    {
        //Canvas上に生成
        Instantiate(clearPrefab, transform.parent);
    }

    public void GameOver()
    {
        Instantiate(gameOverPrefab, transform.parent);
    }
}
