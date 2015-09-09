using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using BuildWatch.DataSource.Common;
using BuildWatch.DataSource.Common.DataService;

namespace DataSource.TC
{
	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
	public partial class builds
	{

		private buildsBuild[] buildField;

		private byte countField;

		private string hrefField;

		private string nextHrefField;

		/// <remarks/>
		[System.Xml.Serialization.XmlElementAttribute("build")]
		public buildsBuild[] build
		{
			get
			{
				return this.buildField;
			}
			set
			{
				this.buildField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public byte count
		{
			get
			{
				return this.countField;
			}
			set
			{
				this.countField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string nextHref
		{
			get
			{
				return this.nextHrefField;
			}
			set
			{
				this.nextHrefField = value;
			}
		}
	}

	/// <remarks/>
	[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
	public partial class buildsBuild
	{

		private ushort idField;

		private string buildTypeIdField;

		private ushort numberField;

		private string statusField;

		private string stateField;

		private string hrefField;

		private string webUrlField;

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ushort id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string buildTypeId
		{
			get
			{
				return this.buildTypeIdField;
			}
			set
			{
				this.buildTypeIdField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public ushort number
		{
			get
			{
				return this.numberField;
			}
			set
			{
				this.numberField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string state
		{
			get
			{
				return this.stateField;
			}
			set
			{
				this.stateField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string href
		{
			get
			{
				return this.hrefField;
			}
			set
			{
				this.hrefField = value;
			}
		}

		/// <remarks/>
		[System.Xml.Serialization.XmlAttributeAttribute()]
		public string webUrl
		{
			get
			{
				return this.webUrlField;
			}
			set
			{
				this.webUrlField = value;
			}
		}
	}

	public class TCDataSource : DataSourceBase
    {
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(TCDataSource));

		public string Password { get; set; }
		public string UserName { get; set; }
		public string BaseUrl { get; set; }

		public override void Poll(IDataService dataService, CancellationToken quitToken)
		{
			//TODO: pull logging up?
			log.Debug("Start polling");

			var httpClient = new HttpClient();

			var auth = Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}", UserName, Password)));
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);

			log.Debug("Retrieving queued builds...");
			var queuedBuilds = GetQueuedBuilds(httpClient);

			log.Debug("Retrieving build information...");
			List<FinishedBuildInfo> builds = GetFinishedBuilds(httpClient);

			log.Debug("Pushing upstream");
			var req = new PushFinishedBuildsRequest
			{
				DataSourceId = 2, // FIXME
				BuildInfo = builds,
				QueuedBuilds = queuedBuilds
			};

			dataService.PushFinishedBuilds(req);

			log.Debug("Polling complete");
		}

		private List<FinishedBuildInfo> GetFinishedBuilds(HttpClient httpClient)
		{
			var stringAsync = httpClient.GetStringAsync(BaseUrl + "/builds");

			var buildObj = (builds)new XmlSerializer(typeof(builds)).Deserialize(new StringReader(stringAsync.Result));

			List<FinishedBuildInfo> finishedBuilds = new List<FinishedBuildInfo>();

			foreach (var build in buildObj.build.OrderBy(x => x.number))
			{
				if(finishedBuilds.Any(x => x.BuildName == build.buildTypeId))
					continue;

				var bi = new FinishedBuildInfo
				{
					BuildInstance = build.number.ToString(),
					BuildName = build.buildTypeId,
					TimeStamp = DateTime.Now,
					Result = string.Compare(build.status, "SUCCESS", StringComparison.InvariantCultureIgnoreCase) == 0 ? "OK" : "FAIL",
					UserName = "TODO",
					UserAction = "TODO"
				};

				finishedBuilds.Add(bi);
			}

			return finishedBuilds;
		}

		private List<QueuedBuildInfo> GetQueuedBuilds(HttpClient httpClient)
		{
			var stringAsync = httpClient.GetStringAsync(BaseUrl + "/builds?locator=running:true");

			var buildObj = (builds) new XmlSerializer(typeof (builds)).Deserialize(new StringReader(stringAsync.Result));

			List<QueuedBuildInfo> queuedBuilds = new List<QueuedBuildInfo>();

			if (buildObj.build != null)
			{
				foreach (var build in buildObj.build)
				{
					var qbi = new QueuedBuildInfo
					{
						BuildName = build.buildTypeId,
						QueueTime = DateTime.Now
					};

					queuedBuilds.Add(qbi);
				}
			}

			return queuedBuilds;
		}

		public override void Initialize(DataSourceConfig config)
		{
			base.Initialize(config);

			BaseUrl = config["TCBaseUrl"];
			UserName = config["TCUserName"];
			Password = config["TCPassword"];
		}
    }
}
