using System.Windows.Input;
using MvvmServiceWPF.Core;
using MvvmServiceWPF.Models;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.ViewModels;

public class AddOrderViewModel : BaseViewModel
{
    private readonly IOrderService _currentView;
    private string _clientName;
    private string _description;
    private string _numberTel;
    private decimal _price;

    public AddOrderViewModel(IOrderService orderService)
    {
        _currentView = orderService;
        CreateCommand = new RelayCommand(x => AddOrder());
    }

    public string ClientName
    {
        get => _clientName;
        set
        {
            _clientName = value;
            OnPropertyChanged();
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            _description = value;
            OnPropertyChanged();
        }
    }

    public string NumberTel
    {
        get => _numberTel;
        set
        {
            _numberTel = value;
            OnPropertyChanged();
        }
    }

    public decimal Price
    {
        get => _price;
        set
        {
            _price = value;
            OnPropertyChanged();
        }
    }

    public ICommand CreateCommand { get; }

    private void AddOrder()
    {
        var allOrders = _currentView.GetAllOrders();
        var id = allOrders.Count + 1;
        var date = DateTime.Now;
        var status = OrderStatus.New;
        var newOrder = new Order
        {
            Id = id,
            Date = date,
            ClientName = ClientName,
            Phone = NumberTel,
            Description = Description,
            Price = Price,
            Status = status
        };
        _currentView.AddOrder(newOrder);
        ClientName = "";
        Description = "";
        NumberTel = "";
        Price = 0;
    }
}