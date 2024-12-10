using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOtherObjectProcess_StepOn : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;    // �������s�킹��I�u�W�F�N�g

    private bool isPressed = false;     // �����ꂽ�����L������

    private void OnTriggerStay(Collider other)
    {
        //��x��������Ă��Ȃ������珈�����s��
        if(!isPressed)
        {
            if (!other.CompareTag("Enemy"))
            {
                if (targetObject)
                {
                    //targetObject���N������铮����������Ă��邩�m�F����
                    IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                    if (objectHavingStartedOperation != null)
                    {
                        //�������Ă��鏈����������
                        if(objectHavingStartedOperation.StartedOperation())
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
                else
                {
                    Debug.LogWarning("�������s�킹��I�u�W�F�N�g���w�肳��Ă��܂���");
                }
            }
        }
    }
}
