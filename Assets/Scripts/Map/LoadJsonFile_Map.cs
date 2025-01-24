using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;
using LitJson;

public class LoadJsonFile_Map : MonoBehaviour
{
    //�ϐ�
    JsonData jsonData;  // json�f�[�^���i�[����

    /// <summary>
    /// json�t�@�C���̓ǂݍ���
    /// </summary>
    /// <param name="stageName"> �X�e�[�W�� </param>
    private void LoadJsonFile(string stageName)
    {
        //�ǂݍ���json�t�@�C���̖��O��ݒ�
        string fileName = "Stage" + stageName + ".json";

        //�p�X�̐ݒ�
        string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

        //�t�@�C�������݂��Ă��邩�m�F����
        if(File.Exists(filePath))
        {
            //json�t�@�C����ǂݍ���
            string jsonText = File.ReadAllText(filePath);

            //json�z��̃f�V���A���C�Y
            jsonData = JsonMapper.ToObject(jsonText);
        }
        //�t�@�C�������݂��Ă��Ȃ�������X�e�[�W1-1��ǂݍ���
        else 
        {
            //�t�@�C������Stage0.json�ɂ���(Stage0�͐�΂ɑ��݂���X�e�[�W)
            fileName = "Stage0.json";

            //�p�X�̐ݒ�
            filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            //json�t�@�C���ǂݍ���
            string jsonText = File.ReadAllText(filePath);

            //json�z��̃f�V���A���C�Y
            jsonData = JsonMapper.ToObject(jsonText);
        }
    }

    /// <summary>
    /// �X�e�[�W�̃f�[�^���擾����
    /// </summary>
    /// <param name="stageName"> �X�e�[�W�� </param>
    /// <param name="classGetData"> �f�[�^���擾����N���X </param>
    public void GetStageData(string stageName, GenerateMap classGetData)
    {
        //json�t�@�C���̓ǂݍ���
        LoadJsonFile(stageName);

        if(jsonData != null)
        {
            //json�f�[�^�������̌^�A�R���N�V�����ɕύX
            //�}�b�v���i�[
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

            //�M�~�b�N��ID�i�[
            List<int> gimmickID = new List<int>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["GimmickIDNum"]; ++i)
            {
                gimmickID.Add((int)jsonData["MapDataInfo"][0]["GimmickID"][i]);
            }

            //�N���I�u�W�F�N�g��ID�i�[
            List<int> bootObjectID = new List<int>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["BootObjectIDNum"]; ++i)
            {
                bootObjectID.Add((int)jsonData["MapDataInfo"][0]["BootObjectID"][i]);
            }

            //�M�~�b�N�ƋN���I�u�W�F�N�g��R�Â�������i�[
            List<int> dictionaryKey = new List<int>();
            List<int> dictionaryID = new List<int>();
            for (int i = 0; i < (int)jsonData["GimmickInfo"][0]["AssociateNum"]; ++i)
            {
                dictionaryKey.Add((int)jsonData["GimmickInfo"][0]["DictionaryKey"][i]);
                dictionaryID.Add((int)jsonData["GimmickInfo"][0]["DictionaryValue"][i]);
            }

            //y����]���̊i�[
            List<float> rotationValue_y = new List<float>();
            for(int i = 0; i < (int)jsonData["MapDataInfo"][0]["DirectionNum"]; ++i)
            {
                rotationValue_y.Add((float)jsonData["MapDataInfo"][0]["Direction"][i]);
            }

            //�X�e�[�W�����̂ɕK�v�ȏ���ݒ肷��
            classGetData.SetStageData(layerWidth, layerHeight, layerNumber, mapData, gimmickID, bootObjectID, dictionaryKey, dictionaryID, rotationValue_y);
        }
    }
}