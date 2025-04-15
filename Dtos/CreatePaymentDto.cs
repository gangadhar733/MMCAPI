using System.ComponentModel.DataAnnotations;

namespace MMC.Api.Dtos;

public record class CreatePaymentDto(
    int UserId,
    [Required] decimal Amount,
    [Required] DateOnly PaidDate
);
