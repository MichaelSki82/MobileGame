﻿using System;

namespace Tools
{
    internal class SubscriptionAction : IReadOnlySubscriptionAction
    {
        private Action _action;

        public void Invoke()
        {
            _action?.Invoke();
        }

        public void SubscribeOnChange(Action  subscriptionAction)
        {
            _action += subscriptionAction;
        }

        public void UnSubscriptionOnChange(Action unsubscriptionAction)
        {
            _action -= unsubscriptionAction;
        }
    }
    
}