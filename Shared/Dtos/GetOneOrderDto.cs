using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos;

public class GetOneOrderDto
{

    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ArticleNumber { get; set; }= null!;
    public int Quantity { get; set; } 

}
