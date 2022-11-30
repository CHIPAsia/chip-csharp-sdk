

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
    /// Connects a Client object to a BillingTemplate having &#x60;is_subscription &#x3D; true&#x60; to store information about a single subscriber.   You will be able to pause an individual subscription client&#39;s cycle by PATCH-ing its&#39; &#x60;status&#x60; field to the value of &#x60;subscription_paused&#x60;.
    /// </summary>
    [DataContract(Name = "BillingTemplateClient")]
    public partial class BillingTemplateClient : IEquatable<BillingTemplateClient>, IValidatableObject
    {
        /// <summary>
        /// For subscriptions, you can edit (&#x60;PATCH /billing_templates/{id}/clients/{id}/&#x60;) this status between &#x60;active&#x60; and &#x60;subscription_paused&#x60; values to pause the client&#39;s subscription. Paused subscriptions run as normal, except for purchases not being created and invoices sent for them. It means that if you pause a BillingTemplateClient&#39;s monthly subscription cycle a day before the billing date, the next day the invoice will not be issued; but, if you unpause the client a day after the planned billing would have taken place, the planned billing in a month (minus one day) will happen as usual.  Read-only if the BillingTemplateClient is in &#x60;inactive&#x60; (internal status not managed through public API) or &#x60;pending&#x60; (see documentation for &#x60;POST /billing_templates/{id}/add_subscriber/&#x60;) statuses.
        /// </summary>
        /// <value>For subscriptions, you can edit (&#x60;PATCH /billing_templates/{id}/clients/{id}/&#x60;) this status between &#x60;active&#x60; and &#x60;subscription_paused&#x60; values to pause the client&#39;s subscription. Paused subscriptions run as normal, except for purchases not being created and invoices sent for them. It means that if you pause a BillingTemplateClient&#39;s monthly subscription cycle a day before the billing date, the next day the invoice will not be issued; but, if you unpause the client a day after the planned billing would have taken place, the planned billing in a month (minus one day) will happen as usual.  Read-only if the BillingTemplateClient is in &#x60;inactive&#x60; (internal status not managed through public API) or &#x60;pending&#x60; (see documentation for &#x60;POST /billing_templates/{id}/add_subscriber/&#x60;) statuses.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum StatusEnum
        {
            /// <summary>
            /// Enum Pending for value: pending
            /// </summary>
            [EnumMember(Value = "pending")]
            Pending = 1,

            /// <summary>
            /// Enum Inactive for value: inactive
            /// </summary>
            [EnumMember(Value = "inactive")]
            Inactive = 2,

            /// <summary>
            /// Enum Active for value: active
            /// </summary>
            [EnumMember(Value = "active")]
            Active = 3,

            /// <summary>
            /// Enum Subscriptionpaused for value: subscription_paused
            /// </summary>
            [EnumMember(Value = "subscription_paused")]
            Subscriptionpaused = 4

        }

        /// <summary>
        /// For subscriptions, you can edit (&#x60;PATCH /billing_templates/{id}/clients/{id}/&#x60;) this status between &#x60;active&#x60; and &#x60;subscription_paused&#x60; values to pause the client&#39;s subscription. Paused subscriptions run as normal, except for purchases not being created and invoices sent for them. It means that if you pause a BillingTemplateClient&#39;s monthly subscription cycle a day before the billing date, the next day the invoice will not be issued; but, if you unpause the client a day after the planned billing would have taken place, the planned billing in a month (minus one day) will happen as usual.  Read-only if the BillingTemplateClient is in &#x60;inactive&#x60; (internal status not managed through public API) or &#x60;pending&#x60; (see documentation for &#x60;POST /billing_templates/{id}/add_subscriber/&#x60;) statuses.
        /// </summary>
        /// <value>For subscriptions, you can edit (&#x60;PATCH /billing_templates/{id}/clients/{id}/&#x60;) this status between &#x60;active&#x60; and &#x60;subscription_paused&#x60; values to pause the client&#39;s subscription. Paused subscriptions run as normal, except for purchases not being created and invoices sent for them. It means that if you pause a BillingTemplateClient&#39;s monthly subscription cycle a day before the billing date, the next day the invoice will not be issued; but, if you unpause the client a day after the planned billing would have taken place, the planned billing in a month (minus one day) will happen as usual.  Read-only if the BillingTemplateClient is in &#x60;inactive&#x60; (internal status not managed through public API) or &#x60;pending&#x60; (see documentation for &#x60;POST /billing_templates/{id}/add_subscriber/&#x60;) statuses.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public StatusEnum? Status { get; set; }

        /// <summary>
        /// Returns false as Status should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeStatus()
        {
            return false;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingTemplateClient" /> class.
        /// </summary>
        /// <param name="clientId">ID of the Client object to add to the BillingTemplate. Read-only after the BillingTemplateClient has been created. Note that the same Client can be added to a BillingTemplate several times. (required).</param>
        /// <param name="paymentMethodWhitelist">An optional whitelist of payment methods availble for purchases generated for this BillingTemplateClient. Copied 1:1 to &#x60;Purchase.payment_method_whitelist&#x60; field on created Purchases (see its description)..</param>
        public BillingTemplateClient(Guid clientId = default(Guid), List<string> paymentMethodWhitelist = default(List<string>))
        {
            this.ClientId = clientId;
            this.PaymentMethodWhitelist = paymentMethodWhitelist;
        }

        /// <summary>
        /// ID of the Client object to add to the BillingTemplate. Read-only after the BillingTemplateClient has been created. Note that the same Client can be added to a BillingTemplate several times.
        /// </summary>
        /// <value>ID of the Client object to add to the BillingTemplate. Read-only after the BillingTemplateClient has been created. Note that the same Client can be added to a BillingTemplate several times.</value>
        [DataMember(Name = "client_id", IsRequired = true, EmitDefaultValue = false)]
        public Guid ClientId { get; set; }

        /// <summary>
        /// If not null, reports the date when the next billing is scheduled for this client.
        /// </summary>
        /// <value>If not null, reports the date when the next billing is scheduled for this client.</value>
        [DataMember(Name = "subscription_billing_scheduled_on", EmitDefaultValue = true)]
        public string SubscriptionBillingScheduledOn { get; private set; }

        /// <summary>
        /// Returns false as SubscriptionBillingScheduledOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeSubscriptionBillingScheduledOn()
        {
            return false;
        }

        /// <summary>
        /// An optional whitelist of payment methods availble for purchases generated for this BillingTemplateClient. Copied 1:1 to &#x60;Purchase.payment_method_whitelist&#x60; field on created Purchases (see its description).
        /// </summary>
        /// <value>An optional whitelist of payment methods availble for purchases generated for this BillingTemplateClient. Copied 1:1 to &#x60;Purchase.payment_method_whitelist&#x60; field on created Purchases (see its description).</value>
        [DataMember(Name = "payment_method_whitelist", EmitDefaultValue = false)]
        public List<string> PaymentMethodWhitelist { get; set; }

        /// <summary>
        /// Object type identifier
        /// </summary>
        /// <value>Object type identifier</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; private set; }

        /// <summary>
        /// Returns false as Type should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeType()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Returns false as Id should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeId()
        {
            return false;
        }

        /// <summary>
        /// Object creation time
        /// </summary>
        /// <value>Object creation time</value>
        [DataMember(Name = "created_on", EmitDefaultValue = false)]
        public int CreatedOn { get; private set; }

        /// <summary>
        /// Returns false as CreatedOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCreatedOn()
        {
            return false;
        }

        /// <summary>
        /// Object last modification time
        /// </summary>
        /// <value>Object last modification time</value>
        [DataMember(Name = "updated_on", EmitDefaultValue = false)]
        public int UpdatedOn { get; private set; }

        /// <summary>
        /// Returns false as UpdatedOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUpdatedOn()
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
            sb.Append("class BillingTemplateClient {\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  SubscriptionBillingScheduledOn: ").Append(SubscriptionBillingScheduledOn).Append("\n");
            sb.Append("  PaymentMethodWhitelist: ").Append(PaymentMethodWhitelist).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
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
            return this.Equals(input as BillingTemplateClient);
        }

        /// <summary>
        /// Returns true if BillingTemplateClient instances are equal
        /// </summary>
        /// <param name="input">Instance of BillingTemplateClient to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BillingTemplateClient input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.ClientId == input.ClientId ||
                    (this.ClientId != null &&
                    this.ClientId.Equals(input.ClientId))
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.SubscriptionBillingScheduledOn == input.SubscriptionBillingScheduledOn ||
                    (this.SubscriptionBillingScheduledOn != null &&
                    this.SubscriptionBillingScheduledOn.Equals(input.SubscriptionBillingScheduledOn))
                ) && 
                (
                    this.PaymentMethodWhitelist == input.PaymentMethodWhitelist ||
                    this.PaymentMethodWhitelist != null &&
                    input.PaymentMethodWhitelist != null &&
                    this.PaymentMethodWhitelist.SequenceEqual(input.PaymentMethodWhitelist)
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CreatedOn == input.CreatedOn ||
                    this.CreatedOn.Equals(input.CreatedOn)
                ) && 
                (
                    this.UpdatedOn == input.UpdatedOn ||
                    this.UpdatedOn.Equals(input.UpdatedOn)
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
                if (this.ClientId != null)
                    hashCode = hashCode * 59 + this.ClientId.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.SubscriptionBillingScheduledOn != null)
                    hashCode = hashCode * 59 + this.SubscriptionBillingScheduledOn.GetHashCode();
                if (this.PaymentMethodWhitelist != null)
                    hashCode = hashCode * 59 + this.PaymentMethodWhitelist.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.CreatedOn.GetHashCode();
                hashCode = hashCode * 59 + this.UpdatedOn.GetHashCode();
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
            yield break;
        }
    }

}
