using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushOutFromWall : MonoBehaviour
{
    ConfirmBuriedWall wallChecker;

    // Start is called before the first frame update
    void Start()
    {
        //�q�I�u�W�F�N�g��WallChecker�����邩�m�F����(WallChecker�������Ƃ��g�p�s��)
        wallChecker = GetComponentInChildren<ConfirmBuriedWall>();
        if(!wallChecker)
        {
            Debug.LogWarning("WallChecker���q�I�u�W�F�N�g�ɂ���܂���");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(wallChecker)
        {
            //�ǂɖ��܂��Ă�����A�ǂ��牟���o��
            if(wallChecker.isTouchingWall)
            {
                //�Փˈʒu�̎擾
                //Vector3 hitPosition = 
            }
        }
    }
}
