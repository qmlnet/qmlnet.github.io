---
title: Registering .NET objects.
order: 0
---

# Registering .NET objects.

To instantiate a .NET type and to begin using it in QML, it must be registered.

```csharp
Qml.RegisterType<YourNetObject>("YourApp", 2, 1);
```

Then you can import and use your type in QML.

```qml
import YourApp 2.1

YourNetObject {
}
```