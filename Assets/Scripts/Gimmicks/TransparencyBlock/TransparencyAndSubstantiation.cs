using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TransparencyAndSubstantiation : MonoBehaviour, IStartedOperation
{
    Material material;      // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Material��ێ�����
    Collider thisCollider;  // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Collider��ێ�����

    [SerializeField]
    bool isTransparentize;  // ���������邩
    float alpha;            // ���l(0�`1�͈̔�)
    

    // Start is called before the first frame update
    void Start()
    {
        //Renderer���擾
        Renderer renderer = GetComponent<Renderer>();

        //Material���擾
        if (renderer)
        {
            material = renderer.material;
        }

        //Collider���擾����
        thisCollider = GetComponent<Collider>();

        //������ԂŊJ�n����Ƃ�
        if (isTransparentize)
        {
            //����������
            Transparency();
        }
    }

    //�������A�܂��͎��̉�����
    public void ProcessWhenPressed()
    {
        //���������Ă���Ƃ�
        if(isTransparentize)
        {
            Substantiation();
        }
        //���������Ă��Ȃ��Ƃ�
        else
        {
            //����������
            Transparency();
        }
    }

    //�������A�܂��͎��̉�����
    public void ProcessWhenStopped()
    {
        //���������Ă���Ƃ�
        if (isTransparentize)
        {
            Substantiation();
        }
        //���������Ă��Ȃ��Ƃ�
        else
        {
            //����������
            Transparency();
        }
    }

    /// <summary>
    /// ����������
    /// </summary>
    void Transparency()
    {
        //�����蔻����Ȃ���
        if(thisCollider)
        {
            //�R���C�_�[�𖳌�������
            thisCollider.enabled = false;
        }

        //�}�e���A���̕ύX
        if (material)
        {
            //���l�̐ݒ�
            SetAlpha(0.25f);

            //���݂̐F�����擾
            Color newColor = material.color;

            //�擾�����F�̃��l��ύX
            newColor.a = alpha;

            //�}�e���A���ɕύX�����F��K�p
            material.color = newColor;
        }

        //�������������Ƃ��L������
        isTransparentize = true;
    }

    /// <summary>
    /// ���̉�����
    /// </summary>
    void Substantiation()
    {
        //�����蔻�������
        if (thisCollider)
        {
            //�R���C�_�[��L��������
            thisCollider.enabled = true;
        }

        //�}�e���A���̕ύX
        if (material)
        {
            //���l�̐ݒ�
            SetAlpha(1f);

            //���݂̐F�����擾
            Color newColor = material.color;

            //�擾�����F�̃��l��ύX
            newColor.a = alpha;

            //�}�e���A���ɕύX�����F��K�p
            material.color = newColor;
        }

        //���̉��������Ƃ��L������
        isTransparentize = false;
    }

    /// <summary>
    /// ���l��setter
    /// </summary>
    /// <param name="SetValue"> �ݒ肷��l </param>
    void SetAlpha(float SetValue)
    {
        //alpha�ɓ���l��0�`1�ɐ�������
        alpha = Mathf.Clamp(SetValue, 0f, 1f);
    }
}
