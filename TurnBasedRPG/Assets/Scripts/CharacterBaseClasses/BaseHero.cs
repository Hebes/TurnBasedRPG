using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����Ӣ������
/// </summary>
[Serializable]
public class BaseHero :BaseCharacterClasses
{
    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public int stamina;

    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public int intellect;

    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public int dexterity;

    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public int agility;
}
