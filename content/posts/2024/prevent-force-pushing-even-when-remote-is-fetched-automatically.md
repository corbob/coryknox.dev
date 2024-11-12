---
title: Prevent force pushing even when remote is fetched automatically
date: 2024-10-23
categories: [learnings]
tags: [TIL]
---

Today I learned from Scott Chacon about the `--force-if-includes` on `git push`. When used alongside `--force-with-lease` will negate the troubles potentially caused by applications that periodically `git fetch` for you. Basically it will prevent you from pushing if you're pushing to a remote that has a commit that is not a common ancestor of what you're pushing I think... If my testing is correct, then it'll act like `--force-with-lease` when the remote has not been fetched down, but in the scenario where it has actually been fetched down. (Source: https://git-scm.com/docs/git-push#Documentation/git-push.txt---no-force-if-includes)
