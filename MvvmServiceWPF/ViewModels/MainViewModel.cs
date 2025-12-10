using System.Windows.Input;
using MvvmServiceWPF.Core;
using MvvmServiceWPF.Models;

namespace MvvmServiceWPF.ViewModels;

public class MainViewModel : BaseViewModel
{
    private readonly ActiveOrdersViewModel _activeOrdersViewModel;
    private readonly AddOrderViewModel _addOrderViewModel;
    private readonly HistoryViewModel _historyViewModel;

    private readonly HomeViewModel _homeViewModel;
    private BaseViewModel _currentView;

    public MainViewModel(HistoryViewModel historyViewModel, ActiveOrdersViewModel activeOrdersViewModel,
        HomeViewModel homeViewModel, AddOrderViewModel addOrderViewModel)
    {
        _activeOrdersViewModel = activeOrdersViewModel;
        _homeViewModel = homeViewModel;
        _historyViewModel = historyViewModel;
        _addOrderViewModel = addOrderViewModel;
        CurrentView = _homeViewModel;
        ShowHomeCommand = new RelayCommand(x => CurrentView = _homeViewModel);
        ShowActiveCommand = new RelayCommand(x => CurrentView = _activeOrdersViewModel);
        ShowHistoryCommand = new RelayCommand(x => CurrentView = _historyViewModel);
        AddOrderCommand = new RelayCommand(x => CurrentView = _addOrderViewModel);
    }

    public BaseViewModel CurrentView
    {
        get => _currentView;
        set
        {
            _currentView = value;
            OnPropertyChanged();
            if (_currentView is IViewModelGrid gridpage) gridpage.UpdateList();
        }
    }

    public ICommand ShowHomeCommand { get; }
    public ICommand ShowActiveCommand { get; }
    public ICommand ShowHistoryCommand { get; }
    public ICommand AddOrderCommand { get; }
}