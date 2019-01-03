﻿using ExcelLibrary.SpreadSheet;
using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// 编辑器XLS工具
/// </summary>
public sealed class EditorStrayFogXLS
{
    #region XLS 变量    
    /// <summary>
    /// 列名称行索引
    /// </summary>
    static readonly int msrColumnNameRowIndex = 0;
    /// <summary>
    /// 列类型行索引
    /// </summary>
    static readonly int msrColumnTypeRowIndex = 1;
    /// <summary>
    /// 列描述行索引
    /// </summary>
    static readonly int msrColumnDescriptionRowIndex = 2;
    /// <summary>
    /// 表数据行起始索引
    /// </summary>
    static readonly int msrColumnDataRowStartIndex = 3;
    /// <summary>
    /// 类名后缀
    /// </summary>
    public static readonly string msrClassNameSuffix = "Xls";
    #endregion

    #region 分隔符 readonly 变量
    /// <summary>
    /// 一维数组分隔符
    /// </summary>
    static readonly string[] msrOneArraySeparate = new string[] { @"|" };
    /// <summary>
    /// 元素分隔符
    /// </summary>
    static readonly string[] msrElementSeparate = new string[] { @"," };
    #endregion

    #region public readonly 变量  
    /// <summary>
    /// 分隔符说明
    /// </summary>
    public static readonly string msrSeparateDescription = "1.一维数组\"|\"竖线分隔,例:1|2|3" + System.Environment.NewLine + "2.二维数组\"|\"竖线分隔一维,\",\"逗号分隔二维,例:A0,A1,A2|B0,B1,B2";


    #endregion

    #region 内部 readonly 变量
    /// <summary>
    /// XLS表架构文件
    /// </summary>
    static readonly EditorEngineAssetConfig msrXlsTableMapingAsset = new EditorEngineAssetConfig("",
        enEditorApplicationFolder.XLS_TableMaping.GetAttribute<EditorApplicationFolderAttribute>().path,
        enFileExt.Asset, "");

    /// <summary>
    /// XLS表架构文件
    /// </summary>
    static readonly EditorEngineAssetConfig msrXlsTableSrcAsset = new EditorEngineAssetConfig("",
        enEditorApplicationFolder.XLS_TableSrc.GetAttribute<EditorApplicationFolderAttribute>().path,
        enFileExt.Xls, "");
    #endregion

    #region ReadXlsSchema 读取XLS表结构框架
    /// <summary>
    /// 读取XLS表结构框架
    /// </summary>
    /// <returns>XLS表结构框架</returns>
    public static List<EditorXlsTableSchema> ReadXlsSchema()
    {
        List<EditorXlsTableSchema> tableSchemas = new List<EditorXlsTableSchema>();
        string[] xlsFolders = new string[1] { msrXlsTableSrcAsset.directory };
        FileExtAttribute fileExt = enFileExt.Xls.GetAttribute<FileExtAttribute>();
        List<EditorSelectionAsset> xlsFiles = EditorStrayFogUtility.collectAsset.CollectAsset<EditorSelectionAsset>(xlsFolders, enEditorAssetFilterClassify.DefaultAsset, false, (n) => { return n.ext.ToUpper() == fileExt.ext.ToUpper(); });
        EditorXlsTableSchema tempTable = null;
        EditorXlsTableColumnSchema tempTableCell = null;
        
        string tempColumnName = string.Empty;
        foreach (EditorSelectionAsset f in xlsFiles)
        {
            Workbook book = Workbook.Open(f.path);
            if (book.Worksheets.Count > 0)
            {
                Worksheet sheet = book.Worksheets[0];
                List<string> lstColumnName = new List<string>();

                #region XLS表中是否有同名列
                if (sheet.Cells.LastRowIndex >= msrColumnNameRowIndex)
                {
                    for (int i = 0; i <= sheet.Cells.LastColIndex; i++)
                    {
                        tempColumnName = sheet.Cells[msrColumnNameRowIndex, i].StringValue.ToUpper();
                        if (!lstColumnName.Contains(tempColumnName))
                        {
                            lstColumnName.Add(tempColumnName);
                        }
                        else
                        {
                            throw new UnityException(string.Format("There are same column【{0}】 in xls【{1}】", 
                                sheet.Cells[msrColumnNameRowIndex, i].StringValue, f.path));
                        }
                    }
                }
                #endregion

                #region 读取已保存的表架构
                msrXlsTableMapingAsset.SetName(f.nameWithoutExtension);
                msrXlsTableMapingAsset.SetType(typeof(EditorXlsTableSchema).FullName);
                if (!msrXlsTableMapingAsset.Exists())
                {
                    msrXlsTableMapingAsset.CreateAsset();
                }
                msrXlsTableMapingAsset.LoadAsset();
                tempTable = (EditorXlsTableSchema)msrXlsTableMapingAsset.engineAsset;
                #endregion

                #region 保存原始列架构
                Dictionary<string, EditorXlsTableColumnSchema> srcEditorXlsTableColumnSchemaMaping = new Dictionary<string, EditorXlsTableColumnSchema>();
                if (tempTable.columns != null && tempTable.columns.Length > 0)
                {
                    foreach (EditorXlsTableColumnSchema c in tempTable.columns)
                    {
                        tempColumnName = c.name.ToUpper();
                        if (!srcEditorXlsTableColumnSchemaMaping.ContainsKey(tempColumnName))
                        {
                            srcEditorXlsTableColumnSchemaMaping.Add(tempColumnName, c);
                        }
                    }
                }
                #endregion

                tempTable.fileName = f.path;
                tempTable.tableName = f.nameWithoutExtension;
                if (sheet.Cells.LastRowIndex >= msrColumnNameRowIndex)
                {
                    tempTable.columns = new EditorXlsTableColumnSchema[sheet.Cells.LastColIndex + 1];
                    for (int i = 0; i <= sheet.Cells.LastColIndex; i++)
                    {
                        tempTableCell = new EditorXlsTableColumnSchema();
                        tempTableCell.name = sheet.Cells[msrColumnNameRowIndex, i].StringValue;
                        tempTableCell.desc = sheet.Cells[msrColumnDescriptionRowIndex, i].StringValue;
                        tempTableCell.type = enSQLiteDataType.String;
                        tempTableCell.arrayDimension = enSQLiteDataTypeArrayDimension.NoArray;
                        tempColumnName = tempTableCell.name.ToUpper();
                        if (srcEditorXlsTableColumnSchemaMaping.ContainsKey(tempColumnName))
                        {
                            tempTableCell.isPK = srcEditorXlsTableColumnSchemaMaping[tempColumnName].isPK;
                            tempTableCell.isNull = srcEditorXlsTableColumnSchemaMaping[tempColumnName].isNull;
                        }
                        StrayFogSQLiteDataTypeHelper.ResolveCSDataType(sheet.Cells[msrColumnTypeRowIndex, i].StringValue,ref tempTableCell.type,ref tempTableCell.arrayDimension);                        
                        tempTable.columns[i] = tempTableCell;
                    }
                }
                tableSchemas.Add(tempTable);
            }
        }
        return tableSchemas;
    }
    #endregion

    #region ExportXlsSchemaToSqlite 生成Xls表结构到Sqlite数据库
    /// <summary>
    /// 生成Xls表结构到Sqlite数据库
    /// </summary>
    /// <returns>true:成功,false:失败</returns>
    public static bool ExportXlsSchemaToSqlite()
    {
        List<EditorXlsTableSchema> tableSchemas = ReadXlsSchema();
        return OnCreateSqliteSchema(tableSchemas) && OnCreateEntityScript(tableSchemas);
    }

    /// <summary>
    /// 创建SQLite数据库结构
    /// </summary>
    /// <param name="_tableSchemas">表架构</param>
    /// <returns>true:成功,false:失败</returns>
    static bool OnCreateSqliteSchema(List<EditorXlsTableSchema> _tableSchemas)
    {
        bool result = false;
        List<string> sbExcuteSql = new List<string>();
        StringBuilder sbSql = new StringBuilder();
        string entitySqlTemplete = EditorResxTemplete.SQLiteCreateTableTemplete;
        string determinantSqlTemplete = EditorResxTemplete.SQLiteCreateDeterminantViewTemplete;

        #region #Column# Templete
        string columnMark = "#Column#";
        string columnReplaceTemplete = string.Empty;
        string columnTemplete = string.Empty;

        StringBuilder sbColumnReplace = new StringBuilder();
        columnTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entitySqlTemplete, columnMark, out columnReplaceTemplete);
        #endregion

        #region #PK# Templete
        string pkMark = "#PK#";
        string pkReplaceTemplete = string.Empty;
        string pkTemplete = string.Empty;

        StringBuilder sbPkReplace = new StringBuilder();
        pkTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entitySqlTemplete, pkMark, out pkReplaceTemplete);
        #endregion

        #region #PRIMARYKEY# Templete
        string primarykeyMark = "#PRIMARYKEY#";
        string primarykeyReplaceTemplete = string.Empty;
        string primarykeyTemplete = string.Empty;

        StringBuilder sbPrimarykeyReplace = new StringBuilder();
        primarykeyTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entitySqlTemplete, primarykeyMark, out primarykeyReplaceTemplete);
        #endregion

        #region #Determinant# Templete
        string determinantMark = "#Determinant#";
        string determinantReplaceTemplete = string.Empty;
        string determinantTemplete = string.Empty;

        StringBuilder sbDeterminantReplace = new StringBuilder();
        determinantTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(determinantSqlTemplete, determinantMark, out determinantReplaceTemplete);
        #endregion        

        #region 生成表        
        int index = 0;
        foreach (EditorXlsTableSchema t in _tableSchemas)
        {
            sbColumnReplace.Length = 0;
            sbPkReplace.Length = 0;
            sbPrimarykeyReplace.Length = 0;
            index = 0;
            foreach (EditorXlsTableColumnSchema c in t.columns)
            {
                index++;
                if (c.isPK)
                {
                    sbPkReplace.Append(
                            pkTemplete.Replace("#Name#", c.name)
                    );
                }
                sbColumnReplace.Append(
                    columnTemplete
                    .Replace("#NotNull#", c.isNull ? "" : "NOT NULL")
                    .Replace("#Name#", c.name)
                    .Replace("#DataType#", StrayFogSQLiteDataTypeHelper.GetSQLiteDataTypeName(c.type, c.arrayDimension))
                    .Replace("#Dot#", index == t.columns.Length && sbPkReplace.Length == 0 ? "" : ",")
                );
            }
            if (sbPkReplace.Length > 0)
            {
                sbPkReplace = sbPkReplace.Remove(sbPkReplace.Length - 1, 1);

                sbPrimarykeyReplace.Append(primarykeyTemplete.Replace(pkReplaceTemplete, sbPkReplace.ToString()));

                sbExcuteSql.Add(entitySqlTemplete
                .Replace("#TableName#", t.name)
                .Replace(columnReplaceTemplete, sbColumnReplace.ToString())
                .Replace(primarykeyReplaceTemplete, sbPrimarykeyReplace.ToString())
                );
            }
            else
            {
                if (sbColumnReplace.Length > 0)
                {
                    sbColumnReplace = sbColumnReplace.Remove(sbColumnReplace.Length - 1, 1);
                }
                sbExcuteSql.Add(entitySqlTemplete
                .Replace("#TableName#", t.name)
                .Replace(columnReplaceTemplete, sbColumnReplace.ToString())
                .Replace(primarykeyReplaceTemplete, sbPrimarykeyReplace.ToString())
                );
            }

            if (t.isDeterminant)
            {
                sbDeterminantReplace.Append(determinantTemplete.Replace("#Name#", t.name));
            }
        }

        if (sbDeterminantReplace.Length > 0)
        {
            int ri = sbDeterminantReplace.ToString().LastIndexOf("UNION");
            sbDeterminantReplace = sbDeterminantReplace.Remove(ri, sbDeterminantReplace.Length - ri);
            sbExcuteSql.Add(determinantSqlTemplete.Replace(determinantReplaceTemplete, sbDeterminantReplace.ToString()));
        }
        result = SQLiteHelper.sqlHelper.ExecuteTransaction(sbExcuteSql.ToArray());
        #endregion

        #region 生成视图
        sbExcuteSql.Clear();
        List<EditorSelectionAsset> views = EditorStrayFogUtility.collectAsset.CollectAsset<EditorSelectionAsset>(new string[1] { EditorResxTemplete.SqliteViewSqlRoot }, enEditorAssetFilterClassify.TextAsset);
        foreach (EditorSelectionAsset v in views)
        {
            TextAsset ta = AssetDatabase.LoadAssetAtPath<TextAsset>(v.path);
            sbExcuteSql.Add(ta.text);
        }
        result = SQLiteHelper.sqlHelper.ExecuteTransaction(sbExcuteSql.ToArray());
        #endregion
        return result;
    }

    /// <summary>
    /// 创建实体脚本
    /// </summary>
    /// <param name="_tableSchemas">表架构</param>
    /// <returns>true:成功,false:失败</returns>
    static bool OnCreateEntityScript(List<EditorXlsTableSchema> _tableSchemas)
    {
        string sqliteFolder = Path.GetFullPath(enEditorApplicationFolder.Game_Script_SQLite.GetAttribute<EditorApplicationFolderAttribute>().path);
        string sqliteEntityFolder = Path.Combine(sqliteFolder, "Entities");
        string sqliteDeterminantEntitiesFolder = Path.Combine(sqliteFolder, "DeterminantEntities");

        //EditorStrayFogUtility.cmd.DeleteFolder(sqliteEntityFolder);
        //EditorStrayFogUtility.cmd.DeleteFolder(sqliteDeterminantEntitiesFolder);

        StringBuilder sbLog = new StringBuilder();
        List<SQLiteEntity> entities = new List<SQLiteEntity>();
        Dictionary<string, Dictionary<string, string>> tableColumnsDescMaping = new Dictionary<string, Dictionary<string, string>>();
        SqliteDataReader pragmaReader = null;
        SqliteDataReader schemaReader = null;
        SQLiteEntity tempEntity = null;
        SQLiteEntityProperty tempEntityProperty = null;
        string tempEntityName = string.Empty;
        string tempEntityType = string.Empty;
        string tempColumnDesc = string.Empty;
        bool tempIsDetermainant = false;
        Type tempPropertyType = null;
        bool tempMatchPropertyType = false;        
        enSQLiteDataType tempSQLiteDataType = enSQLiteDataType.String;
        enSQLiteDataTypeArrayDimension tempSQLiteDataTypeArrayDimension = enSQLiteDataTypeArrayDimension.NoArray;
        Dictionary<string, enSQLiteEntityClassify> classifyMaping = typeof(enSQLiteEntityClassify).NameToEnum<enSQLiteEntityClassify>();
        enSQLiteEntityClassify tempEntityClassify = enSQLiteEntityClassify.Table;
        float progress = 0;
        #region 收集实体            
        Int64 count = SQLiteHelper.sqlHelper.ReadEntityNamesCount();
        SqliteDataReader tableNameReader = SQLiteHelper.sqlHelper.ReadEntityNames();
        while (tableNameReader.Read())
        {
            progress++;
            tempEntityName = tableNameReader.GetString(tableNameReader.GetOrdinal("tbl_name"));
            tempEntityType = tableNameReader.GetString(tableNameReader.GetOrdinal("type"));
            tempIsDetermainant = tableNameReader.GetInt32(tableNameReader.GetOrdinal("isDetermainant")) > 0;
            foreach (KeyValuePair<string, enSQLiteEntityClassify> key in classifyMaping)
            {
                if (key.Key.ToLower().Equals(tempEntityType.ToLower()))
                {
                    tempEntityClassify = key.Value;
                    break;
                }
            }
            tempEntity = new SQLiteEntity(tempEntityName, tempEntityClassify, tempIsDetermainant);
            pragmaReader = SQLiteHelper.sqlHelper.ReadTablePragma(tempEntityName);
            schemaReader = SQLiteHelper.sqlHelper.ReadTableSchema(tempEntityName);
            //收集列名
            while (pragmaReader.Read())
            {
                tempEntityProperty = new SQLiteEntityProperty(pragmaReader.GetString(pragmaReader.GetOrdinal("name")));
                tempEntityProperty.isPK = pragmaReader.GetInt64(pragmaReader.GetOrdinal("pk")) > 0;
                tempEntityProperty.sqliteDataTypeName = pragmaReader.GetString(pragmaReader.GetOrdinal("type"));
                tempEntity.properties.Add(tempEntityProperty);
            }
            //收集列类型
            foreach (SQLiteEntityProperty c in tempEntity.properties)
            {
                tempMatchPropertyType = false;
                tempSQLiteDataType = enSQLiteDataType.String;
                tempSQLiteDataTypeArrayDimension = enSQLiteDataTypeArrayDimension.NoArray;
                tempPropertyType = schemaReader.GetFieldType(schemaReader.GetOrdinal(c.name));                
                c.typeName = StrayFogSQLiteDataTypeHelper.GetCSDataTypeName(tempSQLiteDataType, tempSQLiteDataTypeArrayDimension);
                if (!string.IsNullOrEmpty(c.sqliteDataTypeName))
                {
                    tempMatchPropertyType = StrayFogSQLiteDataTypeHelper.ResolveSQLiteDataType(c.sqliteDataTypeName, ref tempSQLiteDataType, ref tempSQLiteDataTypeArrayDimension);
                    if (tempMatchPropertyType)
                    {
                        c.typeName = StrayFogSQLiteDataTypeHelper.GetCSDataTypeName(tempSQLiteDataType, tempSQLiteDataTypeArrayDimension);
                    }
                    else
                    {
                        c.typeName = tempPropertyType.FullName;
                    }
                }
            }
            entities.Add(tempEntity);
            EditorUtility.DisplayProgressBar("Collection Entity", tempEntity.name, progress / count);
        }
        #endregion

        #region 收集描述
        foreach (EditorXlsTableSchema t in _tableSchemas)
        {
            if (!tableColumnsDescMaping.ContainsKey(t.tableName))
            {
                tableColumnsDescMaping.Add(t.tableName, new Dictionary<string, string>());
            }
            if (t.columns != null && t.columns.Length > 0)
            {
                foreach (EditorXlsTableColumnSchema c in t.columns)
                {
                    if (!tableColumnsDescMaping[t.tableName].ContainsKey(c.name))
                    {
                        tableColumnsDescMaping[t.tableName].Add(c.name, c.desc);
                    }
                }
            }            
        }
        #endregion

        #region 生成实体对象
        EditorTextAssetConfig cfgEntityScript = new EditorTextAssetConfig("", sqliteEntityFolder, enFileExt.CS, "");
        progress = 0;
        foreach (SQLiteEntity t in entities)
        {
            progress++;
            if (t.isDetermainant)
            {
                #region 行列式实体对象
                string entityScriptTemplete = EditorResxTemplete.SQLiteDeterminantEntityScriptTemplete;

                #region #Row#
                string rowMark = "#Row#";
                string rowReplaceTemplete = string.Empty;
                string rowTemplete = string.Empty;
                StringBuilder sbRowReplace = new StringBuilder();
                rowTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entityScriptTemplete, rowMark, out rowReplaceTemplete);
                #endregion

                List<string> determinantColumnName = OnReadDeterminantColumnName(t.name,out tempEntityType);
                //如果对应的XLS数据表则以XLS数据表为主，如果没有则看SQLite数据库里有没有对应的表
                if (determinantColumnName.Count > 0)
                {
                    foreach (string name in determinantColumnName)
                    {
                        tempColumnDesc = name;
                        if (tableColumnsDescMaping.ContainsKey(t.name) && tableColumnsDescMaping[t.name].ContainsKey(name))
                        {
                            tempColumnDesc = OnTransDescToSummary(tableColumnsDescMaping[t.name][name]);
                        }
                        sbRowReplace.Append(
                            rowTemplete
                            .Replace("#Name#", name)
                            .Replace("#Desc#", tempColumnDesc)
                            .Replace("#Type#", tempEntityType)
                            );
                    }                    
                }
                else
                {
                    Dictionary<string, SQLiteEntityProperty> rowMaping = new Dictionary<string, SQLiteEntityProperty>();
                    foreach (SQLiteEntityProperty p in t.properties)
                    {
                        rowMaping.Add(p.name, p);
                    }
                    SqliteDataReader dataReader = SQLiteHelper.sqlHelper.ReadFullTable(t.name);
                    while (dataReader.Read())
                    {
                        tempColumnDesc = dataReader.GetValue(0).ToString();
                        if (tableColumnsDescMaping.ContainsKey(t.name) && tableColumnsDescMaping[t.name].ContainsKey(dataReader.GetValue(0).ToString()))
                        {
                            tempColumnDesc = OnTransDescToSummary(tableColumnsDescMaping[t.name][dataReader.GetValue(0).ToString()]);
                        }

                        sbRowReplace.Append(
                            rowTemplete
                            .Replace("#Name#", dataReader.GetValue(0).ToString())
                            .Replace("#Desc#", tempColumnDesc)
                            .Replace("#Type#", rowMaping[dataReader.GetName(1)].typeName)
                            );
                    }
                }
                cfgEntityScript.SetName(t.className);
                cfgEntityScript.SetDirectory(sqliteDeterminantEntitiesFolder);
                entityScriptTemplete = entityScriptTemplete
                    .Replace("#EntityName#", t.name)
                    .Replace("#ClassName#", cfgEntityScript.name)
                    .Replace(rowReplaceTemplete, sbRowReplace.ToString())
                    ;
                cfgEntityScript.SetText(entityScriptTemplete);
                cfgEntityScript.CreateAsset();
                sbLog.AppendLine(cfgEntityScript.fileName);
                #endregion
            }
            else
            {
                #region 非行列式实体对象
                string entityScriptTemplete = EditorResxTemplete.SQLiteEntityScriptTemplete;

                #region #Properties#
                string propertyMark = "#Properties#";
                string propertyReplaceTemplete = string.Empty;
                string propertyTemplete = string.Empty;
                StringBuilder sbPropertyReplace = new StringBuilder();
                propertyTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entityScriptTemplete, propertyMark, out propertyReplaceTemplete);
                #endregion

                #region #PK#
                string pkMark = "#PK#";
                string pkReplaceTemplete = string.Empty;
                string pkTemplete = string.Empty;
                StringBuilder sbPkReplace = new StringBuilder();
                pkTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(entityScriptTemplete, pkMark, out pkReplaceTemplete);
                #endregion

                foreach (SQLiteEntityProperty p in t.properties)
                {
                    tempColumnDesc = p.name;
                    if (tableColumnsDescMaping.ContainsKey(t.name) && tableColumnsDescMaping[t.name].ContainsKey(p.name))
                    {
                        tempColumnDesc = OnTransDescToSummary(tableColumnsDescMaping[t.name][p.name]);
                    }

                    sbPropertyReplace.Append(
                        propertyTemplete
                        .Replace("#Name#", p.name)
                        .Replace("#Desc#", tempColumnDesc)
                        .Replace("#Type#", p.typeName)
                        );
                    if (p.isPK)
                    {
                        sbPkReplace.Append(pkTemplete.Replace("#Name#", p.name));
                    }
                }
                cfgEntityScript.SetName(t.className);
                cfgEntityScript.SetDirectory(sqliteEntityFolder);
                entityScriptTemplete = entityScriptTemplete
                    .Replace("#EntityName#", t.name)
                    .Replace("#ClassName#", cfgEntityScript.name)
                    .Replace(pkReplaceTemplete, sbPkReplace.ToString())
                    .Replace(propertyReplaceTemplete, sbPropertyReplace.ToString())
                    ;

                cfgEntityScript.SetText(entityScriptTemplete);
                cfgEntityScript.CreateAsset();
                sbLog.AppendLine(cfgEntityScript.fileName);
                #endregion
            }
            EditorUtility.DisplayProgressBar("Build Entity", t.name, progress / entities.Count);
        }
        #endregion

        #region 生成实体操作扩展
        EditorTextAssetConfig cfgHeplerExtendScript = new EditorTextAssetConfig("SQLiteEntityHelperExtend", sqliteFolder, enFileExt.CS, "");
        string helperScriptTemplete = EditorResxTemplete.SQLiteEntityHelperExtendTemplete;

        string entityMapingMark = "#EntityMaping#";
        string entityMapingReplaceTemplete = string.Empty;
        string entityMapingTemplete = string.Empty;
        StringBuilder sbEntityMapingReplace = new StringBuilder();
        entityMapingTemplete = EditorStrayFogUtility.regex.MatchPairMarkTemplete(helperScriptTemplete, entityMapingMark, out entityMapingReplaceTemplete);
        progress = 0;
        foreach (SQLiteEntity t in entities)
        {
            progress++;
            sbEntityMapingReplace.Append(entityMapingTemplete.Replace("#ClassName#", t.className).Replace("#TableName#", t.name));
            EditorUtility.DisplayProgressBar("Build Entity Extend", t.name, progress / entities.Count);
        }
        cfgHeplerExtendScript.SetText(helperScriptTemplete.Replace(entityMapingReplaceTemplete, sbEntityMapingReplace.ToString()));
        cfgHeplerExtendScript.CreateAsset();
        #endregion

        SQLiteHelper.sqlHelper.Close();
        EditorUtility.ClearProgressBar();
        EditorStrayFogApplication.ExecuteMenu_AssetsRefresh();
        Debug.Log(sbLog.ToString());
        Debug.Log("BuildSQLiteEntity Succeed!");
        return true;
    }

    /// <summary>
    /// 读取行列XLS表列名称
    /// </summary>
    /// <param name="_tableName">表名称</param>
    /// <param name="_columnType">列类型</param>
    /// <returns>行列式列名称</returns>
    static List<string> OnReadDeterminantColumnName(string _tableName,out string _columnType)
    {
        List<string> columnNames = new List<string>();
        msrXlsTableSrcAsset.SetName(_tableName);
        Debug.Log(msrXlsTableSrcAsset.fileName);
        Workbook book = Workbook.Open(msrXlsTableSrcAsset.fileName);
        enSQLiteDataType tempSQLiteDataType = enSQLiteDataType.String;
        enSQLiteDataTypeArrayDimension tempSQLiteDataTypeArrayDimension = enSQLiteDataTypeArrayDimension.NoArray;
        if (book.Worksheets.Count > 0)
        {
            Worksheet sheet = book.Worksheets[0];
            if (sheet.Cells.LastRowIndex >= msrColumnTypeRowIndex)
            {
                StrayFogSQLiteDataTypeHelper.ResolveCSDataType(
                    sheet.Cells[msrColumnTypeRowIndex, 1].StringValue, 
                    ref tempSQLiteDataType, ref tempSQLiteDataTypeArrayDimension);                
            }            
            if (sheet.Cells.LastRowIndex >= msrColumnDataRowStartIndex)
            {
                for (int i = msrColumnDataRowStartIndex; i <= sheet.Cells.LastRowIndex; i++)
                {
                    columnNames.Add(sheet.Cells[i, 0].StringValue);
                }
            }
        }
        _columnType = StrayFogSQLiteDataTypeHelper.GetCSDataTypeName(tempSQLiteDataType, tempSQLiteDataTypeArrayDimension);
        return columnNames;
    }

    /// <summary>
    /// 转换描述为Summary形式
    /// </summary>
    /// <param name="_desc">描述</param>
    /// <returns>描述</returns>
    static string OnTransDescToSummary(string _desc)
    {
        StringBuilder descSb = new StringBuilder();
        StringReader reader = new StringReader(_desc);
        string line = string.Empty;
        int num = 0;
        do
        {
            line = reader.ReadLine();
            if (!string.IsNullOrEmpty(line))
            {
                if (num == 0)
                {
                    descSb.Append(line);
                }
                else
                {
                    descSb.Append(Environment.NewLine + "	///" + line);
                }
                num++;
            }
        } while (!string.IsNullOrEmpty(line));
        return descSb.ToString();
    }

    #endregion
}