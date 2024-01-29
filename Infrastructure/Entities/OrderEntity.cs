using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class OrderEntity
{
    [Key]
    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;

    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; } = null!;

    public virtual ICollection<OrderRowEntity> OrderRows { get; set; } = new List<OrderRowEntity>();
}
