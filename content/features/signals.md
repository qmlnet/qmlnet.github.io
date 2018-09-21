---
title: Signals
order: 3
---

# Signals

Your .NET objects can define signals.

```csharp
[Signal("signalName")]
public class NetObject
{
}
```

Your signals can also declare parameters.

```csharp
[Signal("signalName", NetVariantType.String, NetVariantType.Int)]
public class NetObject
{
}
```

You can raise signals from .NET.

```csharp
public void YourMethod()
{
    netObject.ActivateSignal("signalName", "param", 3);
}
```

You can attach signal handlers from both .NET and QML.

```csharp
public void YourMethod()
{
    app.AttachToSignal("signalName", new Action<string, int>((param1, param2) =>
    {
        Console.WriteLine("Signal raised!");
    }));
}
```

```qml
import YourApp 2.1

YourNetObject {
    onSignalName: function(param1, param2) {
        console.log("Signal raised!")
    }
}
```

:::minfo
Signal handlers attached from .NET persist throughout the life-time of the .NET object. As the object is passed back and forth between QML and .NET, the handlers will remain.
:::

:::minfo
Signal handlers attached from QML persist throughout the life-time of the javascript object that *represents* the .NET object. Consider the following. When the javascript object that represents the .NET object is collected, the QML handlers are removed. The .NET object may be passed back to QML, but it will no longer have any QML handlers.
:::
