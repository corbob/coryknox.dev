---
title: Run PowerShell no matter what
date: 2024-02-23
---

VS Code is pretty amazing.
Having the ability to edit virtually all of your files, and launch debugging sessions of them.
But what if you have a codebase that has a `.vscode/launch.json` file and want to debug a PowerShell script?

Unfortunately, by default, `F5` will only debug PowerShell if there are no debugger configurations.
This means that pressing `F5` will attempt to run the configured debugger for your workspace regardless of if you're in a PowerShell file.

Fear not!
Although by default you're not able to run PowerShell code with `F5` when a debugger configuration exists, you are able to tell VS Code you want to do this.
