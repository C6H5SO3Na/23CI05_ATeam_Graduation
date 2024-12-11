using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack_ShowerBall : AttackBase
{
    //UŒ‚ˆ—
    public override bool Attack()
    {
        attackCount++;

        //~‚ç‚¹‚é‹Ê‚Ì”‚ğŒˆ‚ßA¶¬‚·‚é
        if(attackCount == 1)
        {

        }

        //1•bŒã‚É‘Ò‹@ó‘Ô‚É–ß‚é
        if(attackCount == GameManager.gameFPS)
        {
            return true;
        }

        return false;
    }
}
