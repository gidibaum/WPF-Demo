using Microsoft.Practices.Unity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using Base.Prism.Interfaces;

namespace Base.Prism.Services
{
    public class NavigationState<T>
    {
        public T State { get; set; }
        public NavigationState<T> Next { get; set; }
        public NavigationState<T> Previous { get; set; }

        public override string ToString()
        {
            return State.ToString();
        }
    }


    public class StateNavigationService<T> : NavigationService<T>, IStateNavigationService<T>
    {
        #region Property: CanNext

        bool _CanNext;
        public bool CanNext
        {
            get { return _CanNext; }
            set
            {
                SetProperty(ref _CanNext, value);
            }
        }

        #endregion

        #region Property: CanPrevious

        bool _CanBack;
        public bool CanBack
        {
            get { return _CanBack; }
            set
            {
                SetProperty(ref _CanBack, value);
            }
        }


        #endregion

        static List<NavigationState<T>> _States;


        #region Property: CurrentState

        NavigationState<T> _CurrentState;
        public NavigationState<T> CurrentState
        {
            get { return _CurrentState; }
            set
            {
                SetProperty(ref _CurrentState, value);

                CanNext = _CurrentState.Next != null;
                CanBack = _CurrentState.Previous != null;
                View = _CurrentState.State;
            }
        }

        #endregion


        static StateNavigationService()
        {
            _States = Enum.GetValues(typeof(T)).Cast<T>().Select(s=>new NavigationState<T>() { State = s}).ToList();

            for (int i = 0; i < _States.Count; i++)
            {
                if (i != 0)
                {
                    _States[i].Previous = _States[i - 1];
                }

                if (i != _States.Count-1)
                {
                    _States[i].Next = _States[i + 1];
                }
            }
        }

        public StateNavigationService(IRegionManager regionManager, IUnityContainer container) : base(regionManager, container)
        {
            _CurrentState = _States.First();
            CanNext = _CurrentState.Next != null;
        }

        public void Next()
        {
            CurrentState = _CurrentState.Next;
        }

        public void Back()
        {
            CurrentState = _CurrentState.Previous;
        }

    }

}
