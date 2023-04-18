using UnityEngine.Events;
using System;
using System.Reflection;
using SMLHelper.Utility;
using System.Runtime.InteropServices;

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
        onTouchEvent.AddListener(GetCallbackAction());
        onTouch.onTouch = onTouchEvent;
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
        return null;
    }

    delegate void OnTouchCallback(Collider collider);
}