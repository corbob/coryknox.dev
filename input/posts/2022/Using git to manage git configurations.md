Published: 2022-03-05
Title: Using git to manage git configurations
---

Git configuration is one of those things that you typically set it and forget it.
Unfortunately it's also something that you likely don't back up.
If you use multiple computers, this is compounded by having a different configuration for different systems.
Enter my new git configuration setup!

Picture this: you're working away, being super productive.
You change into your git repository and in a force of habit you type `git f`.
Cue git telling you: `'f' is not a git command` 🤦‍♂️.
And you know what?
Git's right!
It's not a git command.
It is however an alias that you've configured on your main computer, and now you need to remember the full command to use it here.

Enter the `gitConfig` repository!
This is a repository to house your `.gitconfig` settings in.
Now, this setup is a little bit unorthodox, but it does allow you to maintain your git configuration within a git repository with all of the benefits that gets you.
