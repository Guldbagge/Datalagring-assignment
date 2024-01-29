using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class BankSwishDetailsEntity
{
    [Key]
    public int DetailsId { get; set; }
    public int UserId { get; set; }
    public string BankInformation { get; set; } = null!;
    public string SwishInformation { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; } = null!;
}
