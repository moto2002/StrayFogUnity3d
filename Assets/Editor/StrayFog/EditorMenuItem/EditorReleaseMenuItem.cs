﻿#if UNITY_EDITOR
using UnityEditor;
/// <summary>
/// 发布菜单 
/// </summary>
static class EditorReleaseMenuItem
{
    #region 常量
    /// <summary>
    /// 菜单
    /// </summary>
    const string mcMenu = "StrayFog/Release/";
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
    const int mcPriority = 100 * (int)enEditorMenuItemPriority.Release;
    #endregion

    #region Build Package
    const string mcBuildPackage = "Build Package";
    /// <summary>
    /// 发布
    /// </summary>
    [MenuItem(mcMenu + mcBuildPackage, false, mcPriority + 1)]
    [MenuItem(mcAssetMenu + mcBuildPackage, false, mcPriority + 1)]
    [MenuItem(mcHierarchy + mcBuildPackage, false, mcPriority + 1)]
    static void EditorDevelopMenuItem_BuildPackageWindow()
    {
        EditorStrayFogExecute.ExecuteBuildPackage();
        EditorStrayFogApplication.MenuItemQuickDisplayDialogSucceed(mcBuildPackage);
    }
    #endregion

    #region Build Bat To Package
    const string mcBuildBatToPackage = "BuildBat To Package";
    /// <summary>
    /// 资源包删除未使用资源批处理
    /// </summary>
    [MenuItem(mcMenu + mcBuildBatToPackage, false, mcPriority + 2)]
    [MenuItem(mcAssetMenu + mcBuildBatToPackage, false, mcPriority + 2)]
    [MenuItem(mcHierarchy + mcBuildBatToPackage, false, mcPriority + 2)]
    static void EditorDevelopMenuItem_BuildBatToPackage()
    {
        EditorStrayFogExecute.ExecuteBuildBatToPackage();
        EditorStrayFogApplication.MenuItemQuickDisplayDialogSucceed(mcBuildBatToPackage);
    }
    #endregion

    #region Build Dll To Package
    const string mcBuildDllToPackage = "Build Dll To Package";
    /// <summary>
    /// 生成dll到资源包
    /// </summary>
    [MenuItem(mcMenu + mcBuildDllToPackage, false, mcPriority + 3)]
    [MenuItem(mcAssetMenu + mcBuildDllToPackage, false, mcPriority + 3)]
    [MenuItem(mcHierarchy + mcBuildDllToPackage, false, mcPriority + 3)]
    static void EditorDevelopMenuItem_BuildDllToPackage()
    {
        EditorStrayFogExecute.ExecuteBuildDllToPackage();
        EditorStrayFogApplication.MenuItemQuickDisplayDialogSucceed(mcBuildDllToPackage);
    }
    #endregion

    #region Build All Xls Data
    const string mcBuildAllXlsData = "Build All Xls Data";
    /// <summary>
    /// 生成所有的XLS表数据
    /// </summary>
    [MenuItem(mcMenu + mcBuildAllXlsData, false, mcPriority + 4)]
    [MenuItem(mcAssetMenu + mcBuildAllXlsData, false, mcPriority + 4)]
    [MenuItem(mcHierarchy + mcBuildAllXlsData, false, mcPriority + 4)]
    static void EditorDevelopMenuItem_BuildAllXlsData()
    {
        EditorStrayFogExecute.ExecuteBuildAllXlsData();
        EditorStrayFogApplication.MenuItemQuickDisplayDialogSucceed(mcBuildAllXlsData);
    }
    #endregion

    #region Copy SQLite Db To Package
    const string mcCopySQLiteDbToPackage = "Copy SQLite Db To Package";
    /// <summary>
    /// 复制SQLite数据库到资源包
    /// </summary>
    [MenuItem(mcMenu + mcCopySQLiteDbToPackage, false, mcPriority + 5)]
    [MenuItem(mcAssetMenu + mcCopySQLiteDbToPackage, false, mcPriority + 5)]
    [MenuItem(mcHierarchy + mcCopySQLiteDbToPackage, false, mcPriority + 5)]
    static void EditorDevelopMenuItem_CopySQLiteDbToPackage()
    {
        EditorStrayFogExecute.ExecuteCopySQLiteDbToPackage();
        EditorStrayFogApplication.MenuItemQuickDisplayDialogSucceed(mcCopySQLiteDbToPackage);
    }
    #endregion
}
#endif