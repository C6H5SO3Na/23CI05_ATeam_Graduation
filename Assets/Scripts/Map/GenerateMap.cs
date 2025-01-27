using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    //-------------------------------------------------------------------------------
    // �R���X�g���N�^
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
    // �ϐ�
    //-------------------------------------------------------------------------------
    [SerializeField]
    List<GameObject> mapPrefab;   // ���ɂȂ�u���b�N�̃v���n�u

    Dictionary<int, GameObject> mapPrefabDictionary;  // �l�Ə��̃v���n�u�̕R�Â�(�������̏����𕪂���₷�����邽��)

    public GameObject player1Instance { get; private set; }             // �v���C���[1�̃C���X�^���X    
    public GameObject player2Instance { get; private set; }             // �v���C���[2�̃C���X�^���X
    public GameObject enemyInstance { get; private set; }               // �G�̃C���X�^���X            
    public GameObject goalInstance { get; private set; }                // �S�[���̃C���X�^���X
    public List<GameObject> startUpObjectInstances { get; private set; } // �M�~�b�N���N��������I�u�W�F�N�g�̃C���X�^���X
    public List<GameObject> startGimmickInstances { get; private set; } // �M�~�b�N�̃C���X�^���X

    int layerWidth = 20 + 2;                    // �w�̉���(���ɂł���͈� + �ǂ��쐬���镔��)
    int layerHeight = 20 + 2;                   // �w�̏c��(���ɂł���͈� + �ǂ��쐬���镔��)
    int layerNumber = 6;                        // �w�̐�
    List<List<List<int>>> mapData;              // ���̃}�b�v�f�[�^
    List<int> gimmickID;                        // �M�~�b�Nid���i�[����
    List<int> bootObjectID;                     // �M�~�b�N�N���I�u�W�F�N�gid���i�[����
    Dictionary<int, int> gimmickAssociationID;  // �M�~�b�N���N���I�u�W�F�N�g�ɕR�t���邽�߂�id���i�[����    
    List<int> rotationValue_y;                  // �M�~�b�N��y����]�̒l���i�[����
    List<int> gimmickPowerIndexs;               // �M�~�b�N�̋��������߂�������i�[����

    //-------------------------------------------------------------------------------
    // �֐�
    //-------------------------------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        //json�t�@�C����ǂݎ��N���X���擾
        LoadJsonFile_Map loadJsonFile = GetComponent<LoadJsonFile_Map>();
        if(loadJsonFile)
        {
            //�X�e�[�W�����擾����

            //json�t�@�C����ǂݍ���Ńf�[�^���擾����
            loadJsonFile.GetStageData(AppManager.StageName, this);
        }

        //�l�̏����l�ݒ�
        for(int i = 0; i < mapPrefab.Count; ++i)
        {
            mapPrefabDictionary[i + 1] = mapPrefab[i];  // i + 1�̓}�b�v�f�[�^��0���󔒂ɂ��邽��
        }

        //�}�b�v�̐���
        Generate();

        //�C���X�^���X��n���N���X���擾
        PassInstance passInstance = GetComponent<PassInstance>();
        if(passInstance)
        {
            //�C���X�^���X�𑼃N���X�ɓn��
            passInstance.PassInstanceToOtherClass(player1Instance, player2Instance, enemyInstance, goalInstance);

            //�M�~�b�N�̃C���X�^���X���M�~�b�N���N������N���X�ɓn��
            passInstance.PassInstanceStartGimmick(startUpObjectInstances, startGimmickInstances, gimmickAssociationID);
        }
    }

    /// <summary>
    /// �}�b�v�̐���
    /// </summary>
    void Generate()
    {
        //�}�b�v�����������z��̗v�f�ɃA�N�Z�X���邽�߂̕ϐ��錾
        int rotationValue_yIndex = 0;   // y����]�̒l���i�[�����z��ɂ̗v�f�ɃA�N�Z�X���邽�߂̒l

        //�}�b�v����
        for (int y = 0; y < layerNumber; ++y)
        {
            for (int z = 0; z < layerHeight; ++z)
            {
                for (int x = 0; x < layerWidth; ++x)
                {
                    //�}�b�v�f�[�^�����݂��Ȃ��v���n�u���w�肵�Ă��Ȃ������琶������
                    if (mapPrefabDictionary.TryGetValue(mapData[y][z][x], out GameObject prefab))
                    {
                        //�z�u����ʒu��ݒ�
                        Vector3 position = new Vector3(x, y, (layerHeight - 1) - z);    // layerHeight - 1��mapData�̌`�ʂ�Ƀ}�b�v����邽�߂ɕK�v

                        switch (mapData[y][z][x])
                        {
                            case 2:     // �v���C���[1�𐶐�����
                                if (player1Instance == null)
                                {
                                    player1Instance = Instantiate(prefab, position, Quaternion.identity);

                                    //�v���C���[���ʔԍ���1�ɐݒ肷��
                                    player1Instance.GetComponent<PlayerController>().OriginalPlayerNum = 1;
                                }
                                else
                                {
                                    Debug.LogWarning("�v���C���[1�͂���ȏ㐶���ł��܂���");
                                }
                                break;

                            case 3:     // �v���C���[2�𐶐�����
                                if (player2Instance == null)
                                {
                                    player2Instance = Instantiate(prefab, position, Quaternion.identity);

                                    //�v���C���[���ʔԍ���2�ɐݒ肷��
                                    player2Instance.GetComponent<PlayerController>().OriginalPlayerNum = 2;
                                }
                                else
                                {
                                    Debug.LogWarning("�v���C���[2�͂���ȏ㐶���ł��܂���");
                                }
                                break;

                            case 4:     // �G�𐶐�����
                                enemyInstance = Instantiate(prefab, position, Quaternion.identity);
                                break;

                            case 5:     // �����𐶐�����
                                position = new Vector3(x, y - 0.4f, (layerHeight - 1) - z); // 0.5�������ƂŐ������ɏ��̏�ɐ��������
                                startUpObjectInstances.Add(Instantiate(prefab, position, Quaternion.identity));
                                break;

                            case 7:     // �S�[���𐶐�����
                                goalInstance = Instantiate(prefab, position, Quaternion.identity);
                                break;

                            case 11:    // ���[�U�[��y��90�x��]���Đ���
                                startGimmickInstances.Add(Instantiate(prefab, position, Quaternion.Euler(0, rotationValue_y[rotationValue_yIndex], 0)));
                                rotationValue_yIndex++;
                                break;

                            default:    // �v���C���[�A�G�A�S�[���A�����ȊO�̂��̂𐶐�����
                                GameObject generateObject = Instantiate(prefab, position, Quaternion.identity);

                                //���������I�u�W�F�N�g���M�~�b�N���N��������I�u�W�F�N�g��������N���I�u�W�F�N�g���X�g�Ɋi�[����
                                if (generateObject.GetComponent<ISetGimmickInstance>() != null)
                                {
                                    startUpObjectInstances.Add(generateObject);
                                }
                                //���������I�u�W�F�N�g���M�~�b�N��������M�~�b�N���X�g�Ɋi�[����
                                else if(generateObject.GetComponent<IStartedOperation>() != null)
                                {
                                    startGimmickInstances.Add(generateObject);
                                }
                                break;
                        }
                    }
                    else if (mapData[y][z][x] == 0)
                    {
                        //mapData��0�̎��ADebug.LogWarning���Ăяo���Ȃ��悤�ɂ��Ă���
                        continue;
                    }
                    else
                    {
                        Debug.LogWarning("�����ݒu����܂���B�Ӑ}���Ȃ��ꍇ��mapData���m�F���Ă�������");
                    }
                }
            }
        }

        //���������M�~�b�N��id��ݒ肷��
        for (int i = 0; i < gimmickID.Count; ++i)
        {
            //�C���X�^���X����id�ݒ�
            startGimmickInstances[i].GetComponent<GimmickBase>().SetID(gimmickID[i]);
            
            
        }

        //�M�~�b�N���N������I�u�W�F�N�g��id��ݒ肷��
        for (int i = 0; i < bootObjectID.Count; ++i)
        {
            //�C���X�^���X����id�ݒ�
            startUpObjectInstances[i].GetComponent<BootObjectBase>().SetID(bootObjectID[i]);
        }
    }

    /// <summary>
    /// �X�e�[�W�𐶐�����̂ɕK�v�ȏ���ݒ肷��
    /// </summary>
    /// <param name="layerWidth"> �w�̉��� </param>
    /// <param name="layerHeight"> �w�̏c�� </param>
    /// <param name="layerNumber"> �w�̐� </param>
    /// <param name="mapData"> �}�b�v�f�[�^ </param>
    /// <param name="gimmickID"> �M�~�b�N���ʗpid </param>
    /// <param name="bootObjectID"> �M�~�b�N�N���I�u�W�F�N�g���ʗpid </param>
    /// <param name="gimmickAssociationKey"> �M�~�b�N���N���I�u�W�F�N�g�ɕR�Â��邽�߂�key </param>
    /// <param name="gimmickAssociationID"> �M�~�b�N���N���I�u�W�F�N�g�ɕR�Â��邽�߂�id </param>
    /// <param name="rotationValue_y"> �M�~�b�N��y����]�̒l </param>
    /// <param name="gimmickPowerIndexs"> �M�~�b�N�̋��������߂�l </param>
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
