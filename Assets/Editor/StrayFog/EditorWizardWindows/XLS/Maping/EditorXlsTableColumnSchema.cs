﻿using System;
using UnityEngine;
/// <summary>
/// XLS表格列架构
/// </summary>
[Serializable]
public class EditorXlsTableColumnSchema
{
    /// <summary>
    /// 列名称
    /// </summary>
    [AliasTooltip("列名称")]
    public string name;
    /// <summary>
    /// 列描述
    /// </summary>
    [AliasTooltip("列描述")]
    public string desc;
    /// <summary>
    /// 列类型
    /// </summary>
    [AliasTooltip("列类型")]
    public enSQLiteDataType type;
    /// <summary>
    /// 数组维度
    /// </summary>
    [AliasTooltip("数组维度")]
    public enSQLiteDataTypeArrayDimension arrayDimension;
}