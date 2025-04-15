using System;
using Microsoft.EntityFrameworkCore;
using MMC.Api.Data;
using MMC.Api.Dtos;
using MMC.Api.Entities;
using MMC.Api.Mapping;

namespace MMC.Api.Endpoints;

public static class UsersEndPoints
{
    const string GetUserEndpointName = "GetUser";

    public static RouteGroupBuilder MapUsersEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("users")
                                .WithParameterValidation();

        // Get /users
        group.MapGet("/", async (UserContext dbContext) =>
            await dbContext.Users
                        .Select(user => user.ToDto())
                        .AsNoTracking()
                        .ToListAsync());

        // Get /users/1
        group.MapGet("/{id}", async (int id, UserContext dbContext) =>
        {
            User? user = await dbContext.Users.FindAsync(id);

            return user is null ? Results.NotFound() : Results.Ok(user.ToDto());
        })
        .WithName(GetUserEndpointName);


        // Post /users
        group.MapPost("/", async (CreateUserDto newUser, UserContext dbContext) =>
        {
            User user = newUser.ToEntity();

            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Results.CreatedAtRoute(
                GetUserEndpointName,
                new { id = user.Id }, user.ToDto());
        });

        // PUT /users/1
        group.MapPut("/{id}", async (int id, UpdateUserDto updatedUser, UserContext dbContext) =>
        {
            var existingUser = await dbContext.Users.FindAsync(id);

            if (existingUser is null)
            {
                return Results.NotFound();
            }

            dbContext.Entry(existingUser)
                    .CurrentValues
                    .SetValues(updatedUser.ToEntity(id));

            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        });

        // DELETE /users/1
        group.MapDelete("/{id}", async (int id, UserContext dbContext) =>
        {
            await dbContext.Users.Where(user => user.Id == id)
                            .ExecuteDeleteAsync();

            return Results.NoContent();
        });

        return group;
    }
}
