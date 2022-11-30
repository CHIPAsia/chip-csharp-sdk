

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
    /// Company Balance in a specific currency
    /// </summary>
    [DataContract(Name = "Balance")]
    public partial class Balance : IEquatable<Balance>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Balance" /> class.
        /// </summary>
        /// <param name="grossBalance">grossBalance.</param>
        /// <param name="balance">balance.</param>
        /// <param name="availableBalance">availableBalance.</param>
        /// <param name="reserved">reserved.</param>
        /// <param name="pendingOutgoing">pendingOutgoing.</param>
        /// <param name="feeSell">feeSell.</param>
        public Balance(int grossBalance = default(int), int balance = default(int), int availableBalance = default(int), int reserved = default(int), int pendingOutgoing = default(int), FeeSell feeSell = default(FeeSell))
        {
            this.GrossBalance = grossBalance;
            this._Balance = balance;
            this.AvailableBalance = availableBalance;
            this.Reserved = reserved;
            this.PendingOutgoing = pendingOutgoing;
            this.FeeSell = feeSell;
        }

        /// <summary>
        /// Gets or Sets GrossBalance
        /// </summary>
        [DataMember(Name = "gross_balance", EmitDefaultValue = false)]
        public int GrossBalance { get; set; }

        /// <summary>
        /// Gets or Sets _Balance
        /// </summary>
        [DataMember(Name = "balance", EmitDefaultValue = false)]
        public int _Balance { get; set; }

        /// <summary>
        /// Gets or Sets AvailableBalance
        /// </summary>
        [DataMember(Name = "available_balance", EmitDefaultValue = false)]
        public int AvailableBalance { get; set; }

        /// <summary>
        /// Gets or Sets Reserved
        /// </summary>
        [DataMember(Name = "reserved", EmitDefaultValue = false)]
        public int Reserved { get; set; }

        /// <summary>
        /// Gets or Sets PendingOutgoing
        /// </summary>
        [DataMember(Name = "pending_outgoing", EmitDefaultValue = false)]
        public int PendingOutgoing { get; set; }

        /// <summary>
        /// Gets or Sets FeeSell
        /// </summary>
        [DataMember(Name = "fee_sell", EmitDefaultValue = false)]
        public FeeSell FeeSell { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Balance {\n");
            sb.Append("  GrossBalance: ").Append(GrossBalance).Append("\n");
            sb.Append("  _Balance: ").Append(_Balance).Append("\n");
            sb.Append("  AvailableBalance: ").Append(AvailableBalance).Append("\n");
            sb.Append("  Reserved: ").Append(Reserved).Append("\n");
            sb.Append("  PendingOutgoing: ").Append(PendingOutgoing).Append("\n");
            sb.Append("  FeeSell: ").Append(FeeSell).Append("\n");
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
            return this.Equals(input as Balance);
        }

        /// <summary>
        /// Returns true if Balance instances are equal
        /// </summary>
        /// <param name="input">Instance of Balance to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Balance input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.GrossBalance == input.GrossBalance ||
                    this.GrossBalance.Equals(input.GrossBalance)
                ) && 
                (
                    this._Balance == input._Balance ||
                    this._Balance.Equals(input._Balance)
                ) && 
                (
                    this.AvailableBalance == input.AvailableBalance ||
                    this.AvailableBalance.Equals(input.AvailableBalance)
                ) && 
                (
                    this.Reserved == input.Reserved ||
                    this.Reserved.Equals(input.Reserved)
                ) && 
                (
                    this.PendingOutgoing == input.PendingOutgoing ||
                    this.PendingOutgoing.Equals(input.PendingOutgoing)
                ) && 
                (
                    this.FeeSell == input.FeeSell ||
                    (this.FeeSell != null &&
                    this.FeeSell.Equals(input.FeeSell))
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
                hashCode = hashCode * 59 + this.GrossBalance.GetHashCode();
                hashCode = hashCode * 59 + this._Balance.GetHashCode();
                hashCode = hashCode * 59 + this.AvailableBalance.GetHashCode();
                hashCode = hashCode * 59 + this.Reserved.GetHashCode();
                hashCode = hashCode * 59 + this.PendingOutgoing.GetHashCode();
                if (this.FeeSell != null)
                    hashCode = hashCode * 59 + this.FeeSell.GetHashCode();
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
