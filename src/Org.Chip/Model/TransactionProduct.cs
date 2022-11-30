

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.Chip.Client.OpenAPIDateConverter;

namespace Org.Chip.Model
{
    /// <summary>
    /// Product category the transaction belongs to.  - bank_payment: bank_payment (Payment.payment_type &#x3D;&#x3D; \&quot;bank_payment\&quot;) - custom_payment: custom_payment (Payment.payment_type &#x3D;&#x3D; \&quot;custom_payment\&quot;) - invoice: Purchase created as an invoice through the merchant portal (Purchase.product &#x3D;&#x3D; \&quot;billing_invoices\&quot;) - payout: Payout to a client&#39;s payment card - payout_balance_transfer: Transfer of funds between the acquirer and payout balances - purchase: Purchase created through the merchant API (Purchase.product &#x3D;&#x3D; \&quot;purchases\&quot;) - refund: refund (Payment.payment_type &#x3D;&#x3D; \&quot;refund\&quot;) - subscription: Purchase created using a subscription (Purchase.product is either \&quot;billing_subscriptions\&quot; or \&quot;billing_subscriptions_invoice\&quot;)
    /// </summary>
    /// <value>Product category the transaction belongs to.  - bank_payment: bank_payment (Payment.payment_type &#x3D;&#x3D; \&quot;bank_payment\&quot;) - custom_payment: custom_payment (Payment.payment_type &#x3D;&#x3D; \&quot;custom_payment\&quot;) - invoice: Purchase created as an invoice through the merchant portal (Purchase.product &#x3D;&#x3D; \&quot;billing_invoices\&quot;) - payout: Payout to a client&#39;s payment card - payout_balance_transfer: Transfer of funds between the acquirer and payout balances - purchase: Purchase created through the merchant API (Purchase.product &#x3D;&#x3D; \&quot;purchases\&quot;) - refund: refund (Payment.payment_type &#x3D;&#x3D; \&quot;refund\&quot;) - subscription: Purchase created using a subscription (Purchase.product is either \&quot;billing_subscriptions\&quot; or \&quot;billing_subscriptions_invoice\&quot;)</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum TransactionProduct
    {
        /// <summary>
        /// Enum Bankpayment for value: bank_payment
        /// </summary>
        [EnumMember(Value = "bank_payment")]
        Bankpayment = 1,

        /// <summary>
        /// Enum Custompayment for value: custom_payment
        /// </summary>
        [EnumMember(Value = "custom_payment")]
        Custompayment = 2,

        /// <summary>
        /// Enum Invoice for value: invoice
        /// </summary>
        [EnumMember(Value = "invoice")]
        Invoice = 3,

        /// <summary>
        /// Enum Payout for value: payout
        /// </summary>
        [EnumMember(Value = "payout")]
        Payout = 4,

        /// <summary>
        /// Enum Payoutbalancetransfer for value: payout_balance_transfer
        /// </summary>
        [EnumMember(Value = "payout_balance_transfer")]
        Payoutbalancetransfer = 5,

        /// <summary>
        /// Enum Purchase for value: purchase
        /// </summary>
        [EnumMember(Value = "purchase")]
        Purchase = 6,

        /// <summary>
        /// Enum Refund for value: refund
        /// </summary>
        [EnumMember(Value = "refund")]
        Refund = 7,

        /// <summary>
        /// Enum Subscription for value: subscription
        /// </summary>
        [EnumMember(Value = "subscription")]
        Subscription = 8

    }

}
