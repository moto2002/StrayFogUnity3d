using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// GameLanguage实体
/// </summary>
public partial class XLS_Config_Table_GameLanguage: AbsStrayFogSQLiteEntity
{
	/// <summary>
    /// PK键组
    /// </summary>
    public override object[] pks { get { return new List<object>() { }.ToArray(); } }

	
	/// <summary>
	/// 引导提示
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray)]
	public string GuideTip { get; private set; }
	/// <summary>
	/// 大厅提示
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray)]
	public string LobbyTip { get; private set; }
}