CREATE PROCEDURE [dbo].[SpGetPersonUnitByPageAndType]
@page INT, -- current page
@size INT, -- count of items in each page
@owner INT -- owner/resident ( filter )  ( 1: owner , 0: resident )
AS
BEGIN
	SELECT p.PersonId as PersonId, p.FullName, p.PhoneNumber, pu.UnitId, pu.IsOwner
	FROM 
	(
		select *
		from PersonUnit
		where IsOwner= case when @owner in (1,0) then @owner
						else IsOwner end
	)as pu
	INNER JOIN [dbo].[person] as p
	ON p.PersonId = pu.PersonId
	ORDER BY PersonId
	OFFSET (@page -1) * @size ROWS
	FETCH NEXT @size ROWS ONLY
END