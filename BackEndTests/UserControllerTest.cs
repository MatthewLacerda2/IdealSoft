using BackEnd.Controllers;
using BackEnd.Data;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
            Assert.Equal(user.name, deserializedUser.name);
            Assert.Equal(user.surname, deserializedUser.surname);
            Assert.Equal(user.telephone, deserializedUser.telephone);
        }

        [Fact]
        public async Task PostUser_ReturnsBadRequest_WhenIdAlreadyExists() {
            // Arrange
            int existingId = 74;

            var controller = new UserController(_context);
            var existingUser = new User { Id = existingId, name = "ExistingName", surname = "ExistingSurname", telephone = "987654321" };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var newUserWithExistingId = new User { Id = existingId, name = "NewName", surname = "NewSurname", telephone = "123456789" };

            // Act
            var result = await controller.PostUser(newUserWithExistingId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Id already taken!", badRequestResult.Value);
        }

        [Fact]
        public async Task PostUser_ReturnsCreatedAtAction_WhenUserFound() {
            // Arrange
            var controller = new UserController(_context);
            var newUser = new User { Id = 95, name = "TestName", surname = "TestSurname", telephone = "123456789" };

            // Act
            var result = await controller.PostUser(newUser);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var createdUser = Assert.IsType<User>(createdAtActionResult.Value);
            Assert.Equal(newUser.Id, createdUser.Id);
            Assert.Equal(newUser.name, createdUser.name);
            Assert.Equal(newUser.surname, createdUser.surname);
            Assert.Equal(newUser.telephone, createdUser.telephone);
        }

        [Fact]
        public async Task PatchUser_ReturnsOk_WhenIdExists() {
            // Arrange
            int existingId = 111;
            var controller = new UserController(_context);
            var existingUser = new User { Id = existingId, name = "ExistingName", surname = "ExistingSurname", telephone = "987654321" };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            var updatedUserData = new User { Id = existingId, name = "UpdatedName", surname = "UpdatedSurname", telephone = "123456789" };

            // Act
            var result = await controller.PatchUser(updatedUserData);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedUser = Assert.IsType<User>(okResult.Value);
            Assert.Equal("UpdatedName", updatedUser.name);
            Assert.Equal("UpdatedSurname", updatedUser.surname);
            Assert.Equal("123456789", updatedUser.telephone);
        }

        [Fact]
        public async Task PatchUser_ReturnsBadRequest_WhenIdNotFound() {
            // Arrange
            var controller = new UserController(_context);
            var updatedUserData = new User { Id = 134, name = "UpdatedName", surname = "UpdatedSurname", telephone = "123456789" };

            // Act
            var result = await controller.PatchUser(updatedUserData);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User Id not found!", badRequestResult.Value);
        }



        [Fact]
        public async Task DeleteUser_ReturnsNoContent_WhenIdExists() {
            // Arrange
            int existingId = 115;

            var controller = new UserController(_context);
            var existingUser = new User { Id = existingId, name = "ExistingName", surname = "ExistingSurname", telephone = "987654321" };
            await _context.Users.AddAsync(existingUser);
            await _context.SaveChangesAsync();

            // Act
            var result = await controller.DeleteUser(existingId);

            // Assert
            Assert.IsType<NoContentResult>(result);

            var deletedUser = await _context.Users.FindAsync(existingId);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task DeleteUser_ReturnsBadRequest_WhenIdNotFound() {
            // Arrange
            var controller = new UserController(_context);

            // Act
            var result = await controller.DeleteUser(172);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("User Id not found!", badRequestResult.Value);
        }
    }
}
