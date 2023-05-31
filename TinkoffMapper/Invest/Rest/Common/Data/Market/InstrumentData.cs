using Newtonsoft.Json;
using System;
using TinkoffMapper.Extensions;
using TinkoffMapper.Invest.Rest.Common.Data.Enum;

namespace TinkoffMapper.Invest.Rest.Common.Data.Market
{
    /// <summary>
    /// Характеристики валют.
    /// </summary>
    public sealed class InstrumentData
    {
        public InstrumentData(string figi, string ticker, string classCode, int lot, string currency, QuotationData klong, 
            QuotationData kshort, QuotationData dlong, QuotationData dshort, QuotationData dlongMin, QuotationData dshortMin, 
            bool shortEnabledFlag, string name, string exchange, DateTimeOffset firstTradeDate, DateTimeOffset lastTradeDate, 
            string futuresType, string assetType, string basicAsset, QuotationData basicAssetSize, string countryOfRisk, 
            string countryOfRiskName, string sector, DateTimeOffset expirationDate, string tradingStatus, 
            bool otcFlag, bool buyAvailableFlag, bool sellAvailableFlag, QuotationData minPriceIncrement, 
            bool apiTradeAvailableFlag, string uid)
        {
            Figi = figi;
            Ticker = ticker;
            ClassCode = classCode;
            Lot = lot;
            Currency = currency;
            Klong = klong;
            Kshort = kshort;
            Dlong = dlong;
            Dshort = dshort;
            DlongMin = dlongMin;
            DshortMin = dshortMin;
            ShortEnabledFlag = shortEnabledFlag;
            Name = name;
            Exchange = exchange;
            FirstTradeDate = firstTradeDate;
            LastTradeDate = lastTradeDate;
            FuturesType = futuresType;
            AssetType = assetType;
            BasicAsset = basicAsset;
            BasicAssetSize = basicAssetSize;
            CountryOfRisk = countryOfRisk;
            CountryOfRiskName = countryOfRiskName;
            Sector = sector;
            ExpirationDate = expirationDate;
            TradingStatus = tradingStatus;
            TradingStatusEnum = TradingStatus.ToEnum<SecurityTradingStatusEnum>();
            OtcFlag = otcFlag;
            BuyAvailableFlag = buyAvailableFlag;
            SellAvailableFlag = sellAvailableFlag;
            MinPriceIncrement = minPriceIncrement;
            ApiTradeAvailableFlag = apiTradeAvailableFlag;
            Uid = uid;
        }

        [JsonProperty("figi")]
        public string Figi { get; set; }

        [JsonProperty("ticker")]
        public string Ticker { get; set; }

        [JsonProperty("classCode")]
        public string ClassCode { get; set; }

        [JsonProperty("lot")]
        public int Lot { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("klong")]
        public QuotationData Klong { get; set; }

        [JsonProperty("kshort")]
        public QuotationData Kshort { get; set; }

        [JsonProperty("dlong")]
        public QuotationData Dlong { get; set; }

        [JsonProperty("dshort")]
        public QuotationData Dshort { get; set; }

        [JsonProperty("dlongMin")]
        public QuotationData DlongMin { get; set; }

        [JsonProperty("dshortMin")]
        public QuotationData DshortMin { get; set; }

        [JsonProperty("shortEnabledFlag")]
        public bool ShortEnabledFlag { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("exchange")]
        public string Exchange { get; set; }

        [JsonProperty("firstTradeDate")]
        public System.DateTimeOffset FirstTradeDate { get; set; }

        [JsonProperty("lastTradeDate")]
        public System.DateTimeOffset LastTradeDate { get; set; }

        [JsonProperty("futuresType")]
        public string FuturesType { get; set; }

        [JsonProperty("assetType")]
        public string AssetType { get; set; }

        [JsonProperty("basicAsset")]
        public string BasicAsset { get; set; }

        [JsonProperty("basicAssetSize")]
        public QuotationData BasicAssetSize { get; set; }

        [JsonProperty("countryOfRisk")]
        public string CountryOfRisk { get; set; }

        [JsonProperty("countryOfRiskName")]
        public string CountryOfRiskName { get; set; }

        [JsonProperty("sector")]
        public string Sector { get; set; }

        [JsonProperty("expirationDate")]
        public System.DateTimeOffset ExpirationDate { get; set; }

        [JsonProperty("tradingStatus")]
        public string TradingStatus { get; set; }
        public SecurityTradingStatusEnum TradingStatusEnum { get; set; }

        [JsonProperty("otcFlag")]
        public bool OtcFlag { get; set; }

        [JsonProperty("buyAvailableFlag")]
        public bool BuyAvailableFlag { get; set; }

        [JsonProperty("sellAvailableFlag")]
        public bool SellAvailableFlag { get; set; }

        [JsonProperty("minPriceIncrement")]
        public QuotationData MinPriceIncrement { get; set; }

        [JsonProperty("apiTradeAvailableFlag")]
        public bool ApiTradeAvailableFlag { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }
    }
}
