--DEBUG
--declare
--@userId int = 9,
--@month datetime = getdate();

with objs as (
	select 1 Type,
		'CObject ' + CAST(obj.Id AS nvarchar) Id,
		null ParentId,
		null SalaryPayId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPay,
		obj.ManagerId ManagerId,
		null PositionId,
		null EmployeeId,
		obj.Address Name,
		'Выплат: ' + CAST(Count(sal.Id) AS nvarchar) Phone,
		null PositionName,
		SUM(okl.Summa) Salary,
		SUM(sal.RateDays) RateDays,
		SUM(sal.FactDays) FactDays,
		SUM(sal.SalaryPay) SalaryPay,
		SUM(sal.Avans) Avans,
		SUM(sal.Penalty) Penalty,
		SUM(sal.Premium) Premium,
		null Comment,
		null Month
	 from [Object] obj
		 left join (
				select *
				from SalaryPay s
				where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)
			) sal on sal.ObjectId = obj.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
		 left join Position pos on pos.Id = sal.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where obj.ManagerId = @userId
		 and obj.IsDeleted = 0
	 group by obj.Id, obj.Address, obj.ManagerId
),
salary as (
	 select 2 Type, 
		'Salary ' + CAST(sal.Id AS nvarchar) Id,
		'CObject ' + CAST(obj.Id AS nvarchar) ParentId,
		sal.Id SalaryPayId,
		obj.Id ObjectId,
		obj.Address ObjectNameForSalaryPay,
		obj.ManagerId ManagerId,
		pos.Id PositionId,
		emp.Id EmployeeId,
		emp.Name Name,
		emp.Phone Phone,
		pos.Name PositionName,
		okl.Summa Salary,
		sal.RateDays RateDays,
		sal.FactDays FactDays,
		sal.SalaryPay SalaryPay,
		sal.Avans Avans,
		sal.Penalty Penalty,
		sal.Premium Premium,
		sal.Comment Comment,
		sal.Month Month
	 from (select * from SalaryPay s where Month(s.Month) = Month(@month) and YEAR(s.Month) = YEAR(@month)) sal
		 left join [Object] obj on sal.ObjectId = obj.Id
		 left join Position pos on pos.Id = sal.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
		 left join Employee emp on emp.Id = sal.EmployeeId
	 where obj.ManagerId = @userId
		 and obj.IsDeleted = 0
)
select * from objs
union all select * from salary
order by Name