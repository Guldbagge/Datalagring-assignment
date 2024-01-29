using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos;

public class OrderDto
{
    public string ArticleNumber { get; set; } = null!;
    public int UserId { get; set; }
    public int Quantity { get; set; }

}
