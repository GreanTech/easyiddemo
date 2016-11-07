# Welcome to the easyID-meets-ASP.NET-site demo repository

*If you need to Know Your Customer - then look no further! 
`easyID` will provide you with the information you need about your end users*

To make this happen on your own website(s), you'll need 2 things:
- An `easyID` account (you can sign up for a free test account on [grean.com](https://www.grean.com))
- A bit of legwork on your website

You can use the guide below, and the contents of this repository, as a step-by-step guide to providing your website with the real-life identity of your end users.

But before you dive in here (and if you haven't already, of course), consider taking a peek at a running version of 
this demo [here](https://www.prove.id). Some of the context will be easier to follow once you have seen it live.

## A bit of technical context
This demo contains the code and configuration needed to get the login process up and running.
You can find examples on how to create an embedded login experience, but also on how to go about 
doing a redirection-based variant. 

We have bootstrapped this demo from a plain-vanilla ASP.NET WebForms template. But if you have 
an ASP.NET MVC site - good news! Almost all of the configuration and runtime mechanics work the same for ASP.NET MVC.
Both stacks can be made to use exactly the same underlying .NET framework components for doing browser-based federated authentication. 
We chose WebForms as a starting point, because most MVC developers will have enough experience with WebForms to understand this demo.
We will make sure to point out any differences in the descriptions that follow.
Please send us a [pull request](https://github.com/GreanTech/easyiddemo/pulls) if you find that we've missed anything.

If you are already familiar with the inner workings of the Windows Identity Framework (aka WIF), 
feel free to skip the rest of this README, and dive straight in to the repo.
If not, a detailed guide follows below. 

## Getting started


