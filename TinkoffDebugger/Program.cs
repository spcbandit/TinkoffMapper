using System;
using System.Collections.Generic;
using System.Net.Http;
using Grpc.Core;
using Grpc.Net.Client;
using Tinkoff.Proto.InvestApi.V1;
using TinkoffMapper.Invest.MarketStreams;
using TinkoffMapper.Invest.Rest;

namespace TinkoffDebugger
{
    internal class Program
    {
        private static string ApiToken = "t.13OjMfN4no8jC0ohRsJSwgchhSJ6d-bJ37YdFJxkp0Ki0WdFbUbM3byEMRPEcSGlZ2PXIM9uPJzRggVkVmmDSQ";
        private static string pathApiGrpc = "https://invest-public-api.tinkoff.ru:443";
        public static string Figi = "FUTYNDF06230"; //Яндекс
        public static RestApiComposition InvestHandler;

        static void Main(string[] args)
        {
            InvestHandler = new RestApiComposition();
            string reponse = @"{
                                                 ""operations"": [
                                                    {
                                                     ""id"": ""3970249892"",
                                                     ""parentOperationId"": ""34968058168"",
                                                     ""currency"": ""rub"",
                                                     ""payment"": {
                                                       ""currency"": ""rub"",
                                                       ""units"": ""0"",
                                                       ""nano"": -410000000
                                                     },
                                                     ""price"": {
                                                       ""currency"": """",
                                                       ""units"": ""0"",
                                                       ""nano"": 0
                                                     },
                                                     ""state"": ""OPERATION_STATE_EXECUTED"",
                                                     ""quantity"": ""0"",
                                                     ""quantityRest"": ""0"",
                                                     ""figi"": ""BBG004S68598"",
                                                     ""instrumentType"": ""share"",
                                                     ""date"": ""2023-03-15T11:12:31.826Z"",
                                                     ""type"": ""Удержание комиссии за операцию"",
                                                     ""operationType"": ""OPERATION_TYPE_BROKER_FEE"",
                                                     ""trades"": [],
                                                     ""assetUid"": ""b9f09cfa-1099-4691-a981-8b1a4c0c72f3"",
                                                     ""instrumentUid"": ""eb4ba863-e85f-4f80-8c29-f2627938ee58""
                                                   },
                                                   {
                                                     ""id"": ""34968058168"",
                                                     ""parentOperationId"": """",
                                                     ""currency"": ""rub"",
                                                     ""payment"": {
                                                       ""currency"": ""rub"",
                                                       ""units"": ""-137"",
                                                       ""nano"": -750000000
                                                     },
                                                     ""price"": {
                                                       ""currency"": ""rub"",
                                                       ""units"": ""137"",
                                                       ""nano"": 750000000
                                                     },
                                                     ""state"": ""OPERATION_STATE_EXECUTED"",
                                                     ""quantity"": ""1"",
                                                     ""quantityRest"": ""0"",
                                                     ""figi"": ""BBG004S68598"",
                                                     ""instrumentType"": ""share"",
                                                     ""date"": ""2023-03-15T11:12:30.826725Z"",
                                                     ""type"": ""Покупка ценных бумаг"",
                                                     ""operationType"": ""OPERATION_TYPE_BUY"",
                                                     ""trades"": [
                                                       {
                                                         ""tradeId"": ""7402752421"",
                                                         ""dateTime"": ""2023-03-15T11:12:30.826725Z"",
                                                         ""quantity"": ""1"",
                                                         ""price"": {
                                                           ""currency"": ""rub"",
                                                           ""units"": ""137"",
                                                           ""nano"": 750000000
                                                         }
                                                       }
                                                     ],
                                                     ""assetUid"": ""b9f09cfa-1099-4691-a981-8b1a4c0c72f3"",
                                                     ""instrumentUid"": ""eb4ba863-e85f-4f80-8c29-f2627938ee58""
                                                   },
                                                   {
                                                     ""id"": ""34966127475"",
                                                     ""parentOperationId"": """",
                                                     ""currency"": ""rub"",
                                                     ""payment"": {
                                                       ""currency"": ""rub"",
                                                       ""units"": ""130"",
                                                       ""nano"": 0
                                                     },
                                                     ""price"": {
                                                       ""currency"": ""rub"",
                                                       ""units"": ""130"",
                                                       ""nano"": 0
                                                     },
                                                     ""state"": ""OPERATION_STATE_CANCELED"",
                                                     ""quantity"": ""1"",
                                                     ""quantityRest"": ""1"",
                                                     ""figi"": ""BBG004S68598"",
                                                     ""instrumentType"": ""share"",
                                                     ""date"": ""2023-03-15T10:00:27.009034Z"",
                                                     ""type"": ""Покупка ценных бумаг"",
                                                     ""operationType"": ""OPERATION_TYPE_BUY"",
                                                     ""trades"": [],
                                                     ""assetUid"": ""b9f09cfa-1099-4691-a981-8b1a4c0c72f3"",
                                                     ""instrumentUid"": ""eb4ba863-e85f-4f80-8c29-f2627938ee58""
                                                   }
                                                 ]
                                                }";

            var a = InvestHandler.HandleOperationsResponse(reponse);

            var test = "{\"code\":13,\"message\":\"internal error\",\"description\":\"70001\"}";
            //var res = InvestHandler.HandleCancelOrderResponse(reponse);
            //var res1 = InvestHandler.HandleGetAccountsResponse(reponse);
            //var res2 = InvestHandler.HandleGetCandlesResponse(reponse);
            //var res3 = InvestHandler.HandleGetLastPricesResponse(reponse);
            var res4 = InvestHandler.HandleWithdrawLimitsResponse(reponse);

            if (res4 != null)
            {

            }

            if (a != null) { }
            //Test();
        }

        private static async void Test()
        {
            try
            { 
                var combineMarket = new CombineMarketSubscriber(ApiToken, pathApiGrpc);
                combineMarket.SubscribeOrderBookResponseEvent += book =>
                {
                    Console.WriteLine(book);
                }; 
                await combineMarket.RunStream(new List<string>(){ Figi });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }       

          
        }
    }
}
