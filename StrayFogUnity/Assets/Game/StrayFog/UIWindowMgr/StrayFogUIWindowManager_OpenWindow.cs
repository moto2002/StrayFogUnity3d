﻿using System;
/// <summary>
/// UI窗口管理器【打开窗口】
/// </summary>
public partial class StrayFogUIWindowManager
{
    /// <summary>
    /// 打开窗口事件
    /// </summary>
    public event UIWindowEntityEventHandler<AbsUIWindowView> OnOpenWindowEventHandler;

    #region FolderId FileId
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_folderId">文件夹id</param>
    /// <param name="_fileId">文件id</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int _folderId, int _fileId, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_folderId, _fileId, (wins, paras) => { }, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_folderIds">文件夹id组</param>
    /// <param name="_fileIds">文件id组</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int[] _folderIds, int[] _fileIds, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_folderIds, _fileIds, (wins, paras) => { }, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_folderId">文件夹id</param>
    /// <param name="_fileId">文件id</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int _folderId, int _fileId, UIWindowEntityEventHandler<AbsUIWindowView> _onCallback, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_folderId, _fileId, _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <typeparam name="W">窗口类型</typeparam>
    /// <param name="_folderId">文件夹id</param>
    /// <param name="_fileId">文件id</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow<W>(int _folderId, int _fileId, UIWindowEntityEventHandler<W> _onCallback, params object[] _parameters)
        where W : AbsUIWindowView
    {
        OpenWindow<W>(OnGetWindowSetting(_folderId, _fileId), _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_folderIds">文件夹id组</param>
    /// <param name="_fileIds">文件id组</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int[] _folderIds, int[] _fileIds, UIWindowEntityEventHandler<AbsUIWindowView> _onCallback, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_folderIds, _fileIds, _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <typeparam name="W">窗口类型</typeparam>
    /// <param name="_folderIds">文件夹id组</param>
    /// <param name="_fileIds">文件id组</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow<W>(int[] _folderIds, int[] _fileIds, UIWindowEntityEventHandler<W> _onCallback, params object[] _parameters)
        where W : AbsUIWindowView
    {
        OpenWindow<W>(OnGetWindowSetting(_folderIds, _fileIds), _onCallback, _parameters);
    }
    #endregion

    #region WindowId
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_windowId">窗口Id</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int _windowId, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_windowId, (wins, paras) => { }, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_windowIds">窗口Id组</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int[] _windowIds, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_windowIds, (wins, paras) => { }, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_windowId">窗口Id</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int _windowId, UIWindowEntityEventHandler<AbsUIWindowView> _onCallback, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_windowId, _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <typeparam name="W">窗口类型</typeparam>
    /// <param name="_windowId">窗口Id</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow<W>(int _windowId, UIWindowEntityEventHandler<W> _onCallback, params object[] _parameters)
        where W : AbsUIWindowView
    {
        OpenWindow<W>(new int[1] { _windowId }, _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="_windowIds">窗口Id组</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow(int[] _windowIds, UIWindowEntityEventHandler<AbsUIWindowView> _onCallback, params object[] _parameters)
    {
        OpenWindow<AbsUIWindowView>(_windowIds, _onCallback, _parameters);
    }
    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <typeparam name="W">窗口类型</typeparam>
    /// <param name="_windowIds">窗口Id组</param>
    /// <param name="_onCallback">回调</param>
    /// <param name="_parameters">参数组</param>
    public void OpenWindow<W>(int[] _windowIds, UIWindowEntityEventHandler<W> _onCallback, params object[] _parameters)
        where W : AbsUIWindowView
    {
        OnOpenWindow<W>(OnGetWindowSetting(_windowIds), _onCallback, _parameters);
    }
    #endregion
}