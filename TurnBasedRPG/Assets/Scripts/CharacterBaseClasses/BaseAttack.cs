using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础攻击方式
/// </summary>
[Serializable]
public class BaseAttack
{
    /// <summary>
    /// 名称
    /// </summary>
    [Tooltip("名称")]
    public string attackName;

    /// <summary>
    /// 攻击描述
    /// </summary>
    [Tooltip("攻击描述")]
    public string attackDescription;

    /// <summary>
    /// 伤害
    /// </summary>
    [Tooltip("伤害")]
    public float attackDamage;//伤害 Base Damage 15，mellee LvL 10 stamina 35 = basedmg + stamina + LvL = 60

    /// <summary>
    /// 法力值消耗
    /// </summary>
    [Tooltip("法力值消耗")]
    public float attackCost;//ManaCost 法力值消耗
}
