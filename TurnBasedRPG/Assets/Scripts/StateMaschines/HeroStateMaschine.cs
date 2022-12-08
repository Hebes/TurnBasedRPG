using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMaschine : MonoBehaviour
{
    /// <summary>
    /// ս��������
    /// </summary>
    public BattleManager BM { get; private set; }

    public BaseHero hero;

    /// <summary>
    /// �غ�״̬
    /// </summary>
    public enum TurnState
    {
        /// <summary>
        /// ����������
        /// </summary>
        PROCESSING,
        /// <summary>
        /// ��ӵ��б���
        /// </summary>
        ADDTOLIST,
        /// <summary>
        /// �ȴ�
        /// </summary>
        WAITING,
        /// <summary>
        /// ѡ��
        /// </summary>
        SELECTING,
        /// <summary>
        /// �ж�
        /// </summary>
        ACTION,
        /// <summary>
        /// ��ȥ��
        /// </summary>
        DEAD,
    }

    public TurnState currentState;

    /// <summary>
    /// ��ǰ��ȴʱ��
    /// </summary>
    private float cur_colldown { get; set; } = 0f;

    /// <summary>
    /// ���ȴʱ��
    /// </summary>
    private float max_colldown { get; set; } = 5f;

    /// <summary>
    /// ��ȴ�������
    /// </summary>
    public Image ProgressBar;

    /// <summary>
    /// ѡ�������� ���ǽ�ɫͷ�϶��Ļ�ɫС����
    /// </summary>
    public GameObject Selector;

    //Ӣ�ۻ���
    public GameObject EnemyToAttack;
    private bool actionStarted = false;
    public Vector3 startPosition;
    private float animSpeed = 10f;

    /// <summary>
    /// dead �Ƿ���
    /// </summary>
    private bool alive = true;

    //������ heroPanel
    private HeroBar stats;
    /// <summary>
    /// ��ҵ��ж���ȴ�� HeroBar
    /// </summary>
    public GameObject HeroPanel;
    private Transform HeroPanelSpacer;
}
