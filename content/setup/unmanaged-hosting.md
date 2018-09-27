---
title: Unmanaged hosting
order: 1
---

# Unmanaged hosting

This approach is a little more involved to setup, but it gives you the maximum amount of control over your application.

##  Benefits

* Support for embedding all resources (.qml files, images, etc) into the final executable.
* Access to the entire Qt/QtQuick/QML api, via C++. This includes custom QML controls, registering ```Q_OBJECT``` types with the QML engine, etc.
* QML/JavaScript debugging/intellisense (via QtCreator).
* The native component to Qml.Net is embedded inside of the final executable.

## Drawbacks

* Lack of support for commands like ```dotnet run``` and ```dotnet publish```, since the executable is managed via QtCreator and it's run/deploy model.
* Limited IDE support for .NET debugging. Since the executable is managed by QtCreator, you're IDE must support starting arbitrary native executables as .NET applications. Visual Studio and Visual Studio for Mac work fine. Rider doesn't support attaching debuggers to native executables (yet, see [here](https://youtrack.jetbrains.com/issue/RIDER-5378)).
* Requires some knowledge of C++.
* Only .NET Core supported.

## Setup

1. Create a .NET console application.
```csharp
class Program
{
    public class TestObject
    {
        public void TestMethod()
        {
            Console.WriteLine("test method");
        }
    }

    static int Main(string[] _)
    {
        // The "_" contains some private arguements to help
        // bootstrap things. It is intended to be passed
        // immediately into Host.Run(...).
        
        // Phase 5
        // Unwrap the magic.
        return Host.Run(_, (args, app, engine, runCallback) =>
        {
            // "args" contains the any user defined arguements passed from
            // CoreHost::run(..) in C++.
            
            // Phase 6
            // Register any .NET types that will be used.
            QQmlApplicationEngine.RegisterType<TestObject>("test");

            // Phase 7
            // This callback passes control back to C++ to perform
            // any file registrations and run the event loop.
            return runCallback();
        });
    }
}
```
Add a reference to Qml.Net.
```bash
dotnet add package Qml.Net
```
You don't need to add a reference to any of the ```Qml.Net.*Binaries``` packages.
2. Create a new Qt project via Qt Creator.
3. Add Qml.Net as a submodule to your project.
```
git submodule add https://github.com/pauldotknopf/Qml.Net
```
3. Include the native components of Qml.Net into your project, as well as the bits needed to host the .NET runtime.
```
include (Qml.Net/src/native/QmlNet/Hosting.pri)
include (Qml.Net/src/native/QmlNet/QmlNet.pri)
```
5. Start the .NET runtime in your ```main.cpp```.
```cpp
#include <QGuiApplication>
#include <QQmlApplicationEngine>
#include <Hosting/CoreHost.h>

static int runCallback(QGuiApplication* app, QQmlApplicationEngine* engine)
{
    // Phase 8
    // At this point, we are in Program.Main of the .NET program.
    // .NET should have registered all of it's types by now.

    // Load some QML files.
    // Maybe these QML files reference types registered in .NET.
    engine->load(QUrl(QStringLiteral("qrc:/main.qml")));
    if (engine->rootObjects().isEmpty())
        return -1;

    // Phase 9
    // Run the event loop.
    return app->exec();
}

int main(int argc, char *argv[])
{
    // Phase 1
    // Initialize Qt/QML like you would normally.
    QCoreApplication::setAttribute(Qt::AA_EnableHighDpiScaling);
    QGuiApplication app(argc, argv);
    QQmlApplicationEngine engine;

    // Phase 2
    // Get the location to the managed exec
    QString netDll = "/path/to/your/net/exec/Lib.dll";

    // Phase 3
    // Find .NET Core and it's libs/paths.
    CoreHost::RunContext runContext;
    runContext.hostFxrContext = CoreHost::findHostFxr();
    runContext.managedExe = netDll;
    // NOTE: You may set entry point to the current executable if
    // the .NET runtime is deployed side-by-side.
    runContext.entryPoint = runContext.hostFxrContext.dotnetRoot;
    runContext.entryPoint.append(CORECLR_DOTNET_EXE_NAME);

    // Phase 4
    // Run the .NET applciation.
    return CoreHost::run(app,
        engine,
        runCallback,
        runContext);
}
```
