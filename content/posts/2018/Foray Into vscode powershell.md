---
Published: 2018-09-03
Title: A Foray into vscode-powershell
---

This is a foray into getting started with the vscode-powershell extension. In particular noting some of the perhaps less obvious things I needed to do in order to get it to build on Ubuntu 18.04.

<!-- more -->

First thing you'll need to do of course is to fork the repository. There are two repositories that we want to fork to our github account: [PowerShellEditorService](https://github.com/PowerShell/PowerShellEditorServices) and [vscode-powershell](https://github.com/PowerShell/vscode-powershell). Once you've forked them (and perhaps made hours of work that Tiler and Rob keep harrassing you as not being good enough...) you need to clone them to your local machine. I highly recommend keeping them side by side and so I clone them into a repos directory. On my Windows machine all of my git repos are stored at `C:\repos\` on my fresh install of Ubuntu Linux 18.04 they're stored at `~/git/`.

# System software to install

Now after freshly installing Ubuntu 18.04, I have installed the following software on top of the base:

1. PowerShell-Preview - PowerShell 6.1 is in Release Candidate, and is the only "Supported" version for Ubuntu 18.04
1. npm - Not entirely sure if I installed this one, or if it's just installed as part of Ubuntu... It's definitely installed though.
1. VS Code Insiders

Once this is installed, and the repos are cloned. I opened vscode-powershell in vscode-insiders. I then selected `Debug > Start Debugging` This greeted me with the message: "The preLaunchTask 'BuildAll' terminated with exit code 1." Clearly we're missing some pre-requisites... Looking in the terminal we see:

```
> Executing task: Invoke-Build BuildAll <

execvp(3) failed.: No such file or directory
The terminal process terminated with exit code: 1

Terminal will be reused by tasks, press any key to close it.
```

This initially felt like we were missing InvokeBuild from the PowerShell Gallery. Unfortunately, after installing that, the error is still present. The build appears to work if we do Invoke-Build from pwsh-preview.

It turns out that this is the trick here. We're using a preview build of pwsh on Ubuntu 18.04, because that is what's available. To resolve this, we can simply create a symlink from pwsh to pwsh-preview... This may need to be resolved once pwsh 6.1 leaves Release Candidate, or the installation of pwsh may resolve it for us. To create the symlink: `sudo ln -s /usr/bin/pwsh-preview /usr/bin/pwsh`. Once this symlink is created, debugging of both vscode-powershell and PowerShellEditorServices seems to go off without a hitch.

## Conclusion

Once getting over the hurdle of pwsh not being available outside of a preview on Ubuntu 18.04, getting vscode-powershell and PowerShellEditorServices development going in Ubuntu 18.04 is actually really easy, and dare I say may be even easier than doing the same thing on Windows 10...

I did note while investigating the pwsh-preview issue that there are already documents that may provide more information (and will at least be more up to date than this document) here: [vscode-powershell Development Document](https://github.com/PowerShell/vscode-powershell/blob/master/docs/development.md) and here: [PowerShellEditorServices ReadMe](https://github.com/PowerShell/PowerShellEditorServices#development).
