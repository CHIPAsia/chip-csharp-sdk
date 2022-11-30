

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
    /// Contains details about the client of a purchase or payment - the remote payer/fund recipient party.
    /// </summary>
    [DataContract(Name = "ClientDetails")]
    public partial class ClientDetails : IEquatable<ClientDetails>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientDetails" /> class.
        /// </summary>
        /// <param name="email">Email address (required).</param>
        /// <param name="phone">Phone number in the &#x60;&lt;country_code&gt; &lt;number&gt;&#x60; format.</param>
        /// <param name="fullName">Name and surname of client.</param>
        /// <param name="personalCode">Personal identification code of client.</param>
        /// <param name="streetAddress">Street house number and flat address where applicable.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;).</param>
        /// <param name="city">City name.</param>
        /// <param name="zipCode">ZIP or postal code.</param>
        /// <param name="shippingStreetAddress">Street house number and flat address where applicable.</param>
        /// <param name="shippingCountry">Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;).</param>
        /// <param name="shippingCity">City name.</param>
        /// <param name="shippingZipCode">ZIP or postal code.</param>
        /// <param name="cc">Email addresses to receive a carbon copy of all notification emails.</param>
        /// <param name="bcc">Email addresses to receive a blind carbon copy of all notification emails.</param>
        /// <param name="legalName">Legal name of company.</param>
        /// <param name="brandName">Company brand name.</param>
        /// <param name="registrationNumber">Registration number of company.</param>
        /// <param name="taxNumber">Tax payer registration number.</param>
        /// <param name="bankAccount">Bank account number (e.g. IBAN).</param>
        /// <param name="bankCode">SWIFT/BIC code of the bank.</param>
        public ClientDetails(string email = default(string), string phone = default(string), string fullName = default(string), string personalCode = default(string), string streetAddress = default(string), string country = default(string), string city = default(string), string zipCode = default(string), string shippingStreetAddress = default(string), string shippingCountry = default(string), string shippingCity = default(string), string shippingZipCode = default(string), List<string> cc = default(List<string>), List<string> bcc = default(List<string>), string legalName = default(string), string brandName = default(string), string registrationNumber = default(string), string taxNumber = default(string), string bankAccount = default(string), string bankCode = default(string))
        {
            // to ensure "email" is required (not null)
            this.Email = email ?? throw new ArgumentNullException("email is a required property for ClientDetails and cannot be null");
            this.Phone = phone;
            this.FullName = fullName;
            this.PersonalCode = personalCode;
            this.StreetAddress = streetAddress;
            this.Country = country;
            this.City = city;
            this.ZipCode = zipCode;
            this.ShippingStreetAddress = shippingStreetAddress;
            this.ShippingCountry = shippingCountry;
            this.ShippingCity = shippingCity;
            this.ShippingZipCode = shippingZipCode;
            this.Cc = cc;
            this.Bcc = bcc;
            this.LegalName = legalName;
            this.BrandName = brandName;
            this.RegistrationNumber = registrationNumber;
            this.TaxNumber = taxNumber;
            this.BankAccount = bankAccount;
            this.BankCode = bankCode;
        }

        /// <summary>
        /// Email address
        /// </summary>
        /// <value>Email address</value>
        [DataMember(Name = "email", IsRequired = true, EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// Phone number in the &#x60;&lt;country_code&gt; &lt;number&gt;&#x60; format
        /// </summary>
        /// <value>Phone number in the &#x60;&lt;country_code&gt; &lt;number&gt;&#x60; format</value>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// Name and surname of client
        /// </summary>
        /// <value>Name and surname of client</value>
        [DataMember(Name = "full_name", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// Personal identification code of client
        /// </summary>
        /// <value>Personal identification code of client</value>
        [DataMember(Name = "personal_code", EmitDefaultValue = false)]
        public string PersonalCode { get; set; }

        /// <summary>
        /// Street house number and flat address where applicable
        /// </summary>
        /// <value>Street house number and flat address where applicable</value>
        [DataMember(Name = "street_address", EmitDefaultValue = false)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;)
        /// </summary>
        /// <value>Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;)</value>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public string Country { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        /// <value>City name</value>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// ZIP or postal code
        /// </summary>
        /// <value>ZIP or postal code</value>
        [DataMember(Name = "zip_code", EmitDefaultValue = false)]
        public string ZipCode { get; set; }

        /// <summary>
        /// Street house number and flat address where applicable
        /// </summary>
        /// <value>Street house number and flat address where applicable</value>
        [DataMember(Name = "shipping_street_address", EmitDefaultValue = false)]
        public string ShippingStreetAddress { get; set; }

        /// <summary>
        /// Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;)
        /// </summary>
        /// <value>Country code in the ISO 3166-1 alpha-2 format (e.g. &#39;GB&#39;)</value>
        [DataMember(Name = "shipping_country", EmitDefaultValue = false)]
        public string ShippingCountry { get; set; }

        /// <summary>
        /// City name
        /// </summary>
        /// <value>City name</value>
        [DataMember(Name = "shipping_city", EmitDefaultValue = false)]
        public string ShippingCity { get; set; }

        /// <summary>
        /// ZIP or postal code
        /// </summary>
        /// <value>ZIP or postal code</value>
        [DataMember(Name = "shipping_zip_code", EmitDefaultValue = false)]
        public string ShippingZipCode { get; set; }

        /// <summary>
        /// Email addresses to receive a carbon copy of all notification emails
        /// </summary>
        /// <value>Email addresses to receive a carbon copy of all notification emails</value>
        [DataMember(Name = "cc", EmitDefaultValue = false)]
        public List<string> Cc { get; set; }

        /// <summary>
        /// Email addresses to receive a blind carbon copy of all notification emails
        /// </summary>
        /// <value>Email addresses to receive a blind carbon copy of all notification emails</value>
        [DataMember(Name = "bcc", EmitDefaultValue = false)]
        public List<string> Bcc { get; set; }

        /// <summary>
        /// Legal name of company
        /// </summary>
        /// <value>Legal name of company</value>
        [DataMember(Name = "legal_name", EmitDefaultValue = false)]
        public string LegalName { get; set; }

        /// <summary>
        /// Company brand name
        /// </summary>
        /// <value>Company brand name</value>
        [DataMember(Name = "brand_name", EmitDefaultValue = false)]
        public string BrandName { get; set; }

        /// <summary>
        /// Registration number of company
        /// </summary>
        /// <value>Registration number of company</value>
        [DataMember(Name = "registration_number", EmitDefaultValue = false)]
        public string RegistrationNumber { get; set; }

        /// <summary>
        /// Tax payer registration number
        /// </summary>
        /// <value>Tax payer registration number</value>
        [DataMember(Name = "tax_number", EmitDefaultValue = false)]
        public string TaxNumber { get; set; }

        /// <summary>
        /// Bank account number (e.g. IBAN)
        /// </summary>
        /// <value>Bank account number (e.g. IBAN)</value>
        [DataMember(Name = "bank_account", EmitDefaultValue = false)]
        public string BankAccount { get; set; }

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
            sb.Append("class ClientDetails {\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  FullName: ").Append(FullName).Append("\n");
            sb.Append("  PersonalCode: ").Append(PersonalCode).Append("\n");
            sb.Append("  StreetAddress: ").Append(StreetAddress).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  ZipCode: ").Append(ZipCode).Append("\n");
            sb.Append("  ShippingStreetAddress: ").Append(ShippingStreetAddress).Append("\n");
            sb.Append("  ShippingCountry: ").Append(ShippingCountry).Append("\n");
            sb.Append("  ShippingCity: ").Append(ShippingCity).Append("\n");
            sb.Append("  ShippingZipCode: ").Append(ShippingZipCode).Append("\n");
            sb.Append("  Cc: ").Append(Cc).Append("\n");
            sb.Append("  Bcc: ").Append(Bcc).Append("\n");
            sb.Append("  LegalName: ").Append(LegalName).Append("\n");
            sb.Append("  BrandName: ").Append(BrandName).Append("\n");
            sb.Append("  RegistrationNumber: ").Append(RegistrationNumber).Append("\n");
            sb.Append("  TaxNumber: ").Append(TaxNumber).Append("\n");
            sb.Append("  BankAccount: ").Append(BankAccount).Append("\n");
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
            return this.Equals(input as ClientDetails);
        }

        /// <summary>
        /// Returns true if ClientDetails instances are equal
        /// </summary>
        /// <param name="input">Instance of ClientDetails to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ClientDetails input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) && 
                (
                    this.Phone == input.Phone ||
                    (this.Phone != null &&
                    this.Phone.Equals(input.Phone))
                ) && 
                (
                    this.FullName == input.FullName ||
                    (this.FullName != null &&
                    this.FullName.Equals(input.FullName))
                ) && 
                (
                    this.PersonalCode == input.PersonalCode ||
                    (this.PersonalCode != null &&
                    this.PersonalCode.Equals(input.PersonalCode))
                ) && 
                (
                    this.StreetAddress == input.StreetAddress ||
                    (this.StreetAddress != null &&
                    this.StreetAddress.Equals(input.StreetAddress))
                ) && 
                (
                    this.Country == input.Country ||
                    (this.Country != null &&
                    this.Country.Equals(input.Country))
                ) && 
                (
                    this.City == input.City ||
                    (this.City != null &&
                    this.City.Equals(input.City))
                ) && 
                (
                    this.ZipCode == input.ZipCode ||
                    (this.ZipCode != null &&
                    this.ZipCode.Equals(input.ZipCode))
                ) && 
                (
                    this.ShippingStreetAddress == input.ShippingStreetAddress ||
                    (this.ShippingStreetAddress != null &&
                    this.ShippingStreetAddress.Equals(input.ShippingStreetAddress))
                ) && 
                (
                    this.ShippingCountry == input.ShippingCountry ||
                    (this.ShippingCountry != null &&
                    this.ShippingCountry.Equals(input.ShippingCountry))
                ) && 
                (
                    this.ShippingCity == input.ShippingCity ||
                    (this.ShippingCity != null &&
                    this.ShippingCity.Equals(input.ShippingCity))
                ) && 
                (
                    this.ShippingZipCode == input.ShippingZipCode ||
                    (this.ShippingZipCode != null &&
                    this.ShippingZipCode.Equals(input.ShippingZipCode))
                ) && 
                (
                    this.Cc == input.Cc ||
                    this.Cc != null &&
                    input.Cc != null &&
                    this.Cc.SequenceEqual(input.Cc)
                ) && 
                (
                    this.Bcc == input.Bcc ||
                    this.Bcc != null &&
                    input.Bcc != null &&
                    this.Bcc.SequenceEqual(input.Bcc)
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
                ) && 
                (
                    this.BankAccount == input.BankAccount ||
                    (this.BankAccount != null &&
                    this.BankAccount.Equals(input.BankAccount))
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
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
                if (this.Phone != null)
                    hashCode = hashCode * 59 + this.Phone.GetHashCode();
                if (this.FullName != null)
                    hashCode = hashCode * 59 + this.FullName.GetHashCode();
                if (this.PersonalCode != null)
                    hashCode = hashCode * 59 + this.PersonalCode.GetHashCode();
                if (this.StreetAddress != null)
                    hashCode = hashCode * 59 + this.StreetAddress.GetHashCode();
                if (this.Country != null)
                    hashCode = hashCode * 59 + this.Country.GetHashCode();
                if (this.City != null)
                    hashCode = hashCode * 59 + this.City.GetHashCode();
                if (this.ZipCode != null)
                    hashCode = hashCode * 59 + this.ZipCode.GetHashCode();
                if (this.ShippingStreetAddress != null)
                    hashCode = hashCode * 59 + this.ShippingStreetAddress.GetHashCode();
                if (this.ShippingCountry != null)
                    hashCode = hashCode * 59 + this.ShippingCountry.GetHashCode();
                if (this.ShippingCity != null)
                    hashCode = hashCode * 59 + this.ShippingCity.GetHashCode();
                if (this.ShippingZipCode != null)
                    hashCode = hashCode * 59 + this.ShippingZipCode.GetHashCode();
                if (this.Cc != null)
                    hashCode = hashCode * 59 + this.Cc.GetHashCode();
                if (this.Bcc != null)
                    hashCode = hashCode * 59 + this.Bcc.GetHashCode();
                if (this.LegalName != null)
                    hashCode = hashCode * 59 + this.LegalName.GetHashCode();
                if (this.BrandName != null)
                    hashCode = hashCode * 59 + this.BrandName.GetHashCode();
                if (this.RegistrationNumber != null)
                    hashCode = hashCode * 59 + this.RegistrationNumber.GetHashCode();
                if (this.TaxNumber != null)
                    hashCode = hashCode * 59 + this.TaxNumber.GetHashCode();
                if (this.BankAccount != null)
                    hashCode = hashCode * 59 + this.BankAccount.GetHashCode();
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
            // Email (string) maxLength
            if(this.Email != null && this.Email.Length > 254)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Email, length must be less than 254.", new [] { "Email" });
            }

            // Phone (string) maxLength
            if(this.Phone != null && this.Phone.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Phone, length must be less than 32.", new [] { "Phone" });
            }

            // FullName (string) maxLength
            if(this.FullName != null && this.FullName.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for FullName, length must be less than 128.", new [] { "FullName" });
            }

            // PersonalCode (string) maxLength
            if(this.PersonalCode != null && this.PersonalCode.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for PersonalCode, length must be less than 32.", new [] { "PersonalCode" });
            }

            // StreetAddress (string) maxLength
            if(this.StreetAddress != null && this.StreetAddress.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for StreetAddress, length must be less than 128.", new [] { "StreetAddress" });
            }

            // Country (string) maxLength
            if(this.Country != null && this.Country.Length > 2)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Country, length must be less than 2.", new [] { "Country" });
            }

            // City (string) maxLength
            if(this.City != null && this.City.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for City, length must be less than 128.", new [] { "City" });
            }

            // ZipCode (string) maxLength
            if(this.ZipCode != null && this.ZipCode.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ZipCode, length must be less than 32.", new [] { "ZipCode" });
            }

            // ShippingStreetAddress (string) maxLength
            if(this.ShippingStreetAddress != null && this.ShippingStreetAddress.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ShippingStreetAddress, length must be less than 128.", new [] { "ShippingStreetAddress" });
            }

            // ShippingCountry (string) maxLength
            if(this.ShippingCountry != null && this.ShippingCountry.Length > 2)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ShippingCountry, length must be less than 2.", new [] { "ShippingCountry" });
            }

            // ShippingCity (string) maxLength
            if(this.ShippingCity != null && this.ShippingCity.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ShippingCity, length must be less than 128.", new [] { "ShippingCity" });
            }

            // ShippingZipCode (string) maxLength
            if(this.ShippingZipCode != null && this.ShippingZipCode.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for ShippingZipCode, length must be less than 32.", new [] { "ShippingZipCode" });
            }

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

            // BankAccount (string) maxLength
            if(this.BankAccount != null && this.BankAccount.Length > 34)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for BankAccount, length must be less than 34.", new [] { "BankAccount" });
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
