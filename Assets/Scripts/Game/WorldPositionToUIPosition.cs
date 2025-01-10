using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionToUIPosition : MonoBehaviour
{
    RectTransform rectTransform;
    [SerializeField] GameObject playerInstance;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        // �I�u�W�F�N�g�̃��[���h���W
        Vector3 worldPosition = playerInstance.transform.position;


        // ���[���h���W���X�N���[�����W�ɕϊ�
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        // UI�G�������g�̈ʒu���X�V
        this.rectTransform.position = screenPosition;

    }
}
