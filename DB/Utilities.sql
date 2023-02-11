/****** Script for SelectTopNRows command from SSMS  ******/
SELECT  [IdImage]
      ,[Name]
      ,[Path]
      ,[LastModified]
      ,[Created]
      ,[Tag]
  FROM [dbTest2023].[dbo].[Images]

  --truncate table Images

select Cast(Created as date), * from Images
where Cast(Created as date) between '2023-02-02' and  '2023-02-03'

select Distinct Cast(LastModified as date) from Images
where Cast(LastModified as date) between '2010-02-01' and  '2023-02-10'