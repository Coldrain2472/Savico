namespace Savico.Infrastructure.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using static Savico.Infrastructure.Data.Constants.DataConstants.TipConstants;

    public class Tip
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string? Content { get; set; }
    }
}
