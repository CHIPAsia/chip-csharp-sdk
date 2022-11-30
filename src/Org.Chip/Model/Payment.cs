

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
    /// A record of a performed financial transaction. Can be generated e.g. as a result of refund operation.
    /// </summary>
    [DataContract(Name = "Payment")]
    public partial class Payment : IEquatable<Payment>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Payment" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public Payment()
        {
        }

        /// <summary>
        /// Gets or Sets _Client
        /// </summary>
        [DataMember(Name = "client", EmitDefaultValue = false)]
        public ClientDetails _Client { get; private set; }

        /// <summary>
        /// Returns false as _Client should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerialize_Client()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets _Payment
        /// </summary>
        [DataMember(Name = "payment", EmitDefaultValue = false)]
        public PaymentDetails _Payment { get; private set; }

        /// <summary>
        /// Returns false as _Payment should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerialize_Payment()
        {
            return false;
        }

        /// <summary>
        /// Payment method-specific, read-only, internal transaction data. Will contain information about all the transaction attempts, if available.
        /// </summary>
        /// <value>Payment method-specific, read-only, internal transaction data. Will contain information about all the transaction attempts, if available.</value>
        [DataMember(Name = "transaction_data", EmitDefaultValue = true)]
        public Object TransactionData { get; private set; }

        /// <summary>
        /// Returns false as TransactionData should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeTransactionData()
        {
            return false;
        }

        /// <summary>
        /// The object type and id this object is related to, if any. E.g. refund Payments are related to a specific Purchase, so this object will contain &#x60;type: purchase&#x60; and &#x60;id: &lt;purchase&#39;s id&gt;&#x60;.
        /// </summary>
        /// <value>The object type and id this object is related to, if any. E.g. refund Payments are related to a specific Purchase, so this object will contain &#x60;type: purchase&#x60; and &#x60;id: &lt;purchase&#39;s id&gt;&#x60;.</value>
        [DataMember(Name = "related_to", EmitDefaultValue = true)]
        public Object RelatedTo { get; private set; }

        /// <summary>
        /// Returns false as RelatedTo should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRelatedTo()
        {
            return false;
        }

        /// <summary>
        /// If an explicit invoice &#x60;reference&#x60; wasn&#39;t provided, this autogenerated value will be used as a reference instead.
        /// </summary>
        /// <value>If an explicit invoice &#x60;reference&#x60; wasn&#39;t provided, this autogenerated value will be used as a reference instead.</value>
        [DataMember(Name = "reference_generated", EmitDefaultValue = false)]
        public string ReferenceGenerated { get; private set; }

        /// <summary>
        /// Returns false as ReferenceGenerated should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeReferenceGenerated()
        {
            return false;
        }

        /// <summary>
        /// Invoice reference.
        /// </summary>
        /// <value>Invoice reference.</value>
        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public string Reference { get; private set; }

        /// <summary>
        /// Returns false as Reference should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeReference()
        {
            return false;
        }

        /// <summary>
        /// ID of an account this Payment is associated with.
        /// </summary>
        /// <value>ID of an account this Payment is associated with.</value>
        [DataMember(Name = "account_id", EmitDefaultValue = false)]
        public Guid AccountId { get; private set; }

        /// <summary>
        /// Returns false as AccountId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeAccountId()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets CompanyId
        /// </summary>
        [DataMember(Name = "company_id", EmitDefaultValue = false)]
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// Returns false as CompanyId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCompanyId()
        {
            return false;
        }

        /// <summary>
        /// Indicates this is a test object, created using test API keys or using Billing section of UI while in test mode.
        /// </summary>
        /// <value>Indicates this is a test object, created using test API keys or using Billing section of UI while in test mode.</value>
        [DataMember(Name = "is_test", EmitDefaultValue = false)]
        public bool IsTest { get; private set; }

        /// <summary>
        /// Returns false as IsTest should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeIsTest()
        {
            return false;
        }

        /// <summary>
        /// ID of user who has created this object in the Billing UI, if applicable.
        /// </summary>
        /// <value>ID of user who has created this object in the Billing UI, if applicable.</value>
        [DataMember(Name = "user_id", EmitDefaultValue = true)]
        public Guid? UserId { get; private set; }

        /// <summary>
        /// Returns false as UserId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUserId()
        {
            return false;
        }

        /// <summary>
        /// ID of the brand this Payment is associated with.
        /// </summary>
        /// <value>ID of the brand this Payment is associated with.</value>
        [DataMember(Name = "brand_id", EmitDefaultValue = false)]
        public Guid BrandId { get; private set; }

        /// <summary>
        /// Returns false as BrandId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeBrandId()
        {
            return false;
        }

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
            sb.Append("class Payment {\n");
            sb.Append("  _Client: ").Append(_Client).Append("\n");
            sb.Append("  _Payment: ").Append(_Payment).Append("\n");
            sb.Append("  TransactionData: ").Append(TransactionData).Append("\n");
            sb.Append("  RelatedTo: ").Append(RelatedTo).Append("\n");
            sb.Append("  ReferenceGenerated: ").Append(ReferenceGenerated).Append("\n");
            sb.Append("  Reference: ").Append(Reference).Append("\n");
            sb.Append("  AccountId: ").Append(AccountId).Append("\n");
            sb.Append("  CompanyId: ").Append(CompanyId).Append("\n");
            sb.Append("  IsTest: ").Append(IsTest).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  BrandId: ").Append(BrandId).Append("\n");
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
            return this.Equals(input as Payment);
        }

        /// <summary>
        /// Returns true if Payment instances are equal
        /// </summary>
        /// <param name="input">Instance of Payment to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Payment input)
        {
            if (input == null)
                return false;

            return 
                (
                    this._Client == input._Client ||
                    (this._Client != null &&
                    this._Client.Equals(input._Client))
                ) && 
                (
                    this._Payment == input._Payment ||
                    (this._Payment != null &&
                    this._Payment.Equals(input._Payment))
                ) && 
                (
                    this.TransactionData == input.TransactionData ||
                    (this.TransactionData != null &&
                    this.TransactionData.Equals(input.TransactionData))
                ) && 
                (
                    this.RelatedTo == input.RelatedTo ||
                    (this.RelatedTo != null &&
                    this.RelatedTo.Equals(input.RelatedTo))
                ) && 
                (
                    this.ReferenceGenerated == input.ReferenceGenerated ||
                    (this.ReferenceGenerated != null &&
                    this.ReferenceGenerated.Equals(input.ReferenceGenerated))
                ) && 
                (
                    this.Reference == input.Reference ||
                    (this.Reference != null &&
                    this.Reference.Equals(input.Reference))
                ) && 
                (
                    this.AccountId == input.AccountId ||
                    (this.AccountId != null &&
                    this.AccountId.Equals(input.AccountId))
                ) && 
                (
                    this.CompanyId == input.CompanyId ||
                    (this.CompanyId != null &&
                    this.CompanyId.Equals(input.CompanyId))
                ) && 
                (
                    this.IsTest == input.IsTest ||
                    this.IsTest.Equals(input.IsTest)
                ) && 
                (
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
                ) && 
                (
                    this.BrandId == input.BrandId ||
                    (this.BrandId != null &&
                    this.BrandId.Equals(input.BrandId))
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
                if (this._Client != null)
                    hashCode = hashCode * 59 + this._Client.GetHashCode();
                if (this._Payment != null)
                    hashCode = hashCode * 59 + this._Payment.GetHashCode();
                if (this.TransactionData != null)
                    hashCode = hashCode * 59 + this.TransactionData.GetHashCode();
                if (this.RelatedTo != null)
                    hashCode = hashCode * 59 + this.RelatedTo.GetHashCode();
                if (this.ReferenceGenerated != null)
                    hashCode = hashCode * 59 + this.ReferenceGenerated.GetHashCode();
                if (this.Reference != null)
                    hashCode = hashCode * 59 + this.Reference.GetHashCode();
                if (this.AccountId != null)
                    hashCode = hashCode * 59 + this.AccountId.GetHashCode();
                if (this.CompanyId != null)
                    hashCode = hashCode * 59 + this.CompanyId.GetHashCode();
                hashCode = hashCode * 59 + this.IsTest.GetHashCode();
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.BrandId != null)
                    hashCode = hashCode * 59 + this.BrandId.GetHashCode();
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
            // Reference (string) maxLength
            if(this.Reference != null && this.Reference.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Reference, length must be less than 128.", new [] { "Reference" });
            }

            yield break;
        }
    }

}