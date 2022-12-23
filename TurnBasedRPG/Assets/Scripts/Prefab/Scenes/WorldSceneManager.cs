using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WorldSceneManager : MonoBehaviour
{
    public Player player { get; private set; }

    public Vector3 laspos;
    private void Awake()
    {

        GameRoot gameRoot = GameRoot.Instance;
        gameRoot.sceneManager.Region = null;
        //添加该场景的怪物
        int id = gameRoot.dataMgr.sceneInfo.Find((sceneInfo) => { return sceneInfo.Scene.Equals(SceneManager.GetActiveScene().name); }).ID;
        SceneInfo sceneInfo = gameRoot.dataMgr.GetData(gameRoot.dataMgr.sceneInfo, id);
        gameRoot.sceneManager.Region = new SceneMG.RegionData()
        {
            regionName = sceneInfo.Scene,
            maxAmountEnemys = 4,
            enemyInfoLists = sceneInfo.Enemy,
        };

        if (!GameObject.Find("HeroCharacter"))
        {
            player = GameRoot.Instance.prefabMgr.GetPrefab<Player>(ConfigUIPrefab.HeroCharacter);
            Player Hero = Instantiate(player, GameRoot.Instance.sceneManager.lastHeroPosition, Quaternion.identity);
            Hero.name = "HeroCharacter";
            gameRoot.sceneManager.player = Hero;

            gameRoot.MainCanmera.GetComponent<Camera_Follow>().Setup(gameRoot.sceneManager.GetCameraPosition, () => 5f, true, true);//70为摄像机的大小
        }
    }
}
