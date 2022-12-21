using UnityEngine;

public class EnemyProcessingState : FSMState
{
    public EnemyStateMaschine enemyStateMaschine { get; private set; }

    public EnemyProcessingState(FSMSystem fSMSystem, EnemyStateMaschine enemyStateMaschine) : base(fSMSystem)
    {
        this.enemyStateMaschine = enemyStateMaschine;
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        UpgradeProgressBar();
    }

    // <summary>
    /// 升级进度条  冷却版
    /// </summary>
    private void UpgradeProgressBar()
    {
        enemyStateMaschine.cur_colldown = enemyStateMaschine.cur_colldown + Time.deltaTime;
        if (enemyStateMaschine.cur_colldown >= enemyStateMaschine.max_colldown)//如果冷却时间到了
            enemyStateMaschine.enemyFSMSystem.ChangeGameState(EnemyStateMaschine.TurnState.CHOOSEACTION.ToString());
    }
}