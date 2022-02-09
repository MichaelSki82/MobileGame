using System;

namespace Tools
{
    public interface IReadOnlySubscriptionActionT<T>
    {
        void Subscribe(Action<T> subscriptionAction);

       
        void UnSubscribe (Action<T> unsubscriptionAction);
    }
}