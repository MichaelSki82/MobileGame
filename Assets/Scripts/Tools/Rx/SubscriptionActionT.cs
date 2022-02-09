using System;

namespace Tools
{
    internal class SubscriptionActionT<T> : IReadOnlySubscriptionActionT<T>
    {
        private Action<T> _action;

        public void Invoke(T argument)
        {
            _action?.Invoke(argument);
        }

        public void Subscribe(Action<T> subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscribe(Action<T> unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }

}