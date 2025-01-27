using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // コンストラクタ
    //-------------------------------------------------------------------------------
    public GenerateMap()
    {
        mapPrefabDictionary = new Dictionary<int, GameObject>();
        startUpObjectInstances = new List<GameObject>();
        startGimmickInstances = new List<GameObject>();
        mapData = new List<List<List<int>>>();
        gimmickID = new List<int>();
        bootObjectID = new List<int>();
        gimmickAssociationID = new Dictionary<int, int>();
        rotationValue_y = new List<int>();
        gimmickPowerIndexs = new List<int>();
    }

    //-------------------------------------------------------------------------------
    // 変数
    //-------------------------------------------------------------------------------
    [SerializeField]
    List<GameObject> mapPrefab;   // 床になるブロックのプレハブ

    Dictionary<int, GameObject> mapPrefabDictionary;  // 値と床のプレハブの紐づけ(生成時の処理を分かりやすくするため)

    public GameObject player1Instance { get; private set; }             // プレイヤー1のインスタンス    
    public GameObject player2Instance { get; private set; }             // プレイヤー2のインスタンス
    public GameObject enemyInstance { get; private set; }               // 敵のインスタンス            
    public GameObject goalInstance { get; private set; }                // ゴールのインスタンス
    public List<GameObject> startUpObjectInstances { get; private set; } // ギミックを起動させるオブジェクトのインスタンス
    public List<GameObject> startGimmickInstances { get; private set; } // ギミックのインスタンス

    int layerWidth = 20 + 2;                    // 層の横幅(床にできる範囲 + 壁を作成する部分)
    int layerHeight = 20 + 2;                   // 層の縦幅(床にできる範囲 + 壁を作成する部分)
    int layerNumber = 6;                        // 層の数
    List<List<List<int>>> mapData;              // 床のマップデータ
    List<int> gimmickID;                        // ギミックidを格納する
    List<int> bootObjectID;                     // ギミック起動オブジェクトidを格納する
    Dictionary<int, int> gimmickAssociationID;  // ギミックを起動オブジェクトに紐付けるためのidを格納する    
    List<int> rotationValue_y;                  // ギミックのy軸回転の値を格納する
    List<int> gimmickPowerIndexs;               // ギミックの強さを決める引数を格納する

    //-------------------------------------------------------------------------------
    // 関数
    //-------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //jsonファイルを読み取るクラスを取得
        LoadJsonFile_Map loadJsonFile = GetComponent<LoadJsonFile_Map>();
        if(loadJsonFile)
        {
            //ステージ名を取得する

            //jsonファイルを読み込んでデータを取得する
            loadJsonFile.GetStageData(AppManager.StageName, this);
        }

        //値の初期値設定
        for(int i = 0; i < mapPrefab.Count; ++i)
        {
            mapPrefabDictionary[i + 1] = mapPrefab[i];  // i + 1はマップデータの0を空白にするため
        }

        //マップの生成
        Generate();

        //インスタンスを渡すクラスを取得
        PassInstance passInstance = GetComponent<PassInstance>();
        if(passInstance)
        {
            //インスタンスを他クラスに渡す
            passInstance.PassInstanceToOtherClass(player1Instance, player2Instance, enemyInstance, goalInstance);

            //ギミックのインスタンスをギミックを起動するクラスに渡す
            passInstance.PassInstanceStartGimmick(startUpObjectInstances, startGimmickInstances, gimmickAssociationID);
        }
    }

    /// <summary>
    /// マップの生成
    /// </summary>
    void Generate()
    {
        //マップ情報を持った配列の要素にアクセスするための変数宣言
        int rotationValue_yIndex = 0;   // y軸回転の値を格納した配列にの要素にアクセスするための値

        //マップ生成
        for (int y = 0; y < layerNumber; ++y)
        {
            for (int z = 0; z < layerHeight; ++z)
            {
                for (int x = 0; x < layerWidth; ++x)
                {
                    //マップデータが存在しないプレハブを指定していなかったら生成する
                    if (mapPrefabDictionary.TryGetValue(mapData[y][z][x], out GameObject prefab))
                    {
                        //配置する位置を設定
                        Vector3 position = new Vector3(x, y, (layerHeight - 1) - z);    // layerHeight - 1はmapDataの形通りにマップを作るために必要

                        switch (mapData[y][z][x])
                        {
                            case 2:     // プレイヤー1を生成する
                                if (player1Instance == null)
                                {
                                    player1Instance = Instantiate(prefab, position, Quaternion.identity);

                                    //プレイヤー識別番号を1に設定する
                                    player1Instance.GetComponent<PlayerController>().OriginalPlayerNum = 1;
                                }
                                else
                                {
                                    Debug.LogWarning("プレイヤー1はこれ以上生成できません");
                                }
                                break;

                            case 3:     // プレイヤー2を生成する
                                if (player2Instance == null)
                                {
                                    player2Instance = Instantiate(prefab, position, Quaternion.identity);

                                    //プレイヤー識別番号を2に設定する
                                    player2Instance.GetComponent<PlayerController>().OriginalPlayerNum = 2;
                                }
                                else
                                {
                                    Debug.LogWarning("プレイヤー2はこれ以上生成できません");
                                }
                                break;

                            case 4:     // 敵を生成する
                                enemyInstance = Instantiate(prefab, position, Quaternion.identity);
                                break;

                            case 5:     // 感圧板を生成する
                                position = new Vector3(x, y - 0.4f, (layerHeight - 1) - z); // 0.5引くことで生成時に床の上に生成される
                                startUpObjectInstances.Add(Instantiate(prefab, position, Quaternion.identity));
                                break;

                            case 7:     // ゴールを生成する
                                goalInstance = Instantiate(prefab, position, Quaternion.identity);
                                break;

                            case 11:    // レーザーをy軸90度回転して生成
                                startGimmickInstances.Add(Instantiate(prefab, position, Quaternion.Euler(0, rotationValue_y[rotationValue_yIndex], 0)));
                                rotationValue_yIndex++;
                                break;

                            default:    // プレイヤー、敵、ゴール、感圧板以外のものを生成する
                                GameObject generateObject = Instantiate(prefab, position, Quaternion.identity);

                                //生成したオブジェクトがギミックを起動させるオブジェクトだったら起動オブジェクトリストに格納する
                                if (generateObject.GetComponent<ISetGimmickInstance>() != null)
                                {
                                    startUpObjectInstances.Add(generateObject);
                                }
                                //生成したオブジェクトがギミックだったらギミックリストに格納する
                                else if(generateObject.GetComponent<IStartedOperation>() != null)
                                {
                                    startGimmickInstances.Add(generateObject);
                                }
                                break;
                        }
                    }
                    else if (mapData[y][z][x] == 0)
                    {
                        //mapDataが0の時、Debug.LogWarningを呼び出さないようにしている
                        continue;
                    }
                    else
                    {
                        Debug.LogWarning("何も設置されません。意図しない場合はmapDataを確認してください");
                    }
                }
            }
        }

        //生成したギミックのidを設定する
        for (int i = 0; i < gimmickID.Count; ++i)
        {
            //インスタンス毎のid設定
            startGimmickInstances[i].GetComponent<GimmickBase>().SetID(gimmickID[i]);
            
            
        }

        //ギミックを起動するオブジェクトにidを設定する
        for (int i = 0; i < bootObjectID.Count; ++i)
        {
            //インスタンス毎のid設定
            startUpObjectInstances[i].GetComponent<BootObjectBase>().SetID(bootObjectID[i]);
        }
    }

    /// <summary>
    /// ステージを生成するのに必要な情報を設定する
    /// </summary>
    /// <param name="layerWidth"> 層の横幅 </param>
    /// <param name="layerHeight"> 層の縦幅 </param>
    /// <param name="layerNumber"> 層の数 </param>
    /// <param name="mapData"> マップデータ </param>
    /// <param name="gimmickID"> ギミック識別用id </param>
    /// <param name="bootObjectID"> ギミック起動オブジェクト識別用id </param>
    /// <param name="gimmickAssociationKey"> ギミックを起動オブジェクトに紐づけるためのkey </param>
    /// <param name="gimmickAssociationID"> ギミックを起動オブジェクトに紐づけるためのid </param>
    /// <param name="rotationValue_y"> ギミックのy軸回転の値 </param>
    /// <param name="gimmickPowerIndexs"> ギミックの強さを決める値 </param>
    public void SetStageData(int layerWidth, int layerHeight, int layerNumber, List<List<List<int>>> mapData, List<int> gimmickID, List<int> bootObjectID, List<int> gimmickAssociationKey, List<int> gimmickAssociationID, List<int> rotationValue_y, List<int> gimmickPowerIndexs)
    {
        this.layerWidth = layerWidth;
        this.layerHeight = layerHeight;
        this.layerNumber = layerNumber;
        this.mapData = mapData;
        this.gimmickID = gimmickID;
        this.bootObjectID = bootObjectID;
        for(int i = 0; i < gimmickAssociationKey.Count; ++i)
        {
            this.gimmickAssociationID[gimmickAssociationKey[i]] = gimmickAssociationID[i];
        }
        this.rotationValue_y = rotationValue_y;
        this.gimmickPowerIndexs = gimmickPowerIndexs;
    }
}
