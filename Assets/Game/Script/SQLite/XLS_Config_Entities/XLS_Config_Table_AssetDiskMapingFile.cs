using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// AssetDiskMapingFile实体
/// </summary>
[SQLiteTableMap(-206577796,"Assets/Game/Editor/XLS_Config/AssetDiskMapingFile.xlsx","AssetDiskMapingFile", enSQLiteEntityClassify.Table,false, 1,4,2,4,"c__1581584321",typeof(XLS_Config_Table_AssetDiskMapingFile))]
public partial class XLS_Config_Table_AssetDiskMapingFile: AbsStrayFogSQLiteEntity
{
	/// <summary>
    /// PK键组
    /// </summary>
    public override object[] pks { get { return new List<object>() { fileId,folderId}.ToArray(); } }

	
	/// <summary>
	/// 文件id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray)]
	public int fileId { get; private set; }
	/// <summary>
	/// 文件夹id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray)]
	public int folderId { get; private set; }
	/// <summary>
	/// 内部资源路径
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray)]
	public string inSide { get; private set; }
	/// <summary>
	/// 扩展名
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray)]
	public string ext { get; private set; }
	/// <summary>
	/// 外部资源路径
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray)]
	public string outSide { get; private set; }
	/// <summary>
	/// 扩展名枚举值
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray)]
	public int extEnumValue { get; private set; }
}