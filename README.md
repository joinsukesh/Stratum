# Stratum
A boilerplate Visual Studio solution for Sitecore projects.

The initial setting up of a Sitecore project can be a time-consuming task, especially when you have to create from scratch every time. But what if you could save yourself some valuable time and effort by using a module, that sets up the most typically used projects and code for you?

Introducing <b>Stratum</b>, a boilerplate Visual Studio solution for your Sitecore projects. In just a few steps, you can generate a basic but functional Sitecore website. The projects which are organized according to the Helix principles, have some commonly used functionality that you need to get started quickly and easily.

But that's not all. This module also includes serialized items for a demo page that showcases the code included in the solution. This could be a useful starting point for your own development, allowing you to focus on customizing and refining the code rather than building everything from scratch.

The code is on .NET Framework 4.8. I have tested this module with <i>Sitecore 10.3</i>, but it might work with other versions too.

These are some of features included - Helix publishing pipeline, basic Solr search, User account functionalities like registration, login, forgot password & reset password, Admin pages to view & download Sitecore Form data, Encrypt & Decrypt strings, Site & Page Metadata feature, option to add assets at Site, Page & Rendering levels, common page components like Header, Footer, Banner, Accordion, Gallery etc.

This module could help you streamline your Sitecore development process. So, let's get started!

## Prerequisites
1. Visual Studio 2019 or later
2. A Sitecore instance (Optional if you don't want to view the demo page)

### NOTE: 
I have used <i>MyCompany</i> as the VS Solution name, my instance name is <i>sample.local</i> and its webroot folder is <i>D:\inetpub\wwwroot\sample.local</i>.
If you have chosen different settings, ensure to replace them while following the instructions.

## Step 1: Setup Visual Studio Solution

1. Download the latest version of <a target="_blank" href="https://github.com/sukesh-y/Downloads/tree/main/Stratum">GetStratum.zip</a> file, and extract its contents.
2. Open <i>Windows PowerShell</i> with administrator privileges, and change the directory to the extracted folder. Execute the <b>GetStratumCode.ps1</b> script.

![stratum_1](https://user-images.githubusercontent.com/24619393/235687985-2cb64452-60b5-4f9c-a560-9e312aa990e7.png)

3. Enter the inputs.

![stratum_2](https://user-images.githubusercontent.com/24619393/235832661-2fa106f4-e3e6-40f0-8cc9-48d689d1b2a5.png)

4. This will create the Visual Studio solution in the specified target directory. Once that is done, open the solution file in Visual Studio and build it.


## Step 2: Deploy Sitecore Items
1. For this, you need to have Sitecore CLI setup in your instance. If not yet done, refer this <a target="_blank" href="https://saltandsitecore.wordpress.com/2023/04/24/setup-sitecore-cli/">article</a> and follow <b>Steps 1 to 4</b>. 

If you have already installed <i>Sitecore Management Services</i> in your instance, then just refer <b>Steps 3 and 4</b> in that article. 
(Ignore the rest of the steps, as they are not needed for now).

2. Once that is done, open <i>Windows Command Prompt</i> with administrator privileges and change the directory to your VS solutions folder. 
3. Execute this command to login to the CMS:

<code>dotnet sitecore login --authority https://sample.identityserver.local --cm https://sample.local --allow-write true</code>

![stratum_3](https://user-images.githubusercontent.com/24619393/235833861-319c9e5f-40ca-42bc-b650-61ab1bf8b640.png)

This will open the CMS in a browser. Select the checkboxes and click on <b>Yes, Allow</b>.

![stratum_4](https://user-images.githubusercontent.com/24619393/235833924-159d13b8-7ed2-4bb3-8d0a-6ef6d7cfbfd1.png)

![stratum_5](https://user-images.githubusercontent.com/24619393/235833834-eba08dd8-c00a-4c45-bbad-423861f9cbd6.png)

4. Now, execute the <b>Push</b> command to push the serialized items from disk to CMS.

<code>dotnet sitecore ser push</code>

![stratum_6](https://user-images.githubusercontent.com/24619393/235855800-b63bad16-b10e-4f5b-abbc-e86b6dd14199.png)

![stratum_7](https://user-images.githubusercontent.com/24619393/235855821-9619db18-f161-4f29-866c-335cdd2a0cff.png)

![stratum_8](https://user-images.githubusercontent.com/24619393/235855845-e7282b3c-fb58-409b-9ad5-65b5d25cc204.png)

5. That would create all the Sitecore items in the CMS. There is no need to publish as the demo site uses the <i>master</i> database.


## Step 3: Configurations to View Demo Page
<b>1. Configure domain URL</b> 

Navigate to this item - <code>/sitecore/system/Settings/MyCompany/Project/Site URL</code>, and update the <i>Phrase</i> field value to your instance URL. In this example, it is <i>https://sample.local</i>

This value will be used as link prefixes for email content.

<b>2. Execute Form Report Scripts</b>

In your VS solution, copy the SQL scripts from here - <code>MyCompany.Feature.AdminPages > SQLScripts > ScFormReport</code>. Execute them in your instance's <code>ExperienceForms</code> database, in the following order.

<i>uv_ScFormData.sql</i> - This is a view for the Sitecore Form's data.

<i>usp_GetSitecoreFormData.sql</i> - This stored procedure is to fetch data of a Sitecore Form submitted by FormId and date range, from the view.

These will be used in an admin page to view Form data, which is explained later in this document.

<b>3. Create Custom Solr Indexes</b>

The search functionality in the demo site uses custom Solr indexes. Here is how to create them:

- Navigate to the Solr sub folder where your default Sitecore indexes exist. In my case it was - <i>D:\Solr\Solr-8.11.2\server\solr</i>.
- Make two copies of <i>sample_master_index</i> folder and rename them as <i>MyCompany_products_master_index</i> & <i>MyCompany_products_web_index</i>. Because, these will be the names that are specified in the Solr configs here - <code>MyCompany.Feature.PageContent > App_Config > Include > zzz.MyCompany > Feature</code>

![image](https://user-images.githubusercontent.com/24619393/235883375-504abe36-c9f0-400e-be62-6d55793e372e.png)

- In the two custom index folders that you have just created, Keep just the <code>conf</code> folder and its contents. Delete everything else including the <i>data</i> folder and the <i>core.properties<i> file.

<b>4. Create Solr Cores</b>
  - Open the Solr portal, select <i>Core Admin</i>, click on <i>Add Core</i>, and create the <i>master</i> core like this, with the same names you have used in the previous step:
  
  ![stratum_9](https://user-images.githubusercontent.com/24619393/235885512-7f45c266-984c-420e-9e0a-ed36350a0f63.png)

  - Similarly create the core for <i>web</i>.
  - Restart Solr Windows service.
  
<b>5. Publish Code Files</b>
- Take a backup of your instance's webroot folder. 
- Open VS Solution and navaigate to <code>Publish > Website > MyCompany.Publish.Website</code>. Publish this project.
- This will publish files from all the projects to <i>C:\out\MyCompany</i>. If the publish doesn't complete for more than a minute, cancel it publish again.
- Copy all the files in <i>C:\out\MyCompany</i> and paste them in your instance webroot folder.   
  
<b>6. Rebuild Indexes</b>
- Restart IIS and open CMS.
- Navigate to <code>Launch Pad > Control Panel > Indexing > Populate Solr Managed Schema</code>.
- Populate the schema for <i>sitecore_master_index</i> & <i>MyCompany_products_master_index</i>. 
- Open <code>Indexing Manager</code>.
- Rebuild indexes for <i>sitecore_master_index</i> & <i>MyCompany_products_master_index</i>.
  
Thats it!!. Now browse your instance URL to view the demo page.
  
  ![image](https://user-images.githubusercontent.com/24619393/235892899-c8a4c07f-4f9d-401e-845a-a8a8dafe30cf.png)

  

  








