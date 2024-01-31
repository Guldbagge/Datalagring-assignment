using CommunityToolkit.Mvvm.ComponentModel;

namespace Presentation.WPF.Models;

public class GetUserFormModel : ObservableObject
{
    private string _email = string.Empty;
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged(nameof(FirstName));
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged(nameof(LastName));
        }
    }
}
