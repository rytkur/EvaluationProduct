using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EvaluationProduct.Domain.Model
{
    [ExcludeFromCodeCoverage]
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        ////[Column(TypeName = "decimal(18, 2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        [Required]
        //[Column(TypeName = "decimal(18, 2)")]
        [RegularExpression(@"^\d+(\.\d{1,2})?$")]
        [Range(0, 9999999999999999.99)]
        public decimal Quantity { get; set; }
    }
}
