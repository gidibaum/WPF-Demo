using System;

namespace Base
{
    public class ExclusiveAction
    {
        bool _Flag;


        public void Do(Action action)
        {
            if (!_Flag)
            {
                _Flag = true;

                action();

                _Flag = false;
            }
        }
    }
}
