# UnitySingleton
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
