/*
- Script to create a view for sitecore form data
- To be executed in the ExperienceForms database 
*/
CREATE VIEW uv_ScFormData 
AS
SELECT fe.Id AS EntryId, fe.FormDefinitionId AS FormId, fe.ContactId, fd.FieldName, fd.Value AS FieldValue, fe.Created
FROM [sitecore_forms_storage].[FormEntries] fe (NOLOCK)
LEFT JOIN [sitecore_forms_storage].[FieldData] fd (NOLOCK) ON fd.FormEntryId = fe.Id