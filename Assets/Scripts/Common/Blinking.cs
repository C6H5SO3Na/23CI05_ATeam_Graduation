using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    Image image;
    TextMeshProUGUI text;
    Color tmpColor;
    int sign;
    int Sign //ïÑçÜ
    {
        set { sign = Mathf.Clamp(value, -1, 1); }
        get { return sign; }
    }
    public float blinkSpeed;
    [SerializeField] float maxA;
    void Start()
    {
        //ImageÇ≈Ç»Ç©Ç¡ÇΩÇÁTMPro
        if (!TryGetComponent(out image))
        {
            text = GetComponent<TextMeshProUGUI>();
        }

        Sign = -1;
    }

    void Update()
    {
        tmpColor = TryGetComponent(out image) ? tmpColor = image.color : tmpColor = text.color;

        tmpColor.a = Mathf.Clamp(tmpColor.a, -0.01f, 1.01f);
        if (!IsWithinRangeExclusive(tmpColor.a, 0f, maxA))
        {
            ChangeSign();
        }
        tmpColor.a += blinkSpeed * maxA * Time.unscaledDeltaTime * Sign;
        if(image != null)
        {
            image.color = tmpColor;
        }
        else
        {
            text.color = tmpColor;
        }
    }

    void ChangeSign()
    {
        Sign = -Sign;
    }

    bool IsWithinRangeExclusive(float value, float min, float max)
    {
        if (min > max)//ì¸ÇÍë÷Ç¶
        {
            (max, min) = (min, max);
        }
        return min <= value && max >= value;
    }
}
