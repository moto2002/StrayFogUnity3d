﻿/// <summary>
/// SQLite实体
/// </summary>
public abstract class AbsSQLiteEntity
{
    /// <summary>
    /// PK键组
    /// </summary>
    public abstract object[] pks { get; }
    /// <summary>
    /// 解析数据
    /// </summary>
    public void Resolve()
    {
        OnResolve();
    }

    /// <summary>
    /// 解析数据
    /// </summary>
    protected virtual void OnResolve() { }
}