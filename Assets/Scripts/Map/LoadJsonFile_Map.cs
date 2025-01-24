using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using LitJson;

public class LoadJsonFile_Map : MonoBehaviour
{
    //変数
    JsonData jsonData;  // jsonデータを格納する

    /// <summary>
    /// jsonファイルの読み込み
    /// </summary>
    /// <param name="stageName"> ステージ名 </param>
    private void LoadJsonFile(string stageName)
    {
        //読み込むjsonファイルの名前を設定
        string fileName = "Stage" + stageName + ".json";

        //パスの設定
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        //ファイルが存在しているか確認する
        if(File.Exists(filePath))
        {
            //jsonファイルを読み込む
            string jsonText = File.ReadAllText(filePath);

            //json配列のデシリアライズ
            jsonData = JsonMapper.ToObject(jsonText);
        }
        //ファイルが存在していなかったらステージ1-1を読み込む
        else 
        {
            //ファイル名をStage0.jsonにする(Stage0は絶対に存在するステージ)
            fileName = "Stage0.json";

            //パスの設定
            filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            //jsonファイル読み込み
            string jsonText = File.ReadAllText(filePath);

            //json配列のデシリアライズ
            jsonData = JsonMapper.ToObject(jsonText);
        }
    }

    /// <summary>
    /// ステージのデータを取得する
    /// </summary>
    /// <param name="stageName"> ステージ名 </param>
    /// <param name="classGetData"> データを取得するクラス </param>
    public void GetStageData(string stageName, GenerateMap classGetData)
    {
        //jsonファイルの読み込み
        LoadJsonFile(stageName);

        if(jsonData != null)
        {
            //jsonデータを既存の型、コレクションに変更
            //マップ情報格納
            int layerWidth = (int)jsonData["LayerInfo"][0]["LayerWidth"];
            int layerHeight = (int)jsonData["LayerInfo"][0]["LayerHeight"];
            int layerNumber = (int)jsonData["LayerInfo"][0]["LayerNumber"];
            List<List<List<int>>> mapData = new List<List<List<int>>>();
            for(int y = 0; y < layerNumber; ++y)
            {
                mapData.Add(new List<List<int>>());
                for(int z = 0; z < layerHeight; ++z)
                {
                    mapData[y].Add(new List<int>());
                    for(int x = 0; x < layerWidth; ++x)
                    {
                        mapData[y][z].Add((int)jsonData["MapDataInfo"][0]["MapData"][y][z][x]);
                    }
                }
            }

            //ギミックのID格納
            List<int> gimmickID = new List<int>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["GimmickIDNum"]; ++i)
            {
                gimmickID.Add((int)jsonData["MapDataInfo"][0]["GimmickID"][i]);
            }

            //起動オブジェクトのID格納
            List<int> bootObjectID = new List<int>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["BootObjectIDNum"]; ++i)
            {
                bootObjectID.Add((int)jsonData["MapDataInfo"][0]["BootObjectID"][i]);
            }

            //ギミックと起動オブジェクトを紐づける情報を格納
            List<int> dictionaryKey = new List<int>();
            List<int> dictionaryID = new List<int>();
            for (int i = 0; i < (int)jsonData["GimmickInfo"][0]["AssociateNum"]; ++i)
            {
                dictionaryKey.Add((int)jsonData["GimmickInfo"][0]["DictionaryKey"][i]);
                dictionaryID.Add((int)jsonData["GimmickInfo"][0]["DictionaryValue"][i]);
            }

            //y軸回転情報の格納
            List<float> rotationValue_y = new List<float>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["DirectionNum"]; ++i)
            {
                rotationValue_y.Add((float)jsonData["MapDataInfo"][0]["Direction"][i]);
            }

            //ステージを作るのに必要な情報を設定する
            classGetData.SetStageData(layerWidth, layerHeight, layerNumber, mapData, gimmickID, bootObjectID, dictionaryKey, dictionaryID, rotationValue_y);
        }
    }
}