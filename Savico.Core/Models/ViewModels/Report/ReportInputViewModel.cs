namespace Savico.Core.Models.ViewModels.Report
{
    using System.ComponentModel.DataAnnotations;

    public class ReportInputViewModel
    {
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
