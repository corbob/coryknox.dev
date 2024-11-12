---
title: Stash untracked changes in a way that git stash reports them
date: 2024-10-23
categories: [learnings]
tags: [TIL]
---

I've learned that you can do `git stash -u` to stash untracked files as well as tracked files... But nothing will show you those files in the stash by default... Instead, if you do `git add <untracked files>` and then `git stash`, they are magically tracked files even if they're not actually committed to the repository.
