---
title: Introduction
order: 0
---
# Introduction

Qml.Net is a thin layer glueing together two well-established technologies, QtQuick/QML and .NET.

## Brief example

**```Program.cs```**
```csharp
using System;
using System.IO;
using Qml.Net;
using Qml.Net.Runtimes;

class Program
{
    public class NetObject
    {
        public void Method()
        {
            // Called from QML.
        }
    }

    static int Main(string[] args)
    {
        using (var app = new QGuiApplication(args))
        {
            using (var engine = new QQmlApplicationEngine())
            {
                Qml.Net.Qml.RegisterType<NetObject>("test", 1, 1);

                engine.Load("Main.qml");
                
                return app.Exec();
            }
        }
    }
}
```

**```Main.qml```**
```qml
import QtQuick 2.7
import QtQuick.Controls 2.0
import test 1.1

ApplicationWindow {
    visible: true
    width: 640
    height: 480
    title: qsTr("Hello World")

    NetObject {
      id: test
      Component.onCompleted: function() {
          test.method()
      }
    }
}
```

See [Features](/features) for a complete list of interactions that can be done between .NET and QML.
