

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
    /// Transaction counts processed withing the selected filters
    /// </summary>
    [DataContract(Name = "Turnover_count")]
    public partial class TurnoverCount : IEquatable<TurnoverCount>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TurnoverCount" /> class.
        /// </summary>
        /// <param name="all">Number of transactions that contributed to the turnover. This can be used for calculating average &#x60;turnover&#x60; and &#x60;fee_sell&#x60; within the selected filters..</param>
        public TurnoverCount(int all = default(int))
        {
            this.All = all;
        }

        /// <summary>
        /// Number of transactions that contributed to the turnover. This can be used for calculating average &#x60;turnover&#x60; and &#x60;fee_sell&#x60; within the selected filters.
        /// </summary>
        /// <value>Number of transactions that contributed to the turnover. This can be used for calculating average &#x60;turnover&#x60; and &#x60;fee_sell&#x60; within the selected filters.</value>
        [DataMember(Name = "all", EmitDefaultValue = false)]
        public int All { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class TurnoverCount {\n");
            sb.Append("  All: ").Append(All).Append("\n");
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
            return this.Equals(input as TurnoverCount);
        }

        /// <summary>
        /// Returns true if TurnoverCount instances are equal
        /// </summary>
        /// <param name="input">Instance of TurnoverCount to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(TurnoverCount input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.All == input.All ||
                    this.All.Equals(input.All)
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
                hashCode = hashCode * 59 + this.All.GetHashCode();
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
