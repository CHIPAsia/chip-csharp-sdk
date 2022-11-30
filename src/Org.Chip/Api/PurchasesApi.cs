

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using Org.Chip.Client;
using Org.Chip.Model;

namespace Org.Chip.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPurchasesApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Cancel a pending Purchase
        /// </summary>
        /// <remarks>
        /// If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesCancel(Guid id);

        /// <summary>
        /// Cancel a pending Purchase
        /// </summary>
        /// <remarks>
        /// If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesCancelWithHttpInfo(Guid id);
        /// <summary>
        /// Capture a previously authorized payment
        /// </summary>
        /// <remarks>
        /// Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesCapture(Guid id, Amount amount = default(Amount));

        /// <summary>
        /// Capture a previously authorized payment
        /// </summary>
        /// <remarks>
        /// Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesCaptureWithHttpInfo(Guid id, Amount amount = default(Amount));
        /// <summary>
        /// Charge a Purchase using a saved token
        /// </summary>
        /// <remarks>
        /// Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Token</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesCharge(Guid id, string token);

        /// <summary>
        /// Charge a Purchase using a saved token
        /// </summary>
        /// <remarks>
        /// Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Token</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesChargeWithHttpInfo(Guid id, string token);
        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration.
        /// </summary>
        /// <remarks>
        /// To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <returns>Purchase</returns>
        Purchase PurchasesCreate(Purchase purchase);

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration.
        /// </summary>
        /// <remarks>
        /// To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesCreateWithHttpInfo(Purchase purchase);
        /// <summary>
        /// Delete a recurring token associated with a Purchase
        /// </summary>
        /// <remarks>
        /// Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesDeleteRecurringToken(Guid id);

        /// <summary>
        /// Delete a recurring token associated with a Purchase
        /// </summary>
        /// <remarks>
        /// Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesDeleteRecurringTokenWithHttpInfo(Guid id);
        /// <summary>
        /// Retrieve an object by id
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesRead(Guid id);

        /// <summary>
        /// Retrieve an object by id
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesReadWithHttpInfo(Guid id);
        /// <summary>
        /// Refund a paid purchase
        /// </summary>
        /// <remarks>
        /// Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>Payment</returns>
        Payment PurchasesRefund(Guid id, Amount amount = default(Amount));

        /// <summary>
        /// Refund a paid purchase
        /// </summary>
        /// <remarks>
        /// Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>ApiResponse of Payment</returns>
        ApiResponse<Payment> PurchasesRefundWithHttpInfo(Guid id, Amount amount = default(Amount));
        /// <summary>
        /// Release funds on hold
        /// </summary>
        /// <remarks>
        /// Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        Purchase PurchasesRelease(Guid id);

        /// <summary>
        /// Release funds on hold
        /// </summary>
        /// <remarks>
        /// Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        ApiResponse<Purchase> PurchasesReleaseWithHttpInfo(Guid id);
        #endregion Synchronous Operations

        bool VerifyData(string message, string hash, string certificate);
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPurchasesApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Cancel a pending Purchase
        /// </summary>
        /// <remarks>
        /// If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesCancelAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Cancel a pending Purchase
        /// </summary>
        /// <remarks>
        /// If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesCancelWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Capture a previously authorized payment
        /// </summary>
        /// <remarks>
        /// Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesCaptureAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Capture a previously authorized payment
        /// </summary>
        /// <remarks>
        /// Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesCaptureWithHttpInfoAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Charge a Purchase using a saved token
        /// </summary>
        /// <remarks>
        /// Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesChargeAsync(Guid id, string token, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Charge a Purchase using a saved token
        /// </summary>
        /// <remarks>
        /// Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesChargeWithHttpInfoAsync(Guid id, string token, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration.
        /// </summary>
        /// <remarks>
        /// To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesCreateAsync(Purchase purchase, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration.
        /// </summary>
        /// <remarks>
        /// To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesCreateWithHttpInfoAsync(Purchase purchase, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Delete a recurring token associated with a Purchase
        /// </summary>
        /// <remarks>
        /// Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesDeleteRecurringTokenAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Delete a recurring token associated with a Purchase
        /// </summary>
        /// <remarks>
        /// Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesDeleteRecurringTokenWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Retrieve an object by id
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesReadAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Retrieve an object by id
        /// </summary>
        /// <remarks>
        /// 
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesReadWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Refund a paid purchase
        /// </summary>
        /// <remarks>
        /// Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Payment</returns>
        System.Threading.Tasks.Task<Payment> PurchasesRefundAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Refund a paid purchase
        /// </summary>
        /// <remarks>
        /// Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Payment)</returns>
        System.Threading.Tasks.Task<ApiResponse<Payment>> PurchasesRefundWithHttpInfoAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        /// <summary>
        /// Release funds on hold
        /// </summary>
        /// <remarks>
        /// Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        System.Threading.Tasks.Task<Purchase> PurchasesReleaseAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Release funds on hold
        /// </summary>
        /// <remarks>
        /// Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        System.Threading.Tasks.Task<ApiResponse<Purchase>> PurchasesReleaseWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPurchasesApi : IPurchasesApiSync, IPurchasesApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PurchasesApi : IPurchasesApi
    {
        private Org.Chip.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PurchasesApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasesApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PurchasesApi(String basePath)
        {
            this.Configuration = Org.Chip.Client.Configuration.MergeConfigurations(
                Org.Chip.Client.GlobalConfiguration.Instance,
                new Org.Chip.Client.Configuration { BasePath = basePath }
            );
            this.Client = new Org.Chip.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.Chip.Client.ApiClient(this.Configuration.BasePath);
            this.ExceptionFactory = Org.Chip.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasesApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public PurchasesApi(Org.Chip.Client.Configuration configuration)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Configuration = Org.Chip.Client.Configuration.MergeConfigurations(
                Org.Chip.Client.GlobalConfiguration.Instance,
                configuration
            );
            this.Client = new Org.Chip.Client.ApiClient(this.Configuration.BasePath);
            this.AsynchronousClient = new Org.Chip.Client.ApiClient(this.Configuration.BasePath);
            ExceptionFactory = Org.Chip.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PurchasesApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public PurchasesApi(Org.Chip.Client.ISynchronousClient client, Org.Chip.Client.IAsynchronousClient asyncClient, Org.Chip.Client.IReadableConfiguration configuration)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (asyncClient == null) throw new ArgumentNullException("asyncClient");
            if (configuration == null) throw new ArgumentNullException("configuration");

            this.Client = client;
            this.AsynchronousClient = asyncClient;
            this.Configuration = configuration;
            this.ExceptionFactory = Org.Chip.Client.Configuration.DefaultExceptionFactory;
        }

        /// <summary>
        /// The client for accessing this underlying API asynchronously.
        /// </summary>
        public Org.Chip.Client.IAsynchronousClient AsynchronousClient { get; set; }

        /// <summary>
        /// The client for accessing this underlying API synchronously.
        /// </summary>
        public Org.Chip.Client.ISynchronousClient Client { get; set; }

        /// <summary>
        /// Gets the base path of the API client.
        /// </summary>
        /// <value>The base path</value>
        public String GetBasePath()
        {
            return this.Configuration.BasePath;
        }

        /// <summary>
        /// Gets or sets the configuration object
        /// </summary>
        /// <value>An instance of the Configuration</value>
        public Org.Chip.Client.IReadableConfiguration Configuration { get; set; }

        /// <summary>
        /// Provides a factory method hook for the creation of exceptions.
        /// </summary>
        public Org.Chip.Client.ExceptionFactory ExceptionFactory
        {
            get
            {
                if (_exceptionFactory != null && _exceptionFactory.GetInvocationList().Length > 1)
                {
                    throw new InvalidOperationException("Multicast delegate for ExceptionFactory is unsupported.");
                }
                return _exceptionFactory;
            }
            set { _exceptionFactory = value; }
        }

        /// <summary>
        /// Cancel a pending Purchase If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesCancel(Guid id)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesCancelWithHttpInfo(id);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Cancel a pending Purchase If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesCancelWithHttpInfo(Guid id)
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/{id}/cancel/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCancel", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Cancel a pending Purchase If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesCancelAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesCancelWithHttpInfoAsync(id, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Cancel a pending Purchase If you have a Purchase that payment is possible for, using this request you can guarantee that it won&#39;t be paid.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesCancelWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/{id}/cancel/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCancel", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Capture a previously authorized payment Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesCapture(Guid id, Amount amount = default(Amount))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesCaptureWithHttpInfo(id, amount);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Capture a previously authorized payment Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesCaptureWithHttpInfo(Guid id, Amount amount = default(Amount))
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            if (amount != null)
            {
                localVarRequestOptions.Data = amount;
            }


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/{id}/capture/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCapture", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Capture a previously authorized payment Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesCaptureAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesCaptureWithHttpInfoAsync(id, amount, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Capture a previously authorized payment Capture funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_capture&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_capture&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.captured&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;.  If capture fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_capture_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to capture. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter. By default the full amount is captured, the &#x60;amount&#x60; body param is optional.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesCaptureWithHttpInfoAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            if (amount != null)
            {
                localVarRequestOptions.Data = amount;
            }


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/{id}/capture/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCapture", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Charge a Purchase using a saved token Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesCharge(Guid id, string token)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesChargeWithHttpInfo(id, token);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Charge a Purchase using a saved token Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesChargeWithHttpInfo(Guid id, string token)
        {
            // verify the required parameter 'token' is set
            if (token == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'token' when calling PurchasesApi->PurchasesCharge");

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            var data = new Dictionary<string, string>();
            data.Add("recurring_token", token);
            localVarRequestOptions.Data = data;


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/{id}/charge/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCharge", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Charge a Purchase using a saved token Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesChargeAsync(Guid id, string token, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesChargeWithHttpInfoAsync(id, token, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Charge a Purchase using a saved token Charge a purchase using a &#x60;recurring_token&#x60; provided in the request body. Its value should be an &#x60;id&#x60; of a Purchase that has &#x60;is_recurring_token &#x3D;&#x3D; true&#x60;. This purchase will be paid using the same method (e.g. same card) as the one used to pay the &#x60;recurring_token&#x60; purchase.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_charge&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_charge&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.paid&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;paid&#x60;. Alternatively, if charge fails, you will receive a &#x60;purchase.payment_failure&#x60; callback event.  If recurring charge fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_charge_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to charge. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="token">Recurring token</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesChargeWithHttpInfoAsync(Guid id, string token, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'token' is set
            if (token == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'token' when calling PurchasesApi->PurchasesCharge");


            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            var data = new Dictionary<string, string>();
            data.Add("recurring_token", token);
            localVarRequestOptions.Data = data;


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/{id}/charge/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCharge", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration. To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesCreate(Purchase purchase)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesCreateWithHttpInfo(purchase);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration. To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesCreateWithHttpInfo(Purchase purchase)
        {
            // verify the required parameter 'purchase' is set
            if (purchase == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'purchase' when calling PurchasesApi->PurchasesCreate");

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = purchase;


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCreate", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration. To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesCreateAsync(Purchase purchase, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesCreateWithHttpInfoAsync(purchase, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Create a Purchase. The main request for any e-commerce integration. To run payments in your application use &#x60;POST /purchases/&#x60;, request to register payments and receive the checkout link (&#x60;checkout_url&#x60;). After the payment is processed, gateway will redirect the client back to your website (take note of &#x60;success_redirect&#x60;, &#x60;failure_redirect&#x60;).  You have three options to check payment status: 1) use &#x60;success_callback&#x60; parameter of &#x60;Purchase&#x60; object; 2) use &#x60;GET /purchases/&lt;purchase_id&gt;/&#x60; request; 3) set up a Webhook using the UI or Webhook API to listen to &#x60;purchase.paid&#x60; or &#x60;purchase.payment_failure&#x60; event on your server.  Using &#x60;skip_capture&#x60; flag allows you to separate the authentication and payment execution steps, allowing you to reserve funds on payer’s card account for some time. This flag can also enable preauthorization capability, allowing you to save the card without a financial transaction, if available.  In case making a purchase client agrees to store his card for the upcoming purchases, next time he will be able to pay in a single click.  Instead of a redirect you can also utilize Direct Post checkout: you can create an HTML &#x60;&lt;form&gt;&#x60; on your website with &#x60;method&#x3D;\&quot;POST\&quot;&#x60; and &#x60;action&#x60; pointing to &#x60;direct_post_url&#x60; of a created Purchase. You will also need to saturate form with &#x60;&lt;input&gt;&#x60;-s for card data fields. As a result, when a payer submits their card data, it will be posted straight to our system, allowing you to customize the checkout as you wish while your PCI DSS requirement is only raised to SAQ A-EP, as your system doesn&#39;t receive or process card data. For more details, see the documentation on Purchase&#39;s &#x60;direct_post_url&#x60; field.  To pay for test Purchases, use &#x60;4444 3333 2222 1111&#x60; as the card number, &#x60;123&#x60; as CVC, any date/month greater than now as expiry and any (Latin) cardholder name. Any other card number/CVC/expiry not greater or equal than the current month will all fail a test payment.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="purchase"></param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesCreateWithHttpInfoAsync(Purchase purchase, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'purchase' is set
            if (purchase == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'purchase' when calling PurchasesApi->PurchasesCreate");


            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.Data = purchase;


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesCreate", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a recurring token associated with a Purchase Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesDeleteRecurringToken(Guid id)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesDeleteRecurringTokenWithHttpInfo(id);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a recurring token associated with a Purchase Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesDeleteRecurringTokenWithHttpInfo(Guid id)
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/{id}/delete_recurring_token/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesDeleteRecurringToken", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Delete a recurring token associated with a Purchase Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesDeleteRecurringTokenAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesDeleteRecurringTokenWithHttpInfoAsync(id, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Delete a recurring token associated with a Purchase Will set &#x60;is_recurring_token&#x60; to &#x60;false&#x60;. You won&#39;t be able to use this Purchase&#39;s ID as a &#x60;recurring_token&#x60; anymore. The respective ClientRecurringToken, if any, will also be deleted.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 a corresponding Webhook callback for a &#x60;purchase.pending_recurring_token_delete&#x60; event. To be notified of a successful operation completion, please subscribe to &#x60;purchase.recurring_token_deleted&#x60; callback event.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesDeleteRecurringTokenWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/{id}/delete_recurring_token/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesDeleteRecurringToken", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Retrieve an object by id 
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesRead(Guid id)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesReadWithHttpInfo(id);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieve an object by id 
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesReadWithHttpInfo(Guid id)
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request
            var localVarResponse = this.Client.Get<Purchase>("/purchases/{id}/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRead", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Retrieve an object by id 
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesReadAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesReadWithHttpInfoAsync(id, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Retrieve an object by id 
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesReadWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<Purchase>("/purchases/{id}/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRead", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Refund a paid purchase Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>Payment</returns>
        public Payment PurchasesRefund(Guid id, Amount amount = default(Amount))
        {
            Org.Chip.Client.ApiResponse<Payment> localVarResponse = PurchasesRefundWithHttpInfo(id, amount);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Refund a paid purchase Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <returns>ApiResponse of Payment</returns>
        public Org.Chip.Client.ApiResponse<Payment> PurchasesRefundWithHttpInfo(Guid id, Amount amount = default(Amount))
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            if (amount != null)
            {
                localVarRequestOptions.Data = amount;
            }
            

            // make the HTTP request
            var localVarResponse = this.Client.Post<Payment>("/purchases/{id}/refund/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRefund", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Refund a paid purchase Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Payment</returns>
        public async System.Threading.Tasks.Task<Payment> PurchasesRefundAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Payment> localVarResponse = await PurchasesRefundWithHttpInfoAsync(id, amount, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Refund a paid purchase Will generate a Payment object and return it as a successful response.   Optional &#x60;amount&#x60; argument can be included in the request body to request a partial refund.  Consult &#x60;refund_availability&#x60; field on Purchase on details whether this Purchase can be refunded or not.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_refund&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_refund&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;payment.refunded&#x60; callback event - it will deliver a Payment generated by this refund.  If refund fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_refund_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to refund. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="amount">Amount (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Payment)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Payment>> PurchasesRefundWithHttpInfoAsync(Guid id, Amount amount = default(Amount), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
                "application/json"
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter
            var data = new Dictionary<string, string>();
            if (amount != null)
            {
                localVarRequestOptions.Data = amount;
            }


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Payment>("/purchases/{id}/refund/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRefund", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Release funds on hold Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>Purchase</returns>
        public Purchase PurchasesRelease(Guid id)
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = PurchasesReleaseWithHttpInfo(id);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Release funds on hold Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <returns>ApiResponse of Purchase</returns>
        public Org.Chip.Client.ApiResponse<Purchase> PurchasesReleaseWithHttpInfo(Guid id)
        {
            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };

            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request
            var localVarResponse = this.Client.Post<Purchase>("/purchases/{id}/release/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRelease", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Release funds on hold Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of Purchase</returns>
        public async System.Threading.Tasks.Task<Purchase> PurchasesReleaseAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<Purchase> localVarResponse = await PurchasesReleaseWithHttpInfoAsync(id, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Release funds on hold Release funds reserved for a Purchase (&#x60;status &#x3D;&#x3D; hold&#x60;). You can place a &#x60;hold&#x60; (authenticate the payment) using &#x60;skip_capture &#x3D;&#x3D; true&#x60; when creating the Purchase and ensuring your client submits the payment form.  If this operation takes too long to be processed on the acquirer side - you will get a response with status code 200 and a Purchase object having &#x60;status&#x60; &#x3D; &#x60;pending_release&#x60; in body (you will receive a corresponding Webhook callback too for a &#x60;purchase.pending_release&#x60; event). To be notified of a successful operation completion, please subscribe to &#x60;purchase.released&#x60; callback event - it will deliver an updated Purchase with &#x60;status&#x60; &#x3D; &#x60;released&#x60;.  If fund release fails due to payment processing error, you will receive HTTP response code 400 with error code &#x60;purchase_release_error&#x60;. In this case, to get more details about the error, you should perform a &#x60;GET /purchase/&#x60; request for the Purchase you tried to release funds for. In &#x60;transaction_data.attempts[]&#x60; array (newest element first) you&#39;ll find the corresponding attempt with error code and description in &#x60;.error&#x60; parameter.
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="id">Object ID (UUID)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (Purchase)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<Purchase>> PurchasesReleaseWithHttpInfoAsync(Guid id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {

            Org.Chip.Client.RequestOptions localVarRequestOptions = new Org.Chip.Client.RequestOptions();

            String[] _contentTypes = new String[] {
            };

            // to determine the Accept header
            String[] _accepts = new String[] {
                "application/json"
            };


            var localVarContentType = Org.Chip.Client.ClientUtils.SelectHeaderContentType(_contentTypes);
            if (localVarContentType != null) localVarRequestOptions.HeaderParameters.Add("Content-Type", localVarContentType);

            var localVarAccept = Org.Chip.Client.ClientUtils.SelectHeaderAccept(_accepts);
            if (localVarAccept != null) localVarRequestOptions.HeaderParameters.Add("Accept", localVarAccept);

            localVarRequestOptions.PathParameters.Add("id", Org.Chip.Client.ClientUtils.ParameterToString(id)); // path parameter


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.PostAsync<Purchase>("/purchases/{id}/release/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PurchasesRelease", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        public bool VerifyData(string message, string hash, string certificate)
        {
            bool success = false;
            using (var rsa = new RSACryptoServiceProvider())
            {
                var encoder = new UTF8Encoding();
                byte[] bytesToVerify = encoder.GetBytes(message);

                byte[] signedBytes = Convert.FromBase64String(hash);
                try
                {
                    var replaced = certificate.Replace("\\n", Environment.NewLine);
                    var pemreader = new PemReader(new StringReader(replaced));
                    var cert = (BouncyCastle.Crypto.Parameters.RsaKeyParameters)pemreader.ReadObject();

                    var signer = SignerUtilities.GetSigner("SHA-256withRSA");
                    signer.Init(false, cert);
                    signer.BlockUpdate(bytesToVerify, 0, bytesToVerify.Length);
                    success = signer.VerifySignature(signedBytes);

                }
                catch (CryptographicException e)
                {
                    throw e;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }

            }
            return success;
        }

    }
}
