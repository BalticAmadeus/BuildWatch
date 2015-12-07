using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BalticAmadeus.BuildServer.Interfaces;
using NHibernate;
using NHibernate.Linq;

namespace BalticAmadeus.BuildServer.Controllers
{
	public class BuildsController : ApiController
	{
		private readonly ISession _session;

		public BuildsController(ISession session)
		{
			_session = session;
		}

		[Route("api/builds/createBuild")]
		[HttpPost]
		public async Task<IHttpActionResult> CreateBuild([FromBody] CreateBuildData data)
		{
			await Task.Delay(1);

			using (var tx = _session.BeginTransaction())
			{
				var build = new Build(data.Title);

				_session.Save(build);

				tx.Commit();
			}

			return Ok();
		}

		[Route("api/builds/renameBuild")]
		[HttpPost]
		public async Task<IHttpActionResult> RenameBuild([FromBody] RenameBuildData data)
		{
			await Task.Delay(1);

			using (var tx = _session.BeginTransaction())
			{
				var build = _session.Load<Build>(data.BuildId);
				build.Rename(data.Title);

				tx.Commit();
			}
			
			return Ok();
		}

		[Route("api/builds/deleteBuild")]
		[HttpPost]
		public async Task<IHttpActionResult> DeleteBuild([FromBody] DeleteBuildData data)
		{
			await Task.Delay(1);

			using (var tx = _session.BeginTransaction())
			{
				var build = _session.Load<Build>(data.BuildId);
				build.Delete();

				tx.Commit();
			}

			return Ok();
		}

		[Route("api/builds/addBuildRun")]
		[HttpPost]
		public async Task<IHttpActionResult> AddBuildRun([FromBody] AddBuildRunData data)
		{
			await Task.Delay(1);

			using (var tx = _session.BeginTransaction())
			{
				var build = _session.Load<Build>(data.BuildId);
				var buildRun = new BuildRun(build.Id, data.Instance, data.TimeStamp, data.Username);
				build.AddBuild(buildRun);

				tx.Commit();
			}

			return Ok();
		}

		[Route("api/builds/finishBuildRun")]
		[HttpPost]
		public async Task<IHttpActionResult> FinishBuildRun([FromBody] MarkBuildRunFinishedData data)
		{
			await Task.Delay(1);

			using (var tx = _session.BeginTransaction())
			{
				var build = _session.Load<Build>(data.BuildId);
				var buildRun = build.BuildRuns.Single(x => x.Id == data.BuildRunId);
				buildRun.Finish(data.TimeStamp, (BuildRunStatus) data.Status);

				tx.Commit();
			}

			return Ok();
		}
	}

	public class CreateBuildData
	{
		public string Title { get; set; }
	}

	public class RenameBuildData
	{
		public int BuildId { get; set; }
		public string Title { get; set; }
	}

	public class DeleteBuildData
	{
		public int BuildId { get; set; }
	}

	public class AddBuildRunData
	{
		public int BuildId { get; set; }
		public string Instance { get; set; }
		public DateTime TimeStamp { get; set; }
		public string Username { get; set; }
	}

	public class MarkBuildRunFinishedData
	{
		public int BuildId { get; set; }
		public int BuildRunId { get; set; }
		public int Status { get; set; }
		public DateTime TimeStamp { get; set; }
	}
}
