using CommunityToolkit.Mvvm.ComponentModel;
using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.WPF.ViewModels;

public class GetAllUserViewModel : ObservableObject
{
    private List<UserEntity> _users;

    public List<UserEntity> Users
    {
        get { return _users; }
        set
        {
            _users = value;
            OnPropertyChanged(nameof(Users));
        }
    }
}

