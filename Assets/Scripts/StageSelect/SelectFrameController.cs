using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SelectImageManager;

public class SelectFrameController : MonoBehaviour
{
    int selectNumX = 0;
    //プロパティ
    public int SelectNumX
    {
        private set
        {
            selectNumX = value;
        }
        get
        {
            return selectNumX;
        }
    }
    int selectNumY = 0;

    //プロパティ
    public int SelectNumY
    {
        private set
        {
            selectNumY = value;
        }
        get
        {
            return selectNumY;
        }
    }
    RectTransform rectTransform;
    [SerializeField] int maxSelectX;
    [SerializeField] int maxSelectY;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    int Wrap(int value, int low, int high)
    {
        int n = (value - low) % (high - low);
        return (n >= 0) ? (n + low) : (n + high);
    }

    public void Move(float signX, float signY)
    {
        if (signX != 0f)
        {
            selectNumX += (int)Mathf.Sign(signX);
            selectNumX = Wrap(selectNumX, 0, maxSelectX);
        }

        if (signY != 0f)
        {
            selectNumY -= (int)Mathf.Sign(signY);
            selectNumY = Wrap(selectNumY, 0, maxSelectY);
        }
        rectTransform.DOAnchorPos(new Vector3(450 * selectNumX - 675, 200 - 400 * selectNumY), 0.5f);//.SetEase(Ease.InOutSine);
    }
}
