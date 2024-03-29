# Stratum :building_construction:
A boilerplate Visual Studio solution for Sitecore projects.

The initial setting up of a Sitecore project can be a time-consuming task, especially when you have to create from scratch every time. But what if you could save yourself some valuable time and effort by using a module, that sets up the most typically used projects and code for you:interrobang:

Introducing <b>Stratum</b>, a boilerplate Visual Studio solution for your Sitecore projects. In just a few steps, you can generate a basic but functional Sitecore website. The projects which are organized according to the Helix principles, have some commonly used functionality that you need to get started quickly and easily :sparkles:.

But that's not all. This module also includes serialized items for a demo page :page_with_curl: that showcases the code included in the solution. This could be a useful starting point for your own development, allowing you to focus on customizing and refining the code rather than building everything from scratch.

The code is on <b>.NET Framework 4.8</b>. I have tested this module with <i>Sitecore 10.3</i>, but it might work with other versions too.

These are some of the features included :gift::
- Helix publishing pipeline.
- Basic Solr search.
- User account functionalities like registration, login, forgot password & reset password.
- Admin pages to view & download Sitecore Form data, Encrypt & Decrypt strings.
- Site & Page level Metadata feature.
- Option to add assets at Site, Page & Rendering levels.
- Common page components like Header, Footer, Banner, Accordion, Gallery.
- Contains serialized items for <i>core</i> database to disable the "Publish Site" option, and also custom User & User Profile items, with which users can be created. 
- Frequently used helper methods & Item extensions in code.

The document appears lengthy, but it's only a few steps I promise 😄. The rest of it expains the projects and their purpose.  
This module could help you streamline your Sitecore development process. So, let's get started :construction_worker:!


## Prerequisites
1. Visual Studio 2019 or later
2. A Sitecore instance (Optional if you don't want to view the demo page)

### NOTE: 
I have used <i>MyCompany</i> as the VS Solution name, my instance name is <i>sample.local</i> and its webroot folder is <i>D:\inetpub\wwwroot\sample.local</i>.
If you have chosen different settings, ensure to replace them while following the instructions.

## Step :one:: Setup Visual Studio Solution

1. Download the latest version of <a href="https://github.com/joinsukesh/Downloads/blob/main/Stratum/GetStratum_v1.zip" target="_blank">GetStratum.zip</a> file, and extract its contents.
2. Open <i>Windows PowerShell</i> with administrator privileges, and change the directory to the extracted folder. Execute the <b>GetStratumCode.ps1</b> script.

![stratum_1](https://user-images.githubusercontent.com/24619393/235687985-2cb64452-60b5-4f9c-a560-9e312aa990e7.png)

3. Enter the inputs.

![stratum_2](https://user-images.githubusercontent.com/24619393/235832661-2fa106f4-e3e6-40f0-8cc9-48d689d1b2a5.png)

4. This will create the Visual Studio solution in the specified target directory. Once that is done, open the solution file in Visual Studio and build it.

![image](https://user-images.githubusercontent.com/24619393/236735287-ebe7ac06-ba72-4c9c-aaeb-f8beef92dec0.png)


## Step :two:: Deploy Sitecore Items
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


## Step :three:: Configurations to View Demo Page
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

- In the two custom index folders that you have just created, Keep just the <code>conf</code> folder and its contents. Delete everything else including the <i>data</i> folder and the <i>core.properties</i> file.

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
  
  
<b>7. Configure Fake SMTP</b>
- This is to quickly test the email functionality in the demo page used in features like <i>User Registration</i> & <i>Forgot Password</i>.
- Browse https://ethereal.email and click on <i>Create Ethereal Account</i>

![image](https://user-images.githubusercontent.com/24619393/235899437-15ff0c84-e3d6-4975-9156-e9a52bc5b86d.png)

- This will generate account details. Copy the <i>Host</i>, <i>Port</i>, <i>Username</i> & <i>Password</i> information.

![image](https://user-images.githubusercontent.com/24619393/235899745-905e5cf4-4d91-445f-91ab-bee379685239.png)

- In your instance webroot, navigate to <code>\App_config\Include\zzz.MyCompany\Project\MyCompany.Project.Website.config</code> and update the values in the respective settings.

![image](https://user-images.githubusercontent.com/24619393/235900380-c1c59e40-0f8e-4fb2-8787-5d160f48656e.png)

<b>8. Add appSetting keys</b>

- Add these two keys in your instance's <i>web.config</i>, inside the <code><appSettings</code> node.

````
    <add key="ClientValidationEnabled" value="true" />  
    <add key="UnobtrusiveJavaScriptEnabled" value="true" /> 
````
   
   
Thats it!!. Now browse your instance URL to view the demo page.
  
  ![image](https://user-images.githubusercontent.com/24619393/235892899-c8a4c07f-4f9d-401e-845a-a8a8dafe30cf.png)

<hr>
  
## Demo Page 📃 Explained
  
These are the components in order as they appear.
  
### Header  
<b>Project:</b> <i>Feature.Navigation</i>  
  <b>Rendering:</b> <i>Header</i>  
This is the first component on the page. Displays the Menu items. The last item is a dropdown with user icon. It has two links which open respective pages - <i>Sign In</i> & <i>Sign Up</i>.    
If the user is signed in, then this dropdown only shows the <i>Sign Out</i> link.  
  Clicking on any link in the Header menu will scroll to the respective section on the page.
  

### Banner  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Hero Banner</i>  

### About  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Columns Section</i>  
  This rendering has a dynamic placeholder and hence can be used only from <i>Experience Editor</i>. The number of columns can be specified as <i>Rendering Parameters</i>. Each column here uses the <i>Rich Text Section</i> rendering.  
  
  
### Steps  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Numbered Grid Tiles</i>  
  
### Testimonials  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Testimonials</i>  


### Services  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Teaser Tiles</i>  


  ### Gallery  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>MyCompany Gallery</i>  
  The gallery has pagination whose settings can be configured in the datasource.  
  
  ### FAQ  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Accordion</i>  
  
  ### Contact  
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Columns Section</i>  
  Similar to the <i>About</i> section, here too I have used the same rendering and each column has the <i>Rich Text Section</i> rendering.  
  
  ### Form  
<b>Project:</b> <i>Feature.Forms</i>  
  <b>Rendering:</b> <i>Contact Us</i>  
  I have created a <i>Sitecore Form</i> with 4 fields here - <i>/sitecore/client/Applications/FormsBuilder/Pages/Forms</i>. This would create content items in CMS.   

![image](https://user-images.githubusercontent.com/24619393/236162053-4a264f7d-ae01-487d-8d95-107eb5aabbc3.png)  

  Copied those Item IDs & hardcoded in code. When the form is submitted, the logic in code (<code>SaveContactUsFormData()</code>)will save the field values and the respective Form Field Ids in the database.  
  You can view the submitted data in the admin page - <i>/sitecore/admin/MyCompany/scform-report.aspx</i>  
  
  
 ### Footer  
<b>Project:</b> <i>Feature.Navigation</i>  
  <b>Rendering:</b> <i>Footer</i>  
  
  ### Products Listing  (https://sample.local/products)
<b>Project:</b> <i>Feature.PageContent</i>  
  <b>Rendering:</b> <i>Products Listing</i>  
  This page will list the available 3 products and uses Solr search to fecth them.  If you do not see the products, populate the schema & rebuild the indexes again.  
  The backend logic uses Solr search to fetch the products. You can find the code in <code>RenderProductsListing()</code>.
  
  
  ### Sign Up  
<b>Project:</b> <i>Feature.Accounts</i>  
  <b>Rendering:</b> <i>Sign Up</i>  
  Register a new user by providing any email & password.  
  Login to https://ethereal.email with your Ethereal credentials that you have created in <b>Step 3.7</b>  
  You will see the registration email in the inbox. Click the link to complete the registration. 
  
  ### Sign In  
<b>Project:</b> <i>Feature.Accounts</i>  
  <b>Rendering:</b> <i>Sign In</i>  
  You can use the new user credentials to log into the demo site.
  
  ### Forgot Password 
<b>Project:</b> <i>Feature.Accounts</i>  
  <b>Rendering:</b> <i>Forgot Password</i>  
  Provide an existing user email and click on Submit. This will send an email with a link, that you can check in your Ethereal account. Clicking on the link will redirect you to the <i>Reset Password</i> where you can reset your password.
  
  
  ### Reset Password 
<b>Project:</b> <i>Feature.Accounts</i>  
  <b>Rendering:</b> <i>Reset Password</i>  
  
  
  <hr>
  
  ## Projects Explained
  
  ### Feature.AdminPages  
  <b>Page:</b> Sitecore Form Report  
  <b>URL:</b> /sitecore/admin/MyComponent/scform-report.aspx  
  A button for this page is also available in the Launch Pad.  
  
  
  <b>Page:</b> Encrypt & Decrypt 
  <b>URL:</b> /sitecore/admin/MyComponent/encrypt-decrypt.aspx  
  Use this page to encrypt or decrypt a string. 

  ### Feature.Base
  This project is created to host any functionality that can be common for <i>Feaure Projects</i>. 
  The <i>Feature.Base</i> project is refereneced in all <i>Feature Projects</i>.  
  One use case could be, if you want a certain logic to be triggered for any request to any <i>Controller</i>, then inherit <i>BaseController</i> to that controller.  
  
  ### Foundation.Api  
  The <code>ApiServiceManger</code> class has helper methods for the general GET, POST and PATCH requests, including a API request-response logging functionality.  
  
  ### Foundation.Common  
  Has commonly used helper methods, extensions etc to be used across all projects.  
  
  ### Foundation.Search 
  Has the logic for implementing basic Solr search
  
  ### Publish.Website  
  This is the project with the <i>Helix publishing pipeline</i>. Publishing this project will publish all the code files, views & assets from all projects to the output directory.  
  After the initial publish, we can exclude a few items like <i>Global.asax<i>, <i>favicon.ico</i> which are needed for every publish.  
  Open the <i>Publish.Website.wpp.targets</i>, and uncomment the last section to exclude such files from publish.    
  
  ![image](https://user-images.githubusercontent.com/24619393/236172124-c41b7ef8-a267-4861-bd0f-74bacaffca37.png)  

  ### CLI Module Files
  These will be located in your solution folder - <i>/src/SCS/Modules</i>.  
I have included the <i>Content</i> items also for the demo page, but that is not suggested for a real-time project.  

  <hr>  
  
Thats it for now :triumph:. Thank you for reading !!!
  
  
