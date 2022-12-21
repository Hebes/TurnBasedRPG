using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 基础怪物类
/// </summary>
[Serializable]
public class BaseEnemy : BaseCharacterClasses
{
    /// <summary>
    /// 属性
    /// </summary>
    public enum EType
    {
        /// <summary>
        /// 地
        /// </summary>
        GRASS,
        /// <summary>
        /// 火
        /// </summary>
        FIRE,
        /// <summary>
        /// 水
        /// </summary>
        WATER,
        /// <summary>
        /// 电
        /// </summary>
        ELECTRIC,
    }

    /// <summary>
    /// 稀有度
    /// </summary>
    public enum ERarity
    {
        /// <summary>
        /// 常见
        /// </summary>
        COMMON,
        /// <summary>
        /// 不常见
        /// </summary>
        UNCOMMON,
        /// <summary>
        /// 少见
        /// </summary>
        RARE,
        /// <summary>
        /// 超级少见
        /// </summary>
        SUPERRARE,
    }

    /// <summary>
    /// 属性
    /// </summary>
    public EType EnemyType;
    /// <summary>
    /// 稀有度
    /// </summary>
    public ERarity rarity;
}
