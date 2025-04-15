using System;
using Microsoft.EntityFrameworkCore;
using MMC.Api.Data;
using MMC.Api.Dtos;
using MMC.Api.Entities;
using MMC.Api.Mapping;

namespace MMC.Api.Endpoints;

public static class PaymentsEndPoints
{
    const string GetPaymentEndPointName = "Getpayment";

    public static RouteGroupBuilder MapPaymentsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/payments").WithParameterValidation();

        // GET /payments
        group.MapGet("/", async (UserContext dbContext) =>
            await dbContext.Payments.Include(payment => payment.User)
                                    .Select(payment => payment.ToPaymentsDto())
                                    .AsNoTracking()
                                    .ToListAsync())
                                    .WithName(GetPaymentEndPointName); ;

        //GET /payments/1
        group.MapGet("/{userId}/", async (int userId, UserContext dbContext) =>
        {
            var payments = await dbContext.Payments.Include(payment => payment.User)
                                    .Where(payment => payment.UserId == userId)
                                    .ToListAsync();

            var paymentDtos = payments.Select(p => p.ToUserPaymentsDto()).ToList();

            var totalAmount = payments.Sum(p => p.Amount);
            if (!paymentDtos.Any())
            {
                Results.NotFound();
            }
            return Results.Ok(paymentDtos);
        }
        );

        //GET /payments/1/total
        group.MapGet("/{userId}/total", async (int userId, UserContext dbContext) =>
        {
            var totalAmount = await dbContext.Payments
                                    .Where(payment => payment.UserId == userId)
                                    .SumAsync(payment => payment.Amount);

            return totalAmount;
        }
        );

        //POST /payments
        group.MapPost("/", async (CreatePaymentDto newPayment, UserContext dbContext) =>
        {
            Payment payment = newPayment.ToEntity();

            dbContext.Payments.Add(payment);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetPaymentEndPointName,
                new { id = payment.Pid },
                payment.ToPaymentsDto()
            );

        });

        //PUT /payments/1
        group.MapPut("/{pId}", async (int pId, CreatePaymentDto updatedPayment, UserContext dbContext) =>
        {
            var existingPayment = await dbContext.Payments.FindAsync(pId);

            if(existingPayment == null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingPayment)
                     .CurrentValues
                     .SetValues(updatedPayment.ToEntity(pId));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        //DELETE /payments/id
        group.MapDelete("/{pId}", async (int pId, UserContext dbContext) =>
        {
            await dbContext.Payments
                            .Where(p => p.Pid == pId)
                            .ExecuteDeleteAsync();

            return Results.NoContent();

        });

        return group;
    }
}
