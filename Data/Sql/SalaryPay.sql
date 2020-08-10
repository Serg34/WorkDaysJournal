--declare @payId int = 562794;

select pay.*,
	2 Type, 
	'Salary ' + CAST(pay.Id AS nvarchar) ViewModelId,
	'Object ' + CAST(obj.Id AS nvarchar) ParentId,
	emp.Name Name,
	emp.Position Position,
	emp.Phone Phone,
	emp.Salary Salary
from SalaryPay pay
	left join [Object] obj on pay.Object_Id = obj.Id
	left join Employee emp on emp.Id = pay.Employee_Id
where pay.Id = @payId
