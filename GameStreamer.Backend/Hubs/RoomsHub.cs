using GameStreamer.Backend.Services;
using Microsoft.AspNetCore.SignalR;

namespace GameStreamer.Backend.Hubs
{
    public class RoomsHub : Hub
    {

        //private readonly StreamManager _streamManager;

        public RoomsHub(StreamManager streamManager)
        {
            _streamManager = streamManager;
        }

        public List<string> ListStreams()
        {
            return _streamManager.ListStreams();
        }


        public IAsyncEnumerable<string> WatchStream(string streamName, CancellationToken cancellationToken)
        {
            return _streamManager.Subscribe(streamName, cancellationToken);
        }

        public async Task StartStream(string streamName, IAsyncEnumerable<string> streamContent)
        {
            try
            {
                var streamTask = _streamManager.RunStreamAsync(streamName, streamContent);

                // Tell everyone about your stream!
                await Clients.Others.SendAsync("NewStream", streamName);

                await streamTask;
            }
            finally
            {
                await Clients.Others.SendAsync("RemoveStream", streamName);
            }
        }

        /// <summary>
        /// Метод потоковой отправки текущей даты клиентам
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        //public async IAsyncEnumerable<int> StreamCurrentDate(int count, int delay)// [EnumeratorCancellation] CancellationToken cancellationToken)
        //{

        //    for (var i = 0; i < count; i++)
        //    {

        //        // Check the cancellation token regularly so that the server will stop
        //        // producing items if the client disconnects.
        //        //cancellationToken.ThrowIfCancellationRequested();

        //        yield return i;

        //        // Use the cancellationToken in other APIs that accept cancellation
        //        // tokens so the cancellation can flow down to them.
        //        await Task.Delay(delay);// cancellationToken);
        //    }
        //}

        /// <summary>
        /// Тестовый Echo метод
        /// </summary>
        /// <param name="nickName"></param>
        /// <returns></returns>
        public async Task SendHelloWorld(string nickName)
        {
            await Clients.All.SendAsync("ReceiveHelloWorld",$"Hello from SignalR, {nickName}!");
        }
    }
}