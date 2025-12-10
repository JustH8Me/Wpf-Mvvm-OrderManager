using System.Collections.ObjectModel;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.Models;

public interface IOrderService
{
    ObservableCollection<Order> GetAllOrders();
    void AddOrder(Order newOrder);
    void UpdateOrder(Order order);
}