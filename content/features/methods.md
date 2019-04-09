---
title: Methods
order: 1
---

# Invoking .NET methods from QML

```csharp
public class YourNetObject
{
    public int Add(int value1, int value2)
    {
        return value1 + value2;
    }
}
```
```qml
import YourApp 2.1

YourNetObject {
    id: o
    Component.onCompleted: {
        // Outputs "2"
        console.log(o.add(1, 1))
    }
}
```
