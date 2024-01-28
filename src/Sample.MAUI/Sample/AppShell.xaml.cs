using System.Diagnostics;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace Sample;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        string pathC = FileSystem.CacheDirectory;
        string pathA = FileSystem.AppDataDirectory;
		Debug.WriteLine(pathC);
        Debug.WriteLine(pathA);
        ;
    }
}