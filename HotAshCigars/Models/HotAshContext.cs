namespace HotAshCigars.Controllers {
	using Models;
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class HotAshContext : DbContext {
		// Your context has been configured to use a 'HotAshContext' connection string from your application's 
		// configuration file (App.config or Web.config). By default, this connection string targets the 
		// 'HotAshCigars.Controllers.HotAshContext' database on your LocalDb instance. 
		// 
		// If you wish to target a different database and/or database provider, modify the 'HotAshContext' 
		// connection string in the application configuration file.
		public HotAshContext()
			: base("name=HotAshContext") {
		}

		// Add a DbSet for each entity type that you want to include in your model. For more information 
		// on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

		// public virtual DbSet<MyEntity> MyEntities { get; set; }
		public virtual DbSet<Cigar> Cigars { get; set; }
		public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
	}

	//public class MyEntity
	//{
	//    public int Id { get; set; }
	//    public string Name { get; set; }
	//}
}