using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// UserGuideConfig实体
/// </summary>
[SQLiteTableMap(-734907299,"Assets/Game/Editor/XLS_Config/UserGuideConfig.xlsx","UserGuideConfig", enSQLiteEntityClassify.Table,false, 1,4,2,4,"Assets/Game/Editor/XLS_Config/XLS_Config.db","Assets/497757a9c5b2ec17ded656170b51c788/c_334573285",typeof(XLS_Config_Table_UserGuideConfig),true,false)]
public partial class XLS_Config_Table_UserGuideConfig: AbsStrayFogSQLiteEntity
{
	

	
	protected override int OnResolvePkSequenceId()
    {
        return (id.ToString()).UniqueHashCode();
    }
	

	#region Properties	
		
	/// <summary>
	/// 标识id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,1,"id","","@id1",true,false)]	
	public int id { get; private set; }	
		
	/// <summary>
	/// 描述
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.NoArray,2,"desc","","@desc2",false,true)]	
	public string desc { get; private set; }	
		
	/// <summary>
	/// 下一引导id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,3,"nextId","","@nextId3",false,false)]	
	public int nextId { get; private set; }	
		
	/// <summary>
	/// 引导类型
	///0：强引导【禁用玩家操作，等待引导触发】【显示引导窗口】
	///1：弱引导【玩家可操作，等待引导触发】【不显示窗口】
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,4,"guideType","","@guideType4",false,false)]	
	public int guideType { get; private set; }	
		
	/// <summary>
	/// 强引导窗口显示类型
	///0：显示全部
	///1：隐藏引导窗口遮罩背景
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,5,"strongGuidDisplayType","","@strongGuidDisplayType5",false,false)]	
	public int strongGuidDisplayType { get; private set; }	
		
	/// <summary>
	/// 关卡id
	///0：忽略关卡
	///>0：指定关卡
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.NoArray,6,"levelId","","@levelId6",false,false)]	
	public int levelId { get; private set; }	
		
	/// <summary>
	/// 是否忽略服务端返回
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Boolean,enSQLiteDataTypeArrayDimension.NoArray,7,"isIngoreServerBack","","@isIngoreServerBack7",false,false)]	
	public bool isIngoreServerBack { get; private set; }	
		
	/// <summary>
	/// 【引导样式】组[索引与触发参考对象组索引相同]
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,8,"styleIds","","@styleIds8",false,false)]	
	public int[] styleIds { get; private set; }	
		
	/// <summary>
	/// 【触发参考对象】组
	///UserGuideReferObject.Id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,9,"triggerReferObjectIds","","@triggerReferObjectIds9",false,false)]	
	public int[] triggerReferObjectIds { get; private set; }	
		
	/// <summary>
	/// 【触发条件】数据类型
	///0：触发参考对象
	///1：玩家等级
	///2：物品
	///3：装备
	///4：任务
	///5：玩家状态
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,10,"triggerConditionTypes","","@triggerConditionTypes10",false,false)]	
	public int[] triggerConditionTypes { get; private set; }	
		
	/// <summary>
	/// 【触发条件】条件运算符
	///0：必须满足条件【And与运算】
	///1：可选满足条件【Or或运算】
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,11,"triggerConditionOperators","","@triggerConditionOperators11",false,false)]	
	public int[] triggerConditionOperators { get; private set; }	
		
	/// <summary>
	/// 【触发条件】数据值【组分隔符|】
	///0：[0：隐藏,1：显示]
	///1：等级值[M_N]
	///2：物品配置Id_物品数量
	///3：装备配置Id_装备数量
	///4：[0:任意任务,>0指定任务]
	///5：[0:待机,1:攻击...]
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.OneDimensionArray,12,"triggerConditionValues","","@triggerConditionValues12",false,false)]	
	public string[] triggerConditionValues { get; private set; }	
		
	/// <summary>
	/// 【验证参考对象】组
	///UserGuideReferObject.Id
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,13,"validateReferObjectIds","","@validateReferObjectIds13",false,false)]	
	public int[] validateReferObjectIds { get; private set; }	
		
	/// <summary>
	/// 【验证条件】条件运算符
	///0：必须满足条件【And与运算】
	///1：可选满足条件【Or或运算】
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,14,"validateConditionOperators","","@validateConditionOperators14",false,false)]	
	public int[] validateConditionOperators { get; private set; }	
		
	/// <summary>
	/// 【验证条件】类别
	///0：Click 点击触发参考对象
	///1：MoveTo 触发参考对象移动到验证参考对象
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.Int32,enSQLiteDataTypeArrayDimension.OneDimensionArray,15,"validateConditionTypes","","@validateConditionTypes15",false,false)]	
	public int[] validateConditionTypes { get; private set; }	
		
	/// <summary>
	/// 【验证条件】值
	///0：[0:点击任意触发参考对象,0_1_2：按索引触发参考对象]
	///1：[0:拖拽任意参考对象到任意验证对象,0_1|2_3指定索引的参考对象拖拽到指定验证对象]
	/// </summary>
	[SQLiteFieldType(enSQLiteDataType.String,enSQLiteDataTypeArrayDimension.OneDimensionArray,16,"validateConditionValues","","@validateConditionValues16",false,false)]	
	public string[] validateConditionValues { get; private set; }	
	
	#endregion

	#region Set Properties
	
	#endregion
}