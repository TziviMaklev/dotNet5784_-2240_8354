﻿
namespace BO;

public class Engineer
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Email { get; set; }
    public EngineerExperience Level { get; set; }
    public double Cost { get; set; }
    public override string ToString() => this.ToStringProperty();
}
