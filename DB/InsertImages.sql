CREATE PROCEDURE dbo.InsertImages (
	@Name varchar(50),
	@Path varchar(max),
	@LastModified datetime,
	@Created datetime,
	@Tag varchar(max)
)
AS
BEGIN
	INSERT INTO [dbo].[Images]
			   ([Name]
			   ,[Path]
			   ,[LastModified]
			   ,[Created]
			   ,[Tag])
		 VALUES
			   (@Name,
			   @Path,
			   @LastModified,
			   @Created,
			   @Tag)

	END 
GO



