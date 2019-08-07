﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
/// <summary>
/// 地形关卡
/// </summary>
[AddComponentMenu("StrayFog/Game/Example/Level/ExampleTerrainLevel")]
public class ExampleTerrainLevel : AbsLevel
{
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        StrayFogGamePools.gameManager.Initialization(() =>
        {
            StrayFogGamePools.uiWindowManager.AfterToggleScene(() =>
            {
                StartCoroutine(LoadTerrain());
            });            
        });
    }

    /// <summary>
    /// 加载角色
    /// </summary>
    /// <returns>异步</returns>
    IEnumerator LoadTerrain()
    {
        yield return new WaitForSeconds(1);
        Stopwatch watch = new Stopwatch();
        watch.Start();
        StrayFogGamePools.assetBundleManager.LoadAssetInMemory(
            enAssetDiskMapingFile.f_Terrain_prefab,
            enAssetDiskMapingFolder.Assets_Example_AssetBundles_Terrain,
            (output) =>
            {
                output.Instantiate<GameObject>((result) =>
                {
                    GameObject terrain = (GameObject)result.asset;
                    Stopwatch w = (Stopwatch)result.input.extraParameter[0];
                    w.Stop();
                    UnityEngine.Debug.Log(w.Elapsed + "=>" + terrain.gameObject);
                });
            }, watch);
    }

    /// <summary>
    /// OnGUI
    /// </summary>
    private void OnGUI()
    {
        StrayFogGamePools.sceneManager.DrawLevelSelectButtonOnGUI();
        StrayFogGamePools.eventHandlerManager.DrawLevelSelectButtonOnGUI();
    }
}
