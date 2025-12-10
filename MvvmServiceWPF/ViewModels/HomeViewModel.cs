using MvvmServiceWPF.Core;
using MvvmServiceWPF.Models;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.ViewModels;

public class HomeViewModel : BaseViewModel, IViewModelGrid
{
    private readonly IOrderService _orderService;

    private int _activeCount;

    private int _completedCount;

    private DateTime? _endDate;
    private DateTime? _startDate;
    private decimal _totalEarnings;

    public HomeViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        UpdateList();
    }

    public DateTime? StartDate
    {
        get => _startDate;
        set
        {
            _startDate = value;
            OnPropertyChanged();
            UpdateList();
        }
    }

    public DateTime? EndDate
    {
        get => _endDate;
        set
        {
            _endDate = value;
            OnPropertyChanged();
            UpdateList();
        }
    }

    public decimal TotalEarnings
    {
        get => _totalEarnings;
        set
        {
            _totalEarnings = value;
            OnPropertyChanged();
        }
    }

    public int ActiveCount
    {
        get => _activeCount;
        set
        {
            _activeCount = value;
            OnPropertyChanged();
        }
    }

    public int CompletedCount
    {
        get => _completedCount;
        set
        {
            _completedCount = value;
            OnPropertyChanged();
        }
    }

    public void UpdateList()
    {
        var orders = _orderService.GetAllOrders().AsEnumerable();
        if (StartDate.HasValue) orders = orders.Where(x => x.Date >= StartDate.Value);

        if (EndDate.HasValue)
        {
            var endDay = EndDate.Value.AddDays(1).Date;
            orders = orders.Where(x => x.Date < endDay);
        }

        var filtrOrders = orders.ToList();
        TotalEarnings = filtrOrders.Where(x => x.Status == OrderStatus.Completed).Sum(x => x.Price);
        ActiveCount = filtrOrders.Count(x => x.Status is OrderStatus.New or OrderStatus.Processing);
        CompletedCount = filtrOrders.Count(x => x.Status == OrderStatus.Completed);
    }
}