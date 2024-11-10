using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactApp.Server.Models;

public partial class Personinfo
{
    [Key]
    public int No { get; set; }

    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Note { get; set; }
}
