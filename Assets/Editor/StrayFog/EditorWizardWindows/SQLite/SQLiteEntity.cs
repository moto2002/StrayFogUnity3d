﻿#if UNITY_EDITOR
using System.Collections.Generic;

/// <summary>
/// SQLite表格
/// </summary>
public class SQLiteEntity
{
    /// <summary>
    /// SQLite表格
    /// </summary>
    /// <param name="_name">表格名称</param>
    /// <param name="_fileName">文件路径</param>
    /// <param name="_classify">表格分类</param>
    /// <param name="_isDetermainant">是否是行列式</param>
    public SQLiteEntity(string _name, string _fileName, enSQLiteEntityClassify _classify, bool _isDetermainant)
    {
        name = _name;
        fileName = _fileName;
        classify = _classify;
        isDetermainant = _isDetermainant;
        string prefix = _classify.ToString() + "_";
        if (_name.StartsWith(prefix))
        {
            className = _name;
        }
        else
        {
            className = prefix + _name;
        }
        properties = new List<SQLiteEntityProperty>();
    }
    /// <summary>
    /// 分类
    /// </summary>
    public enSQLiteEntityClassify classify { get; private set; }
    /// <summary>
    /// 类名称
    /// </summary>
    public string className { get; private set; }
    /// <summary>
    /// 文件路径
    /// </summary>
    public string fileName { get; private set; }
    /// <summary>
    /// 表格名称
    /// </summary>
    public string name { get; private set; }
    /// <summary>
    /// 是否是行列式
    /// </summary>
    public bool isDetermainant { get; private set; }
    /// <summary>
    /// 属性组
    /// </summary>
    public List<SQLiteEntityProperty> properties { get; private set; }
}
#endif
