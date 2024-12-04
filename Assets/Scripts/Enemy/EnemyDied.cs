using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDied
{
    /// <summary>
    /// €–Sˆ—
    /// </summary>
    /// <param name="dieEnemy"> €–S‚·‚é“G </param>
    public void Died(Enemy dieEnemy)
    {
        Object.Destroy(dieEnemy.gameObject);
    }
}
