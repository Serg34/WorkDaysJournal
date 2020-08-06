--DEBUG
--declare @userId int = 66;
--declare @userId int = 9;

if (select Role_Id from [User] where Id = @userId) = 4
	select o.*
	from Object o
		left Join Project p
			on o.Project_Id = p.Id
	where p.Rukovod_Id = @userId

else select	o.*
	from Object o
	where o.Manager_Id = @userId
