using Grpc.Core;
using Grpc.Net.Client;
using System;
using Tinkoff.Proto.InvestApi.V1;

namespace TinkoffMapper.Invest.UserStreams
{
    public class CombineUserSubscriber
    {
        /// <summary>
        /// Stream сделок пользователя
        /// </summary>
        public AsyncServerStreamingCall<TradesStreamResponse> ordersStream { get; set; }
        /// <summary>
        /// Server-side stream обновлений портфеля
        /// </summary>
        public AsyncServerStreamingCall<PortfolioStreamResponse> portfolioStream { get; set; }
        /// <summary>
        /// Server-side stream обновлений информации по изменению позиций портфеля
        /// </summary>
        public AsyncServerStreamingCall<PositionsStreamResponse> positionsStream { get; set; }

        private GrpcChannel channel;
        private OrdersStreamService.OrdersStreamServiceClient ordersClient;
        private OperationsStreamService.OperationsStreamServiceClient operationsClient;


        public CombineUserSubscriber(string apiKey, string apiUrl, string accountId)
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentException(nameof(apiKey));

            if (string.IsNullOrEmpty(apiUrl))
                throw new ArgumentException(nameof(apiUrl));

            if (string.IsNullOrEmpty(accountId))
                throw new ArgumentException(nameof(accountId));

            channel = GrpcChannel.ForAddress(apiUrl);

            ordersClient = new OrdersStreamService.OrdersStreamServiceClient(channel);
            operationsClient = new OperationsStreamService.OperationsStreamServiceClient(channel);

            var headers = GetLoginData(apiKey);
            AsyncServerStreamingCall<TradesStreamResponse> stream;
            channel = GrpcChannel.ForAddress(apiUrl);
            
            var tradesStreamRequest = new TradesStreamRequest() { Accounts = { accountId } };
            ordersStream = ordersClient.TradesStream(tradesStreamRequest, headers);

            var portfolioStreamRequest = new PortfolioStreamRequest() { Accounts = { accountId } };
            portfolioStream = operationsClient.PortfolioStream(portfolioStreamRequest, headers);

            var positionsStreamRequest = new PositionsStreamRequest() { Accounts = { accountId } };
            positionsStream = operationsClient.PositionsStream(positionsStreamRequest, headers);
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        public Metadata GetLoginData(string apiToken)
        {
            return new Metadata
            {
                { "Authorization", $"Bearer {apiToken}" }
            };
        }

        /// <summary>
        /// Stream сделок пользователя
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        public TradesStreamRequest TradesStreamSub(string account)
        {
            var tradesStreamRequest = new TradesStreamRequest
            {
                Accounts = { account }
            };
            return tradesStreamRequest;
        }

        /// <summary>
        /// Stream обновлений портфеля
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        public PortfolioStreamRequest PortfolioStreamSub(string account)
        {
            var portfolioStreamRequest = new PortfolioStreamRequest
            {
                Accounts = { account }
            };
            return portfolioStreamRequest;
        }

        /// <summary>
        /// Stream обновлений информации по изменению позиций портфеля
        /// </summary>
        /// <param name="account">Аккаунт</param>
        /// <returns></returns>
        public PositionsStreamRequest PositionsStreamSub(string account)
        {
            var positionsStreamRequest = new PositionsStreamRequest
            {
                Accounts = { account }
            };
            return positionsStreamRequest;
        }
    }
}
