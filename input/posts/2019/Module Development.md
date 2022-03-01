---
Published: 2019-01-14
RedirectFrom: /2019/01/Module Development
Title: Everything you didn't know you didn't know about PSModuleDevelopment
---
![wordcloud]({{ site.baseurl }}/images/ModuleDevelopment.png)

Late in 2018, I had the distinct pleasure of spending a few hours with [@FredWeinmann](https://twitter.com/FredWeinmann). We spent the time fixing up my PSCUCM Module (Available from the [PowerShell Gallery](https://www.powershellgallery.com/packages/PSCUCM) and [GitHub](https://github.com/corbob/PSCUCM)). During this session we went through a number of commands and setups utilizing his PSModuleDevelopment module and PSFramework. What will ensue is a multi-part series on Developing PowerShell Modules using PSFramework and PSModuleDevelopment.

<!--more-->

And so, without further ado, I present part 1:

# Setting up pre-requisites

We're going to be using modules from the [Powershell Framework Collective](https://github.com/PowershellFrameworkCollective). Specifically: `PSFramework` and `PSModuleDevelopment`. However, I recommend looking at `PSUtil` as it contains some great functions that make working in PowerShell even more delightful.

Due to the way the PowerShell Gallery works you can install both modules with a single command: `Install-Module PSModuleDevelopment -Scope CurrentUser`

# Setting up directory structures

With our modules installed, the first thing we need to do is setup our directory structures. To build a module prepared for Azure DevOps (formerly <abbr title="Visual Studio Team Services">VSTS</abbr>), we will use the `PSFProject` template that comes with `PSModuleDevelopment`. I encourage you to explore the various templates to see what's available.

To get started we run the following code: `Invoke-PSMDTemplate PSFProject` This will prompt for a `name` and `description`, provide these and the function will establish the scaffolding for our project. The directory layout for a module called DemoModule will be something like the below:

```text
DemoModule
├── build
│   ├── filesAfter.txt
│   ├── filesBefore.txt
│   ├── vsts-build.ps1
│   ├── vsts-prerequisites.ps1
│   └── vsts-validate.ps1
├── install.ps1
├── library
│   └── DemoModule
│       ├── DemoModule
│       │   ├── Class1.cs
│       │   └── DemoModule.csproj
│       └── DemoModule.sln
├── LICENSE
├── DemoModule
│   ├── bin
│   │   └── readme.md
│   ├── en-us
│   │   └── about_DemoModule.help.txt
│   ├── functions
│   │   └── readme.md
│   ├── internal
│   │   ├── configurations
│   │   │   ├── configuration.ps1
│   │   │   └── readme.md
│   │   ├── functions
│   │   │   └── readme.md
│   │   ├── scripts
│   │   │   ├── license.ps1
│   │   │   ├── postimport.ps1
│   │   │   └── preimport.ps1
│   │   └── tepp
│   │       ├── assignment.ps1
│   │       ├── example.tepp.ps1
│   │       └── readme.md
│   ├── DemoModule.psd1
│   ├── DemoModule.psm1
│   ├── readme.md
│   ├── tests
│   │   ├── functions
│   │   │   └── readme.md
│   │   ├── general
│   │   │   ├── FileIntegrity.Exceptions.ps1
│   │   │   ├── FileIntegrity.Tests.ps1
│   │   │   ├── Help.Exceptions.ps1
│   │   │   ├── Help.Tests.ps1
│   │   │   ├── Manifest.Tests.ps1
│   │   │   └── PSScriptAnalyzer.Tests.ps1
│   │   ├── pester.ps1
│   │   └── readme.md
│   └── xml
│       ├── DemoModule.Format.ps1xml
│       ├── DemoModule.Types.ps1xml
│       └── readme.md
└── README.md
```

# Directory structure overview

First thing we will notice is that it created a directory titled DemoModule. This is clearly where we store our module and the related item. Of note is the numerous readme files present in the template. These will give you an overview of the areas that they are found so you can take further advantage of them. What follows is a sumarization in my own words what these directories are for, or at least what I use them for.

## build

Within the DemoModule directory we have the build directory. This contains the scripts that are all preconfigured for use within an Azure DevOps pipeline. These will be covered in more depth in a coming installment of this series.

## library

Next is the library. This contains the files and directories needed for a C# project that will result in a DLL. If we use this, it will automatically put he DLL into the appropriate places for the rest of our template to take advantage of. This is an area of the template I have not looked at, nor have I taken advantage of *yet*.

## DemoModule

Up next is the DemoModule directory. This directory contains all of the files to actually make our module a module. This includes the manifest file, and the module file itself.

### DemoModule/bin

The bin directory contains any binary data that is part of your module.

### DemoModule/en-us

This is your Engligh US based help files. I haven't made use of this yet, I've been using Comment Based Help... So you're on your own for this one ¯\_(ツ)_/¯

### DemoModule/functions

Your public facing functions should be placed in the functions directory. The build scripts will expect this, and will check that you're exporting every function that's in here.

### DemoModule/internal

Internal will contain the... Wait for it... internal components of our module. This includes configuration, functions, internal scripts, and tab expansion settings (tepp).

#### DemoModule/internal/configurations

Configurations will house the configurations for your module. In a PSFramework module you will likely put your PSFConfig initializations in here.

#### DemoModule/internal/functions

Functions will house the internal functions. These are typically functions that do make things easier for your when developing the module, but don't need to extend beyond the module. Examples may be consistently fetching certain data, or creating objects consistently. Basically anything that you will need to do multiple times but the end user won't need to do should go in this directory. These functions should also follow best practices and follow the Verb-Noun principal with approved verbs.

#### DemoModule/internal/scripts

The default scripts are: `license.ps1`, `preimport.ps1`, and `postimport.ps1`. These files contain your license (default is MIT license), any commands that should be processed before your module is imported (perhaps some verification of module or application installations?), and the commands to be processed after your module is imported. By default the preimport.ps1 is effectively empty. The important file in the default scaffolding is the `postimport.ps1` file. This file will import all of your configurations, your tab expansion, and finally your license.

#### DemoModule/internal/tepp

At last we reach the tepp directory... tepp stands for Tab Expansion Plus Plus. This is the module that first brough tab expansion to PowerShell. PSFramework includes functions for registering these tab expansions that work by default in Windows 10 running PowerShell 5, as well as PowerShell 6 on all Operating Systems that I've tested it on. The benefit of registering tab expansion over using a ValidateSet is that a ValidateSet requires that the parameter match the set, while a tepp registered set is only some of the possible entries. For instance, in creating a custom wrapper for PSCUCM's `New-Phone` function, I could register tab expansions for the phones that we have. This allows new phones to be added without the need to immediately update the tepp as you could just specify the new phone. A ValidateSet would require that we update the set prior to trying to use the new phone with the function.

### DemoModule/tests

The tests directory. This contains all the tests that you're obviously going to include with your module...

#### DemoModule/tests/functions

The functions directory within tests is for your modules functions. You can divide this directory up as you see fit. If you're going to test your internal functions (and why wouldn't you?), then you might want a `internal` directory. My recommendation would be to follow the structure that you use for your public functions, and to have a test file for each function file.

#### DemoModule/tests/general

The general directory contains the tests included with the scaffolding. These tests will check *all* of your functions for some form of help for *all* parameters. It will check the Examples for a description (if using comment based help, the description comes after a blank line following the example).

### DemoModule/xml

The xml directory contains the Type and Format Definitions for your module. The PSModuleDevelopment includes some helpful functions for creating these xml files so you don't need to work with them by hand. If you have need for some of these files, I highly recommend investigating the readme file within as it does an excellent job of describing the process.

# Conclusion

Now that we've gone over the basics of getting started, you can go out and get your own modules started. Stay tuned for part 2 where we'll take our next step in the journey.

