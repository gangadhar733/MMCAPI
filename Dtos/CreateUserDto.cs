using System.ComponentModel.DataAnnotations;

namespace MMC.Api.Dtos;

public record class CreateUserDto(
    [Required, StringLength(50)] string Name
);
