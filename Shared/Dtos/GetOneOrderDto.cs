using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Dtos;

public class GetOneOrderDto
{
    //public String ArticleNumber { get; set; } = null!;
    //public string Username { get; set; } = null!;
    //public String Title { get; set; } = null!;
    //public int Price { get; set; }
    //public int Quantity { get; set; }

    public int OrderId { get; set; }
    public int UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public string ArticleNumber { get; set; }= null!;
    public int Quantity { get; set; } 

}
