using System.ComponentModel.DataAnnotations;

namespace HR.LeaveManagement.BlazorWASM.Models.LeaveTypes;

public class LeaveTypeViewModel
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "Default Number of Days")]
    public int DefaultDays { get; set; }
}
