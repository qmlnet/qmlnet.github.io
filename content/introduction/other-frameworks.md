---
title: Other frameworks
order: 2
---

# Other frameworks

There are other frameworks that are available (or in beta) that provide cross-platform GUIs for .NET.

* [GTK#](https://www.mono-project.com/docs/gui/gtksharp/)
* [XWT](https://github.com/mono/xwt)
* [Avalonia](https://github.com/AvaloniaUI/Avalonia)
* [Eto.Forms](https://github.com/picoe/Eto)

## The problems

Each of these frameworks are great in their own right. I will leave it up to you to visit each project to discover the unique benefits of each. With that said, each of them have a blend of the following issues that was the motivation for creating Qml.Net.

### Too young

Some of the projects mentioned are still young. Because of this, they either have:
1. Not enough controls/features.
2. Bugs still to be found on each platform.

Since Qml.Net uses Qt/QtQuick/QML, it has access to the wide-range of controls and features it provides, benefiting from the years of bug finding/fixes on it's supported platforms.

You could argue that Qml.Net itself is also young, but I'd argue that it does't matter as much. Qml.Net is a thin layer between two well-established platforms. This glue layer may have bugs, but the surface area is a lot smaller than that of a complete GUI framework.

### PInvoke chatty

Some projects attempt to render their own controls in .NET which require many calls to correctly draw the 2D scenes. It is expensive to call into native libraries. It is because of this reason that Microsoft created ```PresentationFramework.dll``` for WPF which handles most of the DirectX calls while exposing the key pieces needed to render controls in .NET/WPF.

Qml.Net solves this by leaving the entire GUI (controls, styles, event handlers, etc) in Qt's world (C++). While there are some PInvoke calls in Qml.Net, it is limited to the loading of data into QML to be presented to the user. Once the data is loaded in QML, that data can be animated/styled/controlled completely in native code, removing the need to perform any PInvoke calls.