using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.Models;

public class OrderModel : ObservableObject
{
    public String ArticleNumber { get; set; } = null!;
    public int UserId { get; set; }
    public int Quantity { get; set; }

}
