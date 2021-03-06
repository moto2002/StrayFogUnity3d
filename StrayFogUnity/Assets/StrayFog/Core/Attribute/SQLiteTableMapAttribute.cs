﻿using System;
/// <summary>
/// 代码特性
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class SQLiteTableMapAttribute : AliasTooltipAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="_id">ID</param>
    /// <param name="_xlsFilePath">XLS表名称</param>
    /// <param name="_sqliteTableName">数据库表名称</param>
    /// <param name="_sqliteTableType">数据库表类别</param>
    /// <param name="_isDeterminant">是否是行列式表</param>
    /// <param name="_xlsColumnNameIndex">XLS表列名称索引</param>
    /// <param name="_xlsColumnValueIndex">XLS表列值索引</param>
    /// <param name="_xlsColumnTypeIndex">XLS表列类型索引</param>
    /// <param name="_xlsDataStartRowIndex">XLS表数据起始行索引</param>
    /// <param name="_dbSQLitePath">数据库内部资源包路径</param>
    /// <param name="_dbSQLiteAssetBundleName">数据库外部资源包路径</param>
    /// <param name="_tableClassType">表类类型</param>
    /// <param name="_hasPkColumn">是否有主键列</param>
    /// <param name="_canModifyData">是否可修改数据</param>
    public SQLiteTableMapAttribute(int _id,string _xlsFilePath,string _sqliteTableName, enSQLiteEntityClassify _sqliteTableType,
    bool _isDeterminant, int _xlsColumnNameIndex, int _xlsColumnValueIndex,
    int _xlsColumnTypeIndex,int _xlsDataStartRowIndex, string _dbSQLitePath,string _dbSQLiteAssetBundleName,
    Type _tableClassType,bool _hasPkColumn,bool _canModifyData)
        : base(_sqliteTableName, _xlsFilePath)
    {
        id = _id;
        xlsFilePath = _xlsFilePath;
        sqliteTableName = _sqliteTableName;
        sqliteTableType = _sqliteTableType;
        isDeterminant = _isDeterminant;
        xlsColumnNameIndex = _xlsColumnNameIndex;
        xlsColumnValueIndex = _xlsColumnValueIndex;
        xlsColumnTypeIndex = _xlsColumnTypeIndex;
        xlsDataStartRowIndex = _xlsDataStartRowIndex;
        dbSQLitePath = _dbSQLitePath;
        dbSQLiteAssetBundleName = _dbSQLiteAssetBundleName;
        dbSQLiteKey = Guid.NewGuid().ToString().UniqueHashCode();
        tableClassType = _tableClassType;
        hasPkColumn = _hasPkColumn;
        canModifyData = _canModifyData;
    }
    /// <summary>
    /// ID
    /// </summary>
    public int id { get; private set; }
    /// <summary>
    /// XLS表名称
    /// </summary>
    public string xlsFilePath { get; private set; }
    /// <summary>
    /// 数据库表名称
    /// </summary>
    public string sqliteTableName { get; private set; }
    /// <summary>
    /// 数据库表类别
    /// </summary>
    public enSQLiteEntityClassify sqliteTableType { get; private set; }
    /// <summary>
    /// 是否是行列式表
    /// </summary>
    public bool isDeterminant { get; private set; }
    /// <summary>
    /// XLS表列名称索引
    /// </summary>
    public int xlsColumnNameIndex { get; private set; }
    /// <summary>
    /// XLS表列值索引
    /// </summary>
    public int xlsColumnValueIndex { get; private set; }
    /// <summary>
    /// XLS表列类型索引
    /// </summary>
    public int xlsColumnTypeIndex { get; private set; }
    /// <summary>
    /// XLS表数据起始行索引
    /// </summary>
    public int xlsDataStartRowIndex { get; private set; }
    /// <summary>
    /// 数据库内部资源包路径
    /// </summary>
    public string dbSQLitePath { get; private set; }
    /// <summary>
    /// 数据库外部资源包路径
    /// </summary>
    public string dbSQLiteAssetBundleName { get; private set; }
    /// <summary>
    /// 数据库Key
    /// </summary>
    public int dbSQLiteKey { get; private set; }
    /// <summary>
    /// 表类类型
    /// </summary>
    public Type tableClassType { get; private set; }
    /// <summary>
    /// 是否有主键列
    /// </summary>
    public bool hasPkColumn { get; private set; }

    /// <summary>
    /// 是否可修改数据
    /// </summary>
    public bool canModifyData { get; private set; }
}

