﻿using System;
using UnityEngine;
/// <summary>
/// 抽象MonoBehaviour【IXLua接口】
/// </summary>
public abstract partial class AbsMonoBehaviour : IXLua
{
    #region LoadXLua 加载xLua文件
    /// <summary>
    /// 加载xLua文件
    /// </summary>
    /// <param name="_xLuaFileId">xLua文件ID</param>
    /// <param name="_xLuaFolderId">xLua文件夹ID</param>
    /// <param name="_onComplete">完成回调</param>
    public void LoadXLua(int _xLuaFileId, int _xLuaFolderId, Action<LoadXLuaResult> _onComplete)
    {
        StrayFogRunningUtility.SingleScriptableObject<StrayFogRunningApplication>().LoadXLua(_xLuaFileId, _xLuaFolderId, _onComplete);
    }
    #endregion
}