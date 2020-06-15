using System;
using System.Reflection;

namespace SystemManager.EventAggregator
{
    public sealed class Subscription<TMessage> : IDisposable
    {
        public readonly MethodInfo MethodInfo;
        private readonly EventAggregator EventAggregator;
        public readonly WeakReference TargetObjet;
        public readonly bool IsStatic;

        private bool isDisposed;
        public Subscription(Action<TMessage> action, EventAggregator eventAggregator)
        {
            MethodInfo = action.Method;
            if (action.Target == null)
                IsStatic = true;
            TargetObjet = new WeakReference(action.Target);
            EventAggregator = eventAggregator;
        }

        ~Subscription()
        {
            if (!isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            EventAggregator.UnSubscribe(this);
            isDisposed = true;
        }

        public Action<TMessage> CreatAction()
        {
            if (TargetObjet.Target != null && TargetObjet.IsAlive)
                return (Action<TMessage>)Delegate.CreateDelegate(typeof(Action<TMessage>), TargetObjet.Target, MethodInfo);
            if (IsStatic)
                return (Action<TMessage>)Delegate.CreateDelegate(typeof(Action<TMessage>), MethodInfo);

            return null;
        }
    }
}
