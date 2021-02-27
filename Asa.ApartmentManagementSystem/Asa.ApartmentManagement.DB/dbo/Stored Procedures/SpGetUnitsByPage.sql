CREATE PROCEDURE [dbo].[SpGetUnitsByPage]
@page INT, -- current page
@size INT -- count of items in each page
AS
BEGIN
	select UnitId , Number, Area, MAX([OwnerName]) as [Owner] , MAX([ResidentName]) as [Resident]
	from(
		select u.UnitId, Number, IsOwner, FullName, Area , case when IsOwner=1 then FullName end   as [OwnerName], case when IsOwner = 0 then Fullname end as[ResidentName]
		from Unit u
		left join PersonUnit pu on pu.UnitId = u.UnitId
		left join Person p on p.PersonId = pu.PersonId
		where pu.[To] is null
	) as derivedTable
	group by UnitId, Number, Area
	ORDER BY UnitId
	OFFSET (@page -1) * @size ROWS
	FETCH NEXT @size ROWS ONLY
END