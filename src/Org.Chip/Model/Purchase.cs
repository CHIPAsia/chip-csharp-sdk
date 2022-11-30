

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
    /// Record of a single purchase operation, either a transaction originating from e-commerce integration or invoice sent. Has a status attribute, e.g. can be &#x60;created&#x60;, &#x60;paid&#x60; or &#x60;refunded&#x60;.
    /// </summary>
    [DataContract(Name = "Purchase")]
    public partial class Purchase : IEquatable<Purchase>, IValidatableObject
    {
        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public PurchaseStatus? Status { get; set; }
        /// <summary>
        /// Specifies, if the purchase can be refunded fully and partially, only fully, partially or not at all.
        /// </summary>
        /// <value>Specifies, if the purchase can be refunded fully and partially, only fully, partially or not at all.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum RefundAvailabilityEnum
        {
            /// <summary>
            /// Enum All for value: all
            /// </summary>
            [EnumMember(Value = "all")]
            All = 1,

            /// <summary>
            /// Enum Fullonly for value: full_only
            /// </summary>
            [EnumMember(Value = "full_only")]
            Fullonly = 2,

            /// <summary>
            /// Enum Partialonly for value: partial_only
            /// </summary>
            [EnumMember(Value = "partial_only")]
            Partialonly = 3,

            /// <summary>
            /// Enum None for value: none
            /// </summary>
            [EnumMember(Value = "none")]
            None = 4

        }

        /// <summary>
        /// Specifies, if the purchase can be refunded fully and partially, only fully, partially or not at all.
        /// </summary>
        /// <value>Specifies, if the purchase can be refunded fully and partially, only fully, partially or not at all.</value>
        [DataMember(Name = "refund_availability", EmitDefaultValue = false)]
        public RefundAvailabilityEnum? RefundAvailability { get; set; }

        /// <summary>
        /// Returns false as RefundAvailability should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRefundAvailability()
        {
            return false;
        }
        /// <summary>
        /// Platform this Purchase was created on.
        /// </summary>
        /// <value>Platform this Purchase was created on.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum PlatformEnum
        {
            /// <summary>
            /// Enum Web for value: web
            /// </summary>
            [EnumMember(Value = "web")]
            Web = 1,

            /// <summary>
            /// Enum Api for value: api
            /// </summary>
            [EnumMember(Value = "api")]
            Api = 2,

            /// <summary>
            /// Enum Ios for value: ios
            /// </summary>
            [EnumMember(Value = "ios")]
            Ios = 3,

            /// <summary>
            /// Enum Android for value: android
            /// </summary>
            [EnumMember(Value = "android")]
            Android = 4,

            /// <summary>
            /// Enum Macos for value: macos
            /// </summary>
            [EnumMember(Value = "macos")]
            Macos = 5,

            /// <summary>
            /// Enum Windows for value: windows
            /// </summary>
            [EnumMember(Value = "windows")]
            Windows = 6

        }

        /// <summary>
        /// Platform this Purchase was created on.
        /// </summary>
        /// <value>Platform this Purchase was created on.</value>
        [DataMember(Name = "platform", EmitDefaultValue = false)]
        public PlatformEnum? Platform { get; set; }
        /// <summary>
        /// Defines which gateway product was used to create this Purchase.
        /// </summary>
        /// <value>Defines which gateway product was used to create this Purchase.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ProductEnum
        {
            /// <summary>
            /// Enum Purchases for value: purchases
            /// </summary>
            [EnumMember(Value = "purchases")]
            Purchases = 1,

            /// <summary>
            /// Enum Billinginvoices for value: billing_invoices
            /// </summary>
            [EnumMember(Value = "billing_invoices")]
            Billinginvoices = 2,

            /// <summary>
            /// Enum Billingsubscriptions for value: billing_subscriptions
            /// </summary>
            [EnumMember(Value = "billing_subscriptions")]
            Billingsubscriptions = 3,

            /// <summary>
            /// Enum Billingsubscriptionsinvoice for value: billing_subscriptions_invoice
            /// </summary>
            [EnumMember(Value = "billing_subscriptions_invoice")]
            Billingsubscriptionsinvoice = 4

        }

        /// <summary>
        /// Defines which gateway product was used to create this Purchase.
        /// </summary>
        /// <value>Defines which gateway product was used to create this Purchase.</value>
        [DataMember(Name = "product", EmitDefaultValue = false)]
        public ProductEnum? Product { get; set; }

        /// <summary>
        /// Returns false as Product should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeProduct()
        {
            return false;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Purchase" /> class.
        /// </summary>
        /// <param name="_client">Either this or &#x60;.client_id&#x60; is required. (required).</param>
        /// <param name="purchase">purchase (required).</param>
        /// <param name="status">status.</param>
        /// <param name="brandId">ID of the brand to create this Purchase for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;. (required).</param>
        /// <param name="clientId">ID of a Client object used to initialize ClientDetails (&#x60;.client&#x60;) of this Purchase. Either this field or specifying &#x60;.client&#x60; object is required (you can only specify a value for one of these fields). All &#x60;ClientDetails&#x60; fields from the Client will be copied to &#x60;.client&#x60; object. Note that editing Client object won&#39;t change the respective fields in already created Purchases.   If you specify this field and your client saves a &#x60;recurring_token&#x60; (for instance, by saving their card), the respective ClientRecurringToken will be created. See the &#x60;/clients/{id}/recurring_tokens/&#x60; endpoint..</param>
        /// <param name="sendReceipt">Whether to send receipt email for this Purchase when it&#39;s paid. (default to false).</param>
        /// <param name="skipCapture">Card payment-specific: if set to true, only authorize the payment (place funds on hold) when payer enters his card data and pays. This option requires a &#x60;POST /capture/&#x60; or &#x60;POST /release/&#x60; later on.   You can use the preauthorization feature if you set this parameter to true and make the Purchase with &#x60;purchase.total &#x3D;&#x3D; 0&#x60; (this can be achieved by providing a list of &#x60;purchase.products&#x60; with a total &#x60;price&#x60; of 0, or simply overriding the total using &#x60;purchase.total_override&#x60; to 0). The resulting Purchase can only be \&quot;paid\&quot; by the client (only cardholder data verification will happen, without a financial transaction) by card and will enforce saving the client&#39;s card. When this happens, the Purchase will have &#x60;status&#x60; of &#x60;preauthorized&#x60; and the &#x60;purchase.preauthorized&#x60; webhook callbacks will be emitted.   Trying to use skip_capture (or preauthorization) without any payment methods that support the respective actions (this can be a result of &#x60;payment_method_whitelist&#x60; field being used) will result in an error on Purchase creation request step. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager. (default to false).</param>
        /// <param name="forceRecurring">If the used payment method supports recurring payment functionality, forces the customer&#39;s payment credentials to be saved for possible later recurring payments, without giving the customer a choice in the matter. (default to false).</param>
        /// <param name="reference">Invoice reference..</param>
        /// <param name="issued">Value for &#39;Invoice issued&#39; field. Display-only, does not get validated. If not provided, will be generated as the current date in &#x60;purchase.timezone&#x60; at the moment of Purchase&#39;s creation..</param>
        /// <param name="due">When the payment is due for this Purchase. The default behaviour is to still allow payment once this moment passes. To change that, set &#x60;purchase.due_strict&#x60; to true..</param>
        /// <param name="paymentMethodWhitelist">An optional whitelist of payment methods availble for this purchase. Use this field if you want to restrict your payer to pay using only one or several specific methods.   Using this field and at the same time trying to use specific capabilities of a Purchase (e.g. &#x60;skip_capture&#x60; or charging it using a saved card token using &#x60;POST /purchases/{id}/charge/&#x60;) can cause a situation when there are no payment methods available for paying this Purchase. This will cause a validation error on Purchase creation. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager..</param>
        /// <param name="successRedirect">When Purchase is paid for successfully, your customer will be taken to this link. Otherwise a standard screen will be displayed..</param>
        /// <param name="failureRedirect">If there&#39;s a payment failure for this Purchase, your customer will be taken to this link. Otherwise a standard screen will be displayed..</param>
        /// <param name="cancelRedirect">If you provide this link, customer will have an option to go to it instead of making payment (a button with &#39;Return to seller&#39; text will be displayed). Can&#39;t contain any of the following symbols: &#x60;&lt;&gt;&#39;\&quot;&#x60; .  Be aware that this does not cancel the payment (e.g. does not do the equivalent of doing the &#x60;POST /purchases/{id}/cancel/&#x60; request); the client will still be able to press &#39;Back&#39; in the browser and perform the payment..</param>
        /// <param name="successCallback">When Purchase is paid for successfully, the &#x60;success_callback&#x60; URL will receive a POST request with the Purchase object&#39;s data in body..</param>
        /// <param name="creatorAgent">Identification of software (e.g. an ecommerce module and version) used to create this purchase, if any..</param>
        /// <param name="platform">Platform this Purchase was created on..</param>
        public Purchase(ClientDetails _client = default(ClientDetails), PurchaseDetails purchase = default(PurchaseDetails), PurchaseStatus? status = default(PurchaseStatus?), Guid brandId = default(Guid), Guid? clientId = default(Guid?), bool sendReceipt = false, bool skipCapture = false, bool forceRecurring = false, string reference = default(string), string issued = default(string), int? due = default(int?), List<string> paymentMethodWhitelist = default(List<string>), string successRedirect = default(string), string failureRedirect = default(string), string cancelRedirect = default(string), string successCallback = default(string), string creatorAgent = default(string), PlatformEnum? platform = default(PlatformEnum?))
        {
            // to ensure "_client" is required (not null)
            this._Client = _client ?? throw new ArgumentNullException("_client is a required property for Purchase and cannot be null");
            // to ensure "purchase" is required (not null)
            this._Purchase = purchase ?? throw new ArgumentNullException("purchase is a required property for Purchase and cannot be null");
            this.BrandId = brandId;
            this.Status = status;
            this.ClientId = clientId;
            this.SendReceipt = sendReceipt;
            this.SkipCapture = skipCapture;
            this.ForceRecurring = forceRecurring;
            this.Reference = reference;
            this.Issued = issued;
            this.Due = due;
            this.PaymentMethodWhitelist = paymentMethodWhitelist;
            this.SuccessRedirect = successRedirect;
            this.FailureRedirect = failureRedirect;
            this.CancelRedirect = cancelRedirect;
            this.SuccessCallback = successCallback;
            this.CreatorAgent = creatorAgent;
            this.Platform = platform;
        }

        /// <summary>
        /// Either this or &#x60;.client_id&#x60; is required.
        /// </summary>
        /// <value>Either this or &#x60;.client_id&#x60; is required.</value>
        [DataMember(Name = "client", IsRequired = true, EmitDefaultValue = false)]
        public ClientDetails _Client { get; set; }

        /// <summary>
        /// Gets or Sets _Purchase
        /// </summary>
        [DataMember(Name = "purchase", IsRequired = true, EmitDefaultValue = false)]
        public PurchaseDetails _Purchase { get; set; }

        /// <summary>
        /// Gets or Sets Payment
        /// </summary>
        [DataMember(Name = "payment", EmitDefaultValue = true)]
        public PaymentDetails Payment { get; private set; }

        /// <summary>
        /// Returns false as Payment should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializePayment()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets IssuerDetails
        /// </summary>
        [DataMember(Name = "issuer_details", EmitDefaultValue = false)]
        public IssuerDetails IssuerDetails { get; private set; }

        /// <summary>
        /// Returns false as IssuerDetails should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeIssuerDetails()
        {
            return false;
        }

        /// <summary>
        /// Payment method-specific, read-only transaction data. Will contain information about all the transaction attempts and possible errors, if available.
        /// </summary>
        /// <value>Payment method-specific, read-only transaction data. Will contain information about all the transaction attempts and possible errors, if available.</value>
        [DataMember(Name = "transaction_data", EmitDefaultValue = false)]
        public Object TransactionData { get; private set; }

        /// <summary>
        /// Returns false as TransactionData should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeTransactionData()
        {
            return false;
        }

        /// <summary>
        /// History of status changes, latest last. Might contain entry about a related object, e.g. status change to &#x60;refunded&#x60; will contain a reference to the refund Payment.
        /// </summary>
        /// <value>History of status changes, latest last. Might contain entry about a related object, e.g. status change to &#x60;refunded&#x60; will contain a reference to the refund Payment.</value>
        [DataMember(Name = "status_history", EmitDefaultValue = false)]
        public List<Object> StatusHistory { get; private set; }

        /// <summary>
        /// Returns false as StatusHistory should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeStatusHistory()
        {
            return false;
        }

        /// <summary>
        /// Time the payment form or invoice page was first viewed on
        /// </summary>
        /// <value>Time the payment form or invoice page was first viewed on</value>
        [DataMember(Name = "viewed_on", EmitDefaultValue = true)]
        public int? ViewedOn { get; private set; }

        /// <summary>
        /// Returns false as ViewedOn should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeViewedOn()
        {
            return false;
        }

        /// <summary>
        /// Gets or Sets CompanyId
        /// </summary>
        [DataMember(Name = "company_id", EmitDefaultValue = false)]
        public Guid CompanyId { get; private set; }

        /// <summary>
        /// Returns false as CompanyId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCompanyId()
        {
            return false;
        }

        /// <summary>
        /// Indicates this is a test object, created using test API keys or using Billing section of UI while in test mode.
        /// </summary>
        /// <value>Indicates this is a test object, created using test API keys or using Billing section of UI while in test mode.</value>
        [DataMember(Name = "is_test", EmitDefaultValue = false)]
        public bool IsTest { get; private set; }

        /// <summary>
        /// Returns false as IsTest should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeIsTest()
        {
            return false;
        }

        /// <summary>
        /// ID of user who has created this object in the Billing UI, if applicable.
        /// </summary>
        /// <value>ID of user who has created this object in the Billing UI, if applicable.</value>
        [DataMember(Name = "user_id", EmitDefaultValue = true)]
        public Guid? UserId { get; private set; }

        /// <summary>
        /// Returns false as UserId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeUserId()
        {
            return false;
        }

        /// <summary>
        /// ID of the brand to create this Purchase for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;.
        /// </summary>
        /// <value>ID of the brand to create this Purchase for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;.</value>
        [DataMember(Name = "brand_id", IsRequired = true, EmitDefaultValue = false)]
        public Guid BrandId { get; set; }

        /// <summary>
        /// ID of a BillingTemplate that has spawned this Purchase, if any.
        /// </summary>
        /// <value>ID of a BillingTemplate that has spawned this Purchase, if any.</value>
        [DataMember(Name = "billing_template_id", EmitDefaultValue = true)]
        public Guid? BillingTemplateId { get; private set; }

        /// <summary>
        /// Returns false as BillingTemplateId should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeBillingTemplateId()
        {
            return false;
        }

        /// <summary>
        /// ID of a Client object used to initialize ClientDetails (&#x60;.client&#x60;) of this Purchase. Either this field or specifying &#x60;.client&#x60; object is required (you can only specify a value for one of these fields). All &#x60;ClientDetails&#x60; fields from the Client will be copied to &#x60;.client&#x60; object. Note that editing Client object won&#39;t change the respective fields in already created Purchases.   If you specify this field and your client saves a &#x60;recurring_token&#x60; (for instance, by saving their card), the respective ClientRecurringToken will be created. See the &#x60;/clients/{id}/recurring_tokens/&#x60; endpoint.
        /// </summary>
        /// <value>ID of a Client object used to initialize ClientDetails (&#x60;.client&#x60;) of this Purchase. Either this field or specifying &#x60;.client&#x60; object is required (you can only specify a value for one of these fields). All &#x60;ClientDetails&#x60; fields from the Client will be copied to &#x60;.client&#x60; object. Note that editing Client object won&#39;t change the respective fields in already created Purchases.   If you specify this field and your client saves a &#x60;recurring_token&#x60; (for instance, by saving their card), the respective ClientRecurringToken will be created. See the &#x60;/clients/{id}/recurring_tokens/&#x60; endpoint.</value>
        [DataMember(Name = "client_id", EmitDefaultValue = false)]
        public Guid? ClientId { get; set; }

        /// <summary>
        /// Whether to send receipt email for this Purchase when it&#39;s paid.
        /// </summary>
        /// <value>Whether to send receipt email for this Purchase when it&#39;s paid.</value>
        [DataMember(Name = "send_receipt", EmitDefaultValue = false)]
        public bool SendReceipt { get; set; }

        /// <summary>
        /// Indicates whether a recurring token (e.g. for card payments - card token) was saved for this Purchase. If this is &#x60;true&#x60;, the &#x60;id&#x60; of this Purchase can be used as a &#x60;recurring_token&#x60; in &#x60;POST /purchases/{id}/charge/&#x60;, enabling you to pay for that Purchase using the same method (same card for card payments) that this one was paid with.
        /// </summary>
        /// <value>Indicates whether a recurring token (e.g. for card payments - card token) was saved for this Purchase. If this is &#x60;true&#x60;, the &#x60;id&#x60; of this Purchase can be used as a &#x60;recurring_token&#x60; in &#x60;POST /purchases/{id}/charge/&#x60;, enabling you to pay for that Purchase using the same method (same card for card payments) that this one was paid with.</value>
        [DataMember(Name = "is_recurring_token", EmitDefaultValue = false)]
        public bool IsRecurringToken { get; private set; }

        /// <summary>
        /// Returns false as IsRecurringToken should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeIsRecurringToken()
        {
            return false;
        }

        /// <summary>
        /// ID of a recurring token (Purchase having &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;) that was used to pay this Purchase, if any.
        /// </summary>
        /// <value>ID of a recurring token (Purchase having &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;) that was used to pay this Purchase, if any.</value>
        [DataMember(Name = "recurring_token", EmitDefaultValue = true)]
        public Guid? RecurringToken { get; private set; }

        /// <summary>
        /// Returns false as RecurringToken should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRecurringToken()
        {
            return false;
        }

        /// <summary>
        /// Card payment-specific: if set to true, only authorize the payment (place funds on hold) when payer enters his card data and pays. This option requires a &#x60;POST /capture/&#x60; or &#x60;POST /release/&#x60; later on.   You can use the preauthorization feature if you set this parameter to true and make the Purchase with &#x60;purchase.total &#x3D;&#x3D; 0&#x60; (this can be achieved by providing a list of &#x60;purchase.products&#x60; with a total &#x60;price&#x60; of 0, or simply overriding the total using &#x60;purchase.total_override&#x60; to 0). The resulting Purchase can only be \&quot;paid\&quot; by the client (only cardholder data verification will happen, without a financial transaction) by card and will enforce saving the client&#39;s card. When this happens, the Purchase will have &#x60;status&#x60; of &#x60;preauthorized&#x60; and the &#x60;purchase.preauthorized&#x60; webhook callbacks will be emitted.   Trying to use skip_capture (or preauthorization) without any payment methods that support the respective actions (this can be a result of &#x60;payment_method_whitelist&#x60; field being used) will result in an error on Purchase creation request step. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager.
        /// </summary>
        /// <value>Card payment-specific: if set to true, only authorize the payment (place funds on hold) when payer enters his card data and pays. This option requires a &#x60;POST /capture/&#x60; or &#x60;POST /release/&#x60; later on.   You can use the preauthorization feature if you set this parameter to true and make the Purchase with &#x60;purchase.total &#x3D;&#x3D; 0&#x60; (this can be achieved by providing a list of &#x60;purchase.products&#x60; with a total &#x60;price&#x60; of 0, or simply overriding the total using &#x60;purchase.total_override&#x60; to 0). The resulting Purchase can only be \&quot;paid\&quot; by the client (only cardholder data verification will happen, without a financial transaction) by card and will enforce saving the client&#39;s card. When this happens, the Purchase will have &#x60;status&#x60; of &#x60;preauthorized&#x60; and the &#x60;purchase.preauthorized&#x60; webhook callbacks will be emitted.   Trying to use skip_capture (or preauthorization) without any payment methods that support the respective actions (this can be a result of &#x60;payment_method_whitelist&#x60; field being used) will result in an error on Purchase creation request step. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager.</value>
        [DataMember(Name = "skip_capture", EmitDefaultValue = false)]
        public bool SkipCapture { get; set; }

        /// <summary>
        /// If the used payment method supports recurring payment functionality, forces the customer&#39;s payment credentials to be saved for possible later recurring payments, without giving the customer a choice in the matter.
        /// </summary>
        /// <value>If the used payment method supports recurring payment functionality, forces the customer&#39;s payment credentials to be saved for possible later recurring payments, without giving the customer a choice in the matter.</value>
        [DataMember(Name = "force_recurring", EmitDefaultValue = false)]
        public bool ForceRecurring { get; set; }

        /// <summary>
        /// If you don&#39;t provide an invoice &#x60;reference&#x60; yourself, this autogenerated value will be used as a reference instead.
        /// </summary>
        /// <value>If you don&#39;t provide an invoice &#x60;reference&#x60; yourself, this autogenerated value will be used as a reference instead.</value>
        [DataMember(Name = "reference_generated", EmitDefaultValue = false)]
        public string ReferenceGenerated { get; private set; }

        /// <summary>
        /// Returns false as ReferenceGenerated should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeReferenceGenerated()
        {
            return false;
        }

        /// <summary>
        /// Invoice reference.
        /// </summary>
        /// <value>Invoice reference.</value>
        [DataMember(Name = "reference", EmitDefaultValue = false)]
        public string Reference { get; set; }

        /// <summary>
        /// Value for &#39;Invoice issued&#39; field. Display-only, does not get validated. If not provided, will be generated as the current date in &#x60;purchase.timezone&#x60; at the moment of Purchase&#39;s creation.
        /// </summary>
        /// <value>Value for &#39;Invoice issued&#39; field. Display-only, does not get validated. If not provided, will be generated as the current date in &#x60;purchase.timezone&#x60; at the moment of Purchase&#39;s creation.</value>
        [DataMember(Name = "issued", EmitDefaultValue = false)]
        public string Issued { get; set; }

        /// <summary>
        /// When the payment is due for this Purchase. The default behaviour is to still allow payment once this moment passes. To change that, set &#x60;purchase.due_strict&#x60; to true.
        /// </summary>
        /// <value>When the payment is due for this Purchase. The default behaviour is to still allow payment once this moment passes. To change that, set &#x60;purchase.due_strict&#x60; to true.</value>
        [DataMember(Name = "due", EmitDefaultValue = false)]
        public int? Due { get; set; }

        /// <summary>
        /// Gets or Sets RefundableAmount
        /// </summary>
        [DataMember(Name = "refundable_amount", EmitDefaultValue = false)]
        public int RefundableAmount { get; private set; }

        /// <summary>
        /// Returns false as RefundableAmount should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeRefundableAmount()
        {
            return false;
        }

        /// <summary>
        /// This object is present when automatic currency conversion has occurred upon creation of the purchase. Purchase&#39;s original currency was changed and its original amount was converted using the exchange rate shown here.
        /// </summary>
        /// <value>This object is present when automatic currency conversion has occurred upon creation of the purchase. Purchase&#39;s original currency was changed and its original amount was converted using the exchange rate shown here.</value>
        [DataMember(Name = "currency_conversion", EmitDefaultValue = true)]
        public Object CurrencyConversion { get; private set; }

        /// <summary>
        /// Returns false as CurrencyConversion should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCurrencyConversion()
        {
            return false;
        }

        /// <summary>
        /// An optional whitelist of payment methods availble for this purchase. Use this field if you want to restrict your payer to pay using only one or several specific methods.   Using this field and at the same time trying to use specific capabilities of a Purchase (e.g. &#x60;skip_capture&#x60; or charging it using a saved card token using &#x60;POST /purchases/{id}/charge/&#x60;) can cause a situation when there are no payment methods available for paying this Purchase. This will cause a validation error on Purchase creation. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager.
        /// </summary>
        /// <value>An optional whitelist of payment methods availble for this purchase. Use this field if you want to restrict your payer to pay using only one or several specific methods.   Using this field and at the same time trying to use specific capabilities of a Purchase (e.g. &#x60;skip_capture&#x60; or charging it using a saved card token using &#x60;POST /purchases/{id}/charge/&#x60;) can cause a situation when there are no payment methods available for paying this Purchase. This will cause a validation error on Purchase creation. Please check the &#x60;GET /payment_methods/&#x60; response for your desired Purchase parameters and/or consult with your account manager.</value>
        [DataMember(Name = "payment_method_whitelist", EmitDefaultValue = false)]
        public List<string> PaymentMethodWhitelist { get; set; }

        /// <summary>
        /// When Purchase is paid for successfully, your customer will be taken to this link. Otherwise a standard screen will be displayed.
        /// </summary>
        /// <value>When Purchase is paid for successfully, your customer will be taken to this link. Otherwise a standard screen will be displayed.</value>
        [DataMember(Name = "success_redirect", EmitDefaultValue = false)]
        public string SuccessRedirect { get; set; }

        /// <summary>
        /// If there&#39;s a payment failure for this Purchase, your customer will be taken to this link. Otherwise a standard screen will be displayed.
        /// </summary>
        /// <value>If there&#39;s a payment failure for this Purchase, your customer will be taken to this link. Otherwise a standard screen will be displayed.</value>
        [DataMember(Name = "failure_redirect", EmitDefaultValue = false)]
        public string FailureRedirect { get; set; }

        /// <summary>
        /// If you provide this link, customer will have an option to go to it instead of making payment (a button with &#39;Return to seller&#39; text will be displayed). Can&#39;t contain any of the following symbols: &#x60;&lt;&gt;&#39;\&quot;&#x60; .  Be aware that this does not cancel the payment (e.g. does not do the equivalent of doing the &#x60;POST /purchases/{id}/cancel/&#x60; request); the client will still be able to press &#39;Back&#39; in the browser and perform the payment.
        /// </summary>
        /// <value>If you provide this link, customer will have an option to go to it instead of making payment (a button with &#39;Return to seller&#39; text will be displayed). Can&#39;t contain any of the following symbols: &#x60;&lt;&gt;&#39;\&quot;&#x60; .  Be aware that this does not cancel the payment (e.g. does not do the equivalent of doing the &#x60;POST /purchases/{id}/cancel/&#x60; request); the client will still be able to press &#39;Back&#39; in the browser and perform the payment.</value>
        [DataMember(Name = "cancel_redirect", EmitDefaultValue = false)]
        public string CancelRedirect { get; set; }

        /// <summary>
        /// When Purchase is paid for successfully, the &#x60;success_callback&#x60; URL will receive a POST request with the Purchase object&#39;s data in body.
        /// </summary>
        /// <value>When Purchase is paid for successfully, the &#x60;success_callback&#x60; URL will receive a POST request with the Purchase object&#39;s data in body.</value>
        [DataMember(Name = "success_callback", EmitDefaultValue = false)]
        public string SuccessCallback { get; set; }

        /// <summary>
        /// Identification of software (e.g. an ecommerce module and version) used to create this purchase, if any.
        /// </summary>
        /// <value>Identification of software (e.g. an ecommerce module and version) used to create this purchase, if any.</value>
        [DataMember(Name = "creator_agent", EmitDefaultValue = false)]
        public string CreatorAgent { get; set; }

        /// <summary>
        /// IP the Purchase was created from.
        /// </summary>
        /// <value>IP the Purchase was created from.</value>
        [DataMember(Name = "created_from_ip", EmitDefaultValue = false)]
        public string CreatedFromIp { get; private set; }

        /// <summary>
        /// Returns false as CreatedFromIp should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCreatedFromIp()
        {
            return false;
        }

        /// <summary>
        /// URL you will be able to access invoice for this Purchase at, if applicable
        /// </summary>
        /// <value>URL you will be able to access invoice for this Purchase at, if applicable</value>
        [DataMember(Name = "invoice_url", EmitDefaultValue = true)]
        public string InvoiceUrl { get; private set; }

        /// <summary>
        /// Returns false as InvoiceUrl should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeInvoiceUrl()
        {
            return false;
        }

        /// <summary>
        /// URL you will be able to access the checkout for this Purchase at, if payment for it is possible. When building integrations, redirect the customer to this URL once purchase is created.   You can add the &#x60;preferred&#x60; query arg to the &#x60;checkout_url&#x60; in order to force redirect the client straight to the checkout for a specific payment method (&#x60;?preferred&#x3D;{payment_method}&#x60;, where &#x60;{payment_method}&#x60; is the payment method name as returned by &#x60;GET /payment_methods/&#x60;). If this method redirects the client further to a different system and no customer data entry is needed on gateway&#39;s checkout page, your payer will be taken straight to that page (not seeing the gateway&#39;s checkout UI); otherwise, he will see the payment method entry UI on the gateway checkout page.
        /// </summary>
        /// <value>URL you will be able to access the checkout for this Purchase at, if payment for it is possible. When building integrations, redirect the customer to this URL once purchase is created.   You can add the &#x60;preferred&#x60; query arg to the &#x60;checkout_url&#x60; in order to force redirect the client straight to the checkout for a specific payment method (&#x60;?preferred&#x3D;{payment_method}&#x60;, where &#x60;{payment_method}&#x60; is the payment method name as returned by &#x60;GET /payment_methods/&#x60;). If this method redirects the client further to a different system and no customer data entry is needed on gateway&#39;s checkout page, your payer will be taken straight to that page (not seeing the gateway&#39;s checkout UI); otherwise, he will see the payment method entry UI on the gateway checkout page.</value>
        [DataMember(Name = "checkout_url", EmitDefaultValue = false)]
        public string CheckoutUrl { get; private set; }

        /// <summary>
        /// Returns false as CheckoutUrl should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeCheckoutUrl()
        {
            return false;
        }

        /// <summary>
        /// URL that can be used for Direct Post integration.   This functionality is activated for each merchant account individually. Please consult with your account manager if you wish to use it.   Will be null if payment for purchase is not possible, &#x60;purchase.request_client_details&#x60; isn&#39;t empty or success_redirect/failure_redirect are not provided - these all break the usual direct post flow.  To leverage Direct Post checkout, create a &#x60;&lt;form&gt;&#x60; having &#x60;method&#x3D;\&quot;POST\&quot; action&#x3D;\&quot;&lt;direct_post_url value&gt;\&quot;&#x60; and include the following inputs:  &#x60;cardholder_name: text, Latin letters only (space and apostrophe (&#x60;&#39;&#x60;), dot (&#x60;.&#x60;), dash (&#x60;-&#x60;) symbols are also allowed), max 40 chars&#x60;  - --  &#x60;card_number: text, digits only, no whitespace, max 19 chars&#x60;  - --  &#x60;expires: text in &#39;MM/YY&#39; format, digits and a slash only /^\\d{2}/\\d{2}$/, max 5 chars&#x60;  - --  &#x60;cvc: number (integer) in [100: 9999] range&#x60;  - --  &#x60;remember_card: checkbox with value&#x3D;\&quot;on\&quot; (the default when omitting value attribute of a checkbox input)&#x60;  Ensure the validation as listed above! Validation errors will be treated as payment failures. Obviously, you can style this form to fit in with the rest of your website.  When your payer submits this form (don&#39;t forget a &#x60;&lt;button&gt;&#x60; or &#x60;&lt;input type&#x3D;\&quot;submit\&quot;&gt;&#x60;), he will POST the data directly to the gateway system. There, with minimal interaction with gateway&#39;s interface, payment will be processed. In the process, your customer might get redirected to authenticate against 3D Secure system of his card issuer bank (this depends on settings of his card and your account). After that, payer will be taken to &#x60;success_redirect&#x60; or &#x60;failure_redirect&#x60; depending on the payment result (as in the usual payment flow).  Be aware, though, that while not having to process card data allows you not to comply with the entirety of PCI DSS SAQ D requirements, having sensitive cardholder data entry form on your website does raise your PCI DSS scope to SAQ A-EP. Contact your account manager to receive advisory and assistance for this integration method.
        /// </summary>
        /// <value>URL that can be used for Direct Post integration.   This functionality is activated for each merchant account individually. Please consult with your account manager if you wish to use it.   Will be null if payment for purchase is not possible, &#x60;purchase.request_client_details&#x60; isn&#39;t empty or success_redirect/failure_redirect are not provided - these all break the usual direct post flow.  To leverage Direct Post checkout, create a &#x60;&lt;form&gt;&#x60; having &#x60;method&#x3D;\&quot;POST\&quot; action&#x3D;\&quot;&lt;direct_post_url value&gt;\&quot;&#x60; and include the following inputs:  &#x60;cardholder_name: text, Latin letters only (space and apostrophe (&#x60;&#39;&#x60;), dot (&#x60;.&#x60;), dash (&#x60;-&#x60;) symbols are also allowed), max 40 chars&#x60;  - --  &#x60;card_number: text, digits only, no whitespace, max 19 chars&#x60;  - --  &#x60;expires: text in &#39;MM/YY&#39; format, digits and a slash only /^\\d{2}/\\d{2}$/, max 5 chars&#x60;  - --  &#x60;cvc: number (integer) in [100: 9999] range&#x60;  - --  &#x60;remember_card: checkbox with value&#x3D;\&quot;on\&quot; (the default when omitting value attribute of a checkbox input)&#x60;  Ensure the validation as listed above! Validation errors will be treated as payment failures. Obviously, you can style this form to fit in with the rest of your website.  When your payer submits this form (don&#39;t forget a &#x60;&lt;button&gt;&#x60; or &#x60;&lt;input type&#x3D;\&quot;submit\&quot;&gt;&#x60;), he will POST the data directly to the gateway system. There, with minimal interaction with gateway&#39;s interface, payment will be processed. In the process, your customer might get redirected to authenticate against 3D Secure system of his card issuer bank (this depends on settings of his card and your account). After that, payer will be taken to &#x60;success_redirect&#x60; or &#x60;failure_redirect&#x60; depending on the payment result (as in the usual payment flow).  Be aware, though, that while not having to process card data allows you not to comply with the entirety of PCI DSS SAQ D requirements, having sensitive cardholder data entry form on your website does raise your PCI DSS scope to SAQ A-EP. Contact your account manager to receive advisory and assistance for this integration method.</value>
        [DataMember(Name = "direct_post_url", EmitDefaultValue = true)]
        public string DirectPostUrl { get; private set; }

        /// <summary>
        /// Returns false as DirectPostUrl should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeDirectPostUrl()
        {
            return false;
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
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Purchase {\n");
            sb.Append("  _Client: ").Append(_Client).Append("\n");
            sb.Append("  _Purchase: ").Append(_Purchase).Append("\n");
            sb.Append("  Payment: ").Append(Payment).Append("\n");
            sb.Append("  IssuerDetails: ").Append(IssuerDetails).Append("\n");
            sb.Append("  TransactionData: ").Append(TransactionData).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StatusHistory: ").Append(StatusHistory).Append("\n");
            sb.Append("  ViewedOn: ").Append(ViewedOn).Append("\n");
            sb.Append("  CompanyId: ").Append(CompanyId).Append("\n");
            sb.Append("  IsTest: ").Append(IsTest).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  BrandId: ").Append(BrandId).Append("\n");
            sb.Append("  BillingTemplateId: ").Append(BillingTemplateId).Append("\n");
            sb.Append("  ClientId: ").Append(ClientId).Append("\n");
            sb.Append("  SendReceipt: ").Append(SendReceipt).Append("\n");
            sb.Append("  IsRecurringToken: ").Append(IsRecurringToken).Append("\n");
            sb.Append("  RecurringToken: ").Append(RecurringToken).Append("\n");
            sb.Append("  SkipCapture: ").Append(SkipCapture).Append("\n");
            sb.Append("  ForceRecurring: ").Append(ForceRecurring).Append("\n");
            sb.Append("  ReferenceGenerated: ").Append(ReferenceGenerated).Append("\n");
            sb.Append("  Reference: ").Append(Reference).Append("\n");
            sb.Append("  Issued: ").Append(Issued).Append("\n");
            sb.Append("  Due: ").Append(Due).Append("\n");
            sb.Append("  RefundAvailability: ").Append(RefundAvailability).Append("\n");
            sb.Append("  RefundableAmount: ").Append(RefundableAmount).Append("\n");
            sb.Append("  CurrencyConversion: ").Append(CurrencyConversion).Append("\n");
            sb.Append("  PaymentMethodWhitelist: ").Append(PaymentMethodWhitelist).Append("\n");
            sb.Append("  SuccessRedirect: ").Append(SuccessRedirect).Append("\n");
            sb.Append("  FailureRedirect: ").Append(FailureRedirect).Append("\n");
            sb.Append("  CancelRedirect: ").Append(CancelRedirect).Append("\n");
            sb.Append("  SuccessCallback: ").Append(SuccessCallback).Append("\n");
            sb.Append("  CreatorAgent: ").Append(CreatorAgent).Append("\n");
            sb.Append("  Platform: ").Append(Platform).Append("\n");
            sb.Append("  Product: ").Append(Product).Append("\n");
            sb.Append("  CreatedFromIp: ").Append(CreatedFromIp).Append("\n");
            sb.Append("  InvoiceUrl: ").Append(InvoiceUrl).Append("\n");
            sb.Append("  CheckoutUrl: ").Append(CheckoutUrl).Append("\n");
            sb.Append("  DirectPostUrl: ").Append(DirectPostUrl).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  CreatedOn: ").Append(CreatedOn).Append("\n");
            sb.Append("  UpdatedOn: ").Append(UpdatedOn).Append("\n");
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
            return this.Equals(input as Purchase);
        }

        /// <summary>
        /// Returns true if Purchase instances are equal
        /// </summary>
        /// <param name="input">Instance of Purchase to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Purchase input)
        {
            if (input == null)
                return false;

            return 
                (
                    this._Client == input._Client ||
                    (this._Client != null &&
                    this._Client.Equals(input._Client))
                ) && 
                (
                    this._Purchase == input._Purchase ||
                    (this._Purchase != null &&
                    this._Purchase.Equals(input._Purchase))
                ) && 
                (
                    this.Payment == input.Payment ||
                    (this.Payment != null &&
                    this.Payment.Equals(input.Payment))
                ) && 
                (
                    this.IssuerDetails == input.IssuerDetails ||
                    (this.IssuerDetails != null &&
                    this.IssuerDetails.Equals(input.IssuerDetails))
                ) && 
                (
                    this.TransactionData == input.TransactionData ||
                    (this.TransactionData != null &&
                    this.TransactionData.Equals(input.TransactionData))
                ) && 
                (
                    this.Status == input.Status ||
                    this.Status.Equals(input.Status)
                ) && 
                (
                    this.StatusHistory == input.StatusHistory ||
                    this.StatusHistory != null &&
                    input.StatusHistory != null &&
                    this.StatusHistory.SequenceEqual(input.StatusHistory)
                ) && 
                (
                    this.ViewedOn == input.ViewedOn ||
                    (this.ViewedOn != null &&
                    this.ViewedOn.Equals(input.ViewedOn))
                ) && 
                (
                    this.CompanyId == input.CompanyId ||
                    (this.CompanyId != null &&
                    this.CompanyId.Equals(input.CompanyId))
                ) && 
                (
                    this.IsTest == input.IsTest ||
                    this.IsTest.Equals(input.IsTest)
                ) && 
                (
                    this.UserId == input.UserId ||
                    (this.UserId != null &&
                    this.UserId.Equals(input.UserId))
                ) && 
                (
                    this.BrandId == input.BrandId ||
                    (this.BrandId != null &&
                    this.BrandId.Equals(input.BrandId))
                ) && 
                (
                    this.BillingTemplateId == input.BillingTemplateId ||
                    (this.BillingTemplateId != null &&
                    this.BillingTemplateId.Equals(input.BillingTemplateId))
                ) && 
                (
                    this.ClientId == input.ClientId ||
                    (this.ClientId != null &&
                    this.ClientId.Equals(input.ClientId))
                ) && 
                (
                    this.SendReceipt == input.SendReceipt ||
                    this.SendReceipt.Equals(input.SendReceipt)
                ) && 
                (
                    this.IsRecurringToken == input.IsRecurringToken ||
                    this.IsRecurringToken.Equals(input.IsRecurringToken)
                ) && 
                (
                    this.RecurringToken == input.RecurringToken ||
                    (this.RecurringToken != null &&
                    this.RecurringToken.Equals(input.RecurringToken))
                ) && 
                (
                    this.SkipCapture == input.SkipCapture ||
                    this.SkipCapture.Equals(input.SkipCapture)
                ) && 
                (
                    this.ForceRecurring == input.ForceRecurring ||
                    this.ForceRecurring.Equals(input.ForceRecurring)
                ) && 
                (
                    this.ReferenceGenerated == input.ReferenceGenerated ||
                    (this.ReferenceGenerated != null &&
                    this.ReferenceGenerated.Equals(input.ReferenceGenerated))
                ) && 
                (
                    this.Reference == input.Reference ||
                    (this.Reference != null &&
                    this.Reference.Equals(input.Reference))
                ) && 
                (
                    this.Issued == input.Issued ||
                    (this.Issued != null &&
                    this.Issued.Equals(input.Issued))
                ) && 
                (
                    this.Due == input.Due ||
                    (this.Due != null &&
                    this.Due.Equals(input.Due))
                ) && 
                (
                    this.RefundAvailability == input.RefundAvailability ||
                    this.RefundAvailability.Equals(input.RefundAvailability)
                ) && 
                (
                    this.RefundableAmount == input.RefundableAmount ||
                    this.RefundableAmount.Equals(input.RefundableAmount)
                ) && 
                (
                    this.CurrencyConversion == input.CurrencyConversion ||
                    (this.CurrencyConversion != null &&
                    this.CurrencyConversion.Equals(input.CurrencyConversion))
                ) && 
                (
                    this.PaymentMethodWhitelist == input.PaymentMethodWhitelist ||
                    this.PaymentMethodWhitelist != null &&
                    input.PaymentMethodWhitelist != null &&
                    this.PaymentMethodWhitelist.SequenceEqual(input.PaymentMethodWhitelist)
                ) && 
                (
                    this.SuccessRedirect == input.SuccessRedirect ||
                    (this.SuccessRedirect != null &&
                    this.SuccessRedirect.Equals(input.SuccessRedirect))
                ) && 
                (
                    this.FailureRedirect == input.FailureRedirect ||
                    (this.FailureRedirect != null &&
                    this.FailureRedirect.Equals(input.FailureRedirect))
                ) && 
                (
                    this.CancelRedirect == input.CancelRedirect ||
                    (this.CancelRedirect != null &&
                    this.CancelRedirect.Equals(input.CancelRedirect))
                ) && 
                (
                    this.SuccessCallback == input.SuccessCallback ||
                    (this.SuccessCallback != null &&
                    this.SuccessCallback.Equals(input.SuccessCallback))
                ) && 
                (
                    this.CreatorAgent == input.CreatorAgent ||
                    (this.CreatorAgent != null &&
                    this.CreatorAgent.Equals(input.CreatorAgent))
                ) && 
                (
                    this.Platform == input.Platform ||
                    this.Platform.Equals(input.Platform)
                ) && 
                (
                    this.Product == input.Product ||
                    this.Product.Equals(input.Product)
                ) && 
                (
                    this.CreatedFromIp == input.CreatedFromIp ||
                    (this.CreatedFromIp != null &&
                    this.CreatedFromIp.Equals(input.CreatedFromIp))
                ) && 
                (
                    this.InvoiceUrl == input.InvoiceUrl ||
                    (this.InvoiceUrl != null &&
                    this.InvoiceUrl.Equals(input.InvoiceUrl))
                ) && 
                (
                    this.CheckoutUrl == input.CheckoutUrl ||
                    (this.CheckoutUrl != null &&
                    this.CheckoutUrl.Equals(input.CheckoutUrl))
                ) && 
                (
                    this.DirectPostUrl == input.DirectPostUrl ||
                    (this.DirectPostUrl != null &&
                    this.DirectPostUrl.Equals(input.DirectPostUrl))
                ) && 
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
                if (this._Client != null)
                    hashCode = hashCode * 59 + this._Client.GetHashCode();
                if (this._Purchase != null)
                    hashCode = hashCode * 59 + this._Purchase.GetHashCode();
                if (this.Payment != null)
                    hashCode = hashCode * 59 + this.Payment.GetHashCode();
                if (this.IssuerDetails != null)
                    hashCode = hashCode * 59 + this.IssuerDetails.GetHashCode();
                if (this.TransactionData != null)
                    hashCode = hashCode * 59 + this.TransactionData.GetHashCode();
                hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.StatusHistory != null)
                    hashCode = hashCode * 59 + this.StatusHistory.GetHashCode();
                if (this.ViewedOn != null)
                    hashCode = hashCode * 59 + this.ViewedOn.GetHashCode();
                if (this.CompanyId != null)
                    hashCode = hashCode * 59 + this.CompanyId.GetHashCode();
                hashCode = hashCode * 59 + this.IsTest.GetHashCode();
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.BrandId != null)
                    hashCode = hashCode * 59 + this.BrandId.GetHashCode();
                if (this.BillingTemplateId != null)
                    hashCode = hashCode * 59 + this.BillingTemplateId.GetHashCode();
                if (this.ClientId != null)
                    hashCode = hashCode * 59 + this.ClientId.GetHashCode();
                hashCode = hashCode * 59 + this.SendReceipt.GetHashCode();
                hashCode = hashCode * 59 + this.IsRecurringToken.GetHashCode();
                if (this.RecurringToken != null)
                    hashCode = hashCode * 59 + this.RecurringToken.GetHashCode();
                hashCode = hashCode * 59 + this.SkipCapture.GetHashCode();
                hashCode = hashCode * 59 + this.ForceRecurring.GetHashCode();
                if (this.ReferenceGenerated != null)
                    hashCode = hashCode * 59 + this.ReferenceGenerated.GetHashCode();
                if (this.Reference != null)
                    hashCode = hashCode * 59 + this.Reference.GetHashCode();
                if (this.Issued != null)
                    hashCode = hashCode * 59 + this.Issued.GetHashCode();
                if (this.Due != null)
                    hashCode = hashCode * 59 + this.Due.GetHashCode();
                hashCode = hashCode * 59 + this.RefundAvailability.GetHashCode();
                hashCode = hashCode * 59 + this.RefundableAmount.GetHashCode();
                if (this.CurrencyConversion != null)
                    hashCode = hashCode * 59 + this.CurrencyConversion.GetHashCode();
                if (this.PaymentMethodWhitelist != null)
                    hashCode = hashCode * 59 + this.PaymentMethodWhitelist.GetHashCode();
                if (this.SuccessRedirect != null)
                    hashCode = hashCode * 59 + this.SuccessRedirect.GetHashCode();
                if (this.FailureRedirect != null)
                    hashCode = hashCode * 59 + this.FailureRedirect.GetHashCode();
                if (this.CancelRedirect != null)
                    hashCode = hashCode * 59 + this.CancelRedirect.GetHashCode();
                if (this.SuccessCallback != null)
                    hashCode = hashCode * 59 + this.SuccessCallback.GetHashCode();
                if (this.CreatorAgent != null)
                    hashCode = hashCode * 59 + this.CreatorAgent.GetHashCode();
                hashCode = hashCode * 59 + this.Platform.GetHashCode();
                hashCode = hashCode * 59 + this.Product.GetHashCode();
                if (this.CreatedFromIp != null)
                    hashCode = hashCode * 59 + this.CreatedFromIp.GetHashCode();
                if (this.InvoiceUrl != null)
                    hashCode = hashCode * 59 + this.InvoiceUrl.GetHashCode();
                if (this.CheckoutUrl != null)
                    hashCode = hashCode * 59 + this.CheckoutUrl.GetHashCode();
                if (this.DirectPostUrl != null)
                    hashCode = hashCode * 59 + this.DirectPostUrl.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                hashCode = hashCode * 59 + this.CreatedOn.GetHashCode();
                hashCode = hashCode * 59 + this.UpdatedOn.GetHashCode();
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
            // Reference (string) maxLength
            if(this.Reference != null && this.Reference.Length > 128)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Reference, length must be less than 128.", new [] { "Reference" });
            }

            // CreatorAgent (string) maxLength
            if(this.CreatorAgent != null && this.CreatorAgent.Length > 32)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for CreatorAgent, length must be less than 32.", new [] { "CreatorAgent" });
            }

            yield break;
        }
    }

}
