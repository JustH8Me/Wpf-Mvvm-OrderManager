using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.Models;

public class SqlOrderService : IOrderService
{
    private readonly AppDbContext _dbContext;
    private readonly ObservableCollection<Order> _orders;

    public SqlOrderService()
    {
        _dbContext = new AppDbContext();
        _dbContext.Database.EnsureCreated();
        _dbContext.Orders.Load();
        _orders = _dbContext.Orders.Local.ToObservableCollection();
    }

    public ObservableCollection<Order> GetAllOrders()
    {
        return _orders;
    }

    public void AddOrder(Order newOrder)
    {
        _orders.Add(newOrder);
        _dbContext.SaveChanges();
    }

    //TODO: Переделать на async
    public void UpdateOrder(Order order)
    {
        _dbContext.Orders.Update(order);
        _dbContext.SaveChanges();
    }
}