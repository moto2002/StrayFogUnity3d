using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Report实体
/// </summary>
[SQLiteTableMap(-546907515,"Assets/Game/Editor/XLS_Report/Report.xlsx","Report", enSQLiteEntityClassify.Table,false, 1,4,2,4,"Assets/Game/Editor/XLS_Report/XLS_Report.db","Assets/497757a9c5b2ec17ded656170b51c788/c_1478679219",typeof(XLS_Report_Table_Report),true,true)]
public partial class XLS_Report_Table_Report: AbsStrayFogSQLiteEntity
{
		
	#region constructor
	/// <summary>
    /// constructor
    /// </summary>
    XLS_Report_Table_Report()
    {
    }

    /// <summary>
    /// constructor
    /// </summary>
   /// <param name="_idCol">idCol</param>	
    public XLS_Report_Table_Report(int _idCol)
    {
		idCol = _idCol;
    }
    #endregion
	

	
	protected override int OnResolvePkSequenceId()
    {
        return (idCol.ToString()).UniqueHashCode();
    }
	

	#region Properties	
		
	/// <summary>
	/// id主键
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,1,"idCol","","@idCol1",true,false)]	
	public int idCol { get; private set; }	
		
	/// <summary>
	/// 字符串
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray,2,"stringCol","","@stringCol2",false,false)]	
	public string stringCol { get; private set; }	
	
	#endregion

	#region Set Properties
	
	/// <summary>
	/// Set stringCol
	/// </summary>
	/// <param name="_stringCol">stringCol value</param>
	public void Set_stringCol(string _stringCol)
	{
		stringCol = _stringCol;
	}
	
	#endregion
}