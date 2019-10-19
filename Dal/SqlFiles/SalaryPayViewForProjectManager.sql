--DEBUG
--declare
--@userId int = 63,
--@month datetime = getdate();

with projects as(
	 select -2 Id,
		 0 Type,
		 'Project ' + CAST(pr.Id AS nvarchar) ViewModelId,
		 null ParentId,
		 null ObjectId,
		 null ManagerId,
		 null PositionId,
		 null EmployeeId,
		 pr.Name Name,
		 'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		 'Объектов: ' + CAST((select Count(*) 
							  from Object 
							  where Project_ID = pr.ID and IsDeleted = 0) 
							  AS nvarchar) PositionName,
		 SUM(okl.Salary) Salary,
		 SUM(sal.RateDays) RateDays,
		 SUM(sal.FactDays) FactDays,
		 SUM(sal.SalaryPay) SalaryPay,
		 SUM(sal.Advance) Advance,
		 SUM(sal.Penalty) Penalty,
		 SUM(sal.Premium) Premium,
		 null Comment,
		 null Month
	 from Project pr
		 left join [Object] obj on obj.ProjectId = pr.Id
		 left join (
			select *
			from SalaryPay s
			where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Salary okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.ProjectManagerId = @userId
		and pr.IsDeleted = 0
		and obj.IsDeleted = 0
	 group by pr.Id, pr.Name
),
objs as (
	select -1 Id,
		1 Type,
		'Object ' + CAST(obj.Id AS nvarchar) ViewModelId,
		'Project ' + CAST(pr.Id AS nvarchar) ParentId,
		obj.Id ObjectId,
		obj.ManagerId ManagerId,
		null PositionId,
		null EmployeeId,
		obj.Address Name,
		'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		null PositionName,
		SUM(okl.Salary) Salary,
		SUM(sal.RateDays) RateDays,
		SUM(sal.FactDays) FactDays,
		SUM(sal.SalaryPay) SalaryPay,
		SUM(sal.Advance) Advance,
		SUM(sal.Penalty) Penalty,
		SUM(sal.Premium) Premium,
		null Comment,
		null Month
	 from [Object] obj
		 left join Project pr on obj.ProjectId = pr.Id
		 left join (
				select *
				from SalaryPay s
				where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Salary okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.ProjectManagerId = @userId
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
	 group by pr.Id, obj.Id, obj.Address, obj.ManagerId
),
pay as (
	 select sal.Id,
		2 Type, 
		'Salary ' + CAST(sal.Id AS nvarchar) ViewModelId,
		'Object ' + CAST(obj.Id AS nvarchar) ParentId,
		obj.Id ObjectId,
		obj.ManagerId ManagerId,
		pos.Id PositionId,
		emp.Id EmployeeId,
		emp.Name Name,
		emp.Phone Phone,
		pos.Name PositionName,
		okl.Salary Salary,
		sal.RateDays RateDays,
		sal.FactDays FactDays,
		sal.SalaryPay SalaryPay,
		sal.Advance Advance,
		sal.Penalty Penalty,
		sal.Premium Premium,
		sal.Comment Comment,
		sal.Month Month
	 from (select * from SalaryPay s where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)) sal
		 left join [Object] obj on sal.ObjectId = obj.Id
		 left join Project pr on obj.ProjectId = pr.Id
		 left join Position pos on pos.Id = sal.PositionId
		 left join Salary okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
	 where pr.ProjectManagerId = @userId
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
)
select * from projects
union all select * from objs
union all select * from pay
order by Name