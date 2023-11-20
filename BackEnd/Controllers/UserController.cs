using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BackEnd.Data;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase {
    


    public UserController(UserManager<User> userManager) {
        
    }
}