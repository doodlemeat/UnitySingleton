using UnityEngine;
using System;

public abstract class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour {
    protected static T instance;

    protected void Awake() {
        HandleAttribute();

        if (instance == null) {
            instance = this as T;
            OnAwake();
        }
    }

    protected virtual void OnAwake() { }

    private void HandleAttribute() {
        UnitySingletonAttribute attribute = Attribute.GetCustomAttribute(typeof(T), typeof(UnitySingletonAttribute)) as UnitySingletonAttribute;
        if (attribute == null) {
            Debug.LogError("Failed to find " + typeof(UnitySingletonAttribute).Name + " on " + typeof(T));
        }
        else {
            if (attribute.mustBeOnlyComponentOnObject) {
                Component[] allComponents = GetComponents<Component>();
                if (allComponents.Length > 2) {
                    Debug.LogError(typeof(T) + " must be the only component on this object");
                }
            }

            if (attribute.mustBeWithinRootObject) {
                if (transform.parent != null) {
                    Debug.LogError(typeof(T) + " is required to be within a root object");
                }
            }

            if (attribute.mustBeOnlyInstanceInScene) {
                var instances = FindObjectsOfType<T>();
                if (instances.Length > 1) {
                    Debug.LogError(instances.Length + " instances of " + typeof(T) + " found in the scene. There can only be one " + typeof(T) + " in the scene.");
                }
            }
        }
    }
}

[AttributeUsage(AttributeTargets.All, Inherited = true)]
public class UnitySingletonAttribute : Attribute {
    public readonly bool mustBeOnlyComponentOnObject;
    public readonly bool mustBeWithinRootObject;
    public readonly bool mustBeOnlyInstanceInScene;

    public UnitySingletonAttribute(bool mustBeOnlyComponentOnObject = false, bool mustBeWithinRootObject = false, bool mustBeOnlyInstanceInScene = true) {
        this.mustBeOnlyComponentOnObject = mustBeOnlyComponentOnObject;
        this.mustBeWithinRootObject = mustBeWithinRootObject;
        this.mustBeOnlyInstanceInScene = mustBeOnlyInstanceInScene;
    }
}
