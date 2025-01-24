using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;

public class GameUIManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] GameObject heart;//ライフ
    [SerializeField] GameObject clearPrefab;//クリア画面
    [SerializeField] GameObject gameOverPrefab;
    [SerializeField] GameObject fade;
    Fade fadeInstance;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        for (int i = 0; i < 3; ++i)
        {
            //Canvas上に生成
            GameObject life = Instantiate(heart, transform.parent);
            life.transform.localPosition = new Vector3(-900 + i * 100, 480);
        }
        fadeInstance = Instantiate(fade, transform.parent).GetComponent<Fade>();
        fadeInstance.StartFadeIn(1f);
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
