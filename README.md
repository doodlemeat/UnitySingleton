# UnitySingleton
Make sure there are only one single instance of a Singleton class in a Unity project and raises errors if something is wrong.

We use this in our project. We have a very large code base, and even if the scenes are structured well, this class have helped us through the development.

## Usage
The simplest example:
```C#
[UnitySingleton]
class MySingleton : UnitySingleton<MySingleton> {
    protected overide void OnAwake() {
        // Same as MonoBehaviour.Awake
    }
}
```

UnitySingleton uses the Awake function to do the neccessary checks so you have to move your Awake() code to OnAwake().


## Default attribute values
```C#
[UnitySingleton(mustBeOnlyComponentOnObject: false, mustBeWithinRootObject: false, mustBeOnlyInstanceInScene: true)]
```

### mustBeOnlyComponentOnObject
If true, an error will be raised if there are three or more component component on the same GameObject. A GameObject can then only have Transform and MySingleton.

### mustBeWithinRootObject
If true, an error will be raised if the GameObject your Singleton lies in has a parent. Which is equivalent to not be a direct child of the root object.

### mustBeOnlyInstanceInScene
If true, an error will be raised if at any time there is two of the same Singleton components in the scene.



## Why not use [DisallowMultipleComponent]?
It is up the the coder to decide if their SingletonClass should have this attribute. 
However, [DisallowMultipleComponent] only disallows adding multiple components of the same type in the editor. To make it work during runtime, mustBeOnlyInstanceInScene must be set to true.
And if the mustBeOnlyInstanceInScene is set to false, having [DisallowMultipleComponent] will prevent you from being able to do so.
