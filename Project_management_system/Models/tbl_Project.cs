#nullable disable
using Project_management_system.Helper;
using System.ComponentModel.DataAnnotations;

namespace Project_management_system.Models
{
    public class tbl_Project
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name is required.")]
        [StringLength(100, ErrorMessage = "Project name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Client name is required.")]
        [StringLength(100, ErrorMessage = "Client name cannot exceed 100 characters.")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [DataType(DataType.Date)]
        [DateGreaterThan("StartDate", ErrorMessage = "End date must be after the start date.")]
        public DateTime EndDate { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Budget must be a non-negative number.")]
        public double Budget { get; set; }
        [Required(ErrorMessage = "Project Status is required.")]
        public string ProjectStatus { get; set; }
    }
}
