namespace MMC.Api.Dtos;

public record class PaymentsDto(
    int PId,
    int UserId,
    decimal Amount,
    DateOnly PaidDate
);
