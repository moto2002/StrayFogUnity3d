﻿using UnityEngine;
/// <summary>
/// MonoBehaviour扩展
/// </summary>
public static class MonoBehaviourExtend
{
    /// <summary>
    /// 添加动态组件
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <param name="_go">GameObject</param>
    /// <returns>组件</returns>
    public static T AddDynamicComponent<T>(this GameObject _go)
        where T : AbsMonoBehaviour
    {
        return _go.AddComponent<T>();
    }

    /// <summary>
    /// 添加动态组件
    /// </summary>
    /// <typeparam name="T">组件</typeparam>
    /// <param name="_go">GameObject</param>
    /// <returns>组件</returns>
    public static T AddUIDynamicComponent<T>(this GameObject _go)
        where T : AbsUIBehaviour
    {
        return _go.AddComponent<T>();
    }
}
