using System;
using MMC.Api.Dtos;
using MMC.Api.Entities;

namespace MMC.Api.Mapping;

public static class PaymentMapping
{
    public static PaymentsDto ToPaymentsDto(this Payment payment)
    {
        return new PaymentsDto(
            payment.Pid,
            payment.UserId,
            payment.Amount,
            payment.PaidDate);
    }

    public static Payment ToEntity(this CreatePaymentDto payment)
    {
        return new Payment()
        {
            UserId = payment.UserId,
            Amount = payment.Amount,
            PaidDate = payment.PaidDate
        };
    }

    public static UserPaymentsDto ToUserPaymentsDto(this Payment payment)
    {
        return new UserPaymentsDto(
            payment.Pid,
            payment.Amount,
            payment.PaidDate);
    }

    public static Payment ToEntity(this CreatePaymentDto payment, int id)
    {
        return new Payment()
        {
            Pid = id,
            UserId = payment.UserId,
            Amount = payment.Amount,
            PaidDate = payment.PaidDate
        };
    }
}
