using LogUtils;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 死亡状态
/// </summary>
internal class EnemyDeadState : FSMState
{
    public EnemyStateMaschine enemyStateMaschine { get; private set; }

    public EnemyDeadState(FSMSystem fSMSystem, EnemyStateMaschine enemyStateMaschine) : base(fSMSystem)
    {
        this.enemyStateMaschine = enemyStateMaschine;
    }

    /// <summary>
    /// dead 是否存活
    /// </summary>
    private bool alive { get; set; } = true;

    public override void DoEnter(object obj)
    {
        base.DoEnter(obj);
        if (!alive)return;
        else
        {
            //change tag 切换标签
            enemyStateMaschine.tag = "DeadEnemy";
            //not attackable by enemy 不能被敌人攻击 从BattleStateMaschine 英雄战斗列表删除自己
            enemyStateMaschine.BM.EnemysInBattle.Remove(enemyStateMaschine.gameObject);
            //deactivate the selector 停用选择器 就是黄色的小物体
            enemyStateMaschine.Selector.SetActive(false);
            //remove item from performlist 敌人的战斗列表
            if (enemyStateMaschine.BM.EnemysInBattle.Count > 0)
            {
                for (int i = 0; i < enemyStateMaschine.BM.PerformList.Count; i++)//执行行动的列表
                {
                    if (i != 0)
                    {
                        //如果被攻击的是这个已经死亡的角色
                        if (enemyStateMaschine.BM.PerformList[i].AttackersTarget == enemyStateMaschine.gameObject)
                            enemyStateMaschine.BM.PerformList[i].AttackersTarget = enemyStateMaschine.BM.EnemysInBattle[Random.Range(0, enemyStateMaschine.BM.EnemysInBattle.Count)];
                        //从执行列表中删除项目
                        if (enemyStateMaschine.BM.PerformList[i].AttackersGameObject == enemyStateMaschine.gameObject)
                            enemyStateMaschine.BM.PerformList.Remove(enemyStateMaschine.BM.PerformList[i]);
                    }
                }
            }
            //change color / play animation 改变颜色/播放动画
            enemyStateMaschine.transform.OnGetSpriteRenderer("T_Image").color = new Color32(105, 105, 105, 255);
            //设置为不存活
            alive = false;
            //重新生成敌人的按钮
            enemyStateMaschine.BM.EnemyButtons();
            //check alive 检查是否存活
            enemyStateMaschine.BM.BattleManagerFSMSystem.ChangeGameState(BattleManager.PerformAction.CHECKALIVE.ToString());
        }
    }
}