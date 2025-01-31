using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TransparencyAndSubstantiation : GimmickBase, IStartedOperation
{
    Material material;      // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Material��ێ�����
    Collider thisCollider;  // ���̃X�N���v�g���A�^�b�`���Ă���I�u�W�F�N�g��Collider��ێ�����

    [SerializeField]
    bool isTransparentize;  // ���������邩
    float alpha;            // ���l(0�`1�͈̔�)

    static bool isPressed;         // �����ꂽ���ǂ���
    static bool isLeft;            // ���ꂽ���ǂ���

    AudioSource audioSource;// AudioSource
    TransparencyBlockSE SE; // SE

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

        //AudioSource���擾����
        audioSource = GameObject.FindGameObjectWithTag("SE").GetComponent<AudioSource>();

        //SE���擾����
        SE = GetComponent<TransparencyBlockSE>();

        //������ԂŊJ�n����Ƃ�
        if (isTransparentize)
        {
            //����������
            Transparency();
        }
    }

    //�������A�܂��͎��̉�����@�@�@
    public void ProcessWhenPressed()
    {
        //��x���������犴���������������Ă���ꍇ�͉����̂���߂�܂ŏ������Ȃ�
        if (HasRunningOnce())
        {
            //���������Ă���Ƃ�
            if (isTransparentize)
            {
                Substantiation();
                if (!isPressed)
                {
                    //SE���Đ�
                    audioSource.PlayOneShot(SE.appearSE);
                    isLeft = false;
                    isPressed = true;
                }
            }
            //���������Ă��Ȃ��Ƃ�
            else
            {
                //����������
                Transparency();

                if (!isPressed)
                {
                    //SE���Đ�
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

    //�������A�܂��͎��̉�����
    public void ProcessWhenStopped()
    {
        //���������Ă���Ƃ�
        if (isTransparentize)
        {
            Substantiation();

            if (!isLeft)
            {
                //SE���Đ�
                audioSource.PlayOneShot(SE.appearSE);
                isLeft = true;
            }
        }
        //���������Ă��Ȃ��Ƃ�
        else
        {
            //����������
            Transparency();

            if (!isLeft)
            {
                //SE���Đ�
                audioSource.PlayOneShot(SE.disappearSE);
                isLeft = true;
            }
        }

        //�܂������Ȃǂ���������M�~�b�N���N���ł���悤�ɂ���
        MakeToLaunchable();
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
