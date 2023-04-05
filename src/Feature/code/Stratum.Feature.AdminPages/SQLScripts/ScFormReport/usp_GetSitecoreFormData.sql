SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Description:	Get sitecore form data for a form by formid & date range
-- =============================================
--exec usp_GetSitecoreFormData '287DE272-D2DA-4986-AE70-ECFF1E0AC5D6', '2023-01-26 00:00:00.000', '2023-01-26 00:00:00.000'
CREATE PROCEDURE [dbo].[usp_GetSitecoreFormData] 
	@FormId uniqueidentifier,
	@StartDate datetime,
	@EndDate datetime
AS
BEGIN
	
	SET NOCOUNT ON;

	--Add 1 day to the end date
	SELECT @EndDate = DATEADD(DD, 1, @EndDate)

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
	SET @query = N'SELECT ContactId, ' + @cols + N'
	            FROM 
				(
					SELECT EntryId, ContactId, FieldName, FieldValue
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
