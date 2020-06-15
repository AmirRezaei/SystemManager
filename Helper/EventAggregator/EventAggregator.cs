using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Helper.EventAggregator
{
    public sealed class EventAggregator
    {
        private readonly object lockObj = new object();
        private readonly Dictionary<Type, IList> subscriber = new Dictionary<Type, IList>();

        // Create a lazy singelton
        private static readonly Lazy<EventAggregator> lazy = new Lazy<EventAggregator>(() => new EventAggregator());
        public static EventAggregator Instance { get { return lazy.Value; } }
        public void Publish<TMessageType>(TMessageType message)
        {
            Type messageType = typeof(TMessageType);
            IList subscriptions;
            if (subscriber.ContainsKey(messageType))
            {
                lock (lockObj)
                {
                    subscriptions = new List<Subscription<TMessageType>>(subscriber[messageType].Cast<Subscription<TMessageType>>());
                }

                foreach (Subscription<TMessageType> subscription in subscriptions)
                {
                    Action<TMessageType> action = subscription.CreateAction();
                    action?.Invoke(message);
                }
            }
        }

        /// Subscribe to message type.
        public Subscription<TMessageType> Subscribe<TMessageType>(Action<TMessageType> action)
        {
            Type messageType = typeof(TMessageType);
            Subscription<TMessageType> actiondetail = new Subscription<TMessageType>(action, this);

            lock (lockObj)
            {
                if (!subscriber.TryGetValue(messageType, out IList actionList))
                {
                    actionList = new List<Subscription<TMessageType>> { actiondetail };
                    subscriber.Add(messageType, actionList);
                }
                else
                {
                    actionList.Add(actiondetail);
                }
            }

            return actiondetail;
        }

        /// <summary>
        /// UnSubscribe to message type.
        /// </summary>
        /// <typeparam name="TMessageType"></typeparam>
        /// <param name="subscription"></param>
        public void UnSubscribe<TMessageType>(Subscription<TMessageType> subscription)
        {
            Type messageType = typeof(TMessageType);
            if (subscriber.ContainsKey(messageType))
            {
                lock (lockObj)
                {
                    subscriber[messageType].Remove(subscription);
                }
            }
        }
    }
}
