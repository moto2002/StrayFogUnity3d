﻿using System;
/// <summary>
/// 资源请求加载到内存
/// </summary>
public interface IAssetBundleRequestInMemory
{
    /// <summary>
    /// 输入
    /// </summary>
    IAssetBundleInput input { get; }
    /// <summary>
    /// 输出事件
    /// </summary>
    Action<IAssetBundleOutput> outputEvent { get; }
    /// <summary>
    /// 进度事件
    /// </summary>
    Action<float, IAssetBundleInput> progressEvent { get; }
}
