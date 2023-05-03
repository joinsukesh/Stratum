# Stratum
A boilerplate Visual Studio solution for Sitecore projects.

The initial setting up of a Sitecore project can be a time-consuming task, especially when you have to create from scratch every time. But what if you could save yourself some valuable time and effort by using a module, that sets up the most typically used projects and code for you?

Introducing <b>Stratum</b>, a boilerplate Visual Studio solution for your Sitecore projects. In just a few steps, you can generate a basic but functional Sitecore website. The projects which are organized according to the Helix principles, have some commonly used functionality that you need to get started quickly and easily.

But that's not all. This module also includes serialized items for a demo page that showcases the code included in the solution. This could be a useful starting point for your own development, allowing you to focus on customizing and refining the code rather than building everything from scratch.

I have tested this module with <i>Sitecore 10.3</i>, but it might work with other versions too.

The following sections explain its key features, and could help you streamline your Sitecore development process. So, let's get started!

## Prerequisites
1. Visual Studio 2019 or later
2. A Sitecore instance (Optional if you don't want to view the demo page)


## Setup Visual Studio Solution

1. Download the latest version of <a target="_blank" href="https://github.com/sukesh-y/Downloads/tree/main/Stratum">GetStratum.zip</a> file, and extract its contents.
2. Open <i>Windows PowerShell</i> with administrator privileges, and change the directory to the extracted folder. Execute the <b>GetStratumCode.ps1</b> script.

![stratum_1](https://user-images.githubusercontent.com/24619393/235687985-2cb64452-60b5-4f9c-a560-9e312aa990e7.png)

3. Enter the inputs. I chose to name my solution <i>MyCompany</i>. I had an existing fresh Sitecore instance named <i>sample.local</i>. 

![stratum_2](https://user-images.githubusercontent.com/24619393/235832661-2fa106f4-e3e6-40f0-8cc9-48d689d1b2a5.png)

4. This will create the Visual Studio solution in the specified target directory. Once that is done, open the solution in Visual Studio and build it.


## Deploy Sitecore Items
1. For this, you need to have Sitecore CLI setup in your instance. If not yet done, refer this <a target="_blank" href="https://saltandsitecore.wordpress.com/2023/04/24/setup-sitecore-cli/">article</a> and follow <b>Steps 1 to 4</b>. 

If you have already installed <i>Sitecore Management Services</i> in your instance, then just refer <b>Steps 3 and 4</b> in that article. 
(Ignore the rest of the steps, as they are not needed for now).

2. Once that is done, open <i>Windows Command Prompt</i> with administrator privileges and change the directory to your VS solutions folder. In my case <i>D:\Projects\Internal\MyCompany</i>.
3. Execute this command to login to the CMS:

<code>dotnet sitecore login --authority https://sample.identityserver.local --cm https://sample.local --allow-write true</code>

![stratum_3](https://user-images.githubusercontent.com/24619393/235833861-319c9e5f-40ca-42bc-b650-61ab1bf8b640.png)

This will open the CMS in a browser. Select the checkboxes and click on <b>Yes, Allow</b>.

![stratum_4](https://user-images.githubusercontent.com/24619393/235833924-159d13b8-7ed2-4bb3-8d0a-6ef6d7cfbfd1.png)

![stratum_5](https://user-images.githubusercontent.com/24619393/235833834-eba08dd8-c00a-4c45-bbad-423861f9cbd6.png)

4. Now, execute the <b>Push</b> command to push the serialized items from disk to CMS

<code>dotnet sitecore ser push</code>

![stratum_6](https://user-images.githubusercontent.com/24619393/235855800-b63bad16-b10e-4f5b-abbc-e86b6dd14199.png)

![stratum_7](https://user-images.githubusercontent.com/24619393/235855821-9619db18-f161-4f29-866c-335cdd2a0cff.png)

![stratum_8](https://user-images.githubusercontent.com/24619393/235855845-e7282b3c-fb58-409b-9ad5-65b5d25cc204.png)







