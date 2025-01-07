using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Blinking : MonoBehaviour
{
    Image image;
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
        image = GetComponent<Image>();
        Sign = -1;
    }

    void Update()
    {
        tmpColor = image.color;
        if (!IsWithinRangeExclusive(tmpColor.a, 0f, maxA))
        {
            ChangeSign();
        }
        tmpColor.a += blinkSpeed * maxA * Time.deltaTime * Sign;
        image.color = tmpColor;
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
