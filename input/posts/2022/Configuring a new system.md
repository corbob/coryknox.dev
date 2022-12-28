---
Published: 2022-05-01
Title: So you need to setup a new system for your development?
---

Recently I felt the need to reinstall Windows for my main development system.
This is always something that's fraught unforseen issues.
So, to help myself with this task the next time I do it, I figured I'd document the process for this.

## First things first

What better place to start than with the first things.
Obviously you install Windows, but then what's next?

Chocolatey is next of course! (Full disclosure: I am employed by Chocolatey, but that was after many years of being a home user of Chocolatey)

Technically, I set the execution policy to Bypass, then I install Chocolatey...
It's a pretty simple one-liner: `Set-ExecutionPolicy Bypass -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://community.chocolatey.org/install.ps1'))`

Once Chocolatey is installed, this time around I figured I'd give Boxstater a try. So I went to the [Chocolatey blog post](https://blog.chocolatey.org/2022/04/announcing-boxstarter-v3-beta) regarding the release of Boxstarter v3 beta and followed the directions to install the beta (`choco install boxstarter --version 3.0.0-beta-20220427-21 --prerelease`).
I then launched the Boxstarter shell and get started with my software installations by issuing the command `Install-BoxstarterPackage -PackageName https://gist.githubusercontent.com/corbob/2bafbed038c1fa21ddca952c36835044/raw/53c6d40d573da68e89d35a22448fef54fb383e1b/boxstarter.ps1`

## But now what?

And this is where we join our hero on this journey.
You see, this is the point where I realized I should be documenting this.
And so, what follows is the stream of consciousness of setting up a new system.
Many of these things may be specific only to me, but some might be more generally applicable.

Now that the softwares are installationed, we need to configurationing them.
First I configure `git` so that everything behaves as I expect it.

Fortunately this is relatively straightforward.
Start with a PowerShell window and set the editor environment variable: `$env:EDITOR = 'code'`.
Then launch the editor for the global git configuration: `git --global --edit`.

In the Visual Studio Code window that opens, we copy/paste the contents from our `gitConfigs` repository on GitHub (currently a private repository, sorry 🤷‍♂️ ).

Now that we have git configured, need to configure gpg so that git doesn't completely lose it at trying to sign with keys we don't yet have 🤔.

## Enter Keybase

I use Keybase to store my GPG keys and a few other things.
This means that I need to get my GPG keys from Keybase, and enter them in gpg4win.
Luckily, Boxstarter handled installing both of these programs for me.

After logging into Keybase, export the key: `keybase pgp export --secret --outfile keybase-private.key`.
Special thanks to Stephen Rees-Carter for the excellent <u>[post on the initial setup](https://stephenreescarter.net/signing-git-commits-with-a-keybase-gpg-key/)</u>
**Note: Keybase will ask you to enter passphrases multiple times. Make sure you click into the window EACH TIME. It may look like it's ready for your input, but it isn't!!!**
I promise I have not been bit by this more than 5 times (yet).

Now I've already added all the users I want to the key, so now I need to trust the key: `gpg --editkey me@coryknox.dev`.
gpg command to issue: `trust`.
Choose `5` (I trust ultimately).
`quit` the program.

The rest of Stephen's post I have already completed, and so I don't need to do most of it here.
One exception is telling `git` where to find `gpg`: `git config --global gpg.program (command gpg).source`

## Configuring SSH keys

Now that I've got git signing my commits, I need to make sure git can get to all of my repositories.
Follow the appropriate directions for using SSH keys with [GitLab](https://docs.gitlab.com/ee/user/ssh.html) and [GitHub](https://docs.gitlab.com/ee/user/ssh.html)

That's really all there is to the SSH keys, since each provider documents them in their docs, I won't duplicate their effort here.

## Configure gh and glab

`gh` and `glab` are great tools to make working with GitHub and GitLab from the CLI easier.
They're also pretty easy to get started using, just issue the command `gh auth login` or `glab auth login` and follow the on-screen prompts.

To confirm they're logged in and working, try cloning down a repository: `gh repo clone corbob/choco`.
