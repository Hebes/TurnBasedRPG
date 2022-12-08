using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// »ù´¡Ó¢ĞÛÊôĞÔ
/// </summary>
[Serializable]
public class BaseHero :BaseCharacterClasses
{
    /// <summary>
    /// ÄÍÁ¦
    /// </summary>
    [Tooltip("ÄÍÁ¦")]
    public int stamina;

    /// <summary>
    /// ÖÇÁ¦
    /// </summary>
    [Tooltip("ÖÇÁ¦")]
    public int intellect;

    /// <summary>
    /// ÁéÇÉ
    /// </summary>
    [Tooltip("ÁéÇÉ")]
    public int dexterity;

    /// <summary>
    /// Ãô½İ
    /// </summary>
    [Tooltip("Ãô½İ")]
    public int agility;
}
