using System.ComponentModel.DataAnnotations;

namespace EternitySagaAPI.Models;

public record UserDto(string id, string nickname, UInt16 level, uint exp);
public record CreateUserDto([Required] string id, [Required] string nickname);
public record UpdateUserDto([Required]string nickname, [Required]UInt16 level, [Required]uint exp);