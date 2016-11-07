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
By far, the easiest way to go about this is to create a new, throw-away, web project, and let the `Change Authentication` wizard do the heavy lifting.
You can then simply copy the relevant parts of the generated `web.config` to your actual web site's config file (see checklist below for elements to copy). 
To complete the wizard, you need the DNS name that you chose for your `easyID` test tenant domain, and the `realm` value that you chose in `easyID` for your application.
You will also need to have a live Internet connection, and the ability to download an XML file from `easyID`. 
For comparison, this demo uses the following values:
- `YOUR-easyID-TEST-DNS-DOMAIN = https://easyid.www.prove.id`
- `YOUR-APP-REALM = urn:grn:app:easyid-demo`

These values are referred to in the list below as `YOUR-easyID-TEST-DNS-DOMAIN` and `YOUR-APP-REALM`:
- Go to `File -> New -> Project`. The `New Project` popup opens.
- Choose `Templates -> Visual C# -> Web` (YMMV if you don't have C# installed).
- Select `ASP.NET Web Application` and click the `OK` button
- Choose either of the `Web Forms`, `MVC` or `Web API` templates in the `ASP.NET 4.6.1 Templates` section 
(YMMV if you use another version of ASP.NET). The important thing is to choose one where the `Change Authentication` button is enabled.
- Click the `Change Authentication` button - the `Change Authentication` popup opens.
- Select the `Work And School Accounts` radio button option
- Select `On-Premises` in the dropdown menu on the right-hand side of the dialog
- Enter `https://YOUR-easyID-TEST-DNS-DOMAIN/metadata/wsfed` in the `On-Premises Authority:` field
- Enter `YOUR-APP-REALM` in the `App ID URI:` field
- Click the `OK` button. If the `Change Authentication` popup closes by itself, you should be good to proceed. 
Otherwise, check for type-o's in the `On-Premises Authority`  field. That is the most likely cause of problems.
If there is no type-o, try opening the same URL in a browser on the same machine that your VS instance runs on.
This should download/offer to download an XML file (YMMV depending on the browser you use).
If it does not, please check your network connectivity, or for signs of any corporate firewall and/or download restrictions getting in your way.
- Click the `OK` button in the `New ASP.NET Project` dialog, and let VS set up the scaffolding.

After VS finishes creating the project, navigate to the `web.config` file and follow the guide below to find the relevant elements to copy to your real `web.config` file.

## Config elements to copy
