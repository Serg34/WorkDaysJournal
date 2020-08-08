--declare @login nvarchar(50) = 'User1';
--declare @password nvarchar(50) = '123';

select 
	u.*,
	Role.Name RoleName
from [User] u
	left join Role
		on u.Role_Id = Role.Id
where Login = @login
	and Password = @password
