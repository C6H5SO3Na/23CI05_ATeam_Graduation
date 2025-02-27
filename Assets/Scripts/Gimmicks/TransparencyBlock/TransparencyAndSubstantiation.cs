using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyAndSubstantiation : GimmickBase, IStartedOperation
{
    Material material;      // このスクリプトをアタッチしているオブジェクトのMaterialを保持する
    Collider thisCollider;  // このスクリプトをアタッチしているオブジェクトのColliderを保持する

    [SerializeField]
    bool isTransparentize;  // 透明化するか
    float alpha;            // α値(0〜1の範囲)

    static bool isPressed;         // 押されたかどうか
    static bool isLeft;            // 離れたかどうか

    AudioSource audioSource;// AudioSource
    TransparencyBlockSE SE; // SE

    // Start is called before the first frame update
    void Start()
    {
        //Rendererを取得
        Renderer renderer = GetComponent<Renderer>();

        //Materialを取得
        if (renderer)
        {
            material = renderer.material;
        }

        //Colliderを取得する
        thisCollider = GetComponent<Collider>();

        //AudioSourceを取得する
        audioSource = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //SEを取得する
        SE = GetComponent<TransparencyBlockSE>();

        //透明状態で開始するとき
        if (isTransparentize)
        {
            //透明化する
            Transparency();
        }
    }

    //透明化、または実体化する　　　
    public void ProcessWhenPressed()
    {
        //一度処理したら感圧板等を押し続けている場合は押すのをやめるまで処理しない
        if (HasRunningOnce())
        {
            //透明化しているとき
            if (isTransparentize)
            {
                Substantiation();
                if (!isPressed)
                {
                    //SEを再生
                    audioSource.PlayOneShot(SE.appearSE);
                    isLeft = false;
                    isPressed = true;
                }
            }
            //透明化していないとき
            else
            {
                //透明化する
                Transparency();

                if (!isPressed)
                {
                    //SEを再生
                    audioSource.PlayOneShot(SE.disappearSE);
                    isLeft = false;
                    isPressed = true;
                }
            }
        }
        else
        {
            isPressed = false;
        }
    }

    //透明化、または実体化する
    public void ProcessWhenStopped()
    {
        //透明化しているとき
        if (isTransparentize)
        {
            Substantiation();

            if (!isLeft)
            {
                //SEを再生
                audioSource.PlayOneShot(SE.appearSE);
                isLeft = true;
            }
        }
        //透明化していないとき
        else
        {
            //透明化する
            Transparency();

            if (!isLeft)
            {
                //SEを再生
                audioSource.PlayOneShot(SE.disappearSE);
                isLeft = true;
            }
        }

        //また感圧板などを押したらギミックを起動できるようにする
        MakeToLaunchable();
    }

    /// <summary>
    /// 透明化処理
    /// </summary>
    void Transparency()
    {
        //当たり判定をなくす
        if(thisCollider)
        {
            //コライダーを無効化する
            thisCollider.enabled = false;
        }

        //マテリアルの変更
        if (material)
        {
            //α値の設定
            SetAlpha(0.25f);

            //現在の色情報を取得
            Color newColor = material.color;

            //取得した色のα値を変更
            newColor.a = alpha;

            //マテリアルに変更した色を適用
            material.color = newColor;
        }

        //透明化したことを記憶する
        isTransparentize = true;
    }

    /// <summary>
    /// 実体化処理
    /// </summary>
    void Substantiation()
    {
        //当たり判定をつける
        if (thisCollider)
        {
            //コライダーを有効化する
            thisCollider.enabled = true;
        }

        //マテリアルの変更
        if (material)
        {
            //α値の設定
            SetAlpha(1f);

            //現在の色情報を取得
            Color newColor = material.color;

            //取得した色のα値を変更
            newColor.a = alpha;

            //マテリアルに変更した色を適用
            material.color = newColor;
        }

        //実体化したことを記憶する
        isTransparentize = false;
    }

    /// <summary>
    /// α値のsetter
    /// </summary>
    /// <param name="SetValue"> 設定する値 </param>
    void SetAlpha(float SetValue)
    {
        //alphaに入る値を0〜1に制限する
        alpha = Mathf.Clamp(SetValue, 0f, 1f);
    }
}
