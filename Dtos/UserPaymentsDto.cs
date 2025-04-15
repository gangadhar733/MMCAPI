namespace MMC.Api.Dtos;

public record class UserPaymentsDto(
    int PId,
    decimal Amount,
    DateOnly PaidDate
);
