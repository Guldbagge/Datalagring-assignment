using Business.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Shared.Dtos;
using Shared.Utils;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Presentation.WPF.ViewModels
{
    public class OrderRowViewModel : ObservableObject
    {
        private string _articleNumber = string.Empty;
        private int _quantity;
        private int _orderId;
        private int _userId;

        public int OrderId
        {
            get => _orderId;
            set => SetProperty(ref _orderId, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public string ArticleNumber
        {
            get => _articleNumber;
            set => SetProperty(ref _articleNumber, value);
        }

        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }

    }


    public class GetOneOrderViewModel : ObservableObject
    {
        private readonly IOrderService _orderService;
        private readonly IServiceProvider _serviceProvider;

        public GetOneOrderViewModel(IOrderService orderService, IServiceProvider serviceProvider)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            GetOrderCommand = new AsyncRelayCommand(GetOrderAsync);
            GoBackCommand = new RelayCommand(GoBack);

            OrderRows = new ObservableCollection<OrderRowViewModel>();
        }

        private int _orderId;
        private int _userId;
        private DateTime _orderDate;

        public int OrderId
        {
            get => _orderId;
            set => SetProperty(ref _orderId, value);
        }

        public int UserId
        {
            get => _userId;
            set => SetProperty(ref _userId, value);
        }

        public DateTime OrderDate
        {
            get => _orderDate;
            set => SetProperty(ref _orderDate, value);
        }

        public ObservableCollection<OrderRowViewModel> OrderRows { get; }
        public IAsyncRelayCommand GetOrderCommand { get; }
        public RelayCommand GoBackCommand { get; }

        private async Task GetOrderAsync()
        {
            try
            {
                if (OrderId <= 0)
                {
                    Logger.Log("Invalid order ID.", "GetOneOrderViewModel.GetOrderAsync()", LogTypes.Warning);
                    MessageBox.Show("Please provide a valid order ID.");
                    return;
                }

                var getOneOrderDto = new GetOneOrderDto { OrderId = OrderId };
                var order = await _orderService.GetOrderAsync(getOneOrderDto);

                if (order != null)
                {
                    UserId = order.UserId;
                    OrderDate = order.OrderDate;

                    OrderRows.Clear();
                    foreach (var orderRow in order.OrderRows)
                    {
                        if (orderRow != null)
                        {
                            OrderRows.Add(new OrderRowViewModel
                            {
                                OrderId = orderRow.OrderId,
                                UserId = order.UserId,
                                ArticleNumber = orderRow.ArticleNumber,
                                Quantity = orderRow.Quantity
                            });
                        }
                    }



                    Logger.Log($"Order with ID {OrderId} was retrieved successfully.", "GetOneOrderViewModel.GetOrderAsync()", LogTypes.Info);
                }
                else
                {
                    Logger.Log($"Order with ID {OrderId} was not found.", "GetOneOrderViewModel.GetOrderAsync()", LogTypes.Info);
                    MessageBox.Show($"Order with ID {OrderId} was not found.");
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message, "GetOneOrderViewModel.GetOrderAsync()", LogTypes.Error);
            }
        }

        private void GoBack()
        {
            var mainViewModel = _serviceProvider.GetService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetService<MainOptionsViewModel>();
        }
    }
}

