using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterClasses
{
    /// <summary>
    /// ����
    /// </summary>
    [Tooltip("����")]
    public string theName;

    /// <summary>
    /// ����Ѫ��
    /// </summary>
    [Tooltip("����Ѫ��")]
    public float baseHP;

    /// <summary>
    /// ��ǰѪ��
    /// </summary>
    [Tooltip("��ǰѪ��")]
    public float curHP;

    /// <summary>
    /// ����ħ��ֵ
    /// </summary>
    [Tooltip("����ħ��ֵ")]
    public float BaseMP;

    /// <summary>
    /// ��ǰħ��ֵ
    /// </summary>
    [Tooltip("��ǰħ��ֵ")]
    public float curMP;

    /// <summary>
    /// ����������
    /// </summary>
    [Tooltip("����������")]
    public float baeATK;

    /// <summary>
    /// ��ǰ������
    /// </summary>
    [Tooltip("��ǰ������")]
    public float curAtk;

    /// <summary>
    /// ����������
    /// </summary>
    [Tooltip("����������")]
    public float baseDEF;

    /// <summary>
    /// ��ǰ������
    /// </summary>
    [Tooltip("��ǰ������")]
    public float curDEF;

    /// <summary>
    /// ���ܹ�����ʽ
    /// </summary>
    [Tooltip("���ܹ�����ʽ")]
    public List<BaseAttack> attacks = new List<BaseAttack>();

    /// <summary>
    /// ħ��������ʽ
    /// </summary>
    [Tooltip("ħ��������ʽ")]
    public List<BaseAttack> MagicAttack = new List<BaseAttack>();
}
