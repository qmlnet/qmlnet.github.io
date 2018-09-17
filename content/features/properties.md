---
title: Properties
order: 2
---

# Invoking properties from QML.

```csharp
public class NetObject
{
    public NetObject()
    {
        Prop = "hello!";
    }

    public string Prop { get; set; }
}
```
```qml
import YourApp 2.1

YourNetObject {
    id: o
    Component.onCompleted: {
        // Outputs "hello!"
        console.log(o.Prop)
    }
}
```