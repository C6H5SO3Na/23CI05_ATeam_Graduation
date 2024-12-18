using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartOtherObjectProcess_StepOn : MonoBehaviour, ISetGimmickInstance
{
    GameObject targetObject;    // 処理を行わせるオブジェクト
    bool isOnce = false;        // 一度だけしか押せないか(処理しないか)決める
    bool isPressed;             // 押されたかを記憶する
    public string id;

    void Start()
    {
        //値の初期化
        isPressed = false;
    }

    //感圧板を押したとき
    void OnTriggerEnter(Collider other)
    {
        if(!isPressed)
        {
            //ボックスコライダーのみに反応する
            if (other is BoxCollider)
            {
                //プレイヤーか投擲物に反応する
                if (other.CompareTag("Player") || other.CompareTag("ThrowingObject"))
                {
                    if (targetObject)
                    {
                        //targetObjectが起動される動作を実装しているか確認する
                        IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                        if (objectHavingStartedOperation != null)
                        {
                            //実装している「感圧板を押したとき」の処理をさせる
                            objectHavingStartedOperation.ProcessWhenPressed();
                            
                            //一度しか押せない場合
                            if (isOnce)
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
                }
            }
        }
    }

    //感圧板から離れたとき
    void OnTriggerExit(Collider other)
    {
        if (!isPressed)
        {
            //ボックスコライダーのみに反応する
            if (other is BoxCollider)
            {
                //プレイヤーか投擲物に反応する
                if (other.CompareTag("Player") || other.CompareTag("ThrowingObject"))
                {
                    if (targetObject)
                    {
                        //targetObjectが起動される動作を実装しているか確認する
                        IStartedOperation objectHavingStartedOperation = targetObject.GetComponent<IStartedOperation>();
                        if (objectHavingStartedOperation != null)
                        {
                            //実装している「感圧板から離れたとき」の処理をさせる
                            objectHavingStartedOperation.ProcessWhenStopped();
                        }
                        else
                        {
                            Debug.LogWarning($"{targetObject.name}は起動される処理が実装されていません");
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 一度しか押せないかどうか決める関数
    /// </summary>
    /// <param name="isOnceValue"> 一度しか押せない場合true、何度でも押せる場合false </param>
    public void SetIsOnce(bool isOnceValue)
    {
        isOnce = isOnceValue;
    }

    public void SetGimmickInstance(GameObject targetObject)
    {
        this.targetObject = targetObject;
    }
}
