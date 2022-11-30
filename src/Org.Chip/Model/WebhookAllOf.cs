

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
    /// Defines a webhook rule to an external server. The &#x60;callback&#x60; URL will receive a POST request with the related object&#39;s (e.g. Purchase for &#x60;purchase.*&#x60; webhooks) data in body when any of the events (see the description of &#x60;events&#x60; field below) it is configured to listen for are triggered. The payload object will additionally include an \&quot;event_type\&quot; field to indicate which event type (see the Webhook.events field) triggered the webhook.   Note that, as well as with the rest of dataset, test and live Webhooks are separate; test webhooks will not handle events caused by live Purchases, and vice-versa.
    /// </summary>
    [DataContract(Name = "Webhook_allOf")]
    public partial class WebhookAllOf : IEquatable<WebhookAllOf>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookAllOf" /> class.
        /// </summary>
        [JsonConstructorAttribute]
        protected WebhookAllOf() { }
        /// <summary>
        /// Initializes a new instance of the <see cref="WebhookAllOf" /> class.
        /// </summary>
        /// <param name="title">Arbitrary title of webhook (required).</param>
        /// <param name="allEvents">Specifies this webhook should trigger on all event types. Either this or &#x60;events&#x60; is required. (default to false).</param>
        /// <param name="events">List of events to trigger webhook callbacks for. Either this or &#x60;all_events&#x60; is required. (required).</param>
        /// <param name="callback">callback (required).</param>
        public WebhookAllOf(string title = default(string), bool allEvents = false, List<Event> events = default(List<Event>), string callback = default(string))
        {
            // to ensure "title" is required (not null)
            this.Title = title ?? throw new ArgumentNullException("title is a required property for WebhookAllOf and cannot be null");
            // to ensure "events" is required (not null)
            this.Events = events ?? throw new ArgumentNullException("events is a required property for WebhookAllOf and cannot be null");
            // to ensure "callback" is required (not null)
            this.Callback = callback ?? throw new ArgumentNullException("callback is a required property for WebhookAllOf and cannot be null");
            this.AllEvents = allEvents;
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
            sb.Append("class WebhookAllOf {\n");
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
            return this.Equals(input as WebhookAllOf);
        }

        /// <summary>
        /// Returns true if WebhookAllOf instances are equal
        /// </summary>
        /// <param name="input">Instance of WebhookAllOf to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(WebhookAllOf input)
        {
            if (input == null)
                return false;

            return 
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
