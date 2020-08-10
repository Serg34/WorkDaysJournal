--declare @userId int = 1027;
--declare @year int = 2019;
--declare @month int = 4;

with pays as (
	 select pay.Id,
		2 Type, 
		'Salary ' + CAST(pay.Id as nvarchar) ViewModelId,
		'Object ' + CAST(obj.Id as nvarchar) ParentId,
		pr.Id Project_Id,
		obj.Id Object_Id,
		obj.Manager_Id Manager_Id,
		emp.Id Employee_Id,
		@year Year,
		@month Month,
		0 ObjectCount,
		1 EmployeeCount,
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
	 from (select * from SalaryPay p where p.Month = @month and p.Year = @year) pay
		 left join [Object] obj on pay.Object_Id = obj.Id
		 left join Project pr on obj.Project_Id = pr.Id
		 left join Employee emp on emp.Id = pay.Employee_Id
	 where (@userId = 0 or pr.ProjectManager_Id = @userId)
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
),
objs as (
	select -1 Id,
		1 Type,
		'Object ' + CAST(obj.Id as nvarchar) ViewModelId,
		'Project ' + CAST(pr.Id as nvarchar) ParentId,
		pr.Id Project_Id,
		obj.Id Object_Id,
		obj.Manager_Id Manager_Id,
		-1 Employee_Id,
		@year Year,
		@month Month,
		1 ObjectCount,
		Count(pay.Id) EmployeeCount,
		obj.Name Name,
		obj.Address Position,
		'Сотрудников: ' + CAST(Count(pay.Id) as nvarchar) Phone,
		SUM(emp.Salary) Salary,
		SUM(pay.RateDays) RateDays,
		SUM(pay.FactDays) FactDays,
		SUM(pay.Advance) Advance,
		SUM(pay.Penalty) Penalty,
		SUM(pay.Premium) Premium,
		SUM(pay.SalaryToPay) SalaryToPay,
		null Comment
	 from [Object] obj
		 left join Project pr on obj.Project_Id = pr.Id
		 left join pays pay on pay.Object_Id = obj.Id
		 left join Employee emp on emp.Id = pay.Employee_Id
	 where (@userId = 0 or pr.ProjectManager_Id = @userId)
		 and pr.IsDeleted = 0
		 and obj.IsDeleted = 0
	 group by pr.Id, obj.Id, obj.Name, obj.Address, obj.Manager_Id
),
projects as(
	 select -1 Id,
		 0 Type,
		 'Project ' + CAST(pr.Id AS nvarchar) ViewModelId,
		 null ParentId,
		 pr.Id Project_Id,
		 -1 Object_Id,
		 -1 Manager_Id,
		 -1 Employee_Id,
		 @year Year,
		 @month Month,
		 Sum(obj.ObjectCount) ObjectCount,
		 Sum(obj.EmployeeCount) EmployeeCount,
		 pr.Name Name,
		 'Объектов: ' + CAST(Sum(obj.ObjectCount) as nvarchar) Position,
		 'Сотрудников: ' + CAST(Sum(obj.EmployeeCount) as nvarchar) Phone,
		 SUM(obj.Salary) Salary,
		 SUM(obj.RateDays) RateDays,
		 SUM(obj.FactDays) FactDays,
		 SUM(obj.Advance) Advance,
		 SUM(obj.Penalty) Penalty,
		 SUM(obj.Premium) Premium,
		 SUM(obj.SalaryToPay) SalaryToPay,
		 null Comment
	 from Project pr
		 left join objs obj on obj.Project_Id = pr.Id
	 where (@userId = 0 or pr.ProjectManager_Id = @userId)
		and pr.IsDeleted = 0
	 group by pr.Id, pr.Name
),
allPays as(
	select * from projects
	union all select * from objs
	union all select * from pays
)
select * from allPays
union all select -1 Id,
		 3 Type,
		 'Summary' ViewModelId,
		 null ParentId,
		 -1 Project_Id,
		 -1 Object_Id,
		 -1 Manager_Id,
		 -1 Employee_Id,
		 @year Year,
		 @month Month,
		 Sum(ObjectCount) ObjectCount,
		 Sum(EmployeeCount) EmployeeCount,
		 'Итого: проектов ' + Cast(Count(Id) as nvarchar) Name,
		 'Объектов: ' + CAST(Sum(ObjectCount) as nvarchar) Position,
		 'Сотрудников: ' + CAST(Sum(EmployeeCount) as nvarchar) Phone,
		 SUM(Salary) Salary,
		 SUM(RateDays) RateDays,
		 SUM(FactDays) FactDays,
		 SUM(Advance) Advance,
		 SUM(Penalty) Penalty,
		 SUM(Premium) Premium,
		 SUM(SalaryToPay) SalaryToPay,
		 null Comment
from projects 
group by Type
order by Type, Name