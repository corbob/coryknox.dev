---
Published: 2018-8-29
RedirectFrom: /2018/08/Azure-Functions-FTW
Title: Azure Functions FTW - Using Azure Functions to schedule GitHub Pages posts
---

If you're reading this... And it's after August 29th 2018 at 9 AM PDT... Then that means it worked. I mean I know it works because this isn't the first post of it's kind, but it's the first I'm actually going to keep up.

<!--more-->

It all started when I was discussing having finally setup this blog on Twitter. Someone asked if I could schedule blog posts. At the time I didn't know if it was possible, but a simple web search found the answer over here: [Scheduling posts on Github pages with AWS lambda functions by: Evert Pot][1] Of course, this is fine and dandy if you already have AWS. But! What if you have Azure? Could you do something similar with it?

# Setup

From what I could make of AWS lambda functions they are somewhat analogous to Azure Functions. It appears that the billing is somewhat similar, and different as we'll see. First we need to setup an Azure Function App within our Azure account. I'll leave that as an exercise for the user, but hit me up in the comments or on Twitter if you need some pointers.

Once I had the Azure Function App setup, I attempted to make a javascript function following the code provided by Evert. Unfortunately it would seem some of the conventions used don't work with Azure Functions (although it seems perhaps they do, you just need to turn on a preview of v2 or something of the like). The main thing I was tripping on was that await and async didn't seem to be available. Further, no matter what I did, the query was always returning undefined. Instead of fight with a language I understand, but in a runspace I do not understand, I started investigating if it could be done with PowerShell. Looking at the github API documentation, it looks fairly straight forward. You send a request to the API, and it either returns success, or it returns 404.

After determining that we could do this with an Invoke-RestMethod in PowerShell, the next step was to setup the PowerShell function. Within the Function App you want to enable Experimental Language support and then add a new timer trigger. Like Evert, I have mine set to fire every hour on the hour. Once we've created the function, we need to populate it with the code.

# Code

The below is not fully tested in production. As with most things, I got the command working and have left it as is for my setup. However, for this blog post we need it to be a tad prettier... so I have moved the username and token into variables and expanded the command from irm. I'm open to Pull Requests should this not function properly.

```powershell
$Token = 'YourGitHubToken'
$Username = 'YourGitHubUsername'
Invoke-RestMethod "https://api.github.com/repos/$Username/$Username.github.io/pages/builds" -Method Post -Headers @{ 'Authorization'="token $Token"; 'Accept'= 'application/vnd.github.mister-fantastic-preview+json' }
```

# Break it on down

I'm trying not to reproduce everything that Evert, but the basic breakdown as listed on his article:

1. Create a [Personal Access Token][2] on GitHub.
2. Make sure you give it at least the `repo` and `user` privileges.
3. Make sure you add `future: false` to your `_config.yaml`.
4. Write a blog post, and set the `date` to some point in the future.

Once we've setup the Token, and set our `_config.yaml` properly, we can go ahead with the function. If we check out the GitHub [API documentation][3] we'll see that we can just make a simple web request to the API endpoint with our token and voila it initiates a build.

# Costs

I told you I'd get to costs... Unfortunately, it is looking like this isn't quite as cheap as AWS lambda functions. I haven't gone a full billing cycle, but at 9 days in, this is showing as costing $0.01 CAD so far. This isn't due to runtime of the function though. This is due entirely to the underlying storage costs. Apparently we need to pay for somewhere to store our function. And really, if it's going to cost us a few pennies a month, it's close enough to free.

# Caveats

One thing to be aware of, if you use the RSS feed. Every time this function fires, the date at the top of the RSS file updates (this also makes a convenient place to check if it's running). I don't believe this to be an issue for anything, but perhaps if you use a feed reader you could let me know if you see duplicate posts or anything strange.

# Next steps/Follow-up

I will try to remind myself to update this post when my Azure month rolls over in Mid September. I fully expect that costs shouldn't change much.

[1]: https://evertpot.com/scheduling-github-pages-lamdbas/
[2]: https://github.com/settings/tokens
[3]: https://developer.github.com/v3/repos/pages/
