﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
/// <summary>
/// 引擎程序集
/// </summary>
public sealed class StrayFogAssembly
{
    /// <summary>
    /// 动态程序集组
    /// </summary>
    public static List<Assembly> dynamicAssemblies { get; private set; }

    #region LoadDynamicAssembly 加载所有dll动态库
    /// <summary>
    /// 加载所有dll动态库
    /// </summary>
    public static void LoadDynamicAssembly()
    {
        if (dynamicAssemblies == null)
        {
            dynamicAssemblies = new List<Assembly>();
            List<XLS_Config_Table_AsmdefMap> maps = StrayFogSQLiteEntityHelper.Select<XLS_Config_Table_AsmdefMap>();
            string path = string.Empty;
            Assembly tmpAssembly = null;
            foreach (XLS_Config_Table_AsmdefMap m in maps)
            {
                if (StrayFogGamePools.setting.isInternal)
                {
                    path = m.asmdefDLLPath;
                }
                else
                {
                    path = Path.Combine(StrayFogGamePools.setting.assetBundleRoot, m.asmdefAssetbundleName);
                }                
                if (File.Exists(path))
                {
                    tmpAssembly = Assembly.Load(File.ReadAllBytes(path));
                    dynamicAssemblies.Add(tmpAssembly);
                }
            }
            tmpAssembly = Assembly.GetCallingAssembly();
            if (tmpAssembly != null && !dynamicAssemblies.Contains(tmpAssembly))
            {
                dynamicAssemblies.Add(tmpAssembly);
            }
            tmpAssembly = Assembly.GetEntryAssembly();
            if (tmpAssembly != null && !dynamicAssemblies.Contains(tmpAssembly))
            {
                dynamicAssemblies.Add(tmpAssembly);
            }
            tmpAssembly = Assembly.GetExecutingAssembly();
            if (tmpAssembly != null && !dynamicAssemblies.Contains(tmpAssembly))
            {
                dynamicAssemblies.Add(tmpAssembly);
            }
        }
    }
    #region Old Dll
    //SqliteDataReader reader = StrayFogSQLiteHelper.sqlHelper.ExecuteQuery("SELECT * FROM View_DynamicDll");
    //string path = string.Empty;
    //while (reader.Read())
    //{
    //    if (StrayFogGamePools.setting.isInternal)
    //    {
    //        path = reader.GetString(0).Replace(@"\", "/");
    //    }
    //    else
    //    {
    //        path = Path.Combine(StrayFogGamePools.setting.assetBundleRoot, reader.GetString(1)).Replace(@"\", "/");
    //    }
    //    if (File.Exists(path))
    //    {
    //        mDynamicAssemblyArray.Add(Assembly.LoadFile(path));
    //    }
    //}
    //reader.Close();
    //reader = null;
    #endregion
    #endregion

    #region GetType Type映射
    /// <summary>
    /// Type映射
    /// </summary>
    static Dictionary<int, Type> mTypeMaping = new Dictionary<int, Type>();
    /// <summary>
    /// 获得指定名称的Type
    /// </summary>
    /// <param name="_typeName">Type名称</param>
    /// <returns>Type</returns>
    public static Type GetType(string _typeName)
    {
        int key = _typeName.GetHashCode();
        if (!mTypeMaping.ContainsKey(key))
        {
            Type type = null;
            if (dynamicAssemblies != null && dynamicAssemblies.Count > 0)
            {
                foreach (Assembly m in dynamicAssemblies)
                {
                    type = m.GetType(_typeName);
                    if (type == null)
                    {
                        type = m.GetType(m.GetName().Name + "." + _typeName);
                    }
                    if (type != null)
                    {
                        break;
                    }
                }
            }
            mTypeMaping.Add(key, type);
        }
        return mTypeMaping[key];
    }
    #endregion

    #region GetExportedTypes 获得继承于指定类别的所有类别组
    /// <summary>
    /// 子类组映射
    /// </summary>
    static Dictionary<int, List<Type>> mExportedTypesMaping = new Dictionary<int, List<Type>>();
    /// <summary>
    /// 获得继承于指定类别的所有类别组
    /// </summary>
    /// <param name="_parentType">父类别</param>
    /// <returns>类别组</returns>
    public static List<Type> GetExportedTypes(Type _parentType)
    {
        int key = _parentType.GetHashCode();
        if (!mExportedTypesMaping.ContainsKey(key))
        {
            mExportedTypesMaping.Add(key, new List<Type>());
            if (dynamicAssemblies != null)
            {
                Type[] types = null;
                foreach (Assembly m in dynamicAssemblies)
                {
                    types = m.GetExportedTypes();
                    if (types != null && types.Length > 0)
                    {
                        foreach (Type t in types)
                        {
                            if (t.IsTypeOrSubTypeOf(_parentType))
                            {
                                mExportedTypesMaping[key].Add(t);
                            }
                        }
                    }
                }
            }
        }
        return mExportedTypesMaping[key];
    }
    #endregion
}
