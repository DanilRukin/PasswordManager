using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PasswordManager.Data;
using PasswordManager.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.UnitTests.Data
{
    public class BaseEfTestFixture
    {
        protected string _connectionString = "Filename=:memory:";
        protected SqliteConnection _connection;

        protected PasswordManagerDbContext CreateDatabase()
        {
            _connection = new SqliteConnection(_connectionString);
            _connection.Open();
            var mockEventDispatcher = new Mock<IDomainEventDispatcher>();
            return new PasswordManagerDbContext(mockEventDispatcher.Object, CreateDbContextOptions());
        }

        protected void CloseDatabase(PasswordManagerDbContext context)
        {
            if (_connection != null)
            {
                if (context != null)
                {
                    context.Database.EnsureDeleted();
                    context.Dispose();
                }
                _connection.Close();
                _connection.Dispose();
            }
        }

        private DbContextOptions<PasswordManagerDbContext> CreateDbContextOptions()
        {

            var builder = new DbContextOptionsBuilder<PasswordManagerDbContext>();
            builder.UseSqlite(_connection);

            return builder.Options;
        }
    }
}
