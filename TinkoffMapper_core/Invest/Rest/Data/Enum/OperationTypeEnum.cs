using System.Runtime.Serialization;

namespace TinkoffMapper.Invest.Rest.Data.Enum
{
    /// <summary>
    /// Тип операции
    /// </summary>
    public enum OperationTypeEnum
    {
        [EnumMember(Value = @"OPERATION_TYPE_UNSPECIFIED")]
        OPERATION_TYPE_UNSPECIFIED = 0,

        [EnumMember(Value = @"OPERATION_TYPE_INPUT")]
        OPERATION_TYPE_INPUT = 1,

        [EnumMember(Value = @"OPERATION_TYPE_BOND_TAX")]
        OPERATION_TYPE_BOND_TAX = 2,

        [EnumMember(Value = @"OPERATION_TYPE_OUTPUT_SECURITIES")]
        OPERATION_TYPE_OUTPUT_SECURITIES = 3,

        [EnumMember(Value = @"OPERATION_TYPE_OVERNIGHT")]
        OPERATION_TYPE_OVERNIGHT = 4,

        [EnumMember(Value = @"OPERATION_TYPE_TAX")]
        OPERATION_TYPE_TAX = 5,

        [EnumMember(Value = @"OPERATION_TYPE_BOND_REPAYMENT_FULL")]
        OPERATION_TYPE_BOND_REPAYMENT_FULL = 6,

        [EnumMember(Value = @"OPERATION_TYPE_SELL_CARD")]
        OPERATION_TYPE_SELL_CARD = 7,

        [EnumMember(Value = @"OPERATION_TYPE_DIVIDEND_TAX")]
        OPERATION_TYPE_DIVIDEND_TAX = 8,

        [EnumMember(Value = @"OPERATION_TYPE_OUTPUT")]
        OPERATION_TYPE_OUTPUT = 9,

        [EnumMember(Value = @"OPERATION_TYPE_BOND_REPAYMENT")]
        OPERATION_TYPE_BOND_REPAYMENT = 10,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_CORRECTION")]
        OPERATION_TYPE_TAX_CORRECTION = 11,

        [EnumMember(Value = @"OPERATION_TYPE_SERVICE_FEE")]
        OPERATION_TYPE_SERVICE_FEE = 12,

        [EnumMember(Value = @"OPERATION_TYPE_BENEFIT_TAX")]
        OPERATION_TYPE_BENEFIT_TAX = 13,

        [EnumMember(Value = @"OPERATION_TYPE_MARGIN_FEE")]
        OPERATION_TYPE_MARGIN_FEE = 14,

        [EnumMember(Value = @"OPERATION_TYPE_BUY")]
        OPERATION_TYPE_BUY = 15,

        [EnumMember(Value = @"OPERATION_TYPE_BUY_CARD")]
        OPERATION_TYPE_BUY_CARD = 16,

        [EnumMember(Value = @"OPERATION_TYPE_INPUT_SECURITIES")]
        OPERATION_TYPE_INPUT_SECURITIES = 17,

        [EnumMember(Value = @"OPERATION_TYPE_SELL_MARGIN")]
        OPERATION_TYPE_SELL_MARGIN = 18,

        [EnumMember(Value = @"OPERATION_TYPE_BROKER_FEE")]
        OPERATION_TYPE_BROKER_FEE = 19,

        [EnumMember(Value = @"OPERATION_TYPE_BUY_MARGIN")]
        OPERATION_TYPE_BUY_MARGIN = 20,

        [EnumMember(Value = @"OPERATION_TYPE_DIVIDEND")]
        OPERATION_TYPE_DIVIDEND = 21,

        [EnumMember(Value = @"OPERATION_TYPE_SELL")]
        OPERATION_TYPE_SELL = 22,

        [EnumMember(Value = @"OPERATION_TYPE_COUPON")]
        OPERATION_TYPE_COUPON = 23,

        [EnumMember(Value = @"OPERATION_TYPE_SUCCESS_FEE")]
        OPERATION_TYPE_SUCCESS_FEE = 24,

        [EnumMember(Value = @"OPERATION_TYPE_DIVIDEND_TRANSFER")]
        OPERATION_TYPE_DIVIDEND_TRANSFER = 25,

        [EnumMember(Value = @"OPERATION_TYPE_ACCRUING_VARMARGIN")]
        OPERATION_TYPE_ACCRUING_VARMARGIN = 26,

        [EnumMember(Value = @"OPERATION_TYPE_WRITING_OFF_VARMARGIN")]
        OPERATION_TYPE_WRITING_OFF_VARMARGIN = 27,

        [EnumMember(Value = @"OPERATION_TYPE_DELIVERY_BUY")]
        OPERATION_TYPE_DELIVERY_BUY = 28,

        [EnumMember(Value = @"OPERATION_TYPE_DELIVERY_SELL")]
        OPERATION_TYPE_DELIVERY_SELL = 29,

        [EnumMember(Value = @"OPERATION_TYPE_TRACK_MFEE")]
        OPERATION_TYPE_TRACK_MFEE = 30,

        [EnumMember(Value = @"OPERATION_TYPE_TRACK_PFEE")]
        OPERATION_TYPE_TRACK_PFEE = 31,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_PROGRESSIVE")]
        OPERATION_TYPE_TAX_PROGRESSIVE = 32,

        [EnumMember(Value = @"OPERATION_TYPE_BOND_TAX_PROGRESSIVE")]
        OPERATION_TYPE_BOND_TAX_PROGRESSIVE = 33,

        [EnumMember(Value = @"OPERATION_TYPE_DIVIDEND_TAX_PROGRESSIVE")]
        OPERATION_TYPE_DIVIDEND_TAX_PROGRESSIVE = 34,

        [EnumMember(Value = @"OPERATION_TYPE_BENEFIT_TAX_PROGRESSIVE")]
        OPERATION_TYPE_BENEFIT_TAX_PROGRESSIVE = 35,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_CORRECTION_PROGRESSIVE")]
        OPERATION_TYPE_TAX_CORRECTION_PROGRESSIVE = 36,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO_PROGRESSIVE")]
        OPERATION_TYPE_TAX_REPO_PROGRESSIVE = 37,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO")]
        OPERATION_TYPE_TAX_REPO = 38,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO_HOLD")]
        OPERATION_TYPE_TAX_REPO_HOLD = 39,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO_REFUND")]
        OPERATION_TYPE_TAX_REPO_REFUND = 40,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO_HOLD_PROGRESSIVE")]
        OPERATION_TYPE_TAX_REPO_HOLD_PROGRESSIVE = 41,

        [EnumMember(Value = @"OPERATION_TYPE_TAX_REPO_REFUND_PROGRESSIVE")]
        OPERATION_TYPE_TAX_REPO_REFUND_PROGRESSIVE = 42,

        [EnumMember(Value = @"OPERATION_TYPE_DIV_EXT")]
        OPERATION_TYPE_DIV_EXT = 43,
    }
}
