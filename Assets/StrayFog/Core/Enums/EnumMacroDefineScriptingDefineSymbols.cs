﻿using System;

#region enDeveloperDefine 开发者宏定义
/// <summary>
/// 开发者宏定义
/// </summary>
[Serializable]
[AliasTooltip("开发者宏定义")]
public enum enDeveloperDefine
{
    /// <summary>
    /// UGUI日志
    /// </summary>
    [AliasTooltip("UGUI日志")]
    UGUILOG,
    /// <summary>
    /// 网络日志
    /// </summary>
    [AliasTooltip("网络日志")]
    NETWORKLOG,
    /// <summary>
    /// Config日志
    /// </summary>
    [AliasTooltip("Config日志")]
    CONFIGLOG,
}
#endregion

#region enSystemDefine 系统宏定义
/// <summary>
/// 系统宏定义
/// </summary>
[Serializable]
[AliasTooltip("系统宏定义")]
public enum enSystemDefine
{
    /// <summary>
    /// 调试日志
    /// </summary>
    [AliasTooltip("调试日志")]
    DEBUGLOG,
    /// <summary>
    /// 秒表时间测定
    /// </summary>
    [AliasTooltip("秒表时间测定")]
    STOPWATCH,
    /// <summary>
    /// 强制外部资源加载
    /// </summary>
    [AliasTooltip("强制外部资源加载")]
    FORCEEXTERNALLOADASSET,
    /// <summary>
    /// 强制使用数据库
    /// </summary>
    [AliasTooltip("强制使用数据库")]
    FORCEUSESQLITE
}
#endregion

#region enXLuaDefine xLua宏定义
/// <summary>
/// xLua宏定义
/// </summary>
[Serializable]
[AliasTooltip("xLua宏定义")]
public enum enXLuaDefine
{
    /// <summary>
    /// 打开hotfix功能
    /// </summary>
    [AliasTooltip("打开hotfix功能")]
    HOTFIX_ENABLE,
    /// <summary>
    /// 反射时打印warning
    /// </summary>
    [AliasTooltip("反射时打印warning")]
    NOT_GEN_WARNING,
    /// <summary>
    /// 以偏向减少代码段的方式生成代码
    /// </summary>
    [AliasTooltip("以偏向减少代码段的方式生成代码")]
    GEN_CODE_MINIMIZE,
}
#endregion