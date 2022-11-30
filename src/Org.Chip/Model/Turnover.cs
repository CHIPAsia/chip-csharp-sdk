

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
    /// Company turnover statistics
    /// </summary>
    [DataContract(Name = "Turnover")]
    public partial class Turnover : IEquatable<Turnover>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Turnover" /> class.
        /// </summary>
        /// <param name="turnover">turnover.</param>
        /// <param name="feeSell">feeSell.</param>
        /// <param name="count">count.</param>
        public Turnover(int turnover = default(int), FeeSell feeSell = default(FeeSell), TurnoverCount count = default(TurnoverCount))
        {
            this._Turnover = turnover;
            this.FeeSell = feeSell;
            this.Count = count;
        }

        /// <summary>
        /// Gets or Sets _Turnover
        /// </summary>
        [DataMember(Name = "turnover", EmitDefaultValue = false)]
        public int _Turnover { get; set; }

        /// <summary>
        /// Gets or Sets FeeSell
        /// </summary>
        [DataMember(Name = "fee_sell", EmitDefaultValue = false)]
        public FeeSell FeeSell { get; set; }

        /// <summary>
        /// Gets or Sets Count
        /// </summary>
        [DataMember(Name = "count", EmitDefaultValue = false)]
        public TurnoverCount Count { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Turnover {\n");
            sb.Append("  _Turnover: ").Append(_Turnover).Append("\n");
            sb.Append("  FeeSell: ").Append(FeeSell).Append("\n");
            sb.Append("  Count: ").Append(Count).Append("\n");
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
            return this.Equals(input as Turnover);
        }

        /// <summary>
        /// Returns true if Turnover instances are equal
        /// </summary>
        /// <param name="input">Instance of Turnover to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Turnover input)
        {
            if (input == null)
                return false;

            return 
                (
                    this._Turnover == input._Turnover ||
                    this._Turnover.Equals(input._Turnover)
                ) && 
                (
                    this.FeeSell == input.FeeSell ||
                    (this.FeeSell != null &&
                    this.FeeSell.Equals(input.FeeSell))
                ) && 
                (
                    this.Count == input.Count ||
                    (this.Count != null &&
                    this.Count.Equals(input.Count))
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
                hashCode = hashCode * 59 + this._Turnover.GetHashCode();
                if (this.FeeSell != null)
                    hashCode = hashCode * 59 + this.FeeSell.GetHashCode();
                if (this.Count != null)
                    hashCode = hashCode * 59 + this.Count.GetHashCode();
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
