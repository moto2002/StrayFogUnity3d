﻿using UnityEditor;
/// <summary>
/// Dll菜单 
/// </summary>
static class EditorDllMenuItem
{
    #region 常量
    /// <summary>
    /// 菜单
    /// </summary>
    const string mcMenu = "StrayFog/Dll/";
    /// <summary>
    /// Asset菜单
    /// </summary>
    const string mcAssetMenu = "Assets/" + mcMenu;
    /// <summary>
    /// Hierarchy菜单
    /// </summary>
    const string mcHierarchy = "GameObject/" + mcMenu;
    /// <summary>
    /// 菜单优先级
    /// </summary>
    const int mcPriority = 100 * (int)enEditorMenuItemPriority.Dll;
    #endregion

    #region Look Package Dll
    const string mcLookPackageDll = "Look Package Dll";
    /// <summary>
    /// 查看可打包的dll
    /// </summary>
    [MenuItem(mcMenu + mcLookPackageDll, false, mcPriority + 1)]
    [MenuItem(mcAssetMenu + mcLookPackageDll, false, mcPriority + 1)]
    [MenuItem(mcHierarchy + mcLookPackageDll, false, mcPriority + 1)]
    static void EditorDevelopMenuItem_LookPackageDllWindow()
    {
        EditorStrayFogExecute.ExecuteLookPackageDll();
    }
    #endregion
}