

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
    /// BankAccount
    /// </summary>
    [DataContract(Name = "BankAccount")]
    public partial class BankAccount : IEquatable<BankAccount>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BankAccount" /> class.
        /// </summary>
        /// <param name="bankAccount">Bank account number (e.g. IBAN).</param>
        /// <param name="bankCode">SWIFT/BIC code of the bank.</param>
        public BankAccount(string bankAccount = default(string), string bankCode = default(string))
        {
            this._BankAccount = bankAccount;
            this.BankCode = bankCode;
        }

        /// <summary>
        /// Bank account number (e.g. IBAN)
        /// </summary>
        /// <value>Bank account number (e.g. IBAN)</value>
        [DataMember(Name = "bank_account", EmitDefaultValue = false)]
        public string _BankAccount { get; set; }

        /// <summary>
        /// SWIFT/BIC code of the bank
        /// </summary>
        /// <value>SWIFT/BIC code of the bank</value>
        [DataMember(Name = "bank_code", EmitDefaultValue = false)]
        public string BankCode { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class BankAccount {\n");
            sb.Append("  _BankAccount: ").Append(_BankAccount).Append("\n");
            sb.Append("  BankCode: ").Append(BankCode).Append("\n");
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
            return this.Equals(input as BankAccount);
        }

        /// <summary>
        /// Returns true if BankAccount instances are equal
        /// </summary>
        /// <param name="input">Instance of BankAccount to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BankAccount input)
        {
            if (input == null)
                return false;

            return 
                (
                    this._BankAccount == input._BankAccount ||
                    (this._BankAccount != null &&
                    this._BankAccount.Equals(input._BankAccount))
                ) && 
                (
                    this.BankCode == input.BankCode ||
                    (this.BankCode != null &&
                    this.BankCode.Equals(input.BankCode))
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
                if (this._BankAccount != null)
                    hashCode = hashCode * 59 + this._BankAccount.GetHashCode();
                if (this.BankCode != null)
                    hashCode = hashCode * 59 + this.BankCode.GetHashCode();
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
            // _BankAccount (string) maxLength
            if(this._BankAccount != null && this._BankAccount.Length > 34)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for _BankAccount, length must be less than 34.", new [] { "_BankAccount" });
            }

            // BankCode (string) maxLength
            if(this.BankCode != null && this.BankCode.Length > 11)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for BankCode, length must be less than 11.", new [] { "BankCode" });
            }

            yield break;
        }
    }

}
