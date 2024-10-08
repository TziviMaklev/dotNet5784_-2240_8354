﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class MilestoneInList
{
    string? Description { get; init; }
    string? Alias { get; init; }
    DateTime? CreateDate { get; set; }
    Status? Status { get; set; }
    float ProgressPercentage { get; set; }
    public override string ToString() => this.ToStringProperty();
}



