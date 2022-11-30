

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
    /// Read-only details of issuer company/brand, persisted for invoice display.
    /// </summary>
    [DataContract(Name = "IssuerDetails")]
    public partial class IssuerDetails : IEquatable<IssuerDetails>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IssuerDetails" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        public IssuerDetails()
        {
        }

        /// <summary>
        /// Company website URL
        /// </summary>
        /// <value>Company website URL</value>
        [DataMember(Name = "website", EmitDefaultValue = false)]
        public string Website { get; private set; }

        /// <summary>
        /// Returns false as Website should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeWebsite()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets LegalStreetAddress
        /// </summary>
        [DataMember(Name = "legal_street_address", EmitDefaultValue = false)]
        public string LegalStreetAddress { get; private set; }

        /// <summary>
        /// Returns false as LegalStreetAddress should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLegalStreetAddress()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets LegalCountry
        /// </summary>
        [DataMember(Name = "legal_country", EmitDefaultValue = false)]
        public string LegalCountry { get; private set; }

        /// <summary>
        /// Returns false as LegalCountry should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLegalCountry()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets LegalCity
        /// </summary>
        [DataMember(Name = "legal_city", EmitDefaultValue = false)]
        public string LegalCity { get; private set; }

        /// <summary>
        /// Returns false as LegalCity should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLegalCity()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets LegalZipCode
        /// </summary>
        [DataMember(Name = "legal_zip_code", EmitDefaultValue = false)]
        public string LegalZipCode { get; private set; }

        /// <summary>
        /// Returns false as LegalZipCode should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLegalZipCode()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets BankAccounts
        /// </summary>
        [DataMember(Name = "bank_accounts", EmitDefaultValue = false)]
        public List<BankAccount> BankAccounts { get; private set; }

        /// <summary>
        /// Returns false as BankAccounts should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeBankAccounts()
        {
            return false;
        }

        /// <summary>
        /// Legal name of company
        /// </summary>
        /// <value>Legal name of company</value>
        [DataMember(Name = "legal_name", EmitDefaultValue = false)]
        public string LegalName { get; private set; }

        /// <summary>
        /// Returns false as LegalName should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeLegalName()
        {
            return false;
        }

        /// <summary>
        /// Company brand name
        /// </summary>
        /// <value>Company brand name</value>
        [DataMember(Name = "brand_name", EmitDefaultValue = false)]
        public string BrandName { get; private set; }

        /// <summary>
        /// Returns false as BrandName should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeBrandName()
        {
            return false;
        }

        /// <summary>
        /// Registration number of company
        /// </summary>
        /// <value>Registration number of company</value>
        [DataMember(Name = "registration_number", EmitDefaultValue = false)]
        public string RegistrationNumber { get; private set; }

        /// <summary>
        /// Returns false as RegistrationNumber should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRegistrationNumber()
        {
            return false;
        }

        /// <summary>
        /// Tax payer registration number
        /// </summary>
        /// <value>Tax payer registration number</value>
        [DataMember(Name = "tax_number", EmitDefaultValue = false)]
        public string TaxNumber { get; private set; }

        /// <summary>
        /// Returns false as TaxNumber should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeTaxNumber()
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
            sb.Append("class IssuerDetails {\n");
            sb.Append("  Website: ").Append(Website).Append("\n");
            sb.Append("  LegalStreetAddress: ").Append(LegalStreetAddress).Append("\n");
            sb.Append("  LegalCountry: ").Append(LegalCountry).Append("\n");
            sb.Append("  LegalCity: ").Append(LegalCity).Append("\n");
            sb.Append("  LegalZipCode: ").Append(LegalZipCode).Append("\n");
            sb.Append("  BankAccounts: ").Append(BankAccounts).Append("\n");
            sb.Append("  LegalName: ").Append(LegalName).Append("\n");
            sb.Append("  BrandName: ").Append(BrandName).Append("\n");
            sb.Append("  RegistrationNumber: ").Append(RegistrationNumber).Append("\n");
            sb.Append("  TaxNumber: ").Append(TaxNumber).Append("\n");
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
            return this.Equals(input as IssuerDetails);
        }

        /// <summary>
        /// Returns true if IssuerDetails instances are equal
        /// </summary>
        /// <param name="input">Instance of IssuerDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(IssuerDetails input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Website == input.Website ||
                    (this.Website != null &&
                    this.Website.Equals(input.Website))
                ) && 
                (
                    this.LegalStreetAddress == input.LegalStreetAddress ||
                    (this.LegalStreetAddress != null &&
                    this.LegalStreetAddress.Equals(input.LegalStreetAddress))
                ) && 
                (
                    this.LegalCountry == input.LegalCountry ||
                    (this.LegalCountry != null &&
                    this.LegalCountry.Equals(input.LegalCountry))
                ) && 
                (
                    this.LegalCity == input.LegalCity ||
                    (this.LegalCity != null &&
                    this.LegalCity.Equals(input.LegalCity))
                ) && 
                (
                    this.LegalZipCode == input.LegalZipCode ||
                    (this.LegalZipCode != null &&
                    this.LegalZipCode.Equals(input.LegalZipCode))
                ) && 
                (
                    this.BankAccounts == input.BankAccounts ||
                    this.BankAccounts != null &&
                    input.BankAccounts != null &&
                    this.BankAccounts.SequenceEqual(input.BankAccounts)
                ) && 
                (
                    this.LegalName == input.LegalName ||
                    (this.LegalName != null &&
                    this.LegalName.Equals(input.LegalName))
                ) && 
                (
                    this.BrandName == input.BrandName ||
                    (this.BrandName != null &&
                    this.BrandName.Equals(input.BrandName))
                ) && 
                (
                    this.RegistrationNumber == input.RegistrationNumber ||
                    (this.RegistrationNumber != null &&
                    this.RegistrationNumber.Equals(input.RegistrationNumber))
                ) && 
                (
                    this.TaxNumber == input.TaxNumber ||
                    (this.TaxNumber != null &&
                    this.TaxNumber.Equals(input.TaxNumber))
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
                if (this.Website != null)
                    hashCode = hashCode * 59 + this.Website.GetHashCode();
                if (this.LegalStreetAddress != null)
                    hashCode = hashCode * 59 + this.LegalStreetAddress.GetHashCode();
                if (this.LegalCountry != null)
                    hashCode = hashCode * 59 + this.LegalCountry.GetHashCode();
                if (this.LegalCity != null)
                    hashCode = hashCode * 59 + this.LegalCity.GetHashCode();
                if (this.LegalZipCode != null)
                    hashCode = hashCode * 59 + this.LegalZipCode.GetHashCode();
                if (this.BankAccounts != null)
                    hashCode = hashCode * 59 + this.BankAccounts.GetHashCode();
                if (this.LegalName != null)
                    hashCode = hashCode * 59 + this.LegalName.GetHashCode();
                if (this.BrandName != null)
                    hashCode = hashCode * 59 + this.BrandName.GetHashCode();
                if (this.RegistrationNumber != null)
                    hashCode = hashCode * 59 + this.RegistrationNumber.GetHashCode();
                if (this.TaxNumber != null)
                    hashCode = hashCode * 59 + this.TaxNumber.GetHashCode();
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
            // LegalName (string) maxLength
            if(this.LegalName != null && this.LegalName.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for LegalName, length must be less than 128.", new [] { "LegalName" });
            }

            // BrandName (string) maxLength
            if(this.BrandName != null && this.BrandName.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for BrandName, length must be less than 128.", new [] { "BrandName" });
            }

            // RegistrationNumber (string) maxLength
            if(this.RegistrationNumber != null && this.RegistrationNumber.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for RegistrationNumber, length must be less than 32.", new [] { "RegistrationNumber" });
            }

            // TaxNumber (string) maxLength
            if(this.TaxNumber != null && this.TaxNumber.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for TaxNumber, length must be less than 32.", new [] { "TaxNumber" });
            }

            yield break;
        }
    }

}
