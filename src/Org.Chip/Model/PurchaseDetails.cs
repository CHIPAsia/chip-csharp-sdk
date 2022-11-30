

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
    /// Core information about the Purchase, including the products, total, currency and invoice fields. If you&#39;re using invoicing via &#x60;/billing/&#x60; or &#x60;/billing_templates/&#x60;, this object will be copied 1:1 from BillingTemplate you specify to the resulting Purchases (also to subscription Purchases).
    /// </summary>
    [DataContract(Name = "PurchaseDetails")]
    public partial class PurchaseDetails : IEquatable<PurchaseDetails>, IValidatableObject
    {
        /// <summary>
        /// Defines RequestClientDetails
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RequestClientDetailsEnum
        {
            /// <summary>
            /// Enum Email for value: email
            /// </summary>
            [EnumMember(Value = "email")]
            Email = 1,

            /// <summary>
            /// Enum Phone for value: phone
            /// </summary>
            [EnumMember(Value = "phone")]
            Phone = 2,

            /// <summary>
            /// Enum Fullname for value: full_name
            /// </summary>
            [EnumMember(Value = "full_name")]
            Fullname = 3,

            /// <summary>
            /// Enum Personalcode for value: personal_code
            /// </summary>
            [EnumMember(Value = "personal_code")]
            Personalcode = 4,

            /// <summary>
            /// Enum Brandname for value: brand_name
            /// </summary>
            [EnumMember(Value = "brand_name")]
            Brandname = 5,

            /// <summary>
            /// Enum Legalname for value: legal_name
            /// </summary>
            [EnumMember(Value = "legal_name")]
            Legalname = 6,

            /// <summary>
            /// Enum Registrationnumber for value: registration_number
            /// </summary>
            [EnumMember(Value = "registration_number")]
            Registrationnumber = 7,

            /// <summary>
            /// Enum Taxnumber for value: tax_number
            /// </summary>
            [EnumMember(Value = "tax_number")]
            Taxnumber = 8,

            /// <summary>
            /// Enum Country for value: country
            /// </summary>
            [EnumMember(Value = "country")]
            Country = 9,

            /// <summary>
            /// Enum City for value: city
            /// </summary>
            [EnumMember(Value = "city")]
            City = 10,

            /// <summary>
            /// Enum Streetaddress for value: street_address
            /// </summary>
            [EnumMember(Value = "street_address")]
            Streetaddress = 11,

            /// <summary>
            /// Enum Zipcode for value: zip_code
            /// </summary>
            [EnumMember(Value = "zip_code")]
            Zipcode = 12,

            /// <summary>
            /// Enum Bankaccount for value: bank_account
            /// </summary>
            [EnumMember(Value = "bank_account")]
            Bankaccount = 13,

            /// <summary>
            /// Enum Bankcode for value: bank_code
            /// </summary>
            [EnumMember(Value = "bank_code")]
            Bankcode = 14,

            /// <summary>
            /// Enum Shippingcountry for value: shipping_country
            /// </summary>
            [EnumMember(Value = "shipping_country")]
            Shippingcountry = 15,

            /// <summary>
            /// Enum Shippingcity for value: shipping_city
            /// </summary>
            [EnumMember(Value = "shipping_city")]
            Shippingcity = 16,

            /// <summary>
            /// Enum Shippingstreetaddress for value: shipping_street_address
            /// </summary>
            [EnumMember(Value = "shipping_street_address")]
            Shippingstreetaddress = 17,

            /// <summary>
            /// Enum Shippingzipcode for value: shipping_zip_code
            /// </summary>
            [EnumMember(Value = "shipping_zip_code")]
            Shippingzipcode = 18

        }


        /// <summary>
        /// ClientDetails fields to request from the client before the payment. If a value is passed for a field in ClientDetails, it will be automatically removed from this list.
        /// </summary>
        /// <value>ClientDetails fields to request from the client before the payment. If a value is passed for a field in ClientDetails, it will be automatically removed from this list.</value>
        [DataMember(Name = "request_client_details", EmitDefaultValue = false)]
        public List<RequestClientDetailsEnum> RequestClientDetails { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDetails" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected PurchaseDetails() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PurchaseDetails" /> class.
        /// </summary>
        /// <param name="currency">Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;)..</param>
        /// <param name="products">Line items of the invoice. In case of a transaction with no invoice sent, specify a single Product forming the cost of transaction. (required).</param>
        /// <param name="language">Language code in the ISO 639-1 format (e.g. &#39;en&#39;) (default to &quot;Default value is controlled in Company -&gt; Brand section of merchant portal separately per each Brand used (default value, if no changes are made, is &#x60;en&#x60;). Brand to be used with corresponding Purchase/BillingTemplate specified using brand_id.&quot;).</param>
        /// <param name="notes">notes.</param>
        /// <param name="debt">debt.</param>
        /// <param name="subtotalOverride">subtotalOverride.</param>
        /// <param name="totalTaxOverride">totalTaxOverride.</param>
        /// <param name="totalDiscountOverride">totalDiscountOverride.</param>
        /// <param name="totalOverride">totalOverride.</param>
        /// <param name="requestClientDetails">ClientDetails fields to request from the client before the payment. If a value is passed for a field in ClientDetails, it will be automatically removed from this list..</param>
        /// <param name="timezone">Timezone to localize invoice-specific timestamps in, e.g. to display a concrete date for a &#x60;due&#x60; timestamp on the invoice..</param>
        /// <param name="dueStrict">Whether to permit payments when Purchase&#39;s &#x60;due&#x60; has passed. By default those are permitted (and status will be set to &#x60;overdue&#x60; once &#x60;due&#x60; moment is passed). If this is set to &#x60;true&#x60;, it won&#39;t be possible to pay for an overdue invoice, and when &#x60;due&#x60; is passed the Purchase&#39;s status will be set to &#x60;expired&#x60;. (default to false).</param>
        public PurchaseDetails(string currency = default(string), List<Product> products = default(List<Product>), string language = "en", string notes = default(string), int debt = default(int), int? subtotalOverride = default(int?), int? totalTaxOverride = default(int?), int? totalDiscountOverride = default(int?), int? totalOverride = default(int?), List<RequestClientDetailsEnum> requestClientDetails = default(List<RequestClientDetailsEnum>), string timezone = default(string), bool dueStrict = false)
        {
            // to ensure "products" is required (not null)
            this.Products = products ?? throw new ArgumentNullException("products is a required property for PurchaseDetails and cannot be null");
            this.Currency = currency;
            // use default value if no "language" provided
            this.Language = language ?? "en";
            this.Notes = notes;
            this.Debt = debt;
            this.SubtotalOverride = subtotalOverride;
            this.TotalTaxOverride = totalTaxOverride;
            this.TotalDiscountOverride = totalDiscountOverride;
            this.TotalOverride = totalOverride;
            this.RequestClientDetails = requestClientDetails;
            this.Timezone = timezone;
            this.DueStrict = dueStrict;
        }

        /// <summary>
        /// Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;).
        /// </summary>
        /// <value>Currency code in the ISO 4217 standard (e.g. &#39;EUR&#39;).</value>
        [DataMember(Name = "currency", EmitDefaultValue = false)]
        public string Currency { get; set; }

        /// <summary>
        /// Line items of the invoice. In case of a transaction with no invoice sent, specify a single Product forming the cost of transaction.
        /// </summary>
        /// <value>Line items of the invoice. In case of a transaction with no invoice sent, specify a single Product forming the cost of transaction.</value>
        [DataMember(Name = "products", IsRequired = true, EmitDefaultValue = false)]
        public List<Product> Products { get; set; }

        /// <summary>
        /// Gets or Sets Total
        /// </summary>
        [DataMember(Name = "total", EmitDefaultValue = false)]
        public int Total { get; private set; }

        /// <summary>
        /// Returns false as Total should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeTotal()
        {
            return false;
        }

        /// <summary>
        /// Language code in the ISO 639-1 format (e.g. &#39;en&#39;)
        /// </summary>
        /// <value>Language code in the ISO 639-1 format (e.g. &#39;en&#39;)</value>
        [DataMember(Name = "language", EmitDefaultValue = false)]
        public string Language { get; set; }

        /// <summary>
        /// Gets or Sets Notes
        /// </summary>
        [DataMember(Name = "notes", EmitDefaultValue = false)]
        public string Notes { get; set; }

        /// <summary>
        /// Gets or Sets Debt
        /// </summary>
        [DataMember(Name = "debt", EmitDefaultValue = false)]
        public int Debt { get; set; }

        /// <summary>
        /// Gets or Sets SubtotalOverride
        /// </summary>
        [DataMember(Name = "subtotal_override", EmitDefaultValue = true)]
        public int? SubtotalOverride { get; set; }

        /// <summary>
        /// Gets or Sets TotalTaxOverride
        /// </summary>
        [DataMember(Name = "total_tax_override", EmitDefaultValue = true)]
        public int? TotalTaxOverride { get; set; }

        /// <summary>
        /// Gets or Sets TotalDiscountOverride
        /// </summary>
        [DataMember(Name = "total_discount_override", EmitDefaultValue = true)]
        public int? TotalDiscountOverride { get; set; }

        /// <summary>
        /// Gets or Sets TotalOverride
        /// </summary>
        [DataMember(Name = "total_override", EmitDefaultValue = true)]
        public int? TotalOverride { get; set; }

        /// <summary>
        /// Timezone to localize invoice-specific timestamps in, e.g. to display a concrete date for a &#x60;due&#x60; timestamp on the invoice.
        /// </summary>
        /// <value>Timezone to localize invoice-specific timestamps in, e.g. to display a concrete date for a &#x60;due&#x60; timestamp on the invoice.</value>
        [DataMember(Name = "timezone", EmitDefaultValue = false)]
        public string Timezone { get; set; }

        /// <summary>
        /// Whether to permit payments when Purchase&#39;s &#x60;due&#x60; has passed. By default those are permitted (and status will be set to &#x60;overdue&#x60; once &#x60;due&#x60; moment is passed). If this is set to &#x60;true&#x60;, it won&#39;t be possible to pay for an overdue invoice, and when &#x60;due&#x60; is passed the Purchase&#39;s status will be set to &#x60;expired&#x60;.
        /// </summary>
        /// <value>Whether to permit payments when Purchase&#39;s &#x60;due&#x60; has passed. By default those are permitted (and status will be set to &#x60;overdue&#x60; once &#x60;due&#x60; moment is passed). If this is set to &#x60;true&#x60;, it won&#39;t be possible to pay for an overdue invoice, and when &#x60;due&#x60; is passed the Purchase&#39;s status will be set to &#x60;expired&#x60;.</value>
        [DataMember(Name = "due_strict", EmitDefaultValue = false)]
        public bool DueStrict { get; set; }

        /// <summary>
        /// An optional message to display to your customer in invoice email, e.g. \&quot;Your invoice for June\&quot;.
        /// </summary>
        /// <value>An optional message to display to your customer in invoice email, e.g. \&quot;Your invoice for June\&quot;.</value>
        [DataMember(Name = "email_message", EmitDefaultValue = false)]
        public string EmailMessage { get; private set; }

        /// <summary>
        /// Returns false as EmailMessage should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeEmailMessage()
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
            sb.Append("class PurchaseDetails {\n");
            sb.Append("  Currency: ").Append(Currency).Append("\n");
            sb.Append("  Products: ").Append(Products).Append("\n");
            sb.Append("  Total: ").Append(Total).Append("\n");
            sb.Append("  Language: ").Append(Language).Append("\n");
            sb.Append("  Notes: ").Append(Notes).Append("\n");
            sb.Append("  Debt: ").Append(Debt).Append("\n");
            sb.Append("  SubtotalOverride: ").Append(SubtotalOverride).Append("\n");
            sb.Append("  TotalTaxOverride: ").Append(TotalTaxOverride).Append("\n");
            sb.Append("  TotalDiscountOverride: ").Append(TotalDiscountOverride).Append("\n");
            sb.Append("  TotalOverride: ").Append(TotalOverride).Append("\n");
            sb.Append("  RequestClientDetails: ").Append(RequestClientDetails).Append("\n");
            sb.Append("  Timezone: ").Append(Timezone).Append("\n");
            sb.Append("  DueStrict: ").Append(DueStrict).Append("\n");
            sb.Append("  EmailMessage: ").Append(EmailMessage).Append("\n");
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
            return this.Equals(input as PurchaseDetails);
        }

        /// <summary>
        /// Returns true if PurchaseDetails instances are equal
        /// </summary>
        /// <param name="input">Instance of PurchaseDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PurchaseDetails input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Currency == input.Currency ||
                    (this.Currency != null &&
                    this.Currency.Equals(input.Currency))
                ) && 
                (
                    this.Products == input.Products ||
                    this.Products != null &&
                    input.Products != null &&
                    this.Products.SequenceEqual(input.Products)
                ) && 
                (
                    this.Total == input.Total ||
                    this.Total.Equals(input.Total)
                ) && 
                (
                    this.Language == input.Language ||
                    (this.Language != null &&
                    this.Language.Equals(input.Language))
                ) && 
                (
                    this.Notes == input.Notes ||
                    (this.Notes != null &&
                    this.Notes.Equals(input.Notes))
                ) && 
                (
                    this.Debt == input.Debt ||
                    this.Debt.Equals(input.Debt)
                ) && 
                (
                    this.SubtotalOverride == input.SubtotalOverride ||
                    (this.SubtotalOverride != null &&
                    this.SubtotalOverride.Equals(input.SubtotalOverride))
                ) && 
                (
                    this.TotalTaxOverride == input.TotalTaxOverride ||
                    (this.TotalTaxOverride != null &&
                    this.TotalTaxOverride.Equals(input.TotalTaxOverride))
                ) && 
                (
                    this.TotalDiscountOverride == input.TotalDiscountOverride ||
                    (this.TotalDiscountOverride != null &&
                    this.TotalDiscountOverride.Equals(input.TotalDiscountOverride))
                ) && 
                (
                    this.TotalOverride == input.TotalOverride ||
                    (this.TotalOverride != null &&
                    this.TotalOverride.Equals(input.TotalOverride))
                ) && 
                (
                    this.RequestClientDetails == input.RequestClientDetails ||
                    this.RequestClientDetails.SequenceEqual(input.RequestClientDetails)
                ) && 
                (
                    this.Timezone == input.Timezone ||
                    (this.Timezone != null &&
                    this.Timezone.Equals(input.Timezone))
                ) && 
                (
                    this.DueStrict == input.DueStrict ||
                    this.DueStrict.Equals(input.DueStrict)
                ) && 
                (
                    this.EmailMessage == input.EmailMessage ||
                    (this.EmailMessage != null &&
                    this.EmailMessage.Equals(input.EmailMessage))
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
                if (this.Currency != null)
                    hashCode = hashCode * 59 + this.Currency.GetHashCode();
                if (this.Products != null)
                    hashCode = hashCode * 59 + this.Products.GetHashCode();
                hashCode = hashCode * 59 + this.Total.GetHashCode();
                if (this.Language != null)
                    hashCode = hashCode * 59 + this.Language.GetHashCode();
                if (this.Notes != null)
                    hashCode = hashCode * 59 + this.Notes.GetHashCode();
                hashCode = hashCode * 59 + this.Debt.GetHashCode();
                if (this.SubtotalOverride != null)
                    hashCode = hashCode * 59 + this.SubtotalOverride.GetHashCode();
                if (this.TotalTaxOverride != null)
                    hashCode = hashCode * 59 + this.TotalTaxOverride.GetHashCode();
                if (this.TotalDiscountOverride != null)
                    hashCode = hashCode * 59 + this.TotalDiscountOverride.GetHashCode();
                if (this.TotalOverride != null)
                    hashCode = hashCode * 59 + this.TotalOverride.GetHashCode();
                hashCode = hashCode * 59 + this.RequestClientDetails.GetHashCode();
                if (this.Timezone != null)
                    hashCode = hashCode * 59 + this.Timezone.GetHashCode();
                hashCode = hashCode * 59 + this.DueStrict.GetHashCode();
                if (this.EmailMessage != null)
                    hashCode = hashCode * 59 + this.EmailMessage.GetHashCode();
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

            // Language (string) maxLength
            if(this.Language != null && this.Language.Length > 2)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Language, length must be less than 2.", new [] { "Language" });
            }

            // Notes (string) maxLength
            if(this.Notes != null && this.Notes.Length > 10000)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Notes, length must be less than 10000.", new [] { "Notes" });
            }

            // EmailMessage (string) maxLength
            if(this.EmailMessage != null && this.EmailMessage.Length > 256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for EmailMessage, length must be less than 256.", new [] { "EmailMessage" });
            }

            yield break;
        }
    }

}
