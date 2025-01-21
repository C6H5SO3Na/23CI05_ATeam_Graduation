using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmBuriedWall : MonoBehaviour
{
    public bool isTouchingWall { private set; get; }    // �ǂɐG��Ă��邩�ǂ���
    public Vector3 hitPosition { private set; get; }    // �Փ˂����ꏊ

    // Start is called before the first frame update
    void Start()
    {
        isTouchingWall = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        //�ǂɐG��Ă��邱�Ƃ��L������
        isTouchingWall = true;

        //�Փˈʒu�̎擾
        //hitPosition = other.
    }

    void OnTriggerExit(Collider other)
    {
        //�ǂ��痣�ꂽ���Ƃ��L������
        isTouchingWall = false;
    }


}
