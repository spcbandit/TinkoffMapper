using Grpc.Core;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StreamManager.Data
{
    public class StreamingService<T> : IDisposable
    {
        private AsyncServerStreamingCall<T> stream;
        protected CancellationToken token;

        public StreamingService(AsyncServerStreamingCall<T> stream, CancellationToken token)
        {
            this.stream = stream;
            this.token = token;
        }

        /// <summary>
        /// Запущен ли стрим
        /// </summary>
        public bool IsStarted { get; private set; }

        /// <summary>
        /// Запуск стрима
        /// </summary>
        public async Task Run(Action<T> notify)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                await Listen(notify);
            }
        }

        /// <summary>
        /// Прослушка событий стрима
        /// </summary>
        async Task Listen(Action<T> notify)
        {
            while (await stream.ResponseStream.MoveNext(token))
            {
                if (stream.ResponseStream.Current != null && !token.IsCancellationRequested)
                {
                    notify?.Invoke(stream.ResponseStream.Current);
                }
            }
        }

        /// <summary>
        /// Завершение стрима
        /// </summary>
        public virtual void Stop()
        {
            IsStarted = false;
            Dispose();
        }

        /// <summary>
        /// Освобожение всех ресурсов для стрима
        /// </summary>
        public void Dispose()
        {
            stream.Dispose();
        }

        ~StreamingService()
        {
            this.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
