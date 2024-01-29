using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities;

public class UserTypeEntity
{
    [Key]
    public int UserTypeId { get; set; }
    public int UserId { get; set; }
    public string UserType { get; set; } = null!;

    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; } = null!;
}
