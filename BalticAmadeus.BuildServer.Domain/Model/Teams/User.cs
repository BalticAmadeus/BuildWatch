namespace BalticAmadeus.BuildServer.Domain.Model.Teams
{
	public class User
	{
		public virtual int Id { get; protected set; }
		public virtual string Username { get; protected set; }
		public virtual string Password { get; protected set; }

		public virtual bool IsDeleted { get; protected set; }

		public User()
		{
			//NHibernate
		}
	}
}
