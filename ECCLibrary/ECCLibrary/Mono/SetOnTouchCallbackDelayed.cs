using UnityEngine.Events;
using System;
using System.Reflection;
using SMLHelper.Utility;
using System.Runtime.InteropServices;
using static ProtoBuf.Meta.TypeModel;
using static OnTouch;

namespace ECCLibrary.Mono;

/// <summary>
/// Sets the <see cref="OnTouch.onTouch"/> callback in the Start method.
/// </summary>
public class SetOnTouchCallbackDelayed : MonoBehaviour
{
    /// <summary>
    /// Component to affect.
    /// </summary>
    public OnTouch onTouch;

    /// <summary>
    /// GameObject holding the callback component.
    /// </summary>
    public GameObject callbackGameObject;

    /// <summary>
    /// Callback method type name.
    /// </summary>
    public string callbackTypeName;

    /// <summary>
    /// Callback method name.
    /// </summary>
    public string callbackMethodName;

    private void Start()
    {
        var onTouchEvent = new OnTouch.OnTouchEvent();
        var action = GetCallbackAction();
        if (action != null)
        {
            onTouchEvent.AddListener(action);
            onTouch.onTouch = onTouchEvent;
        }
    }

    private UnityAction<Collider> GetCallbackAction()
    {
        foreach (var component in callbackGameObject.GetComponents<Component>())
        {
            if (component.GetType().Name == callbackTypeName)
            {
                var action = Delegate.CreateDelegate(typeof(OnTouchCallback), component, callbackMethodName);
                return new UnityAction<Collider>((collider) => action.Method.Invoke(action.Target, new object[] { collider }));
            }
        }
        ECCPlugin.logger.LogError($"Error in SetOnTouchCallbackDelayed.GetCallbackAction: Failed to find correct OnTouch target! Search parameters: {callbackGameObject}, {callbackTypeName}, {callbackMethodName}.");
        return null;
    }

    delegate void OnTouchCallback(Collider collider);
}