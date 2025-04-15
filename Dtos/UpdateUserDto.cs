using System.ComponentModel.DataAnnotations;

namespace MMC.Api.Dtos;

public record class UpdateUserDto(
    [Required, StringLength(50)]string Name);
