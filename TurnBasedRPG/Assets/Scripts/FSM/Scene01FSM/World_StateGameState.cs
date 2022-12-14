using System.Collections.Generic;
using UnityEngine;

internal class World_StateGameState : FSMState
{
    public SceneMG scene01Manager { get; private set; }

    public World_StateGameState(FSMSystem fSMSystem, object obj = null) : base(fSMSystem, obj)
    {
        this.scene01Manager = obj as SceneMG;
    }

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        if (scene01Manager.isWalking)
            RandomEncounter();
        if (scene01Manager.gotAttacked)
            scene01Manager.sceneMgrFSMSystem.ChangeGameState(SceneMG.GameStates.BATTLE_STATE.ToString());
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        if (Input.GetKeyDown(KeyCode.Y))
        {
            scene01Manager.sceneMgrFSMSystem.ChangeGameState(SceneMG.GameStates.BATTLE_STATE.ToString());
        }

        //todo 下面代码后面需要改到战斗状态中
        if (Input.GetKeyDown(KeyCode.U))
        {
            string tempname = $"hero1";
            BaseHero hero = new BaseHero()
            {
                theName = $"hero1",
                baseHP = 110,
                curHP = 110,
                BaseMP=10,
                curMP=10,
                baeATK = 10,
                curAtk = 10,
                LV=100,
                attacks = new List<BaseAttack>()
                {
                    { new BaseAttack(){attackName=$"{tempname}物理攻击1",attackDescription=$"{tempname}物理攻击1",attackCost=0,attackDamage=0, } },
                    { new BaseAttack(){attackName=$"{tempname}物理攻击2",attackDescription=$"{tempname}物理攻击2",attackCost=0,attackDamage=0, } },
                },
                MagicAttacks = new List<BaseAttack>() 
                {
                    { new BaseAttack(){attackName=$"{tempname}魔法攻击1",attackDescription=$"{tempname}魔法攻击1",attackCost=0,attackDamage=0, } },
                    { new BaseAttack(){attackName=$"{tempname}魔法攻击2",attackDescription=$"{tempname}魔法攻击2",attackCost=0,attackDamage=0, } },
                },
            };
            scene01Manager.heroBattleLists.Add(hero);
        }
        //todo 下面代码后面需要改到战斗状态中
        if (Input.GetKeyDown(KeyCode.I))
        {
            string tempname = $"hero2";
            BaseHero hero = new BaseHero()
            {
                theName = $"hero2",
                baseHP = 110,
                curHP = 110,
                BaseMP = 20,
                curMP = 20,
                baeATK = 10,
                curAtk = 10,
                LV = 100,
                attacks = new List<BaseAttack>()
                {
                    { new BaseAttack(){attackName=$"{tempname}物理攻击1",attackDescription=$"{tempname}物理攻击1",attackCost=0,attackDamage=0, } },
                    { new BaseAttack(){attackName=$"{tempname}物理攻击2",attackDescription=$"{tempname}物理攻击2",attackCost=0,attackDamage=0, } },
                },
                MagicAttacks = new List<BaseAttack>()
                {
                    { new BaseAttack(){attackName=$"{tempname}魔法攻击1",attackDescription=$"{tempname}魔法攻击1",attackCost=0,attackDamage=0, } },
                    { new BaseAttack(){attackName=$"{tempname}魔法攻击2",attackDescription=$"{tempname}魔法攻击2",attackCost=0,attackDamage=0, } },
                },
            };
            scene01Manager.heroBattleLists.Add(hero);
        }
    }

    /// <summary>
    /// 随机遇敌
    /// </summary>
    private void RandomEncounter()
    {
        if (scene01Manager.isWalking && scene01Manager.canGetEncounter)//正在移动并且是可以遇怪的区域
        {
            if (Random.Range(0, 1000) < 10)
            {
                Debug.Log("可以战斗");
                scene01Manager.gotAttacked = true;
            }
        }
    }
}