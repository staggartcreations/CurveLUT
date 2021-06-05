# CurveLUT
ScriptableImporter to convert a set of curves into a Look up Texture, allowing curves to be used in shaders.

Sampling example: `float curve = tex2D(_LUT, float2(t, 0,));`. The X-component of the UV corresponds to the horizontal axis of the curve (t)

![alt text](https://i.imgur.com/h8Gw4l7.png "")

Usage (Editor)
------------
In the Project window, right click and choose Create->Curve LUT. Adjust the curves as needed and apply the changes. 

You can assign the texture asset where needed.

Usage (Runtime/Monobehaviour)
------------
Assign 4 curves to the CurveLUT.Create function (can be null) and it will return a Texture2D

```
AnimationCurve curveA = AnimationCurve.Linear(0f, 0f, 1f, 1f);
AnimationCurve curveB = AnimationCurve.Linear(0f, 0f, 1f, 1f);
AnimationCurve curveC = AnimationCurve.Linear(0f, 0f, 1f, 1f);
AnimationCurve curveD = AnimationCurve.Linear(0f, 0f, 1f, 1f);

Texture2D lut = CurveLUT.Create(curveA, curveB, curveC, curveD);
```

License
-------
MIT License (see [LICENSE](LICENSE))
