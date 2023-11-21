using BackEnd.Controllers;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xunit;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BackEndTests {

    public class UserControllerTest : IDisposable {
        private readonly BackendDbContext _context;
        private readonly UserController _controller;

        public UserControllerTest() {
            var connectionStringBuilder = new SqliteConnectionStringBuilder {
                DataSource = ":memory:"
            };
            var connection = new SqliteConnection(connectionStringBuilder.ToString());

            DbContextOptions<BackendDbContext> options = new DbContextOptionsBuilder<BackendDbContext>()
                .UseSqlite(connection)
                .Options;

            _context = new BackendDbContext(options);
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();

            _controller = new UserController(_context);
        }

        public void Dispose() {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task ReadClient_ReturnsNotFound_WhenUserNotFound() {
            // Arrange
            int userId = 43;
            // Act
            var result = _controller.ReadClient(userId);
            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ReadClient_ReturnsOkWithUser_WhenUserFound() {
            // Arrange
            var user = new User { Id = 53, name = "John", surname = "Doe", telephone = "123456789" };
            _context.Users.Add(user);
            _context.SaveChanges();

            // Act
            var result = _controller.ReadClient(user.Id);

            // Assert
            Assert.IsType<OkObjectResult>(result);

            var okResult = (OkObjectResult)result;
            var userJson = okResult.Value.ToString();
            var deserializedUser = JsonConvert.DeserializeObject<User>(userJson);

            Assert.Equal(user.Id, deserializedUser.Id);
        }
    }
}
