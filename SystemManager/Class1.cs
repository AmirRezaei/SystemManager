using System;
using SM.EventTypes;

namespace SM
{
    public interface IPublisher<T>
    {
        event EventHandler<MessageArgument<T>> DataPublisher;
        //void OnDataPublisher(Event.MessageArgument<T> args);
        void PublishData(T data);
    }

    public class Publisher<T> : IPublisher<T>
    {
        //Defined datapublisher event
        public event EventHandler<MessageArgument<T>> DataPublisher;

        private void OnDataPublisher(MessageArgument<T> args)
        {
            var handler = DataPublisher;
            if (handler != null)
                handler(this, args);
        }


        public void PublishData(T data)
        {
            MessageArgument<T> message = (MessageArgument<T>)Activator.CreateInstance(typeof(MessageArgument<T>), new object[] { data });
            OnDataPublisher(message);
        }
    }
}
