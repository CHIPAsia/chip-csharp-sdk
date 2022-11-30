

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
    /// Available event types and when they are emitted:  &#x60;purchase.created&#x60;: Emitted when a Purchase is created. This happens as a result of POST /purchases/ request executed successfully or of any of the Billing API methods, including scheduled billing run by a BillingTemplate with is_subscription &#x3D; true. Purchase.status will be &#x3D;&#x3D; &#x60;created&#x60; in the received payload.  - --  &#x60;purchase.paid&#x60;: Emitted when a Purchase is paid for. Purchase.status will be &#x3D;&#x3D; &#x60;paid&#x60;. Happens when a payform is submitted (for a Purchase having &#x60;skip_capture &#x3D;&#x3D; false&#x60;) and a successful payment is done by the payer or in case of /capture/ or /charge/ API requests executed successfully.  - --  &#x60;purchase.payment_failure&#x60;: Emitted when payer submits a payment using the payform, but it doesn&#39;t complete successfully (e.g. because payer&#39;s account balance is insufficient). Purchase.status will be &#x3D;&#x3D; &#x60;error&#x60;.  - --  &#x60;purchase.pending_execute&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_execute&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.paid&#x60;, &#x60;purchase.hold&#x60; or &#x60;purchase.payment_failed&#x60; callback will be emitted.  - --  &#x60;purchase.pending_charge&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_charge&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failed&#x60; callback will be emitted.  - --  &#x60;purchase.cancelled&#x60;: Emitted once POST /purchases/{id}/cancel/ request succeeds. It won&#39;t be possible to pay for the related Purchase after that. Purchase.status will be &#x3D;&#x3D; &#x60;cancelled&#x60;.  - --  &#x60;purchase.hold&#x60;: Emitted when a Purchase having &#x60;skip_capture &#x3D;&#x3D; true&#x60; has its payform submitted and \&quot;payment\&quot; performed successfully. The specified amount of funds will be placed on hold. Purchase.status will be &#x3D;&#x3D; &#x60;hold&#x60;.  - --  &#x60;purchase.captured&#x60;: Emitted when the POST /purchases/{id}/capture/ request for a Purchase that previously had the status of &#x60;hold&#x60; succeeds. Purchase.status will be &#x3D;&#x3D; &#x60;paid&#x60;.  - --  &#x60;purchase.pending_capture&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_capture&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.captured&#x60; callback will be emitted.  - --  &#x60;purchase.released&#x60;: Emitted when the POST /purchases/{id}/release/ request for a Purchase that previously had the status of &#x60;hold&#x60; succeeds. Funds reserved will be released with no payment performed. Purchase.status will be &#x3D;&#x3D; &#x60;released&#x60;.  - --  &#x60;purchase.pending_release&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_release&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.released&#x60; callback will be emitted.  - --  &#x60;purchase.preauthorized&#x60;: Emitted when preauthorization scenario (see description for the Purchase.skip_capture field) is executed successfully. Purchase will have a status of &#x60;preauthorized&#x60;.  - --  &#x60;purchase.recurring_token_deleted&#x60;: Emitted when the POST /purchases/{id}/delete_recurring_token/ request is executed successfully, deleting the recurring token associated with a Purchase. Purchase status will be the same as it were prior to this event.  - --  &#x60;purchase.pending_recurring_token_delete&#x60;: Emitted when token deletion takes longer than expected on the acquirer side. When operation is finalized, a &#x60;purchase.recurring_token_deleted&#x60; callback will be emitted.  - --  &#x60;purchase.subscription_charge_failure&#x60;: Emitted when an attempt to charge some Client&#39;s subscription-generated Purchase, using the token (e.g. card) they saved for their subscription, fails. Can only be emitted for a Purchase spawned from a BillingTemplate having is_subscription &#x3D;&#x3D; true. Usually means the system can&#39;t charge the subscriber Client&#39;s card because e.g. their account balance is insufficient or card is expired, hence an invoice to be paid manually will be automatically mailed to them. Purchase.status in the returned payload will be &#x3D;&#x3D; &#x60;sent&#x60;.  - --  &#x60;purchase.pending_refund&#x60;: Emitted when refund transaction execution takes longer than expected on the acquirer side. See &#x60;pending_refund&#x60; Purchase status. When refund becomes finalized, a &#x60;payment.refunded&#x60; callback will ne emitted.  - --  &#x60;payment.refunded&#x60;: Emitted when a Purchase is refunded (as a result of POST /purchases/{id}/capture/ request done successfully or action performed in company&#39;s frontoffice system). The returned data will be a Payment object generated as a result of this action. A link to the original Purchase (that will have a status of &#x60;refunded&#x60;) will be present in the &#x60;related_to&#x60; field of this Payment.  - --  &#x60;billing_template_client.subscription_billing_cancelled&#x60;: Emitted when a subscriber represented by this event&#39;s related BillingTemplateClient cancels their subscription using an email link available in the receipts he receives. The respective BillingTemplateClient will have its &#x60;status&#x60; set to &#x60;subscription_paused&#x60; as a result.  - --  &#x60;payout.failed&#x60;: Emitted when a Payout processing was completed with an error. Payout.status will be &#x3D;&#x3D; &#x60;error&#x60;. Note that payouts can spend up to 3-5 days (depending on the payout provider) in processing after being initiated.  - --  &#x60;payout.success&#x60;: Emitted when a Payout is successfully processed. Payout.status will be &#x3D;&#x3D; &#x60;success&#x60;. Note that payouts can spend up to 3-5 days (depending on the payout provider) in processing after being initiated.
    /// </summary>
    /// <value>Available event types and when they are emitted:  &#x60;purchase.created&#x60;: Emitted when a Purchase is created. This happens as a result of POST /purchases/ request executed successfully or of any of the Billing API methods, including scheduled billing run by a BillingTemplate with is_subscription &#x3D; true. Purchase.status will be &#x3D;&#x3D; &#x60;created&#x60; in the received payload.  - --  &#x60;purchase.paid&#x60;: Emitted when a Purchase is paid for. Purchase.status will be &#x3D;&#x3D; &#x60;paid&#x60;. Happens when a payform is submitted (for a Purchase having &#x60;skip_capture &#x3D;&#x3D; false&#x60;) and a successful payment is done by the payer or in case of /capture/ or /charge/ API requests executed successfully.  - --  &#x60;purchase.payment_failure&#x60;: Emitted when payer submits a payment using the payform, but it doesn&#39;t complete successfully (e.g. because payer&#39;s account balance is insufficient). Purchase.status will be &#x3D;&#x3D; &#x60;error&#x60;.  - --  &#x60;purchase.pending_execute&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_execute&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.paid&#x60;, &#x60;purchase.hold&#x60; or &#x60;purchase.payment_failed&#x60; callback will be emitted.  - --  &#x60;purchase.pending_charge&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_charge&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failed&#x60; callback will be emitted.  - --  &#x60;purchase.cancelled&#x60;: Emitted once POST /purchases/{id}/cancel/ request succeeds. It won&#39;t be possible to pay for the related Purchase after that. Purchase.status will be &#x3D;&#x3D; &#x60;cancelled&#x60;.  - --  &#x60;purchase.hold&#x60;: Emitted when a Purchase having &#x60;skip_capture &#x3D;&#x3D; true&#x60; has its payform submitted and \&quot;payment\&quot; performed successfully. The specified amount of funds will be placed on hold. Purchase.status will be &#x3D;&#x3D; &#x60;hold&#x60;.  - --  &#x60;purchase.captured&#x60;: Emitted when the POST /purchases/{id}/capture/ request for a Purchase that previously had the status of &#x60;hold&#x60; succeeds. Purchase.status will be &#x3D;&#x3D; &#x60;paid&#x60;.  - --  &#x60;purchase.pending_capture&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_capture&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.captured&#x60; callback will be emitted.  - --  &#x60;purchase.released&#x60;: Emitted when the POST /purchases/{id}/release/ request for a Purchase that previously had the status of &#x60;hold&#x60; succeeds. Funds reserved will be released with no payment performed. Purchase.status will be &#x3D;&#x3D; &#x60;released&#x60;.  - --  &#x60;purchase.pending_release&#x60;: Emitted when transaction execution takes longer than expected on the acquirer side. See &#x60;pending_release&#x60; Purchase status. When transaction becomes finalized, a &#x60;purchase.released&#x60; callback will be emitted.  - --  &#x60;purchase.preauthorized&#x60;: Emitted when preauthorization scenario (see description for the Purchase.skip_capture field) is executed successfully. Purchase will have a status of &#x60;preauthorized&#x60;.  - --  &#x60;purchase.recurring_token_deleted&#x60;: Emitted when the POST /purchases/{id}/delete_recurring_token/ request is executed successfully, deleting the recurring token associated with a Purchase. Purchase status will be the same as it were prior to this event.  - --  &#x60;purchase.pending_recurring_token_delete&#x60;: Emitted when token deletion takes longer than expected on the acquirer side. When operation is finalized, a &#x60;purchase.recurring_token_deleted&#x60; callback will be emitted.  - --  &#x60;purchase.subscription_charge_failure&#x60;: Emitted when an attempt to charge some Client&#39;s subscription-generated Purchase, using the token (e.g. card) they saved for their subscription, fails. Can only be emitted for a Purchase spawned from a BillingTemplate having is_subscription &#x3D;&#x3D; true. Usually means the system can&#39;t charge the subscriber Client&#39;s card because e.g. their account balance is insufficient or card is expired, hence an invoice to be paid manually will be automatically mailed to them. Purchase.status in the returned payload will be &#x3D;&#x3D; &#x60;sent&#x60;.  - --  &#x60;purchase.pending_refund&#x60;: Emitted when refund transaction execution takes longer than expected on the acquirer side. See &#x60;pending_refund&#x60; Purchase status. When refund becomes finalized, a &#x60;payment.refunded&#x60; callback will ne emitted.  - --  &#x60;payment.refunded&#x60;: Emitted when a Purchase is refunded (as a result of POST /purchases/{id}/capture/ request done successfully or action performed in company&#39;s frontoffice system). The returned data will be a Payment object generated as a result of this action. A link to the original Purchase (that will have a status of &#x60;refunded&#x60;) will be present in the &#x60;related_to&#x60; field of this Payment.  - --  &#x60;billing_template_client.subscription_billing_cancelled&#x60;: Emitted when a subscriber represented by this event&#39;s related BillingTemplateClient cancels their subscription using an email link available in the receipts he receives. The respective BillingTemplateClient will have its &#x60;status&#x60; set to &#x60;subscription_paused&#x60; as a result.  - --  &#x60;payout.failed&#x60;: Emitted when a Payout processing was completed with an error. Payout.status will be &#x3D;&#x3D; &#x60;error&#x60;. Note that payouts can spend up to 3-5 days (depending on the payout provider) in processing after being initiated.  - --  &#x60;payout.success&#x60;: Emitted when a Payout is successfully processed. Payout.status will be &#x3D;&#x3D; &#x60;success&#x60;. Note that payouts can spend up to 3-5 days (depending on the payout provider) in processing after being initiated.</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum Event
    {
        /// <summary>
        /// Enum PurchaseCreated for value: purchase.created
        /// </summary>
        [EnumMember(Value = "purchase.created")]
        PurchaseCreated = 1,

        /// <summary>
        /// Enum PurchasePaid for value: purchase.paid
        /// </summary>
        [EnumMember(Value = "purchase.paid")]
        PurchasePaid = 2,

        /// <summary>
        /// Enum PurchasePaymentfailure for value: purchase.payment_failure
        /// </summary>
        [EnumMember(Value = "purchase.payment_failure")]
        PurchasePaymentfailure = 3,

        /// <summary>
        /// Enum PurchasePendingexecute for value: purchase.pending_execute
        /// </summary>
        [EnumMember(Value = "purchase.pending_execute")]
        PurchasePendingexecute = 4,

        /// <summary>
        /// Enum PurchasePendingcharge for value: purchase.pending_charge
        /// </summary>
        [EnumMember(Value = "purchase.pending_charge")]
        PurchasePendingcharge = 5,

        /// <summary>
        /// Enum PurchaseCancelled for value: purchase.cancelled
        /// </summary>
        [EnumMember(Value = "purchase.cancelled")]
        PurchaseCancelled = 6,

        /// <summary>
        /// Enum PurchaseHold for value: purchase.hold
        /// </summary>
        [EnumMember(Value = "purchase.hold")]
        PurchaseHold = 7,

        /// <summary>
        /// Enum PurchaseCaptured for value: purchase.captured
        /// </summary>
        [EnumMember(Value = "purchase.captured")]
        PurchaseCaptured = 8,

        /// <summary>
        /// Enum PurchasePendingcapture for value: purchase.pending_capture
        /// </summary>
        [EnumMember(Value = "purchase.pending_capture")]
        PurchasePendingcapture = 9,

        /// <summary>
        /// Enum PurchaseReleased for value: purchase.released
        /// </summary>
        [EnumMember(Value = "purchase.released")]
        PurchaseReleased = 10,

        /// <summary>
        /// Enum PurchasePendingrelease for value: purchase.pending_release
        /// </summary>
        [EnumMember(Value = "purchase.pending_release")]
        PurchasePendingrelease = 11,

        /// <summary>
        /// Enum PurchasePreauthorized for value: purchase.preauthorized
        /// </summary>
        [EnumMember(Value = "purchase.preauthorized")]
        PurchasePreauthorized = 12,

        /// <summary>
        /// Enum PurchasePendingrecurringtokendelete for value: purchase.pending_recurring_token_delete
        /// </summary>
        [EnumMember(Value = "purchase.pending_recurring_token_delete")]
        PurchasePendingrecurringtokendelete = 13,

        /// <summary>
        /// Enum PurchaseRecurringtokendeleted for value: purchase.recurring_token_deleted
        /// </summary>
        [EnumMember(Value = "purchase.recurring_token_deleted")]
        PurchaseRecurringtokendeleted = 14,

        /// <summary>
        /// Enum PurchaseSubscriptionchargefailure for value: purchase.subscription_charge_failure
        /// </summary>
        [EnumMember(Value = "purchase.subscription_charge_failure")]
        PurchaseSubscriptionchargefailure = 15,

        /// <summary>
        /// Enum PurchasePendingrefund for value: purchase.pending_refund
        /// </summary>
        [EnumMember(Value = "purchase.pending_refund")]
        PurchasePendingrefund = 16,

        /// <summary>
        /// Enum PaymentRefunded for value: payment.refunded
        /// </summary>
        [EnumMember(Value = "payment.refunded")]
        PaymentRefunded = 17,

        /// <summary>
        /// Enum BillingtemplateclientSubscriptionbillingcancelled for value: billing_template_client.subscription_billing_cancelled
        /// </summary>
        [EnumMember(Value = "billing_template_client.subscription_billing_cancelled")]
        BillingtemplateclientSubscriptionbillingcancelled = 18,

        /// <summary>
        /// Enum PayoutFailed for value: payout.failed
        /// </summary>
        [EnumMember(Value = "payout.failed")]
        PayoutFailed = 19,

        /// <summary>
        /// Enum PayoutSuccess for value: payout.success
        /// </summary>
        [EnumMember(Value = "payout.success")]
        PayoutSuccess = 20

    }

}
