using System.ComponentModel;
using System.Windows.Data;
using MvvmServiceWPF.Core;
using MvvmServiceWPF.Models;
using MvvmServiceWPF.Services;

namespace MvvmServiceWPF.ViewModels;

public class HistoryViewModel : BaseViewModel, IViewModelGrid
{
    private readonly IOrderService _orderService;
    private string _searchText;

    public HistoryViewModel(IOrderService orderService)
    {
        _orderService = orderService;
        var rawOrders = _orderService.GetAllOrders();
        //фильтруем
        OrdersView = new ListCollectionView(rawOrders);

        OrdersView.SortDescriptions.Add(new SortDescription("Date", ListSortDirection.Descending));
        OrdersView.Filter = FilterHistory;
    }

    public string SearchText
    {
        get => _searchText;
        set
        {
            _searchText = value;
            OnPropertyChanged();
            OrdersView.Refresh();
        }
    }

    public ICollectionView OrdersView { get; set; }

    public void UpdateList()
    {
        OrdersView.Refresh();
    }

    private bool FilterHistory(object item)
    {
        if (item is Order order)
        {
            var isCorStatus = order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled;
            if (!isCorStatus) return false;
            if (string.IsNullOrEmpty(SearchText)) return true;
            //поиск
            var search = SearchText.ToLower();
            var matchName = order.ClientName != null && order.ClientName.ToLower().Contains(SearchText.ToLower());
            var matchDescription =
                order.Description != null && order.Description.ToLower().Contains(SearchText.ToLower());
            var matchId = order.Id != null && order.Id.ToString().Contains(SearchText.ToLower());
            var matchDate = order.Date != null && order.Date.ToString().Contains(SearchText.ToLower());
            return matchName || matchDescription || matchId || matchDate;
        }

        return false;
    }
}