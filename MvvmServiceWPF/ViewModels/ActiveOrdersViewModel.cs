using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using MvvmServiceWPF.Core;
using MvvmServiceWPF.Models;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.ViewModels;

public class ActiveOrdersViewModel : BaseViewModel, IViewModelGrid
{
    private readonly IOrderService _orderService;

    public ActiveOrdersViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        var rawOrders = orderService.GetAllOrders();
        //фильтруем
        OrdersView = new ListCollectionView(rawOrders);
        OrdersView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        OrdersView.Filter = FilterOrders;
        //Кнопка выполнить заказ
        CompleteOrderCommand = new RelayCommand(obj =>
        {
            if (obj is Order selectedOrder)
            {
                selectedOrder.Status = OrderStatus.Completed;
                _orderService.UpdateOrder(selectedOrder);
                UpdateList();
            }
        });
        CancelOrderCommand = new RelayCommand(obj =>
            {
                if (obj is Order selectedOrder)
                {
                    selectedOrder.Status = OrderStatus.Cancelled;
                    _orderService.UpdateOrder(selectedOrder);
                    UpdateList();
                }
            }
        );
        GoWorkOrderCommand = new RelayCommand(obj =>
            {
                if (obj is Order selectedOrder)
                {
                    selectedOrder.Status = OrderStatus.Processing;
                    _orderService.UpdateOrder(selectedOrder);
                    UpdateList();
                }
            }
        );
    }

    public ICollectionView OrdersView { get; set; }
    public ICommand CompleteOrderCommand { get; }
    public ICommand CancelOrderCommand { get; }
    public ICommand GoWorkOrderCommand { get; }

    public void UpdateList()
    {
        OrdersView?.Refresh();
    }

    private bool FilterOrders(object item)
    {
        if (item is Order order)
        {
            var isActive = order.Status != OrderStatus.Completed && order.Status != OrderStatus.Cancelled;
            return isActive;
        }

        return false;
    }
}