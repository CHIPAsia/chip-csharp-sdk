

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
    /// A record of one of &#x60;recurring_token&#x60;-s saved for a specific client. &#x60;id&#x60; of this object will be the same as the &#x60;recurring_token&#x60; saved.
    /// </summary>
    [DataContract(Name = "ClientRecurringToken_allOf")]
    public partial class ClientRecurringTokenAllOf : IEquatable<ClientRecurringTokenAllOf>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRecurringTokenAllOf" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public ClientRecurringTokenAllOf()
        {
        }

        /// <summary>
        /// Payment method used to create this token, e.g. &#x60;card&#x60;.
        /// </summary>
        /// <value>Payment method used to create this token, e.g. &#x60;card&#x60;.</value>
        [DataMember(Name = "payment_method", EmitDefaultValue = false)]
        public string PaymentMethod { get; private set; }

        /// <summary>
        /// Returns false as PaymentMethod should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePaymentMethod()
        {
            return false;
        }

        /// <summary>
        /// Description of this token, if available. For card payments, this field will contain the masked card number.
        /// </summary>
        /// <value>Description of this token, if available. For card payments, this field will contain the masked card number.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; private set; }

        /// <summary>
        /// Returns false as Description should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeDescription()
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
            sb.Append("class ClientRecurringTokenAllOf {\n");
            sb.Append("  PaymentMethod: ").Append(PaymentMethod).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
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
            return this.Equals(input as ClientRecurringTokenAllOf);
        }

        /// <summary>
        /// Returns true if ClientRecurringTokenAllOf instances are equal
        /// </summary>
        /// <param name="input">Instance of ClientRecurringTokenAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ClientRecurringTokenAllOf input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.PaymentMethod == input.PaymentMethod ||
                    (this.PaymentMethod != null &&
                    this.PaymentMethod.Equals(input.PaymentMethod))
                ) && 
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
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
                if (this.PaymentMethod != null)
                    hashCode = hashCode * 59 + this.PaymentMethod.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
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
