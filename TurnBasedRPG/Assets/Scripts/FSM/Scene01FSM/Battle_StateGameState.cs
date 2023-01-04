using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 战斗
/// </summary>
internal class Battle_StateGameState : FSMState
{
    public SceneMG scene01Manager { get; private set; }

    public Battle_StateGameState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
        this.scene01Manager = obj as SceneMG;
    }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        //加载战斗场景
        StartBattle();
        //去往IDLE状态
        scene01Manager.sceneMgrFSMSystem.ChangeGameState(SceneMG.GameStates.IDLE.ToString());
    }

    /// <summary>
    /// 开始战斗
    /// </summary>
    private void StartBattle()
    {
        //AMOUNT OF ENEMYS 随机敌人数量
        int enemyAmount = Random.Range(1, scene01Manager.Region.maxAmountEnemys + 1);//随机怪物数量
        //添加敌人到需要实例化怪物战斗列表
        for (int i = 0; i < enemyAmount; i++)
        {
            EnemyInfo enemyInfo = GetEnemyData();//获取技能数据
            SetEnemyData(enemyInfo);//添加技能信息
        }
        //HERO 
        scene01Manager.lastHeroPosition = scene01Manager.player.transform.position;
        scene01Manager.nextHeroPosition = scene01Manager.lastHeroPosition;
        scene01Manager.lastScene = SceneManager.GetActiveScene().name;//设置最后一个场景的名称
        //LOAD LEVEL
        //GameRoot.Instance.scenesModule.LoadSceneAsyn(ConfigScenes.BattleScene,null);
        GameRoot.Instance.scenesModule.LoadScene(ConfigScenes.BattleScene, null);
        //SceneManager.LoadScene(scene01Manager.Regions[curRegions].BattleScene);
        //RESET HERO
        scene01Manager.isWalking = false;
        scene01Manager.gotAttacked = false;
        scene01Manager.canGetEncounter = false;
    }

    /// <summary>
    /// 获取敌人数据
    /// </summary>
    /// <returns></returns>
    private EnemyInfo GetEnemyData()
    {
        //随机一个数字
        int number = Random.Range(0, scene01Manager.Region.enemyInfoLists.Count);
        //从enmylists中获取随机的敌人
        int numberTemp = 0;
        for (int i = 0; i < scene01Manager.Region.enemyInfoLists.Count; i++)
        {
            if (number == i)
                numberTemp = scene01Manager.Region.enemyInfoLists[i];
        }
        return GameRoot.Instance.dataMgr.GetData<EnemyInfo>(GameRoot.Instance.dataMgr.enemyInfoLists, numberTemp);//获取敌人数据
    }

    /// <summary>
    /// 设置敌人的信息
    /// </summary>
    /// <param name="enemyInfo"></param>
    private void SetEnemyData(EnemyInfo enemyInfo)
    {
        //获取技能信息，用于后续设置
        List<BaseAttack> baseAttacks = new List<BaseAttack>();//物理技能
        List<BaseAttack> MagicAttacks = new List<BaseAttack>();//魔法技能
        for (int i = 0; i < enemyInfo.Attacks?.Count; i++)
        {
            int tempNumber = enemyInfo.Attacks[i];
            SkillsInfo attackSkillInfo = GameRoot.Instance.dataMgr.GetData(GameRoot.Instance.dataMgr.SkillLists, tempNumber);
            switch (attackSkillInfo.SkillType)
            {
                case 0://物理攻击
                    baseAttacks.Add(new BaseAttack()
                    {
                        attackName = attackSkillInfo.AttackName,
                        attackDescription = attackSkillInfo.AttackDescription,
                        attackDamage = attackSkillInfo.AttackDamage,
                        attackCost = attackSkillInfo.AttackCost,
                    });
                    break;
                case 1://魔法攻击
                    MagicAttacks.Add(new BaseAttack()
                    {
                        attackName = attackSkillInfo.AttackName,
                        attackDescription = attackSkillInfo.AttackDescription,
                        attackDamage = attackSkillInfo.AttackDamage,
                        attackCost = attackSkillInfo.AttackCost,
                    });
                    break;
            }
        }
        //设置敌人属性
        BaseEnemy baseEnemy = new BaseEnemy()
        {
            theName = enemyInfo.Name,
            baseHP = enemyInfo.BaseHP,
            curHP = enemyInfo.BaseHP,
            baeATK = enemyInfo.BaseATK,
            curAtk = enemyInfo.BaseATK,
            baseDEF = enemyInfo.BaseDef,
            curDEF = enemyInfo.BaseDef,
            BaseMP = enemyInfo.BaseMP,
            curMP = enemyInfo.BaseMP,
            attacks = baseAttacks,
            MagicAttacks = MagicAttacks,
        };
        scene01Manager.enemysToBattleLists.Add(baseEnemy);
    }
}