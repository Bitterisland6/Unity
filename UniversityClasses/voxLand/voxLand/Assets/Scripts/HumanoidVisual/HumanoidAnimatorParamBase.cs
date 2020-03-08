using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class HumanoidAnimatorParamBase : MonoBehaviour
{
    //speed relative to player facing direction, x = 1, y = 0 walking forward, x = 0, y = 1 walking left
    /// <summary>
    /// speed relative to player facing direction, Vector3.x = 1 => walk forward, Vector3.z = 1 => walk left
    /// </summary>
    /// <returns></returns>
    abstract public Vector3 MoveSpeed();
    /// <summary>
    /// whether is humanoid grounded or not
    /// </summary>
    /// <returns></returns>
    abstract public bool IsGrounded();
    /// <summary>
    /// whether is humanoid rolling or not
    /// </summary>
    /// <returns></returns>
    abstract public bool IsRolling();
    abstract public int LightAttack();

    abstract public bool LandAttack();
}
