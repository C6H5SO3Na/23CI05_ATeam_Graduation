using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBoot_PressurePlate : BootObjectBase, ISetGimmickInstance
{
    //�ϐ�
    List<GameObject> targetObjects = new List<GameObject>(); // �������s�킹��I�u�W�F�N�g
    bool isOnce = false;            // ��x�������������Ȃ���(�������Ȃ���)���߂�
    bool isPressed;                 // �����ꂽ�����L������

    void Start()
    {
        //�l�̏�����
        isPressed = false;
    }

    //�������������Ƃ�
    void OnTriggerStay(Collider other)
    {
        if(!isPressed)
        {
            //�{�b�N�X�R���C�_�[�݂̂ɔ�������
            if (other is BoxCollider)
            {
                //�v���C���[���������ɔ�������
                if (other.CompareTag("Player") || other.CompareTag("ThrowingObject"))
                {
                    foreach (GameObject targetObject in targetObjects)
                    {
                        if (targetObject)
                        {
                            //targetObject���N������铮����������Ă��邩�m�F����
                            IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                            if (objectHavingStartedOperation != null)
                            {
                                //�������Ă���u�������������Ƃ��v�̏�����������
                                objectHavingStartedOperation.ProcessWhenPressed();

                                //��x���������Ȃ��ꍇ
                                if (isOnce)
                                {
                                    //�������s��ꂽ��A�����ꂽ���Ƃ��L������
                                    isPressed = true;
                                }
                            }
                            else
                            {
                                Debug.LogWarning($"{targetObject.name}�͋N������鏈������������Ă��܂���");
                            }
                        }
                    }
                }
            }
        }
    }

    //�������痣�ꂽ�Ƃ�
    void OnTriggerExit(Collider other)
    {
        if (!isPressed)
        {
            //�{�b�N�X�R���C�_�[�݂̂ɔ�������
            if (other is BoxCollider)
            {
                //�v���C���[���������ɔ�������
                if (other.CompareTag("Player") || other.CompareTag("ThrowingObject"))
                {
                    foreach(GameObject targetObject in targetObjects)
                    {
                        if (targetObject)
                        {
                            //targetObject���N������铮����������Ă��邩�m�F����
                            IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                            if (objectHavingStartedOperation != null)
                            {
                                //�������Ă���u�������痣�ꂽ�Ƃ��v�̏�����������
                                objectHavingStartedOperation.ProcessWhenStopped();
                            }
                            else
                            {
                                Debug.LogWarning($"{targetObject.name}�͋N������鏈������������Ă��܂���");
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ��x���������Ȃ����ǂ������߂�֐�
    /// </summary>
    /// <param name="isOnceValue"> ��x���������Ȃ��ꍇtrue�A���x�ł�������ꍇfalse </param>
    public void SetIsOnce(bool isOnceValue)
    {
        isOnce = isOnceValue;
    }

    public void SetGimmickInstance(GameObject targetObject)
    {
        targetObjects.Add(targetObject);
    }
}
