using System.Collections.Generic;

namespace BalticAmadeus.BuildServer.Domain.Model.Teams
{
	public class Person
	{
		public int Id { get; protected set; }
		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }

		public bool IsDeleted { get; protected set; }

		public IEnumerable<string> UserIds => _userIds; 

		private readonly IList<string> _userIds = new List<string>();

		public Person(string firstName, string lastName)
		{
			FirstName = firstName;
			LastName = lastName;

			IsDeleted = false;
		}

		public Person()
		{
			//NHibernate
		}
	}
}