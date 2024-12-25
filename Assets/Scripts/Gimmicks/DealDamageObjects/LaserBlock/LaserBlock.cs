using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserBlock : DealDamageObjectBase, IStartedOperation
{
    //�ϐ�
    float maxDistance = 100f;                   // ���[�U�[�̍ő勗��
    [SerializeField] LayerMask collisionMask;   // �Փ˂��郌�C���[�}�X�N
    LineRenderer lineRenderer;                  // ���[�U�[�̌����ڕύX�p
    bool shouldToPutLaser = true;               // ���[�U�[���o����
    Vector3 laserStartPoint;                    // ���[�U�[�̊J�n�n�_
    Vector3 laserDirection;                     // ���[�U�[�̔��˕���

    // Start is called before the first frame update
    void Start()
    {
        //LineRenderer�̎擾
        lineRenderer = GetComponent<LineRenderer>();
        //ray�̌����ڕύX
        if(lineRenderer)
        {
            lineRenderer.startWidth = 0.5f;
            lineRenderer.endWidth = 0.5f;
        }

        //�^����_���[�W�ʂ̐ݒ�
        SetDealingDamageQuantity(3);

        //���[�U�[�̊J�n�n�_�ƕ�����ݒ�
        laserStartPoint = transform.position;
        laserDirection = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //���[�U�[���o��������
        if(shouldToPutLaser)
        {
            FireLaser();
        }
    }

    //���[�U�[�̔���
    void FireLaser()
    {
        //Raycast�ŏՓ˔��������
        RaycastHit hit;
        if(Physics.Raycast(laserStartPoint, laserDirection, out hit, maxDistance, collisionMask))
        {
            //�����ɏՓ˂����ꍇ�A�Փ˂����ꏊ�܂Ń��[�U�[��`�悷��
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, laserStartPoint);
                lineRenderer.SetPosition(1, hit.point);
            }

            //�Փ˂����̂��v���C���[��������_���[�W��^����
            if (hit.collider.CompareTag("Player"))
            {
                //�_���[�W��^����
                DealDamage(hit.collider);
            }
        }
        else
        {
            //�Փ˂�����������ő勗���܂ł̃��[�U�[��`��
            if (lineRenderer)
            {
                lineRenderer.SetPosition(0, laserStartPoint);
                lineRenderer.SetPosition(1, laserStartPoint + laserDirection * maxDistance);
            }
        }
    }

    public void ProcessWhenPressed()
    {
        //���[�U�[�̔��˂���߂�
        shouldToPutLaser = false;

        if(lineRenderer)
        {
            lineRenderer.enabled = false;
        }
    }

    public void ProcessWhenStopped()
    {
        //���[�U�[�𔭎˂���
        shouldToPutLaser = true;

        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
    }
}
