using System;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ExampleGuideWindow
/// </summary>
[AddComponentMenu("StrayFog/Example/UIWindow/ExampleGuideWindow")]
public class ExampleGuideWindow : AbsUIGuideWindowView
{
    /// <summary>
    /// �����ļ�
    /// </summary>
    protected override enAssetDiskMapingFile materialFile { get { return enAssetDiskMapingFile.f_GuideMaskShader_mat; } }

    /// <summary>
    /// �����ļ���
    /// </summary>
    protected override enAssetDiskMapingFolder materialFolder { get { return enAssetDiskMapingFolder.Assets_Game_AssetBundles_Materials; } }
    
}