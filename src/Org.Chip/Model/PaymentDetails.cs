

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
    /// Details of an executed transaction. Read-only for &#x60;Purchase&#x60;s and &#x60;Payout&#x60;s. For an unpaid &#x60;Purchase&#x60;, this object will be &#x60;null&#x60;.
    /// </summary>
    [DataContract(Name = "PaymentDetails")]
    public partial class PaymentDetails : IEquatable<PaymentDetails>, IValidatableObject
    {
        /// <summary>
        /// Defines PaymentType
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PaymentTypeEnum
        {
            /// <summary>
            /// Enum Purchase for value: purchase
            /// </summary>
            [EnumMember(Value = "purchase")]
            Purchase = 1,

            /// <summary>
            /// Enum Purchasecharge for value: purchase_charge
            /// </summary>
            [EnumMember(Value = "purchase_charge")]
            Purchasecharge = 2,

            /// <summary>
            /// Enum Payout for value: payout
            /// </summary>
            [EnumMember(Value = "payout")]
            Payout = 3,

            /// <summary>
            /// Enum Bankpayment for value: bank_payment
            /// </summary>
            [EnumMember(Value = "bank_payment")]
            Bankpayment = 4,

            /// <summary>
            /// Enum Refund for value: refund
            /// </summary>
            [EnumMember(Value = "refund")]
            Refund = 5,

            /// <summary>
            /// Enum Custom for value: custom
            /// </summary>
            [EnumMember(Value = "custom")]
            Custom = 6

        }

        /// <summary>
        /// Gets or Sets PaymentType
        /// </summary>
        [DataMember(Name = "payment_type", EmitDefaultValue = false)]
        public PaymentTypeEnum? PaymentType { get; set; }

        /// <summary>
        /// Returns false as PaymentType should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePaymentType()
        {
            return false;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentDetails" /> class.
        /// </summary>
        /// <param name="isOutgoing">Denotes the direction of payment, e.g. for a paid Purchase, is granted to be &#x60;false&#x60;, &#x60;true&#x60; for payouts. (default to false).</param>
        /// <param name="amount">Amount of money as the smallest indivisible units of the currency. Examples: 1 cent for EUR and 1 Yen for JPY..</param>
        /// <param name="currency">Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;)..</param>
        /// <param name="description">description.</param>
        public PaymentDetails(bool isOutgoing = false, int amount = default(int), string currency = default(string), string description = default(string))
        {
            this.IsOutgoing = isOutgoing;
            this.Amount = amount;
            this.Currency = currency;
            this.Description = description;
        }

        /// <summary>
        /// Denotes the direction of payment, e.g. for a paid Purchase, is granted to be &#x60;false&#x60;, &#x60;true&#x60; for payouts.
        /// </summary>
        /// <value>Denotes the direction of payment, e.g. for a paid Purchase, is granted to be &#x60;false&#x60;, &#x60;true&#x60; for payouts.</value>
        [DataMember(Name = "is_outgoing", EmitDefaultValue = false)]
        public bool IsOutgoing { get; set; }

        /// <summary>
        /// Amount of money as the smallest indivisible units of the currency. Examples: 1 cent for EUR and 1 Yen for JPY.
        /// </summary>
        /// <value>Amount of money as the smallest indivisible units of the currency. Examples: 1 cent for EUR and 1 Yen for JPY.</value>
        [DataMember(Name = "amount", EmitDefaultValue = false)]
        public int Amount { get; set; }

        /// <summary>
        /// Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;).
        /// </summary>
        /// <value>Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;).</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Gets or Sets NetAmount
        /// </summary>
        [DataMember(Name = "net_amount", EmitDefaultValue = false)]
        public int NetAmount { get; private set; }

        /// <summary>
        /// Returns false as NetAmount should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeNetAmount()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets FeeAmount
        /// </summary>
        [DataMember(Name = "fee_amount", EmitDefaultValue = false)]
        public int FeeAmount { get; private set; }

        /// <summary>
        /// Returns false as FeeAmount should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeFeeAmount()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets PendingAmount
        /// </summary>
        [DataMember(Name = "pending_amount", EmitDefaultValue = false)]
        public int PendingAmount { get; private set; }

        /// <summary>
        /// Returns false as PendingAmount should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePendingAmount()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets PendingUnfreezeOn
        /// </summary>
        [DataMember(Name = "pending_unfreeze_on", EmitDefaultValue = true)]
        public int? PendingUnfreezeOn { get; private set; }

        /// <summary>
        /// Returns false as PendingUnfreezeOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePendingUnfreezeOn()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// When the payment was accepted in (&#x60;is_outgoing &#x3D;&#x3D; false&#x60;) or sent from (&#x60;is_outgoing &#x3D;&#x3D; true&#x60;) the gateway system.
        /// </summary>
        /// <value>When the payment was accepted in (&#x60;is_outgoing &#x3D;&#x3D; false&#x60;) or sent from (&#x60;is_outgoing &#x3D;&#x3D; true&#x60;) the gateway system.</value>
        [DataMember(Name = "paid_on", EmitDefaultValue = false)]
        public int PaidOn { get; private set; }

        /// <summary>
        /// Returns false as PaidOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePaidOn()
        {
            return false;
        }

        /// <summary>
        /// If available, this field will report the date the payment was sent by the remote payer (&#x60;is_outgoing &#x3D;&#x3D; false&#x60;) or when funds arrived to the remote beneficiary (&#x60;is_outgoing &#x3D;&#x3D; true&#x60;).
        /// </summary>
        /// <value>If available, this field will report the date the payment was sent by the remote payer (&#x60;is_outgoing &#x3D;&#x3D; false&#x60;) or when funds arrived to the remote beneficiary (&#x60;is_outgoing &#x3D;&#x3D; true&#x60;).</value>
        [DataMember(Name = "remote_paid_on", EmitDefaultValue = false)]
        public int RemotePaidOn { get; private set; }

        /// <summary>
        /// Returns false as RemotePaidOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRemotePaidOn()
        {
            return false;
        }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PaymentDetails {\n");
            sb.Append("  IsOutgoing: ").Append(IsOutgoing).Append("\n");
            sb.Append("  PaymentType: ").Append(PaymentType).Append("\n");
            sb.Append("  Amount: ").Append(Amount).Append("\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  NetAmount: ").Append(NetAmount).Append("\n");
            sb.Append("  FeeAmount: ").Append(FeeAmount).Append("\n");
            sb.Append("  PendingAmount: ").Append(PendingAmount).Append("\n");
            sb.Append("  PendingUnfreezeOn: ").Append(PendingUnfreezeOn).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  PaidOn: ").Append(PaidOn).Append("\n");
            sb.Append("  RemotePaidOn: ").Append(RemotePaidOn).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as PaymentDetails);
        }

        /// <summary>
        /// Returns true if PaymentDetails instances are equal
        /// </summary>
        /// <param name="input">Instance of PaymentDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PaymentDetails input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.IsOutgoing == input.IsOutgoing ||
                    this.IsOutgoing.Equals(input.IsOutgoing)
                ) && 
                (
                    this.PaymentType == input.PaymentType ||
                    this.PaymentType.Equals(input.PaymentType)
                ) && 
                (
                    this.Amount == input.Amount ||
                    this.Amount.Equals(input.Amount)
                ) && 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.NetAmount == input.NetAmount ||
                    this.NetAmount.Equals(input.NetAmount)
                ) && 
                (
                    this.FeeAmount == input.FeeAmount ||
                    this.FeeAmount.Equals(input.FeeAmount)
                ) && 
                (
                    this.PendingAmount == input.PendingAmount ||
                    this.PendingAmount.Equals(input.PendingAmount)
                ) && 
                (
                    this.PendingUnfreezeOn == input.PendingUnfreezeOn ||
                    (this.PendingUnfreezeOn != null &&
                    this.PendingUnfreezeOn.Equals(input.PendingUnfreezeOn))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) && 
                (
                    this.PaidOn == input.PaidOn ||
                    this.PaidOn.Equals(input.PaidOn)
                ) && 
                (
                    this.RemotePaidOn == input.RemotePaidOn ||
                    this.RemotePaidOn.Equals(input.RemotePaidOn)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                hashCode = hashCode * 59 + this.IsOutgoing.GetHashCode();
                hashCode = hashCode * 59 + this.PaymentType.GetHashCode();
                hashCode = hashCode * 59 + this.Amount.GetHashCode();
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                hashCode = hashCode * 59 + this.NetAmount.GetHashCode();
                hashCode = hashCode * 59 + this.FeeAmount.GetHashCode();
                hashCode = hashCode * 59 + this.PendingAmount.GetHashCode();
                if (this.PendingUnfreezeOn != null)
                    hashCode = hashCode * 59 + this.PendingUnfreezeOn.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                hashCode = hashCode * 59 + this.PaidOn.GetHashCode();
                hashCode = hashCode * 59 + this.RemotePaidOn.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            // Currency (string) maxLength
            if(this.Currency != null && this.Currency.Length > 3)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Currency, length must be less than 3.", new [] { "Currency" });
            }

            // Description (string) maxLength
            if(this.Description != null && this.Description.Length > 256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Description, length must be less than 256.", new [] { "Description" });
            }

            yield break;
        }
    }

}
