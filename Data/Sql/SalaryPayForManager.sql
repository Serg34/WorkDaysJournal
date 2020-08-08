--declare @userId int = 1003;
--declare @year int = 2019;
--declare @month int = 8;

with pays as (
	 select pay.Id,
		2 Type, 
		'Salary ' + CAST(pay.Id AS nvarchar) ViewModelId,
		'Object ' + CAST(obj.Id AS nvarchar) ParentId,
		@year Year,
		@month Month,
		1 EmployeeCount,
		obj.Id Object_Id,
		obj.Manager_Id Manager_Id,
		emp.Id Employee_Id,
		emp.Name Name,
		emp.Position Position,
		emp.Phone Phone,
		emp.Salary Salary,
		pay.RateDays RateDays,
		pay.FactDays FactDays,
		pay.Advance Advance,
		pay.Penalty Penalty,
		pay.Premium Premium,
		pay.SalaryToPay SalaryToPay,
		pay.Comment Comment
	 from (select * from SalaryPay s where s.Month = @month and s.Year = @year) pay
		 left join [Object] obj on pay.Object_Id = obj.Id
		 left join Employee emp on emp.Id = pay.Employee_Id
	 where obj.Manager_Id = @userId
		 and obj.IsDeleted = 0
),
objs as (
	select -1 Id,
		1 Type,
		'Object ' + CAST(obj.Id AS nvarchar) ViewModelId,
		null ParentId,
		@year Year,
		@month Month,
		Count(pay.Id) EmployeeCount,
		obj.Id Object_Id,
		obj.Manager_Id Manager_Id,
		-1 Employee_Id,
		obj.Name Name,
		obj.Address Position,
		'Сотрудников: ' + CAST(Count(pay.Id) AS nvarchar) Phone,
		SUM(emp.Salary) Salary,
		SUM(pay.RateDays) RateDays,
		SUM(pay.FactDays) FactDays,
		SUM(pay.Advance) Advance,
		SUM(pay.Penalty) Penalty,
		SUM(pay.Premium) Premium,
		SUM(pay.SalaryToPay) SalaryToPay,
		null Comment
	 from [Object] obj
		 left join pays pay on pay.Object_Id = obj.Id
		 left join Employee emp on emp.Id = pay.Employee_Id
	 where obj.Manager_Id = @userId
		 and obj.IsDeleted = 0
	 group by obj.Id, obj.Name, obj.Address, obj.Manager_Id
),
allPays as(
	select * from objs
	union all select * from pays
)
select * from allPays
union all select -1 Id,
		 3 Type,
		 'Summary' ViewModelId,
		 null ParentId,
		 -1 Object_Id,
		 -1 Manager_Id,
		 -1 Employee_Id,
		 @year Year,
		 @month Month,
		 Sum(EmployeeCount) EmployeeCount,
		 'Итого:' Name,
		 'Объектов: ' + CAST(Count(Id) as nvarchar) Position,
		 'Сотрудников: ' + CAST(Sum(EmployeeCount) as nvarchar) Phone,
		 SUM(Salary) Salary,
		 SUM(RateDays) RateDays,
		 SUM(FactDays) FactDays,
		 SUM(Advance) Advance,
		 SUM(Penalty) Penalty,
		 SUM(Premium) Premium,
		 SUM(SalaryToPay) SalaryToPay,
		 null Comment
from objs 
group by Type
order by Type, Name