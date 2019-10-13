--DEBUG
--declare
--@objectId int = 'User1',

select
	p.Id,
	p.Name,
	p.dtc,
	o.Summa Salary
from Position p
	join Oklad o on o.Position_Id = p.Id
where o.Object_Id in (@objectId)