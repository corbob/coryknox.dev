---
title: Automatically setup remote tracking branch
date: 2024-10-23
categories: [learnings]
tags: [TIL]
---


I learned this on [CSharpFritz](https://jeffreyfritz.com/)'s [stream](https://twitch.tv/csharpfritz): `git config --global --add --bool push.autoSetupRemote true`. This allows you to be able to just do `git push` and it'll automatically setup a remote tracking branch on your default remote.
