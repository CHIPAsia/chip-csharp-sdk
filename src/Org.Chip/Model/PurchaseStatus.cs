

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
    /// Purchase status. Can have the following values:   &#x60;created&#x60;: Purchase was created using POST /purchases/ or Billing API capabilities.  - --  &#x60;sent&#x60;: Invoice for this purchase was sent over email using Billing API capabilities.  - --  &#x60;viewed&#x60;: The client has viewed the payform and/or invoice details for this purchase.  - --  &#x60;error&#x60;: There was a failed payment attempt for this purchase because of a problem with customer&#39;s payment instrument (e.g. low account balance). You can analyze the &#x60;.transaction_data&#x60; to get information on reason of the failure.  - --  &#x60;cancelled&#x60;: Purchase was cancelled using the &#x60;POST /purchases/{id}/cancel/&#x60; endpoint; payment for it is not possible anymore.  - --  &#x60;overdue&#x60;: Purchase is past its&#39; &#x60;.due&#x60;, but payment for it is still possible (unless e.g. POST /purchases/{id}/cancel/ is used).  - --  &#x60;expired&#x60;: Purchase is past its&#39; &#x60;.due&#x60; and payment for it isn&#39;t possible anymore (as a result of &#x60;purchase.due_strict&#x60; having been set to &#x60;true&#x60;).  - --  &#x60;blocked&#x60;: Like &#x60;error&#x60;, but payment attempt was blocked due to fraud scoring below threshold or other security checks not passing.  - --  &#x60;hold&#x60;: Funds are on hold for this Purchase (&#x60;.skip_capture: true&#x60; was used). You can now run &#x60;POST /capture/&#x60; or &#x60;POST /release/&#x60; for this payment to capture the payment or return funds to the client, respectively.  - --  &#x60;released&#x60;: This Purchase previously had &#x60;hold&#x60; status, but funds have since been released and returned to the customer&#39;s card.  - --  &#x60;pending_release&#x60;: release of funds for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/release/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;pending_capture&#x60;: capture of funds for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/capture/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;preauthorized&#x60;: A preauthorization of a card (authorization of card data without a financial transaction) was executed successfully using this Purchase. See the description of the &#x60;.skip_capture&#x60; field for more details.  - --  &#x60;paid&#x60;: Purchase was successfully paid for.  - --  &#x60;pending_execute&#x60;: Payment (or &#x60;hold&#x60; in case of &#x60;skip_capture&#x60;) for this Purchase is in processing, but is not finalized on the acquirer side yet.  - --  &#x60;pending_charge&#x60;: Recurring payment for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/charge/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;cleared&#x60;: Funds for this Purchase (that was already &#x60;paid&#x60;) have been transferred for clearing in payment card network. All non-card payment methods and some card payment methods (depends on configuration) don&#39;t use this status and Purchases paid using them stay in &#x60;paid&#x60; status instead.  - --  &#x60;settled&#x60;: Settlement was issued for funds for this Purchase (that was already &#x60;paid&#x60;). All non-card payment methods and some card payment methods (depends on configuration) don&#39;t use this status and Purchases paid using them stay in &#x60;paid&#x60; status instead.  - --  &#x60;chargeback&#x60;: A chargeback was registered for this, previously paid, Purchase.  - --  &#x60;pending_refund&#x60;: a refund (full or partial) for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/refund/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;refunded&#x60;: This Purchase had its payment refunded, fully or partially.
    /// </summary>
    /// <value>Purchase status. Can have the following values:   &#x60;created&#x60;: Purchase was created using POST /purchases/ or Billing API capabilities.  - --  &#x60;sent&#x60;: Invoice for this purchase was sent over email using Billing API capabilities.  - --  &#x60;viewed&#x60;: The client has viewed the payform and/or invoice details for this purchase.  - --  &#x60;error&#x60;: There was a failed payment attempt for this purchase because of a problem with customer&#39;s payment instrument (e.g. low account balance). You can analyze the &#x60;.transaction_data&#x60; to get information on reason of the failure.  - --  &#x60;cancelled&#x60;: Purchase was cancelled using the &#x60;POST /purchases/{id}/cancel/&#x60; endpoint; payment for it is not possible anymore.  - --  &#x60;overdue&#x60;: Purchase is past its&#39; &#x60;.due&#x60;, but payment for it is still possible (unless e.g. POST /purchases/{id}/cancel/ is used).  - --  &#x60;expired&#x60;: Purchase is past its&#39; &#x60;.due&#x60; and payment for it isn&#39;t possible anymore (as a result of &#x60;purchase.due_strict&#x60; having been set to &#x60;true&#x60;).  - --  &#x60;blocked&#x60;: Like &#x60;error&#x60;, but payment attempt was blocked due to fraud scoring below threshold or other security checks not passing.  - --  &#x60;hold&#x60;: Funds are on hold for this Purchase (&#x60;.skip_capture: true&#x60; was used). You can now run &#x60;POST /capture/&#x60; or &#x60;POST /release/&#x60; for this payment to capture the payment or return funds to the client, respectively.  - --  &#x60;released&#x60;: This Purchase previously had &#x60;hold&#x60; status, but funds have since been released and returned to the customer&#39;s card.  - --  &#x60;pending_release&#x60;: release of funds for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/release/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;pending_capture&#x60;: capture of funds for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/capture/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;preauthorized&#x60;: A preauthorization of a card (authorization of card data without a financial transaction) was executed successfully using this Purchase. See the description of the &#x60;.skip_capture&#x60; field for more details.  - --  &#x60;paid&#x60;: Purchase was successfully paid for.  - --  &#x60;pending_execute&#x60;: Payment (or &#x60;hold&#x60; in case of &#x60;skip_capture&#x60;) for this Purchase is in processing, but is not finalized on the acquirer side yet.  - --  &#x60;pending_charge&#x60;: Recurring payment for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/charge/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;cleared&#x60;: Funds for this Purchase (that was already &#x60;paid&#x60;) have been transferred for clearing in payment card network. All non-card payment methods and some card payment methods (depends on configuration) don&#39;t use this status and Purchases paid using them stay in &#x60;paid&#x60; status instead.  - --  &#x60;settled&#x60;: Settlement was issued for funds for this Purchase (that was already &#x60;paid&#x60;). All non-card payment methods and some card payment methods (depends on configuration) don&#39;t use this status and Purchases paid using them stay in &#x60;paid&#x60; status instead.  - --  &#x60;chargeback&#x60;: A chargeback was registered for this, previously paid, Purchase.  - --  &#x60;pending_refund&#x60;: a refund (full or partial) for this Purchase is in processing, but is not finalized on the acquirer side yet. Is set by &#x60;POST /purchases/{id}/refund/&#x60; operation when it takes longer than expected to process on the acquirer side.  - --  &#x60;refunded&#x60;: This Purchase had its payment refunded, fully or partially.</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum PurchaseStatus
    {
        /// <summary>
        /// Enum Created for value: created
        /// </summary>
        [EnumMember(Value = "created")]
        Created = 1,

        /// <summary>
        /// Enum Sent for value: sent
        /// </summary>
        [EnumMember(Value = "sent")]
        Sent = 2,

        /// <summary>
        /// Enum Viewed for value: viewed
        /// </summary>
        [EnumMember(Value = "viewed")]
        Viewed = 3,

        /// <summary>
        /// Enum Error for value: error
        /// </summary>
        [EnumMember(Value = "error")]
        Error = 4,

        /// <summary>
        /// Enum Cancelled for value: cancelled
        /// </summary>
        [EnumMember(Value = "cancelled")]
        Cancelled = 5,

        /// <summary>
        /// Enum Overdue for value: overdue
        /// </summary>
        [EnumMember(Value = "overdue")]
        Overdue = 6,

        /// <summary>
        /// Enum Expired for value: expired
        /// </summary>
        [EnumMember(Value = "expired")]
        Expired = 7,

        /// <summary>
        /// Enum Blocked for value: blocked
        /// </summary>
        [EnumMember(Value = "blocked")]
        Blocked = 8,

        /// <summary>
        /// Enum Hold for value: hold
        /// </summary>
        [EnumMember(Value = "hold")]
        Hold = 9,

        /// <summary>
        /// Enum Released for value: released
        /// </summary>
        [EnumMember(Value = "released")]
        Released = 10,

        /// <summary>
        /// Enum Pendingrelease for value: pending_release
        /// </summary>
        [EnumMember(Value = "pending_release")]
        Pendingrelease = 11,

        /// <summary>
        /// Enum Pendingcapture for value: pending_capture
        /// </summary>
        [EnumMember(Value = "pending_capture")]
        Pendingcapture = 12,

        /// <summary>
        /// Enum Preauthorized for value: preauthorized
        /// </summary>
        [EnumMember(Value = "preauthorized")]
        Preauthorized = 13,

        /// <summary>
        /// Enum Paid for value: paid
        /// </summary>
        [EnumMember(Value = "paid")]
        Paid = 14,

        /// <summary>
        /// Enum Pendingexecute for value: pending_execute
        /// </summary>
        [EnumMember(Value = "pending_execute")]
        Pendingexecute = 15,

        /// <summary>
        /// Enum Pendingcharge for value: pending_charge
        /// </summary>
        [EnumMember(Value = "pending_charge")]
        Pendingcharge = 16,

        /// <summary>
        /// Enum Cleared for value: cleared
        /// </summary>
        [EnumMember(Value = "cleared")]
        Cleared = 17,

        /// <summary>
        /// Enum Settled for value: settled
        /// </summary>
        [EnumMember(Value = "settled")]
        Settled = 18,

        /// <summary>
        /// Enum Chargeback for value: chargeback
        /// </summary>
        [EnumMember(Value = "chargeback")]
        Chargeback = 19,

        /// <summary>
        /// Enum Pendingrefund for value: pending_refund
        /// </summary>
        [EnumMember(Value = "pending_refund")]
        Pendingrefund = 20,

        /// <summary>
        /// Enum Refunded for value: refunded
        /// </summary>
        [EnumMember(Value = "refunded")]
        Refunded = 21

    }

}
