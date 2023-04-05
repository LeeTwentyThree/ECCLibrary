using UnityEngine.Events;
using System;

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
    /// Callback method.
    /// </summary>
    public Action<Collider> callback;

    private void Start()
    {
        var onTouchEvent = new OnTouch.OnTouchEvent();
        onTouchEvent.AddListener(new UnityAction<Collider>(callback));
        onTouch.onTouch = onTouchEvent;
    }
}