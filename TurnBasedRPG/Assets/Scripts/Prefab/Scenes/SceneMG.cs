using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMG : SingletonAutoMono<SceneMG>
{
    public FSMSystem sceneMgrFSMSystem { get; private set; }
    /// <summary>游戏状态</summary>
    public enum GameStates
    {
        /// <summary>
        /// 世界
        /// </summary>
        WORLD_STATE,
        /// <summary>
        /// 城镇
        /// </summary>
        TOWN_STATE,
        /// <summary>
        /// 战斗
        /// </summary>
        BATTLE_STATE,
        /// <summary>
        /// 闲置的
        /// </summary>
        IDLE,
    }

    /// <summary>地区怪物数据 随机怪物</summary>
    [System.Serializable]
    public class RegionData
    {
        /// <summary>区域名称</summary>
        public string regionName;
        /// <summary>最大的怪物数量 </summary>
        public int maxAmountEnemys = 4;
        /// <summary>怪物ID</summary>
        public List<int> enemyInfoLists = new List<int>();
    }

    /// <summary>敌人数量</summary>
    public int enemyAmount;
    /// <summary>战斗的怪物列表 </summary>
    public List<GameObject> enemysToBattleLists = new List<GameObject>();
    /// <summary>每个场景对应的有哪些怪物怪物</summary>
    public RegionData Region;
    /// <summary>英雄进入战斗后生成的列表</summary>
    public List<GameObject> heroBattleLists = new List<GameObject>();

    /// <summary>重生点 SPAWNPOINTS</summary>
    public string nextSpawnPoint;
    /// <summary>POSITIONS 下一个英雄的位置</summary>
    public Vector3 nextHeroPosition;
    /// <summary>英雄最后的位置</summary>
    public Vector3 lastHeroPosition;

    /// <summary>B0OLS  是否移动</summary>
    public bool isWalking { get; set; } = false;
    /// <summary>可以遇敌  就是战斗</summary>
    public bool canGetEncounter { get; set; } = false;
    /// <summary>遭遇战斗</summary>
    public bool gotAttacked { get; set; } = false;


    public Player player { get; set; }
    //public GameObject player;

    [Tooltip("当前场景的名称")] public string sceneToLoad;//下一个场景的名称
    [Tooltip("最后一个场景的名称")] public string lastScene;//BATTLE 最后一个场景的名称


    public void SceneManagerInit()
    {
        sceneMgrFSMSystem = new FSMSystem();
        sceneMgrFSMSystem.stateDic = new Dictionary<string, FSMState>()
        {
            {GameStates.WORLD_STATE.ToString(),new World_StateGameState(sceneMgrFSMSystem,this) },//世界
            {GameStates.TOWN_STATE.ToString(),new Town_StateGameState(sceneMgrFSMSystem,this)},//城镇
            {GameStates.BATTLE_STATE.ToString(),new Battle_StateGameState(sceneMgrFSMSystem,this) },//战斗
            {GameStates.IDLE.ToString(),new IdleGameState(sceneMgrFSMSystem,this) },//等待
        };
        sceneMgrFSMSystem.ChangeGameState(GameStates.WORLD_STATE.ToString(), this);
    }

    private void Update()
    {
        sceneMgrFSMSystem?.Update();
    }

    /// <summary>
    /// 加载下一个场景
    /// </summary>
    public void LoadNextScene() => GameRoot.Instance.scenesModule.LoadScene(sceneToLoad);

    /// <summary>
    /// 加载最后一次场景  战斗完毕后
    /// </summary>
    public void LoadSceneAfterBattle() => GameRoot.Instance.scenesModule.LoadScene(lastScene);
}
