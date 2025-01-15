using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldPositionToUIPosition : MonoBehaviour
{
    RectTransform rectTransform;
    PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        player = GetComponent<PlayerUIManager>().GetInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }
        //�I�u�W�F�N�g�̃��[���h���W
        Vector3 worldPosition = player.transform.position;

        //���[���h���W���X�N���[�����W�ɕϊ�
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

        //UI�̈ʒu���X�V
        this.rectTransform.position = screenPosition;

    }
}
