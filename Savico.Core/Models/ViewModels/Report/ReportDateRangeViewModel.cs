namespace Savico.Core.Models.ViewModels.Report
{
    using System.ComponentModel.DataAnnotations;

    public class ReportDateRangeViewModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}
