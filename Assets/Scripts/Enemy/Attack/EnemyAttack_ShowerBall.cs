using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_ShowerBall : AttackBase
{
    //�U������
    public override bool Attack()
    {
        attackCount++;

        //�~�点��ʂ̐������߁A��������
        if(attackCount == 1)
        {

        }

        //1�b��ɑҋ@��Ԃɖ߂�
        if(attackCount == GameManager.gameFPS)
        {
            return true;
        }

        return false;
    }
}
