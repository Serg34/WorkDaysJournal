--DEBUG
--declare
--@login nvarchar(50) = 'User1',
--@password nvarchar(50) = '123'

select 
	u.Id, 
	u.Login, 
	u.Name, 
	u.Role_Id, 
	Role.Name RoleName
from [User] u 
	left join Role 
		on u.Role_Id = Role.Id
where Login = @login
	and Pass = @password
