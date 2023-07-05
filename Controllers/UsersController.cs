using EternitySagaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EternitySagaAPI.Controllers;

[ApiController, Route("[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<UserDto> users = new()
    {
        new UserDto("0x001", "Garamm", 2, 200),
        new UserDto("0x002", "Alrich", 5, 900),
        new UserDto("0x003", "Adev", 3, 250),
        new UserDto("0x004", "Ian", 3, 280),
    };
    private readonly ILogger<UsersController> _logger;
    public UsersController(ILogger<UsersController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetUsers"), Produces("Application/json")]
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public ActionResult<IEnumerable<UserDto>> GetAllUser()
    {
        return Ok(users);
    }

    [HttpGet("{id}", Name = "GetUser"), Produces("Application/json")]
    [ProducesResponseType(typeof(UserDto), 200), ProducesResponseType(typeof(NotFoundResult), 404)]
    public ActionResult<UserDto> GetUserById(string id)
    {
        var result = users.SingleOrDefault(user => user.id == id);
        return result != null ? Ok(result) : NotFound();
    }

    [HttpPost(Name = "AddUser"), Produces("Application/json")]
    [ProducesResponseType(typeof(UserDto), 201), ProducesResponseType(typeof(BadRequestResult), 400)]
    public ActionResult AddUser(CreateUserDto newUser)
    {
        var user = new UserDto(newUser.id, newUser.nickname, 1, 0);
        users.Add(user);
        return CreatedAtAction(nameof(GetUserById), new {id = user.id}, user);
    }

    [HttpPut("{id}"), Produces("Application/json")]
    [ProducesResponseType(typeof(UserDto), 200), ProducesResponseType(typeof(BadRequestResult), 400), ProducesResponseType(typeof(NotFoundResult), 404)]
    public ActionResult<UserDto> PutItem(string id, UpdateUserDto newUser)
    {
        var indexOf = users.FindIndex(x => x.id == id);
        if (indexOf < 0) return NotFound();
        var existingItem = users[indexOf];
        var updatedItem = new UserDto(existingItem.id, newUser.nickname, newUser.level, newUser.exp);
        users[indexOf] = updatedItem;
        return Ok(existingItem);
    }

    [HttpDelete("{id}"), Produces("Application/json")]
    [ProducesResponseType(typeof(NoContentResult), 204), ProducesResponseType(typeof(NotFoundResult), 404)]
    public ActionResult DeleteItem(string id)
    {
        var existingItem = users.FirstOrDefault(x => x.id == id);
        if (existingItem == null) return NotFound();
        users.Remove(existingItem);
        return NoContent();
    }
}