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
    /// Callback method type.
    /// </summary>
    public Type callbackType;

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
            if (component.GetType() == callbackType)
            {
                var action = Delegate.CreateDelegate(typeof(OnTouchCallback), component, callbackMethodName);
                return new UnityAction<Collider>((collider) => action.Method.Invoke(action.Target, new object[] { collider }));
            }
        }
        return null;
    }

    delegate void OnTouchCallback(Collider collider);
}