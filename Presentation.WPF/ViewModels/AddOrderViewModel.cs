using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Presentation.WPF.Models;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Presentation.WPF.ViewModels;

public partial class AddOrderViewModel : ObservableObject
{

    private readonly IOrderService _orderService;

    [ObservableProperty]
    private OrderModel _form = new();

    public AddOrderViewModel(IOrderService orderService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
    }

    [RelayCommand]
    private async Task PlaceOrder()
    {
        try
        {
            var orderDto = OrderFactory.CreateOrderDto(Form.ArticleNumber, Form.UserId, Form.Quantity);

            LogOrderDtoInfo(orderDto);

            Form = new();

            Logger.Log("Before calling PlaceOrderAsync", "PlaceOrderViewModel.PlaceOrder()", LogTypes.Info);

            var result = await _orderService.PlaceOrderAsync(orderDto);

            Logger.Log($"After calling PlaceOrderAsync, result: {result}", "PlaceOrderViewModel.PlaceOrder()", LogTypes.Info);

            if (result)
            {
                Logger.Log("Order was placed successfully.", "PlaceOrderViewModel.PlaceOrder()", LogTypes.Info);
                MessageBox.Show("Order was placed successfully.");
            }
            else
            {
                Logger.Log("Order was not placed successfully.", "PlaceOrderViewModel.PlaceOrder()", LogTypes.Info);
                MessageBox.Show("Something went wrong.");
            }
        }
        catch (Exception ex)
        {
            Logger.Log(ex.Message, "PlaceOrderViewModel.PlaceOrder()", LogTypes.Error);
        }
    }

    private void LogOrderDtoInfo(OrderDto orderDto)
    {
        Logger.Log($"OrderDto Information: ArticleNumber={orderDto.ArticleNumber}, UserId={orderDto.UserId}, Quantity={orderDto.Quantity}", "PlaceOrderViewModel.PlaceOrder()", LogTypes.Info);
    }
}
