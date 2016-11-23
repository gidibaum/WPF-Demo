using Base;
using Base.WPF;

namespace AnimationDemo.ViewModels
{
    class MainViewModel : ObservableObject
    {

        public Command ToggleCmd { get; }

        #region Property: IsBlinking

        bool _IsBlinking;

        public bool IsBlinking
        {
            get { return _IsBlinking; }
            set { SetProperty(ref _IsBlinking, value); }
        }

        #endregion

        public MainViewModel()
        {
            ToggleCmd = new Command(() => IsBlinking = !IsBlinking);
        }
    }
}
