using UnityEngine;
/// <summary>
/// OnRenderImage
/// </summary>
[AddComponentMenu("StrayFog/Game/SimulateMonoBehaviour/SimulateMonoBehaviour_OnRenderImage")]
public sealed class SimulateMonoBehaviour_OnRenderImage : AbsSimulateMonoBehaviourMethod
{
	/// <summary>
    /// 方法分类
    /// </summary>
    public override int methodClassify { get { return -1899917187; } }

	/// <summary>
    /// OnRenderImage
    /// </summary>
    void OnRenderImage(UnityEngine.RenderTexture _source,UnityEngine.RenderTexture _destination)
    {
        simulateMonoBehaviour.OnRenderImage(_source,_destination);
    }
}