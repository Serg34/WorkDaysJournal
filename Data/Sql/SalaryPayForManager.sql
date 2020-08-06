--DEBUG
--declare @userId int = 9;
--declare @year int = 2020;
--declare @month int = 4;

with objs as (
	select -1 Id,
		1 Type,
		'Object ' + CAST(obj.Id AS nvarchar) ViewModelId,
		null ParentId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPay,
		obj.ManagerId ManagerId,
		null PositionId,
		null EmployeeId,
		obj.Address Name,
		'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		null PositionName,
		SUM(norm.Salary) Salary,
		SUM(sal.RateDays) RateDays,
		SUM(sal.FactDays) FactDays,
		SUM(sal.SalaryPay) SalaryPay,
		SUM(sal.Advance) Advance,
		SUM(sal.Penalty) Penalty,
		SUM(sal.Premium) Premium,
		null Comment,
		null Month
	 from [Object] obj
		 left join (
				select *
				from SalaryPay s
				where Month(s.Month) = @month and YEAR(s.Month) = @year
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Salary norm on norm.ObjectId = obj.Id and norm.PositionId = pos.Id
	 where obj.ManagerId = @userId
		 and obj.IsDeleted = 0
	 group by obj.Id, obj.Address, obj.ManagerId
),
pay as (
	 select sal.Id,
		2 Type, 
		'Salary ' + CAST(sal.Id AS nvarchar) ViewModelId,
		'Object ' + CAST(obj.Id AS nvarchar) ParentId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPay,
		obj.ManagerId ManagerId,
		pos.Id PositionId,
		emp.Id EmployeeId,
		emp.Name Name,
		emp.Phone Phone,
		pos.Name PositionName,
		norm.Salary Salary,
		sal.RateDays RateDays,
		sal.FactDays FactDays,
		sal.SalaryPay SalaryPay,
		sal.Advance Advance,
		sal.Penalty Penalty,
		sal.Premium Premium,
		sal.Comment Comment,
		sal.Month Month
	 from (select * from SalaryPay s where Month(s.Month) = @month and YEAR(s.Month) = @year) sal
		 left join [Object] obj on sal.ObjectId = obj.Id
		 left join Position pos on pos.Id = sal.PositionId
		 left join Salary norm on norm.ObjectId = obj.Id and norm.PositionId = pos.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
	 where obj.ManagerId = @userId
		 and obj.IsDeleted = 0
)
select * from objs
union all select * from pay
order by Name