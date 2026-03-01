using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plausibility.API.Models;

[Table("tcd_EmployeesRangeNumber", Schema = "plausibility")]
public class EmployeesRangeNumber
{
    [Key]
    public int EmployeesRangeNumberID { get; set; }
    public int LifeCycleStateID { get; set; }
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;

    public ICollection<EmployeesRangeNumberTranslation> Translations { get; set; } = new List<EmployeesRangeNumberTranslation>();}
