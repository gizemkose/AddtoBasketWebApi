using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Entities
{
    public class ApiContext : DbContext
    { public ApiContext(DbContextOptions<ApiContext> options)
    : base(options)
        {
        
        }

        public DbSet<Basket> Basket { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyItem> CompanyItem { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<User> User { get; set; }

        public void LoadTestDBData(int? loopIterator)
        {
            for (int i = 1; i <= loopIterator; i++)
            {
                var item = new Item
                {
                    Id = i,
                    Code = i + 1000,
                    Name = $"Oje{i}"
                };
                _ = Item.Add(item);
                SaveChanges();

                var user = new User
                {
                    Id = i,
                    Name = $"Gizem KipTest{i}"
                };
                _ = User.Add(user);
                SaveChanges();

                var company = new Company
                {
                    Id = i,
                    Name = $"KozmetikFirması{i}",
                    Address = $"Bağlıca{i}",
                    OtherInfo = $"No info"
                };
                _ = Company.Add(company);
                SaveChanges();

                //var basket = new Basket
                //{
                //    Id = i,
                //    ItemId = i,
                //    ItemCount = 0,
                //    UserId = i
                //};
                //_ = Basket.Add(basket);
                //SaveChanges();

                var companyItem = new CompanyItem
                {
                    Id = i,
                    ItemId = i,
                    CompanyId = i,
                    Stock = i,
                    Price = i

                };
                _ = CompanyItem.Add(companyItem);
                SaveChanges();

            }
        }
        public void LoadUserBasketTestData(int? loopIterator)
        {
            for (int i = 1; i <= loopIterator; i++)
            {
                var item = new Item
                {
                    Id = i,
                    Code = i + 1000,
                    Name = $"Oje{i}"
                };
                _ = Item.Add(item);
                SaveChanges();

                var user = new User
                {
                    Id = i,
                    Name = $"Gizem KipTest{i}"
                };
                _ = User.Add(user);
                SaveChanges();

                var company = new Company
                {
                    Id = i,
                    Name = $"KozmetikFirması{i}",
                    Address = $"Bağlıca{i}",
                    OtherInfo = $"No info"
                };
                _ = Company.Add(company);
                SaveChanges();

                var basket = new Basket
                {
                    Id = i,
                    ItemId = i,
                    ItemCount = i,
                    UserId = i
                };
                _ = Basket.Add(basket);
                SaveChanges();

                var companyItem = new CompanyItem
                {
                    Id = i,
                    ItemId = i,
                    CompanyId = i,
                    Stock = i,
                    Price = i

                };
                _ = CompanyItem.Add(companyItem);
                SaveChanges();

            }
        }
    }
}
