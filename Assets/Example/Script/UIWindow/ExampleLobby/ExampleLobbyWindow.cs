using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// ExampleLobbyWindow
/// </summary>
[AddComponentMenu("StrayFog/Example/UIWindow/ExampleLobbyWindow")]
public class ExampleLobbyWindow : AbsUIWindowView
{
    /// <summary>
    /// Awake
    /// </summary>
    void Awake()
    {
        Text txt = transform.Find("btnRadarBg/Text").GetComponent<Text>();
        Button btnRadarBg = transform.Find("btnRadarBg").GetComponent<Button>();
        btnRadarBg.onClick.AddListener(() =>
        {
            Vector3 scale = Vector3.one * 4f;
            txt.text = scale.ToString();
            btnRadarBg.transform.localScale = scale;

            if (StrayFogGamePools.uiWindowManager.IsOpenedWindow(enUIWindow.MessageBoxWindow))
            {
                StrayFogGamePools.uiWindowManager.CloseWindow(enUIWindow.MessageBoxWindow);
            }
            else
            {
                StrayFogGamePools.uiWindowManager.OpenWindow(enUIWindow.MessageBoxWindow);
            }            
        });
        transform.Find("btnRadar").gameObject.AddComponent<UIDragMono>();        
    }
}