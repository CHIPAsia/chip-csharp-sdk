

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
    /// PaymentMethods
    /// </summary>
    [DataContract(Name = "payment_methods")]
    public partial class PaymentMethods : IEquatable<PaymentMethods>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethods" /> class.
        /// </summary>
        /// <param name="availablePaymentMethods">availablePaymentMethods.</param>
        /// <param name="byCountry">Payment method names (as returned by &#x60;available_payment_methods&#x60;) grouped by country codes they are available in. &#x60;any&#x60; key returns names of payment method available in all countries..</param>
        /// <param name="countryNames">Human-readable names corresponding to country codes as returned by &#x60;by_country&#x60; property. &#x60;any&#x60; code is also decoded to &#x60;Other&#x60;..</param>
        /// <param name="names">Human-readable names of payment methods as returned by &#x60;available_payment_methods&#x60; property..</param>
        /// <param name="cardMethods">cardMethods.</param>
        public PaymentMethods(List<string> availablePaymentMethods = default(List<string>), Dictionary<string, List<string>> byCountry = default(Dictionary<string, List<string>>), Dictionary<string, string> countryNames = default(Dictionary<string, string>), Dictionary<string, string> names = default(Dictionary<string, string>), List<string> cardMethods = default(List<string>))
        {
            this.AvailablePaymentMethods = availablePaymentMethods;
            this.ByCountry = byCountry;
            this.CountryNames = countryNames;
            this.Names = names;
            this.CardMethods = cardMethods;
        }

        /// <summary>
        /// Gets or Sets AvailablePaymentMethods
        /// </summary>
        [DataMember(Name = "available_payment_methods", EmitDefaultValue = false)]
        public List<string> AvailablePaymentMethods { get; set; }

        /// <summary>
        /// Payment method names (as returned by &#x60;available_payment_methods&#x60;) grouped by country codes they are available in. &#x60;any&#x60; key returns names of payment method available in all countries.
        /// </summary>
        /// <value>Payment method names (as returned by &#x60;available_payment_methods&#x60;) grouped by country codes they are available in. &#x60;any&#x60; key returns names of payment method available in all countries.</value>
        [DataMember(Name = "by_country", EmitDefaultValue = false)]
        public Dictionary<string, List<string>> ByCountry { get; set; }

        /// <summary>
        /// Human-readable names corresponding to country codes as returned by &#x60;by_country&#x60; property. &#x60;any&#x60; code is also decoded to &#x60;Other&#x60;.
        /// </summary>
        /// <value>Human-readable names corresponding to country codes as returned by &#x60;by_country&#x60; property. &#x60;any&#x60; code is also decoded to &#x60;Other&#x60;.</value>
        [DataMember(Name = "country_names", EmitDefaultValue = false)]
        public Dictionary<string, string> CountryNames { get; set; }

        /// <summary>
        /// Human-readable names of payment methods as returned by &#x60;available_payment_methods&#x60; property.
        /// </summary>
        /// <value>Human-readable names of payment methods as returned by &#x60;available_payment_methods&#x60; property.</value>
        [DataMember(Name = "names", EmitDefaultValue = false)]
        public Dictionary<string, string> Names { get; set; }

        /// <summary>
        /// Gets or Sets CardMethods
        /// </summary>
        [DataMember(Name = "card_methods", EmitDefaultValue = false)]
        public List<string> CardMethods { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class PaymentMethods {\n");
            sb.Append("  AvailablePaymentMethods: ").Append(AvailablePaymentMethods).Append("\n");
            sb.Append("  ByCountry: ").Append(ByCountry).Append("\n");
            sb.Append("  CountryNames: ").Append(CountryNames).Append("\n");
            sb.Append("  Names: ").Append(Names).Append("\n");
            sb.Append("  CardMethods: ").Append(CardMethods).Append("\n");
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
            return this.Equals(input as PaymentMethods);
        }

        /// <summary>
        /// Returns true if PaymentMethods instances are equal
        /// </summary>
        /// <param name="input">Instance of PaymentMethods to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(PaymentMethods input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AvailablePaymentMethods == input.AvailablePaymentMethods ||
                    this.AvailablePaymentMethods != null &&
                    input.AvailablePaymentMethods != null &&
                    this.AvailablePaymentMethods.SequenceEqual(input.AvailablePaymentMethods)
                ) && 
                (
                    this.ByCountry == input.ByCountry ||
                    this.ByCountry != null &&
                    input.ByCountry != null &&
                    this.ByCountry.SequenceEqual(input.ByCountry)
                ) && 
                (
                    this.CountryNames == input.CountryNames ||
                    this.CountryNames != null &&
                    input.CountryNames != null &&
                    this.CountryNames.SequenceEqual(input.CountryNames)
                ) && 
                (
                    this.Names == input.Names ||
                    this.Names != null &&
                    input.Names != null &&
                    this.Names.SequenceEqual(input.Names)
                ) && 
                (
                    this.CardMethods == input.CardMethods ||
                    this.CardMethods != null &&
                    input.CardMethods != null &&
                    this.CardMethods.SequenceEqual(input.CardMethods)
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
                if (this.AvailablePaymentMethods != null)
                    hashCode = hashCode * 59 + this.AvailablePaymentMethods.GetHashCode();
                if (this.ByCountry != null)
                    hashCode = hashCode * 59 + this.ByCountry.GetHashCode();
                if (this.CountryNames != null)
                    hashCode = hashCode * 59 + this.CountryNames.GetHashCode();
                if (this.Names != null)
                    hashCode = hashCode * 59 + this.Names.GetHashCode();
                if (this.CardMethods != null)
                    hashCode = hashCode * 59 + this.CardMethods.GetHashCode();
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
