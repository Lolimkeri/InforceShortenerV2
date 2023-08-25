using InforceShortener.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace InforceShortener.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UrlRecord> UrlRecords { get; set; }
        public DbSet<WebContent> WebContents { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
            //AddMockedData();
        }

        private void AddMockedData()
        {
            Users.Add(new User()
            {
                Id = 1,
                Username = "hello1",
                Password = "hello1",
                Role = "User"
            });

            Users.Add(new User()
            {
                Id = 2,
                Username = "hello",
                Password = "hello",
                Role = "Admin"
            });

            SaveChanges();

            UrlRecords.Add(new UrlRecord()
            {
                Id = 1,
                OriginalUrl = "originalOne",
                ShortUrl = "short",
                CreatedDate = new DateTime(2021, 10, 24),
                User = Users.First()
            });

            WebContents.Add(new WebContent()
            {
                Id = 1,
                TextName = "About",
                TextValue = "This is my about page",
                ChangedBy = Users.First()
            });

            SaveChanges();
        }
    }
}
