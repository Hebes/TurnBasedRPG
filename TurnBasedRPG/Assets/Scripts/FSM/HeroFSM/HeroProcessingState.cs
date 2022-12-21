using UnityEngine;

/// <summary>
/// 进度条上升
/// </summary>
public class HeroProcessingState : FSMState
{
    public HeroStateMaschine heroStateMaschine { get; private set; }

    public HeroProcessingState(FSMSystem fSMSystem, HeroStateMaschine heroStateMaschine) : base(fSMSystem)
    {
        this.heroStateMaschine = heroStateMaschine;
    }

    public override void DOUpdata()
    {
        base.DOUpdata();
        UpgradeProgressBar();
    }

    /// <summary>
    /// 进度条升级
    /// </summary>
    private void UpgradeProgressBar()
    {
        heroStateMaschine.cur_colldown = heroStateMaschine.cur_colldown + Time.deltaTime;
        float calc_cooldown = heroStateMaschine.cur_colldown / heroStateMaschine.max_colldown;
        // 将给定值限制在给定的最小浮点值和最大浮点值之间。
        // 如果给定值在最小值和最大值范围内，则返回给定值。
        //显示进度条图片的进度
        heroStateMaschine.ProgressBar.transform.localScale = new Vector3(
            Mathf.Clamp(calc_cooldown, 0, 1),
            heroStateMaschine.ProgressBar.transform.localScale.y,
            heroStateMaschine.ProgressBar.transform.localScale.z);
        if (heroStateMaschine.cur_colldown >= heroStateMaschine.max_colldown)//如果冷却时间到了
            heroStateMaschine.heroFSMSystem.ChangeGameState(HeroStateMaschine.TurnState.ADDTOLIST.ToString(), heroStateMaschine);
    }
}