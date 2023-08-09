/*
- Script to create a stored procedure to get sitecore form data
- To be executed in the ExperienceForms database 
*/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get sitecore form data for a form by formid & date range
-- =============================================
--exec usp_GetSitecoreFormData '60EE0499-1D2C-4962-A63A-4626362D81B1', '2023-05-01 00:00:00.000', '2023-05-01 23:59:59.000'
CREATE PROCEDURE [dbo].[usp_GetSitecoreFormData] 
	@FormId uniqueidentifier,
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;

	DECLARE @cols AS nvarchar(MAX),
    @query  AS nvarchar(MAX)

	--get fieldnames (which are now as rows) to later convert to columns
	--assuming that a form will not have more than 100 fields
	SELECT @cols = STUFF((SELECT TOP 100 ',' + QUOTENAME(FieldName) 
                    FROM uv_ScFormData
					WHERE FormId = @FormId AND Created BETWEEN @StartDate AND @EndDate
                    GROUP BY FieldName                    
					FOR XML PATH(''), TYPE
					).value('.', 'nvarchar(MAX)') 
					,1,1,'')
    --print @cols
	SET @query = N'SELECT ContactId, ' + @cols + ', Created' + N'
	            FROM 
				(
					SELECT EntryId, ContactId, FieldName, FieldValue, Created
					FROM uv_ScFormData (NOLOCK)
					WHERE FormId = ''' + CONVERT(varchar(50), @FormId) + ''' AND Created BETWEEN ''' + CONVERT(varchar(50), @StartDate) + ''' AND ''' + CONVERT(varchar(50), @EndDate) + '''					
				) cols
				PIVOT 
				(
					MAX(FieldValue)
					FOR FieldName in (' + @cols + N')
				) p'
	--print @query
	EXEC sp_executesql @query
	
END
