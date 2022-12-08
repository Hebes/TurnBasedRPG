using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterClasses
{
    /// <summary>
    /// 名称
    /// </summary>
    [Tooltip("名称")]
    public string theName;

    /// <summary>
    /// 基础血量
    /// </summary>
    [Tooltip("基础血量")]
    public float baseHP;

    /// <summary>
    /// 当前血量
    /// </summary>
    [Tooltip("当前血量")]
    public float curHP;

    /// <summary>
    /// 基础魔法值
    /// </summary>
    [Tooltip("基础魔法值")]
    public float BaseMP;

    /// <summary>
    /// 当前魔法值
    /// </summary>
    [Tooltip("当前魔法值")]
    public float curMP;

    /// <summary>
    /// 基础攻击力
    /// </summary>
    [Tooltip("基础攻击力")]
    public float baeATK;

    /// <summary>
    /// 当前攻击力
    /// </summary>
    [Tooltip("当前攻击力")]
    public float curAtk;

    /// <summary>
    /// 基础防御力
    /// </summary>
    [Tooltip("基础防御力")]
    public float baseDEF;

    /// <summary>
    /// 当前防御力
    /// </summary>
    [Tooltip("当前防御力")]
    public float curDEF;

    /// <summary>
    /// 技能攻击方式
    /// </summary>
    [Tooltip("技能攻击方式")]
    public List<BaseAttack> attacks = new List<BaseAttack>();

    /// <summary>
    /// 魔法攻击方式
    /// </summary>
    [Tooltip("魔法攻击方式")]
    public List<BaseAttack> MagicAttack = new List<BaseAttack>();
}
