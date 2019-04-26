﻿using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// UI窗口关卡
/// </summary>
[AddComponentMenu("Game/ExampleLevel/StrayFogUIWindowLevel")]
public class StrayFogUIWindowLevel : AbsLevel
{
    /// <summary>
    /// 按钮可操作窗口
    /// </summary>
    static readonly enUIWindow[] msrBtnWindows = new enUIWindow[2] { enUIWindow.PlayerListWindow, enUIWindow.HeroListWindow };
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        StrayFogGamePools.gameManager.Initialization(() =>
        {
            StrayFogGamePools.uiWindowManager.AfterToggleScene(() =>
            {
                
            });
        });
    }

    void OnGUI()
    {
        StrayFogGamePools.sceneManager.DrawLevelSelectButtonOnGUI();
        StrayFogGamePools.eventAggregatorManager.DrawLevelSelectButtonOnGUI();
        foreach (enUIWindow w in msrBtnWindows)
        {
            if (GUILayout.Button(w.ToString()))
            {
                StrayFogGamePools.uiWindowManager.OpenWindow(w);
            }
        }
    }
}