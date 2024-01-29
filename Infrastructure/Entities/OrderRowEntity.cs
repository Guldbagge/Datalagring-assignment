using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class OrderRowEntity
{
    [Key]
    public int OrderRowId { get; set; }
    public int OrderId { get; set; }
    public string ArticleNumber { get; set; } = null!;
    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    public virtual OrderEntity Order { get; set; } = null!;

    [ForeignKey("ArticleNumber")]
    public virtual Product Product { get; set; } = null!;
}
