--DEBUG
--declare
--@userId int = 68,
--@month datetime = getdate();

with projects as(
	 select 0 Type,
		 'Project ' + CAST(pr.Id AS nvarchar) Id,
		 null ParentId,
		 null SalaryPaidId,
		 null ObjectId,
		 null ObjectNameForSalaryPaid,
		 null ManagerId,
		 null PositionId,
		 null EmployeeId,
		 pr.Name Name,
		 'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		 'Объектов: ' + CAST(Count(obj.Id) AS nvarchar) PositionName,
		 SUM(okl.Summa) Salary,
		 SUM(sal.RateDays) RateDays,
		 SUM(sal.FactDays) FactDays,
		 SUM(sal.SalaryPaid) SalaryPaid,
		 SUM(sal.Avans) Avans,
		 SUM(sal.Penalty) Penalty,
		 SUM(sal.Premium) Premium,
		 null Comment,
		 null Month
	 from Project pr
		 left join [Object] obj on obj.ProjectId = pr.Id
		 left join (
			select *
			from SalaryPaid s
			where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.ProjectManagerId = @userId
		and pr.IsDeleted = 0
		and obj.IsDeleted = 0
	 group by pr.Id, pr.Name
),
objs as (
	select 1 Type,
		'CObject ' + CAST(obj.Id AS nvarchar) Id,
		'Project ' + CAST(pr.Id AS nvarchar) ParentId,
		null SalaryPaidId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPaid,
		obj.ManagerId ManagerId,
		null PositionId,
		null EmployeeId,
		obj.Address Name,
		'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		null PositionName,
		SUM(okl.Summa) Salary,
		SUM(sal.RateDays) RateDays,
		SUM(sal.FactDays) FactDays,
		SUM(sal.SalaryPaid) SalaryPaid,
		SUM(sal.Avans) Avans,
		SUM(sal.Penalty) Penalty,
		SUM(sal.Premium) Premium,
		null Comment,
		null Month
	 from [Object] obj
		 left join Project pr on obj.ProjectId = pr.Id
		 left join (
				select *
				from SalaryPaid s
				where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.ProjectManagerId = @userId
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
	 group by pr.Id, obj.Id, obj.Address, obj.ManagerId
),
salary as (
	 select 2 Type, 
		'Salary ' + CAST(sal.Id AS nvarchar) Id,
		'CObject ' + CAST(obj.Id AS nvarchar) ParentId,
		sal.Id SalaryPaidId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPaid,
		obj.ManagerId ManagerId,
		pos.Id PositionId,
		emp.Id EmployeeId,
		emp.Name Name,
		emp.Phone Phone,
		pos.Name PositionName,
		okl.Summa Salary,
		sal.RateDays RateDays,
		sal.FactDays FactDays,
		sal.SalaryPaid SalaryPaid,
		sal.Avans Avans,
		sal.Penalty Penalty,
		sal.Premium Premium,
		sal.Comment Comment,
		sal.Month Month
	 from (select * from SalaryPaid s where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)) sal
		 left join [Object] obj on sal.ObjectId = obj.Id
		 left join Project pr on obj.ProjectId = pr.Id
		 left join Position pos on pos.Id = sal.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
	 where pr.ProjectManagerId = @userId
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
)
select * from projects
union all select * from objs
union all select * from salary
order by Name