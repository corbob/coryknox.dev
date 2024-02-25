---
title: Preventing Updates on Vagrant Boxes
date: 2023-11-27
Categories: [General Computering]
tags: [vagrant, Windows, PowerShell]
---

Picture the scenario: You have a Vagrant box that you've set up for testing purposes.
It's a Windows Server system, so you don't worry about disabling Windows Updates.
Then you reboot the VM to find that Windows has decided to install updates, and so you must wait for it to do it's thing.
But, what do you do?
How can you prevent this?
WSUS!
But alas, what if you need to install something that is a Windows Update?
You'll need to enable Windows Updates again.

So then, what is the solution to be able to disable Windows Updates when you want them disabled, and enable them otherwise?
Particularly, how do you do this in a way that you don't need to do it for every one of your Vagrant environments?

## Default Vagrantfile

This is the benefits of the default Vagrantfile.
Anything you add in this file will be applied to all of your Vagrant environments.
The default `Vagrantfile` is typically located in `~/.vagrant.d/Vagrantfile`, but you may need to look around for it depending on your configuration.

One drawback of the default `Vagrantfile` is that it is applied to all Vagrant configurations.
This of course would not be much of an issue, except for the fact that you might not want to always disable Windows Updates.
Or perhaps you have non-Windows boxes that you don't want to error during provisioning.

The great thing about this is that you can prevent the provisioning scripts from running by checking the `ARGV` variable to see if it contains the `--provision-with` flag.
This allows us to only have the provisioning scripts when a specific provisioning script is being called.

So, if we make our default `Vagrantfile` look like the below, we then have the ability to disable Windows Updates by running `vagrant provision --provision-with disable-updates` or `vagrant provision VmName --provision-with disable-updates`.

```ruby
Vagrant.configure("2") do |config|
	if ARGV.include? '--provision-with'
		config.vm.provision "shell", name: 'disable-updates', inline: <<-SHELL
			$ErrorActionPreference = "SilentlyContinue"
			Set-Location HKLM:/Software/Policies/Microsoft/Windows
			New-Item WindowsUpdate
			New-ItemProperty -Path WindowsUpdate -Name ElevateNonAdmins -Value 1 -Type DWORD
			New-ItemProperty -Path WindowsUpdate -Name WUServer -Value "https://10.100.100.100:8530" -Type String
			New-ItemProperty -Path WindowsUpdate -Name WUStatusServer -Value "https://10.100.100.100:8530" -Type String
			Set-Location WindowsUpdate
			New-Item AU
			New-ItemProperty -Path AU -Name NoAutoUpdate -Value 0 -Type DWORD
			New-ItemProperty -Path AU -Name AUOptions -Value 3 -Type DWORD
			New-ItemProperty -Path AU -Name ScheduledInstallDay -Value 0 -Type DWORD
			New-ItemProperty -Path AU -Name ScheduledInstallTime -Value 15 -Type DWORD
			New-ItemProperty -Path AU -Name AutoInstallMinorUpdates -Value 1 -Type DWORD
			New-ItemProperty -Path AU -Name UseWUServer -Value 1 -Type DWORD
		SHELL
		config.vm.provision "shell", name: 'enable-updates', inline: <<-SHELL
			Remove-Item HKLM:/Software/Policies/Microsoft/Windows/WindowsUpdate -Recurse -Force -ErrorAction SilentlyContinue
		SHELL
	end
end
```

And then, to enable the updates again, you merely run `vagrant provision --provision-with enable-updates`.
