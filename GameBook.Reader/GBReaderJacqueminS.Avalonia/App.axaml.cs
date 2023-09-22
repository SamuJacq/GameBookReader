using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using GBReaderJacqueminS.Domains;
using GBReaderJacqueminS.Infrastructures;
using GBReaderJacqueminS.Presentations;
using GBReaderJacqueminS.Repositories;
using System;
using System.IO;

namespace GBReaderJacqueminS
{
    public partial class App : Application
    {
        private static string FILE_PATH = Path.Join(Environment.GetEnvironmentVariable("USERPROFILE"), "ue36");
        private static string FILE_NAME = Path.Join(Environment.GetEnvironmentVariable("USERPROFILE"), "ue36", "e200017-session.json");
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                MainWindow mainWindow = new MainWindow();
                DBRepository storage;
                JSONRepository file = new JSONRepository(FILE_PATH, FILE_NAME);
                Reader reader = new Reader();
                try
                {
                    desktop.MainWindow = mainWindow;
                    var factory = DBRepositoryFactory.CreateSession();
                    storage = factory.NewStorage();
                    mainWindow.InitPresentation(new MainPresentation(mainWindow, storage, file, reader));
                    mainWindow.GoTo("Home");
                }
                catch (UnableToConnectException ex)
                {
                    mainWindow.GoTo("Error");
                }

            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
