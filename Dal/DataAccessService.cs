using Furmanov.Dal.Dto;
using Furmanov.Dal.Queries;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Furmanov.Dal
{
	#region IdataAccessService
	public interface IdataAccessService
	{
		LoginPassword GetLoginPassword();
		bool SaveLoginPassword(LoginPassword loginPassword);
		UserDb GetUser(string login, string password);
		List<CObject> GetObjects(UserDb user);
		List<Position> GetPosition(int objectId);
		List<SalaryPay> GetResOps(int[] objectsId, DateTime month);
		List<Employee> GetResources(SelectionResourceMode selectionMode, params int[] objectsId);
		List<CTabel> GetTabels(int? resOpId, DateTime month);

		bool SaveResource(Employee resource);
		bool DeleteResource(Employee resource);
		bool SaveResOp(SalaryPay salaryPay);
		bool DeleteResOp(int resOpId);
		bool DeleteResOp(SalaryPay r);

		bool SaveTabels(params CTabel[] tabels);
		List<ResOPViewModel> GetResOPViewModels(UserDb user, DateTime month);
		DataTable GetVedomost(UserDb user, int objectId = 0);
	}
	#endregion

	public class DataAccessService : IdataAccessService
	{
		private readonly string _connectionString;

		public DataAccessService(string connectionString)
		{
			_connectionString = connectionString;
		}

		public LoginPassword GetLoginPassword()
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SC.Tabel",
				"LoginPassword.xml");

			var res = new XmlDataContractRepository<LoginPassword>(file).Load();
			return res;
		}

		public bool SaveLoginPassword(LoginPassword loginPassword)
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SC.Tabel",
				"LoginPassword.xml");
			new XmlDataContractRepository<LoginPassword>(file).Save(loginPassword);
			return true;
		}
		public UserDb GetUser(string login, string password)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				var sql = QueriesService.Queries["User.sql"];
				var res = db.Query<UserDb>(sql, 
					new DataParameter("@login", login),
					new DataParameter("@password", password))
					.FirstOrDefault();
				return res;
			}
		}
		public List<CObject> GetObjects(UserView user)
		{
			if (user == null) return new List<CObject>();

			using (var db = new DbDataContext(_connectionString))
			{
				var sql = QueriesService.Queries["Objects.sql"];
				var res = db.Query<CObject>(sql,
					new DataParameter("@userId", user.Id))
					.ToList();
				return res;
			}
		}

		public List<Position> GetPosition(int objectId)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				var sql = QueriesService.Queries["Objects.sql"];
				var res = db.Query<Position>(sql, 
					new DataParameter("@objectId", objectId))
					.ToList();
				return res;
			}
		}

		public bool SaveResOp(SalaryPay salaryPay)
		{
			
		}
		public bool DeleteResOp(int resOpId)
		{
			return G.db_exec(@"delete ResOP where Id = {1}", resOpId);
		}
		public bool DeleteResOp(SalaryPay r)
		{
			return G.db_exec(@"delete ResOP where Object_Id = {1}
										and Resource_Id = {2}
										and Year(Month) = {3}
										and Month(Month) = {4}",
								r.Object_Id,
								r.Resource_Id,
								r.Month?.Year,
								r.Month?.Month);
		}

		public List<Employee> GetResources(SelectionResourceMode selectionMode, params int[] objectsId)
		{
			if (objectsId == null || objectsId.Length == 0) return new List<Employee>();
			var objectsIdTxt = objectsId.ToString(",");

			var modeTxts = new Dictionary<SelectionResourceMode, string>
			{
				[SelectionResourceMode.All] = "",
				[SelectionResourceMode.Staff] = "Manager_Id is not null and",
				[SelectionResourceMode.Freelance] = "Manager_Id is null and",
			};

			var sql = @"select * from Resource
						where " + modeTxts[selectionMode] + " OBJECT_Id in ({1})";

			DataTable dt = G.db_select(sql, objectsIdTxt);

			var res = dt.ToObjList(Employee.Factory);
			return res;
		}
		public bool SaveResource(Employee r)
		{
			return G.db_exec(
@"if exists (select id from Resource where id = {1})
		update Resource set
			Name = {2}, Phone = {3}, Description = {4}, Object_Id = {5}, OfficialSalary = {6}, Manager_Id = {7}, Card = {8}
			where id = {1}
	else
		insert Resource(Name, Phone, Description, Object_Id, OfficialSalary, Manager_Id, Card)
		values({2}, {3}, {4}, {5}, {6}, {7}, {8}) select @@IdENTITY",
		r.Id,
			r.Name != null ? $"'{r.Name}'" : "NULL",
			r.Phone != null ? $"'{r.Phone}'" : "NULL",
			r.Description != null ? $"'{r.Description}'" : "NULL",
			r.Object_Id?.ToString() ?? "NULL",
			r.OfficialSalary?.ToString(CultureInfo.CreateSpecificCulture("en-GB")) ?? "NULL",
			r.Manager_Id?.ToString() ?? "NULL",
			r.Card != null ? $"'{r.Card}'" : "NULL");
		}
		public bool DeleteResource(Employee r)
		{
			return G.db_exec(@"delete Resource 
									where Name = {1}
									and Object_Id = {2}
									and Manager_Id = {3}
									and Card = {4}
									and Phone = {5}
									and OfficialSalary = {6}"
				, r.Name != null ? $"'{r.Name}'" : "NULL",
				r.Object_Id?.ToString() ?? "NULL",
				r.Manager_Id?.ToString() ?? "NULL",
				r.Card != null ? $"'{r.Card}'" : "NULL",
				r.Phone != null ? $"'{r.Phone}'" : "NULL",
				r.OfficialSalary?.ToString() ?? "NULL");
		}

		public List<CTabel> GetTabels(int? resOpId, DateTime month)
		{
			if (resOpId == null) return new List<CTabel>();

			var sql = @"SELECT *
						FROM Tabel
						Where ResOP_Id = {1}
						and YEAR(Date) = {2}
						and Month(Date) = {3}";

			DataTable dt = G.db_select(sql, resOpId, month.Year, month.Month);

			var res = dt.ToObjList(CTabel.Factory);
			return res;
		}
		public bool SaveTabels(params CTabel[] tabels)
		{
			var bld = new StringBuilder();

			var deleted = tabels.Where(t => !t.IsExit).ToArray();
			if (deleted.Any())
			{
				var resOP_Ids = deleted.Select(t => t.ResOP_Id).Distinct().ToString(",");
				var years = deleted.Select(t => t.Date?.Year ?? -1).Distinct().ToString(",");
				var months = deleted.Select(t => t.Date?.Month ?? -1).Distinct().ToString(",");
				var days = deleted.Select(t => t.Date?.Day ?? -1).Distinct().ToString(",");

				bld.Append($"delete Tabel where ResOP_Id in ({resOP_Ids})\n");
				bld.Append($"and YEAR(Date) in ({years})\n");
				bld.Append($"and MONTH(Date) in ({months})\n");
				bld.Append($"and Day(Date) in ({days})\n");
			}

			var source = tabels.Where(t => t.IsExit).ToArray();
			if (source.Any())
			{
				bld.Append("merge Tabel using(\n");
				for (int i = 0; i < source.Length; i++)
				{
					if (i > 0) bld.Append("union ");
					bld.Append($"select {source[i].ResOP_Id} ResOP_Id, '{source[i].Date:yyyyMMdd}' Date\n");
				}
				bld.Append(@") as source
							on Tabel.ResOP_Id = source.ResOP_Id 
							and YEAR(Tabel.Date) = YEAR(source.Date)
							and MONTH(Tabel.Date) = MONTH(source.Date)
							and DAY(Tabel.Date) = DAY(source.Date)
						when not matched then
							insert (ResOP_Id, Date)
							values (source.ResOP_Id, source.Date);");
			}

			var sql = bld.ToString();
			return G.db_exec(sql);
		}

		public List<ResOPViewModel> GetResOPViewModels(UserView user, DateTime month)
		{
			if (user == null) return new List<ResOPViewModel>();
			if (user.Role == Role.ProjectManager) return GetResOPViewModelsForRukovod(user.Id, month);
			if (user.Role == Role.Manager) return GetResOPViewModelsForManager(user.Id, month);
			throw new ArgumentException("Нет прав доступа");
		}

		private List<ResOPViewModel> GetResOPViewModelsForRukovod(int rukovodId, DateTime month)
		{
			var sql =
@"with projects as(
	 select 0 Type,
		 'Project ' + CAST(pr.Id AS nvarchar) Id,
		 'Project ' + CAST(pr.Id AS nvarchar) Group_Id,
		 null ParentId, null ResOP_Id, null Object_Id,
		 null ObjectNameForResOp, null Manager_Id, 0 IsStaff,
		 null PositionId, null Resource_Id, pr.Name Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 'Объектов: ' + CAST(Count(obj.Id) AS nvarchar) PositionName, 
		 SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary,
		 SUM(ResOP.RateDays) RateDays, SUM(ResOP.FactDays) FactDays, SUM(ResOP.FactSalary) FactSalary, 
		 SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, SUM(ResOP.Premium) Premium,
		 null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from Project pr
		 left join CObject obj on obj.Project_Id = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.Object_Id = obj.Id
		 left join Resource res on res.Id = resOp.Resource_Id
		 left join Position pos on pos.Id = resOp.Position_Id
		 left join Oklad okl on okl.Object_Id = obj.Id and okl.Position_Id = pos.Id
	 where pr.Rukovod_Id = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by pr.Id, pr.Name
),
objs as (
	 select 1 Type, 'CObject ' + CAST(obj.Id AS nvarchar) Id,
		 'CObject ' + CAST(obj.Id AS nvarchar) Group_Id,
		  'Project ' + CAST(pr.Id AS nvarchar) ParentId, 
		 null ResOP_Id, obj.Id Object_Id, obj.Address ObjectNameForResOp, obj.Manager_Id Manager_Id,
		 0 IsStaff, null PositionId, null Resource_Id, obj.Address Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 null PositionName, SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary, SUM(ResOP.RateDays) RateDays, 
		 SUM(ResOP.FactDays) FactDays, SUM(ResOP.FactSalary) FactSalary, SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, 
		 SUM(ResOP.Premium) Premium, null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from CObject obj
		 left join Project pr on obj.Project_Id = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.Object_Id = obj.Id
		 left join Resource res on res.Id = resOp.Resource_Id
		 left join Position pos on pos.Id = resOp.Position_Id
		 left join Oklad okl on okl.Object_Id = obj.Id and okl.Position_Id = pos.Id
	 where pr.Rukovod_Id = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by pr.Id, obj.Id, obj.Address, obj.Manager_Id
),
resOps as (
	 select 2 Type, 'ResOp ' + CAST(resOp.Id AS nvarchar) Id,
		 'ResOp ' + CAST(resOp.Id AS nvarchar) Group_Id,
		  'CObject ' + CAST(obj.Id AS nvarchar) ParentId, 
		 resOp.Id ResOP_Id, obj.Id Object_Id, obj.Address ObjectNameForResOp, obj.Manager_Id Manager_Id,
		 case when res.Manager_Id is null then 1 else 0 end IsStaff,
		  pos.Id PositionId, res.Id Resource_Id, res.Name Name, res.Phone Phone, 
		 pos.Name PositionName, okl.Summa Salary, res.OfficialSalary OfficialSalary, ResOP.RateDays RateDays, 
		 ResOP.FactDays FactDays, ResOP.FactSalary FactSalary, ResOP.Avans Avans, 
		 ResOP.Penalty Penalty, ResOP.Premium Premium, ResOP.Comment Comment, ResOP.Month Month, 
		 res.MedBook MedBook, res.SpecWear SpecWear
	 from (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp
		 left join CObject obj on resOp.Object_Id = obj.Id
		 left join Project pr on obj.Project_Id = pr.Id
		 left join Position pos on pos.Id = resOp.Position_Id
		 left join Oklad okl on okl.Object_Id = obj.Id and okl.Position_Id = pos.Id
		 left join Resource res on res.Id = resOp.Resource_Id
	 where pr.Rukovod_Id = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
)
select * from projects
union all select * from objs
union all select * from resOps
order by Name";

			DataTable dt = G.db_select(sql, rukovodId, month.Year, month.Month);

			var res = dt.ToObjList(ResOPViewModel.Factory);
			return res;
		}

		private List<ResOPViewModel> GetResOPViewModelsForManager(int managerId, DateTime month)
		{
			var sql =
@"with objs as (
	 select 1 Type, 'CObject ' + CAST(obj.Id AS nvarchar) Id,
		 'CObject ' + CAST(obj.Id AS nvarchar) Group_Id, null ParentId, 
		 null ResOP_Id, obj.Id Object_Id, obj.Address ObjectNameForResOp, obj.Manager_Id Manager_Id,
		 0 IsStaff, null PositionId, null Resource_Id, obj.Address Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 null PositionName, SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary, SUM(ResOP.RateDays) RateDays, 
		 SUM(ResOP.FactDays) FactDays, SUM(ResOP.FactSalary) FactSalary, SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, 
		 SUM(ResOP.Premium) Premium, null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from CObject obj
		 left join Project pr on obj.Project_Id = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.Object_Id = obj.Id
		 left join Position pos on pos.Id = resOp.Position_Id
		 left join Resource res on res.Id = resOp.Resource_Id
		 left join Oklad okl on okl.Object_Id = obj.Id and okl.Position_Id = pos.Id
	 where obj.Manager_Id = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by obj.Id, obj.Address, obj.Manager_Id
),
resOps as (
		 select 2 Type, 'ResOp ' + CAST(resOp.Id AS nvarchar) Id,
		'ResOp ' + CAST(resOp.Id AS nvarchar) Group_Id,
		 'CObject ' + CAST(obj.Id AS nvarchar) ParentId, 
		 resOp.Id ResOP_Id, obj.Id Object_Id, obj.Address ObjectNameForResOp, obj.Manager_Id Manager_Id,
		 case when res.Manager_Id is null then 1 else 0 end IsStaff,
		  pos.Id PositionId, res.Id Resource_Id, res.Name Name, res.Phone Phone, 
		 pos.Name PositionName, okl.Summa Salary, res.OfficialSalary OfficialSalary, ResOP.RateDays RateDays, 
		 ResOP.FactDays FactDays, ResOP.FactSalary FactSalary, ResOP.Avans Avans, 
		 ResOP.Penalty Penalty, ResOP.Premium Premium, ResOP.Comment Comment, ResOP.Month Month, 
		 res.MedBook MedBook, res.SpecWear SpecWear
	 from (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp
		 left join CObject obj on resOp.Object_Id = obj.Id
		 left join Position pos on pos.Id = resOp.Position_Id
		 left join Oklad okl on okl.Object_Id = obj.Id and okl.Position_Id = pos.Id
		 left join Resource res on res.Id = resOp.Resource_Id
	 where obj.Manager_Id = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
)
select * from objs
union all select * from resOps
order by Name";
			DataTable dt = G.db_select(sql, managerId, month.Year, month.Month);

			var res = dt.ToObjList(ResOPViewModel.Factory);
			return res;
		}

		public DataTable GetVedomost(UserDb user, int objectId = 0)
		{
			return G.db_select("GetVedomost {1}, {2}", user.Id, objectId);
		}
	}
}
