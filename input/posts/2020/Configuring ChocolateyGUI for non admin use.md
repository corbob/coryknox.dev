---
Published: 2020-04-17
RedirectFrom:
  2020/04/Configuring-ChocolateyGUI-for-non-admin-use/index.html
Title: "Configuring ChocolateyGUI for non admin use"
---

The other day I needed to fix something on my children's laptops.
This brought to my attention that when I set them up I had given them admin.
Naturally this is not an ideal setup.
But the issue becomes: How do I grant them the ability to install software without needing to reach out to me too often.
Quite simply, they need the ability to run installations as an administrator, but not the ability to just become administrator.

<!--more-->

Naturally this is something that has been solved at my day job.
To solve this type of problem there, we have Microsoft Endpoint Manager Configuration Manager.
This allows us to build silent installs of software; then publish those installs to a central location.
Configuration Manager handles the issues of users not being an admin.

For a long time I've been a huge fan of [Chocolatey](https://www.chocolatey.org).
Chocolatey comes in [multiple flavours](https://www.chocolatey.org/compare).
One of these flavours is Business which includes [Background Mode / Self-Service Installer](https://www.chocolatey.org/docs/features-agent-service).
This looks promising for my use case.

Of course before committing to this as a solution, I need to sell it to the wife.
So let's go see what the cost would be.
Heading over to the purchase page, I see that it's ~$25 CAD per unit per year.
Well, that seems almost reasonable for my household of 6 computers.
Changing the quantity to 6, I'm greeted with the message: "The minimum order quantity is 40."
Well that's unfortunate :(.
Fortunately, there is a Starter Pack :D.
Unfortunately, it's ~$885 CAD per year, not something I can sell the wife on for solving this particular problem.

However, we shall persevere!

So with Chocolatey for Business not being an option, what can we do?
Well, we could create a scheduled task that runs as admin or system and installs Chocolatey packages for us.
Or we could have a scheduled task that launches ChocolateyGUI as admin/system.
Both of these leave somewhat to be desired.
In that, you either have to somehow keep a list of choco packages to install, or you have ChocolateyGUI launching at random times instead of on demand.
I mean, we technically *could* configure the scheduled task to allow adhoc execution from normal users...

One thing's for sure, no matter our solution it is going to be chocolatey based.
So let's install Chocolatey following the standard [installation instructions](https://chocolatey.org/install).
Once it's installed, let's install ChocolateyGUI so that we don't have to explain how to run CLI tools to children.

Now if we happened to use Linux, we would have access to the `sudo` command.
This got me looking for a similar solution but for Windows.
It turns out there are a number of solutions that purport to be sudo for Windows.
To find them, one could simply run `choco list sudo`.
Fortunately for you, I have taken the time to look at all of them.
None of them actually act like sudo.
In fact, the only thing they do is try to elevate you to run a command, not try to run a command elevated.
Thus they're effectively running the PowerShell code: `Start-Process -Verb RunAs`.
Years ago, when we were just starting out with Adobe Creative Cloud, we discovered that it needed nearly weekly updates.
Maintaining an installation that requires that frequent of updates is just untenable.
We experimented with trying to find something like sudo, but for Windows.
Of course most "sudo" solutions for Windows don't actually understand what sudo does.
Most of the actively maintained ones seem to think that you simply want a command to elevate.
This does **not** solve the problem we had, nor does it solve my problem at hand.

Enter [sudowin](https://sourceforge.net/projects/sudowin/), which appears to be the only product that takes the (non-admin) users password and allows them to elevate.
Naturally, it hasn't been updated in quite some time and the documents supporting it are no longer accessible as it would seem SourceForge no longer hosts project websites like they used to.
To top it off, the Internet Archive's Wayback Machine doesn't have an archive of the documentation.
Given that the documentation is somewhat lacking, we need to make guesses based on the default configuration and see what we can sort out.
Fortunate for you dear reader, I have done some of the trial and error myself.

Looking in the install directory (`C:\Program Files (x86)\sudowin\server`) you will find a `sudoers.xml` file.
This file appears fairly straightforward.
The configuration that has worked for my use case is to change the `userGroup` element to set `allowAllCommands` to `false`.
In the `users` element, we change the `name` to match the non-admin user, and set `AllowAllCommands` to `false`.
For the `command` in the `commands` element, we change the `path` to be the path to ChocolateyGUI (`C:\Program Files (x86)\Chocolatey Gui\ChocolateyGUI.exe`).
The final change to the file that I made was to remove all of the `command` elements under the last `commandGroup` immediately following the comment regarding Windows XP SP2 files.

Once the sudoers file was in place, we restart the computer and test the configuration.
Once logged in as the non-admin user, we're able to right-click the ChocolateyGUI icon and select `Sudo...` from the list of options.
This presents us with an option to enter a password.
We enter the password for the non-admin user.
When prompted next to elevate, we can simply click yes.
And voila, we are running ChocolateyGUI as an administrator, without granting our users (children) the ability to run everything as administrator.

Some drawbacks to this solution: the non-admin user must have a password on it, the need to right click and select `Sudo...` could be cumbersome.
To work around these drawbacks, you could have the non-admin user configured to automatically log in.
You could also attempt to use a user with no password (this might be a solution, I have not actually tested this scenario).
If you edit the properties of your ChocolateyGUI shortcut, you could prepend `sudo` to the target which will launch the Console client of sudowin and allow you to enter the password in a terminal window.

This is the configuration I have settled upon for now.
Is it ideal?
No.
Does it work?
Yes.
Have you encountered a similar issue?
What steps did you take to resolve it?
