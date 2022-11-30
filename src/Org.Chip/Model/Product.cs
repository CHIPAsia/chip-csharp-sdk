

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
    /// Product
    /// </summary>
    [DataContract(Name = "Product")]
    public partial class Product : IEquatable<Product>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Product() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Product" /> class.
        /// </summary>
        /// <param name="name">Product name (required).</param>
        /// <param name="quantity">Quantity of these products in invoice (default to &quot;1&quot;).</param>
        /// <param name="price">You can use this field or &#x60;total_override&#x60; with a value of 0 to activate preauthorization scenario. See the description of the &#x60;Purchase.skip_capture&#x60; field. (required).</param>
        /// <param name="discount">Total discount per this product in invoice.</param>
        /// <param name="taxPercent">Percent of tax added to the price of this product (default to &quot;0&quot;).</param>
        public Product(string name = default(string), string quantity = "1", int price = default(int), int discount = default(int), string taxPercent = "0")
        {
            // to ensure "name" is required (not null)
            this.Name = name ?? throw new ArgumentNullException("name is a required property for Product and cannot be null");
            this.Price = price;
            this.Quantity = quantity;
            this.Discount = discount;
            this.TaxPercent = taxPercent;
        }

        /// <summary>
        /// Product name
        /// </summary>
        /// <value>Product name</value>
        [DataMember(Name = "name", IsRequired = true, EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Quantity of these products in invoice
        /// </summary>
        /// <value>Quantity of these products in invoice</value>
        [DataMember(Name = "quantity", EmitDefaultValue = false)]
        public string Quantity { get; set; }

        /// <summary>
        /// You can use this field or &#x60;total_override&#x60; with a value of 0 to activate preauthorization scenario. See the description of the &#x60;Purchase.skip_capture&#x60; field.
        /// </summary>
        /// <value>You can use this field or &#x60;total_override&#x60; with a value of 0 to activate preauthorization scenario. See the description of the &#x60;Purchase.skip_capture&#x60; field.</value>
        [DataMember(Name = "price", IsRequired = true, EmitDefaultValue = false)]
        public int Price { get; set; }

        /// <summary>
        /// Total discount per this product in invoice
        /// </summary>
        /// <value>Total discount per this product in invoice</value>
        [DataMember(Name = "discount", EmitDefaultValue = false)]
        public int Discount { get; set; }

        /// <summary>
        /// Percent of tax added to the price of this product
        /// </summary>
        /// <value>Percent of tax added to the price of this product</value>
        [DataMember(Name = "tax_percent", EmitDefaultValue = false)]
        public string TaxPercent { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Product {\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  Quantity: ").Append(Quantity).Append("\n");
            sb.Append("  Price: ").Append(Price).Append("\n");
            sb.Append("  Discount: ").Append(Discount).Append("\n");
            sb.Append("  TaxPercent: ").Append(TaxPercent).Append("\n");
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
            return this.Equals(input as Product);
        }

        /// <summary>
        /// Returns true if Product instances are equal
        /// </summary>
        /// <param name="input">Instance of Product to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Product input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) && 
                (
                    this.Quantity == input.Quantity ||
                    this.Quantity.Equals(input.Quantity)
                ) && 
                (
                    this.Price == input.Price ||
                    this.Price.Equals(input.Price)
                ) && 
                (
                    this.Discount == input.Discount ||
                    this.Discount.Equals(input.Discount)
                ) && 
                (
                    this.TaxPercent == input.TaxPercent ||
                    this.TaxPercent.Equals(input.TaxPercent)
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
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                hashCode = hashCode * 59 + this.Quantity.GetHashCode();
                hashCode = hashCode * 59 + this.Price.GetHashCode();
                hashCode = hashCode * 59 + this.Discount.GetHashCode();
                hashCode = hashCode * 59 + this.TaxPercent.GetHashCode();
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
            // Name (string) maxLength
            if(this.Name != null && this.Name.Length > 256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Name, length must be less than 256.", new [] { "Name" });
            }

            yield break;
        }
    }

}
