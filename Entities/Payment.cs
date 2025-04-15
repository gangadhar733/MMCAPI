using System;
using System.ComponentModel.DataAnnotations;

namespace MMC.Api.Entities;

public class Payment
{
    [Key]
    public int Pid { get; set; }

    public User? User { get; set; }

    public int UserId { get; set; }

    public required decimal Amount { get; set; }

    public required DateOnly PaidDate { get; set; }

}
