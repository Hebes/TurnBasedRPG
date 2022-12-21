using UnityEngine;

internal class HeroDeadState : FSMState
{
    public HeroStateMaschine heroStateMaschine { get; private set; }

    public HeroDeadState(FSMSystem fSMSystem, HeroStateMaschine heroStateMaschine) : base(fSMSystem)
    {
        this.heroStateMaschine = heroStateMaschine;
    }

    

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);

        if (!heroStateMaschine.isAlive) return;
        else
        {
            //change tag 切换标签
            heroStateMaschine.tag = "DeadHero";
            //not attackable by enemy 不能被敌人攻击 从BattleStateMaschine 英雄战斗列表删除自己
            heroStateMaschine.BM.HerosInBattle.Remove(heroStateMaschine.gameObject);
            //not managable 不可以控制 从BattleStateMaschine 英雄列表的管理列表删除自己
            heroStateMaschine.BM.HerosToManage.Remove(heroStateMaschine.gameObject);
            //deactivate the selector 停用选择器 就是黄色的小物体
            heroStateMaschine.T_SelectorTransform.SetActive(false);
            //reset gui 全部重设 关闭所有选择面板
            heroStateMaschine.BM.battlePanel.ActionPanel(false);// AttackPanel.SetActive(false);
            heroStateMaschine.BM.battlePanel.SelectTargetPanel(false);// EnemySelectPanel.SetActive(false);
            //remove item from performlist 从执行列表中删除项目
            if (heroStateMaschine.BM.HerosInBattle.Count > 0)
            {
                for (int i = 0; i < heroStateMaschine.BM.PerformList.Count; i++)
                {
                    if (i != 0)
                    {
                        // 如果被攻击的是这个已经死亡的角色
                        if (heroStateMaschine.BM.PerformList[i].AttackersTarget == heroStateMaschine.gameObject)
                            heroStateMaschine.BM.PerformList[i].AttackersTarget = heroStateMaschine.BM.HerosInBattle[Random.Range(0, heroStateMaschine.BM.HerosInBattle.Count)];
                        //从执行列表中删除项目
                        if (heroStateMaschine.BM.PerformList[i].AttackersGameObject == heroStateMaschine.gameObject)
                            heroStateMaschine.BM.PerformList.Remove(heroStateMaschine.BM.PerformList[i]);
                    }
                }
            }
            //change color / play animation 改变颜色/播放动画
            heroStateMaschine.transform.OnGetSpriteRenderer("Image").color = new Color32(105, 105, 105, 255);
            //reset heroinput 重置heroinput
            heroStateMaschine.BM.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.CHECKALIVE.ToString());
            heroStateMaschine.isAlive = false;
        }
    }
}