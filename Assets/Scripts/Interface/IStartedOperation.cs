using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStartedOperation
{
    /// <summary>
    /// �{�^�������������Ƃ��ɌĂ΂�鏈��
    /// </summary>
    void ProcessWhenPressed();

    /// <summary>
    /// �{�^�����������̂���߂��Ƃ��ɌĂ΂�鏈��
    /// </summary>
    void ProcessWhenStopped();
}
