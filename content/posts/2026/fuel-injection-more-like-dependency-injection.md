---
title: Fuel Injection? More Like Dependency Injection
date: 2026-09-15
categories: [learnings]
---

{{<include file="includes/dependency-injections-note.md">}}

I've been learning about dependency injection in .NET.
Or, more accurately: I've been realizing that I already knew about dependency injection in .NET.

Now, a common thing we do in software development is to use everyday objects as analogies to try to explain concepts.
This generally works well when trying to explain object-oriented programming and inheritance because we can easily visualize that an apple is a fruit, as is an orange; while an apple is not an orange.
For dependency injection, I've always heard it explained using cars.
And then, we often use diesel and gasoline engines as the example.
But, I find that model hard to grasp.
You see, I'm not a car guy; I suspect you aren't either.
It feels like the differences between a diesel and gasoline car extend well beyond just the engine into the rest of the car.
For instance, I suspect the fuel tank has some differences, even if it's only the size of the opening to accommodate the different nozzles.

However, I've been thinking more about it, and using cars actually makes perfect sense.
But, not at the engine level.
I mean, yes at the engine level, but using it as the differentiator is too low-level.
Instead, I think we should use it as the reference point; and talk about the engine and not the car as a whole.

So I'll say it again, I'm not a car guy; but I do know some things about cars.
I know for instance that most components come in multiple price points.
I assume those price points generally come down to various performance differences.
You have the low-end spark plugs, batteries, tires, oil, etc.
Then you have the more expensive versions of those same components.
This is dependency injection at its most basic level.

Dependency injection allows you to take a dependency (for instance spark plugs), and insert (aka: inject) it into your application at runtime.
Now that dependency must meet the baseline specifications set out for it, but it can also go above and beyond those specifications.
A spark plug that doesn't spark is likely to cause you to have a bad time.

In Chocolatey CLI we use this feature to provide the various commands available.
For example, in the open source version we inject the Install command.
This install command meets all of the requirements of a command, but it specifically installs packages.
We then offer our customers the Chocolatey Licensed Extension which provides its own install command.
This install command installs packages as well as the open source version does, but it also adds functionality into that install command.

But, why would you want this?
Why not just build these components directly into your application?
Well, just like with your car engine, this allows us to change and improve just parts of the overall whole without needing to completely replace the whole.
It also allows us to test specific parts without needing the whole.

In our example of spark plugs, if they were built directly into the engine, what would happen when they inevitably find a production defect?
Well, if it's built in the engine, you need to rip out the engine and replace it with a new one.
Even with injecting the dependency of spark plugs, there's still work you need to do to replace it with the new one.
But, the work is greatly reduced.
