---
#Published: 2022-03-28
Title: Using free tools to enhance virtual meetings
---

## The why

Like everyone else over the last two years, you've likely ended up with most of your meetings being online.
I've recently started attending my local [Toastmasters][toastmasters] meetings.
They too have been online.
A staple of Toastmasters meetings is various roles that are filled by the attendies.
These roles allow attendies to be involved with every meeting.
Recently taking up some of the more involved roles like [Timer][timer], [Grammarian][grammarian], and [Ah-Counter][ah-counter].

Because I am more technical in nature, I have configured some things to make it easier to perform these roles.
Today I'd like to take some time to go over my configuration, and how you could do something similar.
The steps below will give broad overview of using some of these tools.

## The tools

First up, the tools that you'll need.
I use these on my MacBook, but they are also available for Windows.

* [OBS][obs] - Used to create a "virtual" camera. This allows combining your regular camera with some images.
* [XSplit VCam][vcam] - Used to remove your background. This is akin to a virtual background in zoom.
* [PowerShell][powershell] - Used to create and track the timer.

## The setup

### XSplit VCam

Before we can use XSplit VCam with OBS, we need to make sure it can see our camera and remove the background.
The last time I used it on Windows, it was able to remove the background and provide a transparent background.
When I use it on Mac now, it does not provide a transparent background.

This is fixed by creating or downloading a background image that is solid green.
Once you have this, you can configure VCam to replace your background with this green image.

### OBS

Once you have the tools installed, you'll need to configure them.
The bulk of the configuration is done in OBS, with some minor things in PowerShell.

First things first is setting up some scenes in OBS.
I use the following scenes:

* \_Grammarian
* \_Timer
* Camera
* Red
* Yellow
* Green

![OBS Screenshot showing scenes listed above](/assets/img/posts/OBSToastmastersScenes.png)

Now that the base scenes are setup, let's setup the camera scene first.
On the `Camera` scene you'll want to add a Video Capture Device to capture from VCam, and expand it to fill the entire scene.

Right click on this source and select Filters.
Now add an Effect Filter of type Chroma Key, the defaults of which should already be green and likely need no adjustments.

![OBS Filters screenshot showing XSplit VCam in front with a greenscreen background](/assets/img/posts/OBSChromaKey.png)

Now that we have a scene for our camera, we should setup our background scenes.
The `Red`, `Yellow`, and `Green` scenes will be effectively the same.


[toastmasters]:https://www.toastmasters.com
[timer]:https://www.toastmasters.org/membership/club-meeting-roles/timer
[grammarian]:https://www.toastmasters.org/membership/club-meeting-roles/grammarian
[ah-counter]:https://www.toastmasters.org/membership/club-meeting-roles/ah-counter
[obs]: https://obsproject.org
[vcam]: https://www.xsplit.com/vcam
[powershell]: https://aka.ms/powershell
