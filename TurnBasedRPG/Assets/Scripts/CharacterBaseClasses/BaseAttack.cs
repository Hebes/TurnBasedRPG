using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������ʽ
/// </summary>
[Serializable]
public class BaseAttack
{
    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public string attackName;

    /// <summary>
    /// ��������
    /// </summary>
    [Tooltip("��������")]
    public string attackDescription;

    /// <summary>
    /// �˺�
    /// </summary>
    [Tooltip("�˺�")]
    public float attackDamage;//�˺� Base Damage 15��mellee LvL 10 stamina 35 = basedmg + stamina + LvL = 60

    /// <summary>
    /// ����ֵ����
    /// </summary>
    [Tooltip("����ֵ����")]
    public float attackCost;//ManaCost ����ֵ����
}
