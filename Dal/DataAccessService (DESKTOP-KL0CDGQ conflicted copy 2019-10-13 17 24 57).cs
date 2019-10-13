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
using LinqToDB;

namespace Furmanov.Dal
{
	#region IdataAccessService
	public interface IdataAccessService
	{
		LoginPassword GetLoginPassword();
		void SaveLoginPassword(LoginPassword loginPassword);
		UserDb GetUser(string login, string password);
		List<CObject> GetObjects(UserDb user);
		List<Position> GetPosition(int objectId);
		List<SalaryPayDb> GetResOps(int[] objectsId, DateTime month);
		List<Employee> GetResources(SelectionResourceMode selectionMode, params int[] objectsId);
		List<CTabel> GetTabels(int? resOpId, DateTime month);

		void SaveResource(Employee resource);
		void DeleteResource(Employee resource);
		void SaveSalaryPay(SalaryPayDb salaryPayDb);
		void DeleteResOp(int resOpId);
		void DeleteResOp(SalaryPayDb r);

		void SaveTabels(params CTabel[] tabels);
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

		public void SaveLoginPassword(LoginPassword loginPassword)
		{
			var file = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				"SC.Tabel",
				"LoginPassword.xml");
			new XmlDataContractRepository<LoginPassword>(file).Save(loginPassword);
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

		public void SaveSalaryPay(SalaryPayDb salaryPayDb)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				db.Update(salaryPayDb);
			}
		}
		public void DeleteSalaryPay(SalaryPayDb salaryPayDb)
		{
			using (var db = new DbDataContext(_connectionString))
			{
				db.Delete(salaryPayDb);
			}
		}

		public List<CTabel> GetTabels(int? resOpId, DateTime month)
		{
			if (resOpId == null) return new List<CTabel>();

			var sql = @"select *
						from Tabel
						Where ResOPId = {1}
						and YEAR(Date) = {2}
						and Month(Date) = {3}";

			DataTable dt = G.db_select(sql, resOpId, month.Year, month.Month);

			var res = dt.ToObjList(CTabel.Factory);
			return res;
		}
		public bool SaveTabels(params CTabel[] tabels)
		{
			
		}

		public List<SalaryPayView> GetResOPViewModels(UserView user, DateTime month)
		{
			if (user == null) return new List<ResOPViewModel>();
			if (user.Role == Role.ProjectManager) return GetResOPViewModelsForRukovod(user.Id, month);
			if (user.Role == Role.Manager) return GetResOPViewModelsForManager(user.Id, month);
			throw new ArgumentException("Нет прав доступа");
		}

		private List<SalaryPayView> GetResOPViewModelsForRukovod(int rukovodId, DateTime month)
		{
			var sql =
@"with projects as(
	 select 0 Type,
		 'Project ' + CAST(pr.Id AS nvarchar) Id,
		 'Project ' + CAST(pr.Id AS nvarchar) GroupId,
		 null ParentId, null ResOPId, null ObjectId,
		 null ObjectNameForResOp, null ManagerId, 0 IsStaff,
		 null PositionId, null EmployeeId, pr.Name Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 'Объектов: ' + CAST(Count(obj.Id) AS nvarchar) PositionName, 
		 SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary,
		 SUM(ResOP.RateDays) RateDays, SUM(ResOP.FactDays) FactDays, SUM(ResOP.SalaryPaid) SalaryPaid, 
		 SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, SUM(ResOP.Premium) Premium,
		 null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from Project pr
		 left join CObject obj on obj.ProjectId = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.ObjectId = obj.Id
		 left join Resource res on res.Id = resOp.EmployeeId
		 left join Position pos on pos.Id = resOp.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.RukovodId = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by pr.Id, pr.Name
),
objs as (
	 select 1 Type, 'CObject ' + CAST(obj.Id AS nvarchar) Id,
		 'CObject ' + CAST(obj.Id AS nvarchar) GroupId,
		  'Project ' + CAST(pr.Id AS nvarchar) ParentId, 
		 null ResOPId, obj.Id ObjectId, obj.Address ObjectNameForResOp, obj.ManagerId ManagerId,
		 0 IsStaff, null PositionId, null EmployeeId, obj.Address Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 null PositionName, SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary, SUM(ResOP.RateDays) RateDays, 
		 SUM(ResOP.FactDays) FactDays, SUM(ResOP.SalaryPaid) SalaryPaid, SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, 
		 SUM(ResOP.Premium) Premium, null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from CObject obj
		 left join Project pr on obj.ProjectId = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.ObjectId = obj.Id
		 left join Resource res on res.Id = resOp.EmployeeId
		 left join Position pos on pos.Id = resOp.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where pr.RukovodId = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by pr.Id, obj.Id, obj.Address, obj.ManagerId
),
resOps as (
	 select 2 Type, 'ResOp ' + CAST(resOp.Id AS nvarchar) Id,
		 'ResOp ' + CAST(resOp.Id AS nvarchar) GroupId,
		  'CObject ' + CAST(obj.Id AS nvarchar) ParentId, 
		 resOp.Id ResOPId, obj.Id ObjectId, obj.Address ObjectNameForResOp, obj.ManagerId ManagerId,
		 case when res.ManagerId is null then 1 else 0 end IsStaff,
		  pos.Id PositionId, res.Id EmployeeId, res.Name Name, res.Phone Phone, 
		 pos.Name PositionName, okl.Summa Salary, res.OfficialSalary OfficialSalary, ResOP.RateDays RateDays, 
		 ResOP.FactDays FactDays, ResOP.SalaryPaid SalaryPaid, ResOP.Avans Avans, 
		 ResOP.Penalty Penalty, ResOP.Premium Premium, ResOP.Comment Comment, ResOP.Month Month, 
		 res.MedBook MedBook, res.SpecWear SpecWear
	 from (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp
		 left join CObject obj on resOp.ObjectId = obj.Id
		 left join Project pr on obj.ProjectId = pr.Id
		 left join Position pos on pos.Id = resOp.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
		 left join Resource res on res.Id = resOp.EmployeeId
	 where pr.RukovodId = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
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
		 'CObject ' + CAST(obj.Id AS nvarchar) GroupId, null ParentId, 
		 null ResOPId, obj.Id ObjectId, obj.Address ObjectNameForResOp, obj.ManagerId ManagerId,
		 0 IsStaff, null PositionId, null EmployeeId, obj.Address Name,
		 'Выплат: ' + CAST(Count(resOp.Id) AS nvarchar) Phone,
		 null PositionName, SUM(okl.Summa) Salary, SUM(res.OfficialSalary) OfficialSalary, SUM(ResOP.RateDays) RateDays, 
		 SUM(ResOP.FactDays) FactDays, SUM(ResOP.SalaryPaid) SalaryPaid, SUM(ResOP.Avans) Avans, SUM(ResOP.Penalty) Penalty, 
		 SUM(ResOP.Premium) Premium, null Comment, null Month, SUM(res.MedBook) MedBook, SUM(res.SpecWear) SpecWear
	 from CObject obj
		 left join Project pr on obj.ProjectId = pr.Id
		 left join (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp on resOp.ObjectId = obj.Id
		 left join Position pos on pos.Id = resOp.PositionId
		 left join Resource res on res.Id = resOp.EmployeeId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
	 where obj.ManagerId = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
	 group by obj.Id, obj.Address, obj.ManagerId
),
resOps as (
		 select 2 Type, 'ResOp ' + CAST(resOp.Id AS nvarchar) Id,
		'ResOp ' + CAST(resOp.Id AS nvarchar) GroupId,
		 'CObject ' + CAST(obj.Id AS nvarchar) ParentId, 
		 resOp.Id ResOPId, obj.Id ObjectId, obj.Address ObjectNameForResOp, obj.ManagerId ManagerId,
		 case when res.ManagerId is null then 1 else 0 end IsStaff,
		  pos.Id PositionId, res.Id EmployeeId, res.Name Name, res.Phone Phone, 
		 pos.Name PositionName, okl.Summa Salary, res.OfficialSalary OfficialSalary, ResOP.RateDays RateDays, 
		 ResOP.FactDays FactDays, ResOP.SalaryPaid SalaryPaid, ResOP.Avans Avans, 
		 ResOP.Penalty Penalty, ResOP.Premium Premium, ResOP.Comment Comment, ResOP.Month Month, 
		 res.MedBook MedBook, res.SpecWear SpecWear
	 from (select * from ResOP where Month(ResOP.Month) = {3} and YEAR(ResOP.Month) = {2}) resOp
		 left join CObject obj on resOp.ObjectId = obj.Id
		 left join Position pos on pos.Id = resOp.PositionId
		 left join Oklad okl on okl.ObjectId = obj.Id and okl.PositionId = pos.Id
		 left join Resource res on res.Id = resOp.EmployeeId
	 where obj.ManagerId = {1} and pr.IsDeleted = 0 and obj.IsDeleted = 0
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
