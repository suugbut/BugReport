using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MaybeBug;
public class MainPageViewModel
{
    public ObservableCollection<Library> Libraries { get; } = [];

    public ICommand UserStoppedTypingCommand { get; }

    void ReloadLibraries(IEnumerable<Library> libraries)
    {
        Libraries.Clear();
        foreach (var library in libraries)
        {
            Libraries.Add(library);
        }
    }

    public MainPageViewModel()
    {
        ReloadLibraries(LibraryRepo.GetAll());
        UserStoppedTypingCommand = new Command<string>(
        execute: s =>
        {
            if (string.IsNullOrWhiteSpace(s))
                ReloadLibraries(LibraryRepo.GetAll());
            else 
                ReloadLibraries(LibraryRepo.GetByTitle(s));
        });
    }
}


public class Library
{
    public required string Title { get; init; }
    public required ImageSource ImageSource { get; init; }
}


public class LibraryRepo
{
    private static readonly IEnumerable<Library> _libraries =
    [
        new()
        {
            Title = "AAA",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/communitytoolkit.mvvm/8.2.0/icon"
        },
        new()
        {
            Title = "BBB",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/sentry.maui/3.33.1/icon"
        },
        new()
        {
            Title = "CCC",
            ImageSource = "https://api.nuget.org/v3-flatcontainer/esri.arcgisruntime.maui/100.14.1-preview3/icon"
        }
    ];
    public static IEnumerable<Library> GetAll()
    {
        return _libraries;
    }
    public static IEnumerable<Library> GetByTitle(string word)
    {
        return _libraries
            .Where(l => l.Title.Contains(word, StringComparison.OrdinalIgnoreCase));
    }
}


