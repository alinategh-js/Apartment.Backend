CREATE PROCEDURE [dbo].[spGetPersonOwnerTenantByPageAndType]
	@Page INT, -- current page
@Size INT, -- count of items in each page
@Owner INT -- owner/resident ( filter )  ( 1: owner , 0: resident )
AS
BEGIN
	SELECT p.id, p.full_name, p.phone_number, ot.unit_id, ot.is_owner
	FROM 
	(
		select *
		from owner_tenant
		where is_owner= case when @Owner in (1,0) then @Owner
						else is_owner end
	)as ot
	INNER JOIN [dbo].[person] as p
	ON p.id = ot.person_id
	ORDER BY person_id
	OFFSET (@Page -1) * @Size ROWS
	FETCH NEXT @Size ROWS ONLY
END