

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
    /// BillingTemplate generates Purchase objects, either to issue one-time invoices or in a subscription.  It does so by copying over its&#39; &#x60;PurchaseDetails&#x60;, one of its &#x60;BillingTemplateClient&#x60;-s and generating other fields from BillingTemplate&#39;s fields as necessary into a new &#x60;Purchase&#x60; object.  If &#x60;is_subscription&#x60; is &#x60;true&#x60;, it is considered to be a subscription&#39;s BillingTemplate. You will need to specify &#x60;subscription_*&#x60; fields like &#x60;subscription_period&#x60; when creating it and add BillingTemplateClient objects to its billing cycle (&#x60;POST /billing_templates/{id}/add_subscriber/&#x60;). After that the clients will receive recurring invoices (that will be paid for automatically if client saves their card) according to the BillingTemplate settings you have specified.  If &#x60;is_subscription&#x60; is &#x60;false&#x60;, this BillingTemplate is used to send one-time invoices. After creating it and specifying &#x60;invoice_*&#x60; fields, use &#x60;POST /billing_templates/{id}/send_invoice/&#x60; request to send the actual invoices. BillingTemplateClients for non-subscription BillingTemplates are not saved.
    /// </summary>
    [DataContract(Name = "BillingTemplate")]
    public partial class BillingTemplate : IEquatable<BillingTemplate>, IValidatableObject
    {
        /// <summary>
        /// See &#x60;subscription_period&#x60;.
        /// </summary>
        /// <value>See &#x60;subscription_period&#x60;.</value>
        [DataMember(Name = "subscription_period_units", EmitDefaultValue = false)]
        public PeriodUnits? SubscriptionPeriodUnits { get; set; }
        /// <summary>
        /// See &#x60;subscription_due_period&#x60;.
        /// </summary>
        /// <value>See &#x60;subscription_due_period&#x60;.</value>
        [DataMember(Name = "subscription_due_period_units", EmitDefaultValue = false)]
        public PeriodUnits? SubscriptionDuePeriodUnits { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingTemplate" /> class.
        /// </summary>
        /// <param name="purchase">purchase (required).</param>
        /// <param name="brandId">ID of the brand to create this BillingTemplate for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;..</param>
        /// <param name="title">title.</param>
        /// <param name="isSubscription">Defines whether this BillingTemplate issues invoices in a recurring manner - it&#39;s a subscription - or it sends invoices only once. You can&#39;t change this parameter when you edit the BillingTemplate. If this field is &#x60;true&#x60;, you will need to specify &#x60;subscription_*&#x60; fields and &#x60;invoice_*&#x60; fields are read-only, and vice-versa. (required).</param>
        /// <param name="invoiceIssued">Sets &#x60;issued&#x60; on the Purchase objects generated. Generated from current day in &#x60;purchase.timezone&#x60; if not provided. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;..</param>
        /// <param name="invoiceDue">Sets &#x60;due&#x60; on the Purchase objects generated. Required if &#x60;is_subscription &#x3D;&#x3D; false&#x60;, read-only otherwise..</param>
        /// <param name="invoiceSkipCapture">Sets &#x60;skip_capture&#x60; on the Purchase objects generated. &#x60;false&#x60; by default. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;. (default to false).</param>
        /// <param name="invoiceSendReceipt">Sets &#x60;send_receipt&#x60; on the Purchase objects generated. &#x60;true&#x60; by default (unlike in Purchases API, where by default receipts are not sent). Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;. (default to false).</param>
        /// <param name="subscriptionPeriod">Defines how often are the subscription Purchases generated. Used together with &#x60;subscription_period_units&#x60;: to issue Purchases once a month, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;months\&quot;&#x60;.   Variable number of days in a month is respected; e.g. if subscription has a period of 1 month, a client had its billing cycle activated on January 30 and there are 28 days in February that year - billing scheduled for February will happen on 28th.  Both fields are required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; they are read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;. (default to 1).</param>
        /// <param name="subscriptionPeriodUnits">See &#x60;subscription_period&#x60;..</param>
        /// <param name="subscriptionDuePeriod">Used to generate &#x60;due&#x60; on the Purchase objects generated. Used together with &#x60;subscription_due_period_units&#x60;: to set the final &#x60;Purchase.due&#x60; to a week after it&#39;s generated/invoice is sent, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;weeks\&quot;. Required if &#x60;is_subscription &#x3D;&#x3D; true&#x60;, read-only otherwise. (default to 7).</param>
        /// <param name="subscriptionDuePeriodUnits">See &#x60;subscription_due_period&#x60;..</param>
        /// <param name="subscriptionChargePeriodEnd">If this is &#x60;true&#x60;, clients are charged at the end of billing periods, and vice-versa. E.g. if you add a subscriber client to a BillingTemplate, with this value being set to &#x60;false&#x60;, he will receive first invoice today, otherwise - after a single billing period (defined by &#x60;subscription_period&#x60;/&#x60;subscription_period_units&#x60;) passes.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;. (default to false).</param>
        /// <param name="subscriptionTrialPeriods">How many trial periods to give the client prior to starting his billing cycle. If billing period is 1 month and you set this value to 2, subscription will automatically adjust to giving your client 2 months without payments and then charging him for the 3rd month (when exactly depends on &#x60;subscription_charge_period_end&#x60;: 3 months after the subscriber was launched for &#x60;false&#x60;, 4 for &#x60;true&#x60;). &#x60;\&quot;subscription_trial_periods\&quot;: 0&#x60; disables this feature.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;. (default to 0).</param>
        /// <param name="subscriptionActive">Whether this subscription is paused. Has the same effect as setting &#x60;\&quot;status\&quot;: \&quot;subscription_paused\&quot;&#x60; for every BillingTemplateClient launched for this subscription, see the description of &#x60;status&#x60; on BillingTemplateClient for more details.  Ignored (read-only) if &#x60;is_subscription &#x3D;&#x3D; false&#x60;. (default to false).</param>
        public BillingTemplate(PurchaseDetails purchase = default(PurchaseDetails), Guid brandId = default(Guid), string title = default(string), bool isSubscription = default(bool), string invoiceIssued = default(string), int? invoiceDue = default(int?), bool invoiceSkipCapture = false, bool invoiceSendReceipt = false, int subscriptionPeriod = 1, PeriodUnits? subscriptionPeriodUnits = default(PeriodUnits?), int subscriptionDuePeriod = 7, PeriodUnits? subscriptionDuePeriodUnits = default(PeriodUnits?), bool subscriptionChargePeriodEnd = false, int subscriptionTrialPeriods = 0, bool subscriptionActive = false)
        {
            // to ensure "purchase" is required (not null)
            this.Purchase = purchase ?? throw new ArgumentNullException("purchase is a required property for BillingTemplate and cannot be null");
            this.IsSubscription = isSubscription;
            this.BrandId = brandId;
            this.Title = title;
            this.InvoiceIssued = invoiceIssued;
            this.InvoiceDue = invoiceDue;
            this.InvoiceSkipCapture = invoiceSkipCapture;
            this.InvoiceSendReceipt = invoiceSendReceipt;
            this.SubscriptionPeriod = subscriptionPeriod;
            this.SubscriptionPeriodUnits = subscriptionPeriodUnits;
            this.SubscriptionDuePeriod = subscriptionDuePeriod;
            this.SubscriptionDuePeriodUnits = subscriptionDuePeriodUnits;
            this.SubscriptionChargePeriodEnd = subscriptionChargePeriodEnd;
            this.SubscriptionTrialPeriods = subscriptionTrialPeriods;
            this.SubscriptionActive = subscriptionActive;
        }

        /// <summary>
        /// Gets or Sets Purchase
        /// </summary>
        [DataMember(Name = "purchase", IsRequired = true, EmitDefaultValue = false)]
        public PurchaseDetails Purchase { get; set; }

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
        /// ID of the brand to create this BillingTemplate for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;.
        /// </summary>
        /// <value>ID of the brand to create this BillingTemplate for. You can copy it down in the API section, see the \&quot;specify the ID of the Brand\&quot; link in answer to \&quot;How to setup payments on website or in mobile app?\&quot;.</value>
        [DataMember(Name = "brand_id", EmitDefaultValue = false)]
        public Guid BrandId { get; set; }

        /// <summary>
        /// Gets or Sets Title
        /// </summary>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Defines whether this BillingTemplate issues invoices in a recurring manner - it&#39;s a subscription - or it sends invoices only once. You can&#39;t change this parameter when you edit the BillingTemplate. If this field is &#x60;true&#x60;, you will need to specify &#x60;subscription_*&#x60; fields and &#x60;invoice_*&#x60; fields are read-only, and vice-versa.
        /// </summary>
        /// <value>Defines whether this BillingTemplate issues invoices in a recurring manner - it&#39;s a subscription - or it sends invoices only once. You can&#39;t change this parameter when you edit the BillingTemplate. If this field is &#x60;true&#x60;, you will need to specify &#x60;subscription_*&#x60; fields and &#x60;invoice_*&#x60; fields are read-only, and vice-versa.</value>
        [DataMember(Name = "is_subscription", IsRequired = true, EmitDefaultValue = false)]
        public bool IsSubscription { get; set; }

        /// <summary>
        /// Sets &#x60;issued&#x60; on the Purchase objects generated. Generated from current day in &#x60;purchase.timezone&#x60; if not provided. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.
        /// </summary>
        /// <value>Sets &#x60;issued&#x60; on the Purchase objects generated. Generated from current day in &#x60;purchase.timezone&#x60; if not provided. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.</value>
        [DataMember(Name = "invoice_issued", EmitDefaultValue = true)]
        public string InvoiceIssued { get; set; }

        /// <summary>
        /// Sets &#x60;due&#x60; on the Purchase objects generated. Required if &#x60;is_subscription &#x3D;&#x3D; false&#x60;, read-only otherwise.
        /// </summary>
        /// <value>Sets &#x60;due&#x60; on the Purchase objects generated. Required if &#x60;is_subscription &#x3D;&#x3D; false&#x60;, read-only otherwise.</value>
        [DataMember(Name = "invoice_due", EmitDefaultValue = true)]
        public int? InvoiceDue { get; set; }

        /// <summary>
        /// Sets &#x60;skip_capture&#x60; on the Purchase objects generated. &#x60;false&#x60; by default. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.
        /// </summary>
        /// <value>Sets &#x60;skip_capture&#x60; on the Purchase objects generated. &#x60;false&#x60; by default. Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.</value>
        [DataMember(Name = "invoice_skip_capture", EmitDefaultValue = false)]
        public bool InvoiceSkipCapture { get; set; }

        /// <summary>
        /// Sets &#x60;send_receipt&#x60; on the Purchase objects generated. &#x60;true&#x60; by default (unlike in Purchases API, where by default receipts are not sent). Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.
        /// </summary>
        /// <value>Sets &#x60;send_receipt&#x60; on the Purchase objects generated. &#x60;true&#x60; by default (unlike in Purchases API, where by default receipts are not sent). Read-only if &#x60;is_subscription &#x3D;&#x3D; true&#x60;.</value>
        [DataMember(Name = "invoice send_receipt", EmitDefaultValue = false)]
        public bool InvoiceSendReceipt { get; set; }

        /// <summary>
        /// Defines how often are the subscription Purchases generated. Used together with &#x60;subscription_period_units&#x60;: to issue Purchases once a month, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;months\&quot;&#x60;.   Variable number of days in a month is respected; e.g. if subscription has a period of 1 month, a client had its billing cycle activated on January 30 and there are 28 days in February that year - billing scheduled for February will happen on 28th.  Both fields are required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; they are read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.
        /// </summary>
        /// <value>Defines how often are the subscription Purchases generated. Used together with &#x60;subscription_period_units&#x60;: to issue Purchases once a month, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;months\&quot;&#x60;.   Variable number of days in a month is respected; e.g. if subscription has a period of 1 month, a client had its billing cycle activated on January 30 and there are 28 days in February that year - billing scheduled for February will happen on 28th.  Both fields are required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; they are read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.</value>
        [DataMember(Name = "subscription_period", EmitDefaultValue = false)]
        public int SubscriptionPeriod { get; set; }

        /// <summary>
        /// Used to generate &#x60;due&#x60; on the Purchase objects generated. Used together with &#x60;subscription_due_period_units&#x60;: to set the final &#x60;Purchase.due&#x60; to a week after it&#39;s generated/invoice is sent, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;weeks\&quot;. Required if &#x60;is_subscription &#x3D;&#x3D; true&#x60;, read-only otherwise.
        /// </summary>
        /// <value>Used to generate &#x60;due&#x60; on the Purchase objects generated. Used together with &#x60;subscription_due_period_units&#x60;: to set the final &#x60;Purchase.due&#x60; to a week after it&#39;s generated/invoice is sent, use &#x60;\&quot;...period\&quot;: 1&#x60; and &#x60;\&quot;...period_units\&quot; &#x3D;&#x3D; \&quot;weeks\&quot;. Required if &#x60;is_subscription &#x3D;&#x3D; true&#x60;, read-only otherwise.</value>
        [DataMember(Name = "subscription_due_period", EmitDefaultValue = false)]
        public int SubscriptionDuePeriod { get; set; }

        /// <summary>
        /// If this is &#x60;true&#x60;, clients are charged at the end of billing periods, and vice-versa. E.g. if you add a subscriber client to a BillingTemplate, with this value being set to &#x60;false&#x60;, he will receive first invoice today, otherwise - after a single billing period (defined by &#x60;subscription_period&#x60;/&#x60;subscription_period_units&#x60;) passes.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.
        /// </summary>
        /// <value>If this is &#x60;true&#x60;, clients are charged at the end of billing periods, and vice-versa. E.g. if you add a subscriber client to a BillingTemplate, with this value being set to &#x60;false&#x60;, he will receive first invoice today, otherwise - after a single billing period (defined by &#x60;subscription_period&#x60;/&#x60;subscription_period_units&#x60;) passes.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.</value>
        [DataMember(Name = "subscription_charge_period_end", EmitDefaultValue = false)]
        public bool SubscriptionChargePeriodEnd { get; set; }

        /// <summary>
        /// How many trial periods to give the client prior to starting his billing cycle. If billing period is 1 month and you set this value to 2, subscription will automatically adjust to giving your client 2 months without payments and then charging him for the 3rd month (when exactly depends on &#x60;subscription_charge_period_end&#x60;: 3 months after the subscriber was launched for &#x60;false&#x60;, 4 for &#x60;true&#x60;). &#x60;\&quot;subscription_trial_periods\&quot;: 0&#x60; disables this feature.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.
        /// </summary>
        /// <value>How many trial periods to give the client prior to starting his billing cycle. If billing period is 1 month and you set this value to 2, subscription will automatically adjust to giving your client 2 months without payments and then charging him for the 3rd month (when exactly depends on &#x60;subscription_charge_period_end&#x60;: 3 months after the subscriber was launched for &#x60;false&#x60;, 4 for &#x60;true&#x60;). &#x60;\&quot;subscription_trial_periods\&quot;: 0&#x60; disables this feature.   Required when creating a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60;/editing a BillingTemplate with &#x60;is_subscription &#x3D;&#x3D; true&#x60; as long as there aren&#39;t any launched subscribers; read-only otherwise, whether it&#39;s BillingTemplate&#39;s editing when there already are clients activated or if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.</value>
        [DataMember(Name = "subscription_trial_periods", EmitDefaultValue = false)]
        public int SubscriptionTrialPeriods { get; set; }

        /// <summary>
        /// Whether this subscription is paused. Has the same effect as setting &#x60;\&quot;status\&quot;: \&quot;subscription_paused\&quot;&#x60; for every BillingTemplateClient launched for this subscription, see the description of &#x60;status&#x60; on BillingTemplateClient for more details.  Ignored (read-only) if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.
        /// </summary>
        /// <value>Whether this subscription is paused. Has the same effect as setting &#x60;\&quot;status\&quot;: \&quot;subscription_paused\&quot;&#x60; for every BillingTemplateClient launched for this subscription, see the description of &#x60;status&#x60; on BillingTemplateClient for more details.  Ignored (read-only) if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.</value>
        [DataMember(Name = "subscription_active", EmitDefaultValue = false)]
        public bool SubscriptionActive { get; set; }

        /// <summary>
        /// If this is &#x60;true&#x60;, there were launched clients (&#x60;POST /billing_templates/{id}/add_subscriber/&#x60; - or subscribers that were added via the gateway system UI) for this subscription.  While this is &#x60;false&#x60; (it will be as long as you&#39;re only just created the template and haven&#39;t launched any subscribers), you can edit all of &#x60;subscription_*&#x60; fields.   If this is &#x60;true&#x60;, you&#39;re only allowed to edit &#x60;subscription_due_period&#x60;, &#x60;subscription_due_period_units&#x60; and &#x60;subscription_active&#x60;.   Is always &#x60;false&#x60; if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.
        /// </summary>
        /// <value>If this is &#x60;true&#x60;, there were launched clients (&#x60;POST /billing_templates/{id}/add_subscriber/&#x60; - or subscribers that were added via the gateway system UI) for this subscription.  While this is &#x60;false&#x60; (it will be as long as you&#39;re only just created the template and haven&#39;t launched any subscribers), you can edit all of &#x60;subscription_*&#x60; fields.   If this is &#x60;true&#x60;, you&#39;re only allowed to edit &#x60;subscription_due_period&#x60;, &#x60;subscription_due_period_units&#x60; and &#x60;subscription_active&#x60;.   Is always &#x60;false&#x60; if &#x60;is_subscription &#x3D;&#x3D; false&#x60;.</value>
        [DataMember(Name = "subscription_has_active_clients", EmitDefaultValue = false)]
        public bool SubscriptionHasActiveClients { get; private set; }

        /// <summary>
        /// Returns false as SubscriptionHasActiveClients should not be serialized given that it's read-only.
        /// </summary>
        /// <returns>false (boolean)</returns>
        public bool ShouldSerializeSubscriptionHasActiveClients()
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
            sb.Append("class BillingTemplate {\n");
            sb.Append("  Purchase: ").Append(Purchase).Append("\n");
            sb.Append("  CompanyId: ").Append(CompanyId).Append("\n");
            sb.Append("  IsTest: ").Append(IsTest).Append("\n");
            sb.Append("  UserId: ").Append(UserId).Append("\n");
            sb.Append("  BrandId: ").Append(BrandId).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  IsSubscription: ").Append(IsSubscription).Append("\n");
            sb.Append("  InvoiceIssued: ").Append(InvoiceIssued).Append("\n");
            sb.Append("  InvoiceDue: ").Append(InvoiceDue).Append("\n");
            sb.Append("  InvoiceSkipCapture: ").Append(InvoiceSkipCapture).Append("\n");
            sb.Append("  InvoiceSendReceipt: ").Append(InvoiceSendReceipt).Append("\n");
            sb.Append("  SubscriptionPeriod: ").Append(SubscriptionPeriod).Append("\n");
            sb.Append("  SubscriptionPeriodUnits: ").Append(SubscriptionPeriodUnits).Append("\n");
            sb.Append("  SubscriptionDuePeriod: ").Append(SubscriptionDuePeriod).Append("\n");
            sb.Append("  SubscriptionDuePeriodUnits: ").Append(SubscriptionDuePeriodUnits).Append("\n");
            sb.Append("  SubscriptionChargePeriodEnd: ").Append(SubscriptionChargePeriodEnd).Append("\n");
            sb.Append("  SubscriptionTrialPeriods: ").Append(SubscriptionTrialPeriods).Append("\n");
            sb.Append("  SubscriptionActive: ").Append(SubscriptionActive).Append("\n");
            sb.Append("  SubscriptionHasActiveClients: ").Append(SubscriptionHasActiveClients).Append("\n");
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
            return this.Equals(input as BillingTemplate);
        }

        /// <summary>
        /// Returns true if BillingTemplate instances are equal
        /// </summary>
        /// <param name="input">Instance of BillingTemplate to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(BillingTemplate input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Purchase == input.Purchase ||
                    (this.Purchase != null &&
                    this.Purchase.Equals(input.Purchase))
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
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) && 
                (
                    this.IsSubscription == input.IsSubscription ||
                    this.IsSubscription.Equals(input.IsSubscription)
                ) && 
                (
                    this.InvoiceIssued == input.InvoiceIssued ||
                    (this.InvoiceIssued != null &&
                    this.InvoiceIssued.Equals(input.InvoiceIssued))
                ) && 
                (
                    this.InvoiceDue == input.InvoiceDue ||
                    (this.InvoiceDue != null &&
                    this.InvoiceDue.Equals(input.InvoiceDue))
                ) && 
                (
                    this.InvoiceSkipCapture == input.InvoiceSkipCapture ||
                    this.InvoiceSkipCapture.Equals(input.InvoiceSkipCapture)
                ) && 
                (
                    this.InvoiceSendReceipt == input.InvoiceSendReceipt ||
                    this.InvoiceSendReceipt.Equals(input.InvoiceSendReceipt)
                ) && 
                (
                    this.SubscriptionPeriod == input.SubscriptionPeriod ||
                    this.SubscriptionPeriod.Equals(input.SubscriptionPeriod)
                ) && 
                (
                    this.SubscriptionPeriodUnits == input.SubscriptionPeriodUnits ||
                    this.SubscriptionPeriodUnits.Equals(input.SubscriptionPeriodUnits)
                ) && 
                (
                    this.SubscriptionDuePeriod == input.SubscriptionDuePeriod ||
                    this.SubscriptionDuePeriod.Equals(input.SubscriptionDuePeriod)
                ) && 
                (
                    this.SubscriptionDuePeriodUnits == input.SubscriptionDuePeriodUnits ||
                    this.SubscriptionDuePeriodUnits.Equals(input.SubscriptionDuePeriodUnits)
                ) && 
                (
                    this.SubscriptionChargePeriodEnd == input.SubscriptionChargePeriodEnd ||
                    this.SubscriptionChargePeriodEnd.Equals(input.SubscriptionChargePeriodEnd)
                ) && 
                (
                    this.SubscriptionTrialPeriods == input.SubscriptionTrialPeriods ||
                    this.SubscriptionTrialPeriods.Equals(input.SubscriptionTrialPeriods)
                ) && 
                (
                    this.SubscriptionActive == input.SubscriptionActive ||
                    this.SubscriptionActive.Equals(input.SubscriptionActive)
                ) && 
                (
                    this.SubscriptionHasActiveClients == input.SubscriptionHasActiveClients ||
                    this.SubscriptionHasActiveClients.Equals(input.SubscriptionHasActiveClients)
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
                if (this.Purchase != null)
                    hashCode = hashCode * 59 + this.Purchase.GetHashCode();
                if (this.CompanyId != null)
                    hashCode = hashCode * 59 + this.CompanyId.GetHashCode();
                hashCode = hashCode * 59 + this.IsTest.GetHashCode();
                if (this.UserId != null)
                    hashCode = hashCode * 59 + this.UserId.GetHashCode();
                if (this.BrandId != null)
                    hashCode = hashCode * 59 + this.BrandId.GetHashCode();
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                hashCode = hashCode * 59 + this.IsSubscription.GetHashCode();
                if (this.InvoiceIssued != null)
                    hashCode = hashCode * 59 + this.InvoiceIssued.GetHashCode();
                if (this.InvoiceDue != null)
                    hashCode = hashCode * 59 + this.InvoiceDue.GetHashCode();
                hashCode = hashCode * 59 + this.InvoiceSkipCapture.GetHashCode();
                hashCode = hashCode * 59 + this.InvoiceSendReceipt.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionPeriod.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionPeriodUnits.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionDuePeriod.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionDuePeriodUnits.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionChargePeriodEnd.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionTrialPeriods.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionActive.GetHashCode();
                hashCode = hashCode * 59 + this.SubscriptionHasActiveClients.GetHashCode();
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
            // Title (string) maxLength
            if(this.Title != null && this.Title.Length > 256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for Title, length must be less than 256.", new [] { "Title" });
            }

            // SubscriptionPeriod (int) maximum
            if(this.SubscriptionPeriod > (int)256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionPeriod, must be a value less than or equal to 256.", new [] { "SubscriptionPeriod" });
            }

            // SubscriptionPeriod (int) minimum
            if(this.SubscriptionPeriod < (int)1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionPeriod, must be a value greater than or equal to 1.", new [] { "SubscriptionPeriod" });
            }

            // SubscriptionDuePeriod (int) maximum
            if(this.SubscriptionDuePeriod > (int)256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionDuePeriod, must be a value less than or equal to 256.", new [] { "SubscriptionDuePeriod" });
            }

            // SubscriptionDuePeriod (int) minimum
            if(this.SubscriptionDuePeriod < (int)1)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionDuePeriod, must be a value greater than or equal to 1.", new [] { "SubscriptionDuePeriod" });
            }

            // SubscriptionTrialPeriods (int) maximum
            if(this.SubscriptionTrialPeriods > (int)256)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionTrialPeriods, must be a value less than or equal to 256.", new [] { "SubscriptionTrialPeriods" });
            }

            // SubscriptionTrialPeriods (int) minimum
            if(this.SubscriptionTrialPeriods < (int)0)
            {
                yield return new System.ComponentModel.DataAnnotations.ValidationResult("Invalid value for SubscriptionTrialPeriods, must be a value greater than or equal to 0.", new [] { "SubscriptionTrialPeriods" });
            }

            yield break;
        }
    }

}
