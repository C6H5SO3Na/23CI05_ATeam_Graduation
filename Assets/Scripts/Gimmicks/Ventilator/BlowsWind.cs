using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowsWind : GimmickBase, IStartedOperation
{
    //-------------------------------------------------------------------------------
    // �ϐ�
    //-------------------------------------------------------------------------------
    float[] movePowers = {0.1f, 0.5f, 1f};              // �I�u�W�F�N�g�𓮂������̗� �������A���A���̎O���
    float[] jumpPowers = { 1f, 2f, 3f };                // �v���C���[�̃W�����v�͂𑝂₷���̗� �������A���A���̎O���
    int     windPowersIndex = 0;                        // ���̗͂̋�������߂�l
    bool    shouldProcessGimmick;                       // ���𐁂����邩
    Vector3 directionBlowsWind;                         // ������������
    float   playerAddedMoveForceCorrectionValue = 1.5f; // �v���C���[�ɕ���������Ƃ��̕��̋����̕␳�l
    [SerializeField] 
    bool    shouldIncreaseJumpPower;                    // �v���C���[�̃W�����v�͂𑝂₷��(true�̏ꍇ�̓W�����v�͂��グ�����ɕ��ňړ����Ȃ��Ȃ�)
    [SerializeField]
    bool    isBlowsWindToFirst;                         // �ŏ����畗�𐁂����邩
    ParticleSystem particle;
    AudioSource audioSource;
    VentilatorSE SE;
    //-------------------------------------------------------------------------------
    // �֐�
    //-------------------------------------------------------------------------------
    void Awake()
    {
        particle = transform.GetChild(0).GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        SE = GetComponent<VentilatorSE>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //�ŏ����畗�𐁂�����
        if(isBlowsWindToFirst)
        {
            shouldProcessGimmick = true;
        }
        //�ŏ����畗�𐁂����Ȃ�
        else
        {
            shouldProcessGimmick = false;
        }

        //���������������擾
        directionBlowsWind = transform.forward;
    }

    void OnTriggerStay(Collider other)
    {
        //�W�����v�͂𑝂₷�ꍇ
        if(shouldIncreaseJumpPower)
        {
            AddPlayerJumpPower(other);
        }
        //�I�u�W�F�N�g�𓮂����ꍇ
        else
        {
            //�I�u�W�F�N�g�𓮂����l��"���œ����I�u�W�F�N�g"�ɓn��
            if (shouldProcessGimmick)
            {
                //����"���œ����I�u�W�F�N�g"�ɓ������Ă�����A�I�u�W�F�N�g���ړ������鏈��
                MoveObject(other);
            }
            else
            {
                //���̓����蔻�������
                AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                if(windCollider)
                {
                    //���̓����蔻�������
                    windCollider.MakeAddColliderSizeToZero();

                    //�G�t�F�N�g���~�߂�
                    //particle.Stop();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //�W�����v�͂𑝂₷�ꍇ
        if(shouldIncreaseJumpPower)
        {
            RestorePlayerJumpPower(other);
        }
        //�I�u�W�F�N�g�𓮂����ꍇ
        else
        {
            //����"���œ����I�u�W�F�N�g"�ɓ�����Ȃ��Ȃ�����A�I�u�W�F�N�g���ړ������Ȃ��悤�ɂ��鏈��
            StopMoveObject(other);
        }
    }

    public void ProcessWhenPressed()
    {
        //��x���������犴���������������Ă���ꍇ�͉����̂���߂�܂ŏ������Ȃ��悤�ɂ���
        if (HasRunningOnce())
        {
            //�W�����v�͂��グ��ꍇ
            if (shouldIncreaseJumpPower)
            {
                //���̗͂�ω�������
                if (windPowersIndex < jumpPowers.Length - 1)
                {
                    windPowersIndex++;
                }
                else
                {
                    windPowersIndex = 0;
                }
            }
            //�I�u�W�F�N�g���ړ�������ꍇ
            else
            {
                //�ŏ��ɕ��������Ă���ꍇ�A���������Ȃ��悤�ɂ���(���ɓ��������I�u�W�F�N�g�𓮂����Ȃ��悤�ɂ���)
                if (isBlowsWindToFirst)
                {
                    shouldProcessGimmick = false;
                }
                //�ŏ��ɕ��������Ă��Ȃ��ꍇ�A���������悤�ɂ���(���ɓ��������I�u�W�F�N�g�𓮂��悤�ɂ���)
                else
                {
                    shouldProcessGimmick = true;

                    //���̓����蔻����Đ�������
                    AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                    if (windCollider)
                    {
                        windCollider.RestartRaycast();
                    }
                }
            }
        }
    }

    public void ProcessWhenStopped()
    {
        //�I�u�W�F�N�g���ړ�������ꍇ
        if (!shouldIncreaseJumpPower)
        {
            //�ŏ��ɕ��������Ă����ꍇ�A�܂����������悤�ɂ���(���ɓ��������I�u�W�F�N�g�𓮂��悤�ɂ���)
            if(isBlowsWindToFirst)
            {
                shouldProcessGimmick = true;

                //���̓����蔻����Đ�������
                AddColliderUpToPositionToCrashedWall windCollider = GetComponent<AddColliderUpToPositionToCrashedWall>();
                if (windCollider)
                {
                    windCollider.RestartRaycast();
                }
            }
            //�ŏ��ɕ��������Ă��Ȃ��ꍇ�A�܂����������Ȃ��悤�ɂ���(���ɓ��������I�u�W�F�N�g�𓮂����Ȃ��悤�ɂ���)
            else
            {
                shouldProcessGimmick = false;
            }
        }
        
        //�܂������Ȃǂ���������M�~�b�N���N���ł���悤�ɂ���
        MakeToLaunchable();
    }

    /// <summary>
    /// �I�u�W�F�N�g�𕗂ňړ������鏈��
    /// </summary>
    /// <param name="other"> ���ɓ������Ă���I�u�W�F�N�g </param>
    void MoveObject(Collider other)
    {
        //���ɓ������Ă���̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��
                if (player.windPower.Max <= windPowersIndex)
                {
                    //���̐��������Ƌ������v�Z����
                    Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex] * playerAddedMoveForceCorrectionValue;

                    //���̗͂̒l�𕗂œ����I�u�W�F�N�g�ɓn��
                    player.windPower.Max = windPowersIndex;

                    //���ŃI�u�W�F�N�g�������悤�ɂ���
                    player.WindMoveDirection = windForce;
                }
            }
        }
        //�v���C���[�ȊO�����ɓ������Ă���ꍇ
        else
        {
            //���œ����I�u�W�F�N�g�����肷��
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //���̕��Ɠ����ɓ������Ă���ꍇ�A���̐��l����������D�悷��
                if (objectMoveByWind.ReceivingWindPower <= windPowersIndex)
                {
                    //���̐��������Ƌ������v�Z����
                    Vector3 windForce = directionBlowsWind * movePowers[windPowersIndex];

                    //���̗͂̒l�𕗂œ����I�u�W�F�N�g�ɓn��
                    objectMoveByWind.ReceivingWindPower = windPowersIndex;

                    //���ŃI�u�W�F�N�g�������悤�ɂ���
                    objectMoveByWind.WindForce = windForce;
                }
            }
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�𕗂ňړ������Ȃ����鏈��
    /// </summary>
    /// <param name="other"> ���ɓ������Ă���I�u�W�F�N�g </param>
    void StopMoveObject(Collider other)
    {
        //���ɓ������Ă����̂��v���C���[�̏ꍇ
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //�󂯂Ă��镗�̗͂̒l���Ȃ���
                player.windPower.Max = 0;

                //���Ńv���C���[�������Ȃ��悤�ɂ���
                player.WindMoveDirection = Vector3.zero;
            }
        }
        //�v���C���[�ȊO�����ɓ������Ă����ꍇ
        else
        {
            //���œ����I�u�W�F�N�g�����肷��
            ObjectsMoveByWind objectMoveByWind = other.GetComponent<ObjectsMoveByWind>();
            if (objectMoveByWind)
            {
                //�󂯂Ă��镗�̗͂̒l���Ȃ���
                objectMoveByWind.ReceivingWindPower = 0;

                //���ŃI�u�W�F�N�g�������Ȃ��悤�ɂ���
                objectMoveByWind.WindForce = Vector3.zero;
            }
        }
    }

    /// <summary>
    /// �v���C���[�̃W�����v�͂𑝂₷����
    /// </summary>
    void AddPlayerJumpPower(Collider other)
    {
        //�G��Ă���I�u�W�F�N�g���v���C���[���ǂ������肷��
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player)
            {
                //�W�����v�͂��グ��
                player.windPower.Received = jumpPowers[windPowersIndex];
            }
        }
    }
    /// <summary>
    /// �v���C���[�̃W�����v�͂����ɖ߂�����
    /// </summary>
    /// <param name="other"></param>
    void RestorePlayerJumpPower(Collider other)
    {
        //�G��Ă���I�u�W�F�N�g���v���C���[���ǂ������肷��
        if(other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if(player)
            {
                //�W�����v�͂����ɖ߂�
                player.windPower.Received = 0;
            }
        }
    }

    /// <summary>
    /// ���̋�����ݒ肷��
    /// </summary>
    /// <param name="index"> �����̐ݒ� 0 = ��A1 = ���A2 = �� </param>
    public void SetWindPowerIndex(int index)
    {
        //�W�����v�͂��グ��Ƃ�
        if(shouldIncreaseJumpPower)
        {
            //index��"0�`�W�����v�͂𑝂₷���̗͂̔z��̗v�f��-1"�͈̔͂̎��̂ݒl��ݒ肷��
            if (0 <= index && index <= jumpPowers.Length - 1)
            {
                windPowersIndex = index;

                //�p�[�e�B�N���̐ݒ�
                var main = particle.main;
                main.startSpeed = 10 + index * 8;
                particle.Play();

                //SE�Đ�
                audioSource.clip = SE.ventilatorSE[index];
                audioSource.Play();
            }
        }
        //�I�u�W�F�N�g���ړ�������Ƃ�
        else
        {
            //index��"0�`�I�u�W�F�N�g�𓮂������̗͂̔z��̗v�f��-1"�͈̔͂̎��̂ݒl��ݒ肷��
            if(0 <= index && index <= movePowers.Length - 1)
            {
                windPowersIndex = index;

                //�p�[�e�B�N���̐ݒ�
                var main = particle.main;
                main.startSpeed = 10 + index * 8;
                particle.Play();

                //SE�Đ�
                audioSource.clip = SE.ventilatorSE[index];
                audioSource.Play();
            }
        }
    }
}
