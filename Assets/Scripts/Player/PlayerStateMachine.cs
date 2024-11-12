using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface PlayerStateMachine
{
    void Initialize();
    void Think();
    void Move();
}