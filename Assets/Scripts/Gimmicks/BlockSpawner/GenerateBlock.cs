using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBlock : MonoBehaviour, IStartedOperation
{
    [SerializeField]
    private GameObject generatingBlockPrefab;                           // 生成するブロック
    private List<GameObject> generatedBlocks = new List<GameObject>();  // 生成したブロックのリスト
    [SerializeField]
    private int maxObjects;                                              // 生成できるブロックの数

    void Start()
    {
        //maxObjects = 1;
    }

    //ブロックが無ければブロックを生成する
    public bool StartedOperation()
    {
        if (generatingBlockPrefab)
        {
            //生成したブロックが消滅していたらリストから削除しておく
            generatedBlocks.RemoveAll(block => block == null);

            //ブロックが生成されていなかったら生成する
            if(generatedBlocks.Count < maxObjects)
            {
                //生成位置を決める
                Vector3 position = new Vector3(10, 1, 10);

                //ブロックの生成
                GameObject newBlock = Instantiate(generatingBlockPrefab, position, Quaternion.identity);

                //生成したブロックをリストに登録
                generatedBlocks.Add(newBlock);

                return true;
            }
            else
            {
                Debug.LogWarning("既に生成されている");
            }
        }
        else
        {
            Debug.LogWarning("生成するオブジェクトが設定されていない");
        }

        return false;
    }

    //void Update()
    //{
    //    Debug.Log("maxObjects:" + maxObjects);
    //}
}