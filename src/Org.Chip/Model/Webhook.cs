

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
    /// Webhook
    /// </summary>
    [DataContract(Name = "Webhook")]
    public partial class Webhook : IEquatable<Webhook>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhook" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected Webhook() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhook" /> class.
        /// </summary>
        /// <param name="title">Arbitrary title of webhook (required).</param>
        /// <param name="allEvents">Specifies this webhook should trigger on all event types. Either this or &#x60;events&#x60; is required. (default to false).</param>
        /// <param name="events">List of events to trigger webhook callbacks for. Either this or &#x60;all_events&#x60; is required. (required).</param>
        /// <param name="callback">callback (required).</param>
        public Webhook(string title = default(string), bool allEvents = false, List<Event> events = default(List<Event>), string callback = default(string))
        {
            // to ensure "title" is required (not null)
            this.Title = title ?? throw new ArgumentNullException("title is a required property for Webhook and cannot be null");
            // to ensure "events" is required (not null)
            this.Events = events ?? throw new ArgumentNullException("events is a required property for Webhook and cannot be null");
            // to ensure "callback" is required (not null)
            this.Callback = callback ?? throw new ArgumentNullException("callback is a required property for Webhook and cannot be null");
            this.AllEvents = allEvents;
        }

        /// <summary>
        /// Object type identifier
        /// </summary>
        /// <value>Object type identifier</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; private set; }

        /// <summary>
        /// Returns false as Type should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeType()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public Guid Id { get; private set; }

        /// <summary>
        /// Returns false as Id should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeId()
        {
            return false;
        }

        /// <summary>
        /// Object creation time
        /// </summary>
        /// <value>Object creation time</value>
        [DataMember(Name = "created_on", EmitDefaultValue = false)]
        public int CreatedOn { get; private set; }

        /// <summary>
        /// Returns false as CreatedOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCreatedOn()
        {
            return false;
        }

        /// <summary>
        /// Object last modification time
        /// </summary>
        /// <value>Object last modification time</value>
        [DataMember(Name = "updated_on", EmitDefaultValue = false)]
        public int UpdatedOn { get; private set; }

        /// <summary>
        /// Returns false as UpdatedOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUpdatedOn()
        {
            return false;
        }

        /// <summary>
        /// Arbitrary title of webhook
        /// </summary>
        /// <value>Arbitrary title of webhook</value>
        [DataMember(Name = "title", IsRequired = true, EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Specifies this webhook should trigger on all event types. Either this or &#x60;events&#x60; is required.
        /// </summary>
        /// <value>Specifies this webhook should trigger on all event types. Either this or &#x60;events&#x60; is required.</value>
        [DataMember(Name = "all_events", EmitDefaultValue = false)]
        public bool AllEvents { get; set; }

        /// <summary>
        /// PEM-encoded RSA public key for authenticating webhook or callback payloads
        /// </summary>
        /// <value>PEM-encoded RSA public key for authenticating webhook or callback payloads</value>
        [DataMember(Name = "public_key", EmitDefaultValue = false)]
        public string PublicKey { get; private set; }

        /// <summary>
        /// Returns false as PublicKey should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePublicKey()
        {
            return false;
        }

        /// <summary>
        /// List of events to trigger webhook callbacks for. Either this or &#x60;all_events&#x60; is required.
        /// </summary>
        /// <value>List of events to trigger webhook callbacks for. Either this or &#x60;all_events&#x60; is required.</value>
        [DataMember(Name = "events", IsRequired = true, EmitDefaultValue = false)]
        public List<Event> Events { get; set; }

        /// <summary>
        /// Gets or Sets Callback
        /// </summary>
        [DataMember(Name = "callback", IsRequired = true, EmitDefaultValue = false)]
        public string Callback { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Webhook {\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  AllEvents: ").Append(AllEvents).Append("\n");
            sb.Append("  PublicKey: ").Append(PublicKey).Append("\n");
            sb.Append("  Events: ").Append(Events).Append("\n");
            sb.Append("  Callback: ").Append(Callback).Append("\n");
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
            return this.Equals(input as Webhook);
        }

        /// <summary>
        /// Returns true if Webhook instances are equal
        /// </summary>
        /// <param name="input">Instance of Webhook to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Webhook input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.CreatedOn == input.CreatedOn ||
                    this.CreatedOn.Equals(input.CreatedOn)
                ) && 
                (
                    this.UpdatedOn == input.UpdatedOn ||
                    this.UpdatedOn.Equals(input.UpdatedOn)
                ) && 
                (
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) && 
                (
                    this.AllEvents == input.AllEvents ||
                    this.AllEvents.Equals(input.AllEvents)
                ) && 
                (
                    this.PublicKey == input.PublicKey ||
                    (this.PublicKey != null &&
                    this.PublicKey.Equals(input.PublicKey))
                ) && 
                (
                    this.Events == input.Events ||
                    this.Events != null &&
                    input.Events != null &&
                    this.Events.SequenceEqual(input.Events)
                ) && 
                (
                    this.Callback == input.Callback ||
                    (this.Callback != null &&
                    this.Callback.Equals(input.Callback))
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
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.CreatedOn.GetHashCode();
                hashCode = hashCode * 59 + this.UpdatedOn.GetHashCode();
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                hashCode = hashCode * 59 + this.AllEvents.GetHashCode();
                if (this.PublicKey != null)
                    hashCode = hashCode * 59 + this.PublicKey.GetHashCode();
                if (this.Events != null)
                    hashCode = hashCode * 59 + this.Events.GetHashCode();
                if (this.Callback != null)
                    hashCode = hashCode * 59 + this.Callback.GetHashCode();
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
            // Title (string) maxLength
            if(this.Title != null && this.Title.Length > 100)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Title, length must be less than 100.", new [] { "Title" });
            }

            // Callback (string) maxLength
            if(this.Callback != null && this.Callback.Length > 500)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Callback, length must be less than 500.", new [] { "Callback" });
            }

            yield break;
        }
    }

}
