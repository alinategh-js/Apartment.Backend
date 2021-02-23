CREATE procedure [dbo].[SpGetAllPersonUnit]
@unitId int
as 
	SELECT    
		PersonUnit.Id as owner_tenant_id, 
		PersonUnit.[From] as from_date,	
		PersonUnit.[To] as to_date,
		PersonUnit.IsOwner as is_owner,
		person.FullName as full_name,
		person.PersonId as person_id,
		person.PhoneNumber as phone_number,
		Unit.number as unit_number
	FROM         
	Unit left join
		PersonUnit on PersonUnit.UnitId= Unit.UnitId
        left join 
		Person ON PersonUnit.PersonId= Person.PersonId
	where Unit.UnitId =@unitId
	order by PersonUnit.[From] desc