using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOtherObjectProcess_StepOn : MonoBehaviour
{
    [SerializeField]
    private GameObject targetObject;    // 処理を行わせるオブジェクト

    private bool isPressed = false;     // 押されたかを記憶する

    private void OnTriggerStay(Collider other)
    {
        //一度も押されていなかったら処理を行う
        if(!isPressed)
        {
            if (!other.CompareTag("Enemy"))
            {
                if (targetObject)
                {
                    //targetObjectが起動される動作を実装しているか確認する
                    IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                    if (objectHavingStartedOperation != null)
                    {
                        //実装している処理をさせる
                        if(objectHavingStartedOperation.StartedOperation())
                        {
                            //処理が行われたら、押されたことを記憶する
                            isPressed = true;
                        }
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
}
