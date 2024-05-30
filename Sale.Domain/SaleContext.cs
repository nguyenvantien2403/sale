using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sale.Domain
{
	public class SaleContext : IdentityDbContext<IdentityUser>
	{


		private readonly IHttpContextAccessor _httpContextAccessor;
		public SaleContext(DbContextOptions<SaleContext> contexts, IHttpContextAccessor httpContextAccessor) : base(contexts)
		{
			_httpContextAccessor = httpContextAccessor;
		}
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			//this.SeedRoles(builder);
		}

		private void SeedRoles(ModelBuilder builder)
		{
			builder.Entity<IdentityRole>().HasData(
					new IdentityRole() { Name = "Admin", NormalizedName = "Admin", ConcurrencyStamp = "1" },
					new IdentityRole() { Name = "User", NormalizedName = "User", ConcurrencyStamp = "2" }
					);
		}


		public DbSet<Product> Products { get; set; }
		public DbSet<Branch> Branchs { get; set; }
		public DbSet<Origin> Origin { get; set; }
		public DbSet<FileImage> fileImages { get; set; }
		public DbSet<Orders> Orders { get; set; }
		public DbSet<Cart> Carts { get; set; }
		public DbSet<Promotion> Promotions { get ;set;}
		public DbSet<Rate> Rates { get ;set;}
		public DbSet<Comments> Comments { get ;set;}

		public DbSet<NotifiCation> NotifiCations { get ;set; }

	}
}
