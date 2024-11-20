using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOtherObjectProcess_StepOn : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;    // 処理を行わせるオブジェクト

    private void OnTriggerStay(Collider other)
    {
        if(!other.CompareTag("Enemy"))
        {
            if (targetObject)
            {
                //targetObjectが起動される動作を実装しているか確認する
                IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                if (objectHavingStartedOperation != null)
                {
                    //実装している処理をさせる
                    objectHavingStartedOperation.StartedOperation();
                }
                else
                {
                    Debug.LogWarning($"{targetObject.name}は起動される処理が実装されていません");
                }
            }
            else
            {
                Debug.LogWarning("処理を行わせるオブジェクトが指定されていません");
            }
        }
    }
}
