using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Stage1EnemyStateBase : MonoBehaviour
{
    //ŠÖ”
    /// <summary>
    /// ó‘Ô‘JˆÚ‚Ìˆ—
    /// </summary>
    public abstract void StateTransition(Stage1EnemyStateBase nowState);

    /// <summary>
    /// ó‘Ô–ˆ‚Ìs“®ˆ—
    /// </summary>
    public abstract void ActProcessingEachState(in Stage1EnemyStateBase nowState);
}
