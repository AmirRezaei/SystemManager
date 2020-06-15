using System;
using System.Reflection;

namespace Helper.EventAggregator
{
    public sealed class Subscription<TMessage> : IDisposable
    {
        public readonly MethodInfo MethodInfo;
        private readonly global::Helper.EventAggregator.EventAggregator _eventAggregator;
        public readonly WeakReference TargetObject;
        public readonly bool IsStatic;

        private bool _isDisposed;
        public Subscription(Action<TMessage> action, global::Helper.EventAggregator.EventAggregator eventAggregator)
        {
            MethodInfo = action.Method;
            if (action.Target == null)
                IsStatic = true;
            TargetObject = new WeakReference(action.Target);
            _eventAggregator = eventAggregator;
        }

        ~Subscription()
        {
            if (!_isDisposed)
                Dispose();
        }

        public void Dispose()
        {
            _eventAggregator.UnSubscribe(this);
            _isDisposed = true;
        }

        public Action<TMessage> CreateAction()
        {
            if (TargetObject.Target != null && TargetObject.IsAlive)
                return (Action<TMessage>)Delegate.CreateDelegate(typeof(Action<TMessage>), TargetObject.Target, MethodInfo);
            if (IsStatic)
                return (Action<TMessage>)Delegate.CreateDelegate(typeof(Action<TMessage>), MethodInfo);

            return null;
        }
    }
}
