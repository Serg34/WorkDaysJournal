using Furmanov.Data;
using Furmanov.Data.Data;
using Furmanov.Properties;
using Furmanov.Services;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Furmanov.Models
{
	public class MainModel : IDisposable
	{
		#region Fields
#if DEBUG
		private const int TimerInterval = 1_000;
#else
		private const int TimerInterval = 60_000;
#endif
		private readonly string _connectionString = Settings.Default.ConnectionString;
		private DateTime _lastBugDateTime = DateTime.MinValue;
		private DateTime _lastIncidentDateTime = DateTime.MinValue;
		private readonly List<BugDto> _newBugs = new List<BugDto>();
		private readonly List<BugIncidentDto> _newIncidents = new List<BugIncidentDto>();
		#endregion

		public event EventHandler<Bug[]> Updating;

		private bool _isDisposed;

		public void Start()
		{
			new Task(CheckBugs).Start();
			new Task(CheckIncidents).Start();
		}
		public void Update()
		{
			using (var db = new DbContext(_connectionString))
			{
				var bugs = db.Query<Bug>(Resources.Bugs).ToArray();
				if (bugs.Any())
				{
					_lastBugDateTime = bugs.Max(b => b.CreatedDate).AddSeconds(1);
					_lastIncidentDateTime = db.GetTable<BugIncidentDto>()
						.Max(i => i.DateTime).AddSeconds(1);
					foreach (var bug in bugs)
					{
						bug.IsNew = _newBugs.Any(b => b.Id == bug.Id);
						bug.HasNewIncident = _newIncidents.Any(i => i.Bug_Id == bug.Id);
					}
				}

				Updating?.Invoke(this, bugs);
			}
		}

		public void UpdateBug(Bug bug)
		{
			using (var db = new DbContext(_connectionString))
			{
				var bugDto = MapperService.Map<BugDto>(bug);
				db.Update(bugDto);
			}
		}

		public BugIncidentDto[] GetIncidents(int bugId)
		{
			using (var db = new DbContext(_connectionString))
			{
				var incidents = db.GetTable<BugIncidentDto>()
					.Where(i => i.Bug_Id == bugId)
					.ToArray();

				return incidents;
			}
		}
		public void DeleteNewBug(BugDto bug)
		{
			_newBugs.RemoveAll(b => b.Id == bug.Id);
		}
		public void DeleteNewIncident(int bugId)
		{
			_newIncidents.RemoveAll(i => i.Bug_Id == bugId);
		}

		private void CheckBugs()
		{
			using (var db = new DbContext(_connectionString))
			{
				while (!_isDisposed)
				{
					Thread.Sleep(TimerInterval);
					var newBugs = db.GetWhere<BugDto>(b => b.CreatedDate > _lastBugDateTime);
					if (newBugs.Any())
					{
						var needUpdating = false;
						foreach (var bug in newBugs)
						{
							if (!_newBugs.Contains(bug))
							{
								_newBugs.Add(bug);
								needUpdating = true;
							}
						}
						_lastBugDateTime = newBugs.Max(b => b.CreatedDate).AddSeconds(1);
						if (needUpdating) Update();
					}
				}
			}
		}
		private void CheckIncidents()
		{
			using (var db = new DbContext(_connectionString))
			{
				while (!_isDisposed)
				{
					Thread.Sleep(TimerInterval);
					var newIncidents = db.GetWhere<BugIncidentDto>(i => i.DateTime > _lastIncidentDateTime);
					if (newIncidents.Any())
					{
						var needUpdating = false;
						foreach (var incident in newIncidents)
						{
							if (!_newIncidents.Contains(incident))
							{
								_newIncidents.Add(incident);
								needUpdating = true;
							}
						}
						_lastIncidentDateTime = newIncidents.Max(i => i.DateTime).AddSeconds(1);
						if (needUpdating) Update();
					}
				}
			}
		}

		public void Dispose()
		{
			_isDisposed = true;
		}
	}
}
