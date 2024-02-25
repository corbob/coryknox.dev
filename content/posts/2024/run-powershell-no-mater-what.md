---
title: Run PowerShell no matter what
date: 2024-02-23
categories: [learnings]
tags: [PowerShell, VS Code]
---

VS Code is pretty amazing.
Having the ability to edit virtually all of your files, and launch debugging sessions of them.
But what if you have a codebase that has a `.vscode/launch.json` file and want to debug a PowerShell script?

Unfortunately, by default, `F5` will only debug PowerShell if there are no debugger configurations.
This means that pressing `F5` will attempt to run the configured debugger for your workspace regardless of if you're in a PowerShell file.

Fear not!
Although by default you're not able to run PowerShell code with `F5` when a debugger configuration exists, you are able to tell VS Code you want to do this!

## Doing it through the GUI

If you'd like to add this through the VS Code GUI, you can follow these steps:

1. Open the Keyboard Shortcuts UI (`Ctrl + K, Ctrl + S` by default, or search for `Preferences: Keyboard Shortcuts` with the Command Palette), then search for `PowerShell: Run`.
1. Locate and double click the entry for `PowerShell: Run`
1. Press `F5` followed by `Enter`
1. Right click the entry and choose `Change When Expression`
1. Enter the following for the expression: `editorTextFocus && debugState == 'inactive' && editorLangId == 'powershell'`
1. Bask in the glory that is pressing `F5` in a PowerShell script and always having it do what you expect it to do.


## Doing it through the `keybindings.json` file

If you'd prefer to just copy/paste something into the settings files, you can add the below to your `keybindings.json` file:

```json
[
    {
        "key": "f5",
        "command": "PowerShell.Debug.Start",
        "when": "editorTextFocus && debugState == 'inactive' && editorLangId == 'powershell'"
    }
]
```

You can locate your `keybindings.json` file through the Command Palette `Preferences: Open Keyboard Shortcuts (JSON)`.
