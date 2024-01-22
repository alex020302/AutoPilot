using System.Collections.ObjectModel;
using AutoPilot;
using System.Data;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using AutoPilot;
using AutoPilot.Views;

namespace AutoPilot
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand _gotoViewHomeCommand;
        private ICommand _gotoViewData1Command;
        private ICommand _gotoViewData2Command;
        private ICommand _gotoViewRecorderCommand;
        private ICommand _gotoViewEditorCommand;
        private ICommand _gotoViewExecutorCommand;

        private object _currentView;
        public static int _nextView;
        private Home _home;
        private Data1 _data1;
        private Data2 _data2;
        private Recorder _recorder;
        private Editor _editor;
        private Views.Executor _executor;

        public int numberOfColumns;
        
        public MainWindowViewModel()
        {
            _home = new Home();
            _data1 = new Data1();
            _data2 = new Data2();
            _recorder = new Recorder();
            _editor = new Editor();
            _executor = new Views.Executor();
            CurrentView = _home;
        }

        public ICommand GotoViewHomeCommand
        {
            get
            {
                return _gotoViewHomeCommand ?? (_gotoViewHomeCommand = new RelayCommand(
                    x =>
                    {
                        GotoViewHome();
                    }));
            }
        }
        public ICommand GotoViewData1Command
        {
            get
            {
                return _gotoViewData1Command ?? (_gotoViewData1Command = new RelayCommand(
                    x =>
                    {
                        GotoViewData1();
                    }));
            }
        }
        public ICommand GotoViewData2Command
        {
            get
            {
                return _gotoViewData2Command ?? (_gotoViewData2Command = new RelayCommand(
                    x =>
                    {
                        GotoViewData2();
                    }));
            }
        }
        public ICommand GotoViewRecorderCommand
        {
            get
            {
                return _gotoViewRecorderCommand ?? (_gotoViewRecorderCommand = new RelayCommand(
                    x =>
                    {
                        GotoViewRecorder();
                    }));
            }
        }
        public ICommand GotoViewEditorCommand
        {
            get
            {
                return _gotoViewEditorCommand ?? (_gotoViewEditorCommand = new RelayCommand(
                    x =>
                    {
                        GotoViewEditor();
                    }));
            }
        }
        public ICommand GotoViewExecutorCommand
        {
            get
            {
                return _gotoViewExecutorCommand ?? (_gotoViewExecutorCommand = new RelayCommand(
                    x =>
                    {
                        GotoViewExecutor();
                    }));
            }
        }


        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged("CurrentView");
            }
        }
        private void GotoViewHome()
        {
            CurrentView = _home;
        }
        private void GotoViewData1()
        {
            CurrentView = _data1;
        }
        private void GotoViewData2()
        {
            CurrentView = _data2;
        }
        private void GotoViewRecorder()
        {
            CurrentView = _recorder;
        }
        private void GotoViewEditor()
        {
            CurrentView = _editor;
        }

        public void updateEditor(ObservableCollection<Action> actions)
        {
            _editor.updateActions(actions);
        }

        private void GotoViewExecutor()
        {
            CurrentView = _executor;
        }


    }
}