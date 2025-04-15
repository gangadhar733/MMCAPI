using System;
using MMC.Api.Dtos;
using MMC.Api.Entities;

namespace MMC.Api.Mapping;

public static class UserMapping
{
    public static User ToEntity(this CreateUserDto user)
    {
        return new User()
        {
            Name = user.Name
        };
    }

    public static UserDto ToDto(this User user)
    {
        return new(
            user.Id,
            user.Name
        );
    }

    public static User ToEntity(this UpdateUserDto user, int id)
    {
        return new User()
        {
            Id = id,
            Name = user.Name
        };
    }
}
