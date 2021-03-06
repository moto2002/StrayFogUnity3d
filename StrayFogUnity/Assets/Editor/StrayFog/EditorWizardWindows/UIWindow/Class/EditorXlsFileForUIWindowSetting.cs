﻿#if UNITY_EDITOR
using System.IO;
/// <summary>
/// UIWindowSetting配置资源文件夹
/// </summary>
public class EditorXlsFileForUIWindowSetting : AbsEditorSavedAsset
{
    /// <summary>
    /// 文件后缀
    /// </summary>
    static FileExtAttribute msFileExt = typeof(enFileExt).GetAttributeForConstField<FileExtAttribute>(enFileExt.Xlsx);

    /// <summary>
    /// 可保存资源分类
    /// </summary>
    public override enEditorSavedAssetClassify classify
    {
        get
        {
            return enEditorSavedAssetClassify.File;
        }
    }

    /// <summary>
    /// 绘制GUI描述
    /// </summary>
    protected override string drawGUIDesc
    {
        get
        {
            return string.Format("Setting 【UIWindowSetting{0} 】{1}", msFileExt.ext, classify);
        }
    }

    /// <summary>
    /// 合法文件后缀组
    /// </summary>
    /// <returns>合法文件后缀组</returns>
    protected override int[] OnLegalFileExts() { return new int[1] { enFileExt.Xlsx }; }
}
#endif
