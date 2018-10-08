---
title: Managed hosting
order: 0
---

# Managed hosting

This is by far the easiest approach when developing a Qml.Net application. It utilizes a traditional .csproj file, using NuGet packages for all the native components.

##  Benefits

* No knowledge of C++ needed.
* Traditional .csproj format, so all the ```dotnet``` commands work like any other console application.
* Works with any IDE that supports .NET development and debugging.
* Supports .NET Core, Full Framework and Mono.

## Drawbacks

* No embedded files. All .qml files/images must exist locally on disk. Normal Qt applications make use of [Qt's Resource System](http://doc.qt.io/qt-5/resources.html). This makes deployment a little bit more complicated. NOTE: This will eventually be solved via [qmlnet - issue 15](https://github.com/qmlnet/qmlnet/issues/15).
* Limited API. While Qml.Net wraps everything needed to host a QML application, it doesn't wrap *everything* that Qt provides. We will sometimes wrap additional features, but since our interop is hand-crafted/maintained, it will be limited.

## Setup

```bash
dotnet add package Qml.Net
```

**Windows**

```bash
dotnet add package Qml.Net.WindowsBinaries
```

**OSX**

```bash
dotnet add package Qml.Net.OSXBinaries
```

**Linux**

```bash
dotnet add package Qml.Net.LinuxBinaries
```

**```Program.cs```**

```
class Program
{
    static int Main(string[] args)
    {
        using (var app = new QGuiApplication(args))
        {
            using (var engine = new QQmlApplicationEngine())
            {
                // TODO: Register your .NET types.
                // Qml.RegisterType<NetObject>("test");

                engine.Load("Main.qml");
                
                return app.Exec();
            }
        }
    }
}
```
