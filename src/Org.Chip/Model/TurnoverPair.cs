

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
    /// Incoming and outgoing Company turnover statistics
    /// </summary>
    [DataContract(Name = "TurnoverPair")]
    public partial class TurnoverPair : IEquatable<TurnoverPair>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnoverPair" /> class.
        /// </summary>
        /// <param name="incoming">incoming.</param>
        /// <param name="outgoing">outgoing.</param>
        public TurnoverPair(Turnover incoming = default(Turnover), Turnover outgoing = default(Turnover))
        {
            this.Incoming = incoming;
            this.Outgoing = outgoing;
        }

        /// <summary>
        /// Gets or Sets Incoming
        /// </summary>
        [DataMember(Name = "incoming", EmitDefaultValue = false)]
        public Turnover Incoming { get; set; }

        /// <summary>
        /// Gets or Sets Outgoing
        /// </summary>
        [DataMember(Name = "outgoing", EmitDefaultValue = false)]
        public Turnover Outgoing { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TurnoverPair {\n");
            sb.Append("  Incoming: ").Append(Incoming).Append("\n");
            sb.Append("  Outgoing: ").Append(Outgoing).Append("\n");
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
            return this.Equals(input as TurnoverPair);
        }

        /// <summary>
        /// Returns true if TurnoverPair instances are equal
        /// </summary>
        /// <param name="input">Instance of TurnoverPair to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TurnoverPair input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Incoming == input.Incoming ||
                    (this.Incoming != null &&
                    this.Incoming.Equals(input.Incoming))
                ) && 
                (
                    this.Outgoing == input.Outgoing ||
                    (this.Outgoing != null &&
                    this.Outgoing.Equals(input.Outgoing))
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
                if (this.Incoming != null)
                    hashCode = hashCode * 59 + this.Incoming.GetHashCode();
                if (this.Outgoing != null)
                    hashCode = hashCode * 59 + this.Outgoing.GetHashCode();
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
