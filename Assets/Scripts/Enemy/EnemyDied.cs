using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDied
{
    /// <summary>
    /// ���S����
    /// </summary>
    /// <param name="dieEnemy"> ���S����G </param>
    public void Died(Enemy dieEnemy)
    {
        Object.Destroy(dieEnemy.gameObject);
    }
}
