using System;
using System.Text;

namespace SystemManager
{
    public interface IPublisher<T>
    {
        event EventHandler<Event.MessageArgument<T>> DataPublisher;
        //void OnDataPublisher(Event.MessageArgument<T> args);
        void PublishData(T data);
    }

    public class Publisher<T> : IPublisher<T>
    {
        //Defined datapublisher event
        public event EventHandler<Event.MessageArgument<T>> DataPublisher;

        private void OnDataPublisher(Event.MessageArgument<T> args)
        {
            var handler = DataPublisher;
            if (handler != null)
                handler(this, args);
        }


        public void PublishData(T data)
        {
            Event.MessageArgument<T> message = (Event.MessageArgument<T>)Activator.CreateInstance(typeof(Event.MessageArgument<T>), new object[] { data });
            OnDataPublisher(message);
        }
    }
}
