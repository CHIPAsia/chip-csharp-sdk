

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Mime;
using Org.Chip.Client;
using Org.Chip.Model;

namespace Org.Chip.Api
{

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPaymentMethodsApiSync : IApiAccessor
    {
        #region Synchronous Operations
        /// <summary>
        /// Get the list of payment methods available for your Purchase.
        /// </summary>
        /// <remarks>
        /// Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <returns>PaymentMethods</returns>
        PaymentMethods PaymentMethods(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?));

        /// <summary>
        /// Get the list of payment methods available for your Purchase.
        /// </summary>
        /// <remarks>
        /// Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <returns>ApiResponse of PaymentMethods</returns>
        ApiResponse<PaymentMethods> PaymentMethodsWithHttpInfo(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?));
        #endregion Synchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPaymentMethodsApiAsync : IApiAccessor
    {
        #region Asynchronous Operations
        /// <summary>
        /// Get the list of payment methods available for your Purchase.
        /// </summary>
        /// <remarks>
        /// Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PaymentMethods</returns>
        System.Threading.Tasks.Task<PaymentMethods> PaymentMethodsAsync(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Get the list of payment methods available for your Purchase.
        /// </summary>
        /// <remarks>
        /// Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </remarks>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PaymentMethods)</returns>
        System.Threading.Tasks.Task<ApiResponse<PaymentMethods>> PaymentMethodsWithHttpInfoAsync(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
        #endregion Asynchronous Operations
    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public interface IPaymentMethodsApi : IPaymentMethodsApiSync, IPaymentMethodsApiAsync
    {

    }

    /// <summary>
    /// Represents a collection of functions to interact with the API endpoints
    /// </summary>
    public partial class PaymentMethodsApi : IPaymentMethodsApi
    {
        private Org.Chip.Client.ExceptionFactory _exceptionFactory = (name, response) => null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethodsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PaymentMethodsApi() : this((string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethodsApi"/> class.
        /// </summary>
        /// <returns></returns>
        public PaymentMethodsApi(String basePath)
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
        /// Initializes a new instance of the <see cref="PaymentMethodsApi"/> class
        /// using Configuration object
        /// </summary>
        /// <param name="configuration">An instance of Configuration</param>
        /// <returns></returns>
        public PaymentMethodsApi(Org.Chip.Client.Configuration configuration)
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
        /// Initializes a new instance of the <see cref="PaymentMethodsApi"/> class
        /// using a Configuration object and client instance.
        /// </summary>
        /// <param name="client">The client interface for synchronous API access.</param>
        /// <param name="asyncClient">The client interface for asynchronous API access.</param>
        /// <param name="configuration">The configuration object.</param>
        public PaymentMethodsApi(Org.Chip.Client.ISynchronousClient client, Org.Chip.Client.IAsynchronousClient asyncClient, Org.Chip.Client.IReadableConfiguration configuration)
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
        /// Get the list of payment methods available for your Purchase. Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <returns>PaymentMethods</returns>
        public PaymentMethods PaymentMethods(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?))
        {
            Org.Chip.Client.ApiResponse<PaymentMethods> localVarResponse = PaymentMethodsWithHttpInfo(brandId, currency, country, recurring, skipCapture, preauthorization);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the list of payment methods available for your Purchase. Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <returns>ApiResponse of PaymentMethods</returns>
        public Org.Chip.Client.ApiResponse<PaymentMethods> PaymentMethodsWithHttpInfo(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?))
        {
            // verify the required parameter 'brandId' is set
            if (brandId == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'brandId' when calling PaymentMethodsApi->PaymentMethods");

            // verify the required parameter 'currency' is set
            if (currency == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'currency' when calling PaymentMethodsApi->PaymentMethods");

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

            localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "brand_id", brandId));
            localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "currency", currency));
            if (country != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "country", country));
            }
            if (recurring != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "recurring", recurring));
            }
            if (skipCapture != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "skip_capture", skipCapture));
            }
            if (preauthorization != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "preauthorization", preauthorization));
            }


            // make the HTTP request
            var localVarResponse = this.Client.Get<PaymentMethods>("/payment_methods/", localVarRequestOptions, this.Configuration);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PaymentMethods", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

        /// <summary>
        /// Get the list of payment methods available for your Purchase. Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of PaymentMethods</returns>
        public async System.Threading.Tasks.Task<PaymentMethods> PaymentMethodsAsync(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            Org.Chip.Client.ApiResponse<PaymentMethods> localVarResponse = await PaymentMethodsWithHttpInfoAsync(brandId, currency, country, recurring, skipCapture, preauthorization, cancellationToken).ConfigureAwait(false);
            return localVarResponse.Data;
        }

        /// <summary>
        /// Get the list of payment methods available for your Purchase. Send this request providing, at the very least, the &#x60;brand_id&#x60; and &#x60;currency&#x60; query parameters having the same values you&#39;d use to create your Purchase. Be sure to use the same API key you&#39;ll create your Purchase with; it will define the test_mode setting used in the lookup.  In the response body you&#39;ll receive an object with &#x60;available_payment_methods&#x60; property containing the list of payment method names available to use with your Purchase (e.g. those codes can be used in &#x60;payment_method_whitelist&#x60; field or with &#x60;?preferred&#x3D;{payment_method}&#x60; option of &#x60;checkout_url&#x60;).
        /// </summary>
        /// <exception cref="Org.Chip.Client.ApiException">Thrown when fails to make API call</exception>
        /// <param name="brandId">Which brand would you like to lookup the available payment methods for. Use the same value (UUID) you&#39;d set the &#x60;Purchase.brand_id&#x60; to.</param>
        /// <param name="currency">Currency you&#39;d use in your Purchase in ISO 4217 format, e.g. &#x60;EUR&#x60;.</param>
        /// <param name="country">Country code in the ISO 3166-1 alpha-2 format (e.g. &#x60;GB&#x60;). Optional. (optional)</param>
        /// <param name="recurring">If provided in the format of &#x60;recurring&#x3D;true&#x60;, will filter out the methods that don&#39;t support recurring charges (see &#x60;POST /purchases/{id}/charge/&#x60;). (optional)</param>
        /// <param name="skipCapture">If provided in the format of &#x60;skip_capture&#x3D;true&#x60;, will filter out the methods that don&#39;t support &#x60;skip_capture&#x60; functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="preauthorization">If provided in the format of &#x60;preauthorization&#x3D;true&#x60;, will filter out the methods that don&#39;t support preauthorization functionality (see the description for &#x60;Purchase.skip_capture field&#x60;). (optional)</param>
        /// <param name="cancellationToken">Cancellation Token to cancel the request.</param>
        /// <returns>Task of ApiResponse (PaymentMethods)</returns>
        public async System.Threading.Tasks.Task<Org.Chip.Client.ApiResponse<PaymentMethods>> PaymentMethodsWithHttpInfoAsync(string brandId, string currency, string country = default(string), bool? recurring = default(bool?), bool? skipCapture = default(bool?), bool? preauthorization = default(bool?), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
        {
            // verify the required parameter 'brandId' is set
            if (brandId == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'brandId' when calling PaymentMethodsApi->PaymentMethods");

            // verify the required parameter 'currency' is set
            if (currency == null)
                throw new Org.Chip.Client.ApiException(400, "Missing required parameter 'currency' when calling PaymentMethodsApi->PaymentMethods");


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

            localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "brand_id", brandId));
            localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "currency", currency));
            if (country != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "country", country));
            }
            if (recurring != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "recurring", recurring));
            }
            if (skipCapture != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "skip_capture", skipCapture));
            }
            if (preauthorization != null)
            {
                localVarRequestOptions.QueryParameters.Add(Org.Chip.Client.ClientUtils.ParameterToMultiMap("", "preauthorization", preauthorization));
            }


            // make the HTTP request

            var localVarResponse = await this.AsynchronousClient.GetAsync<PaymentMethods>("/payment_methods/", localVarRequestOptions, this.Configuration, cancellationToken).ConfigureAwait(false);

            if (this.ExceptionFactory != null)
            {
                Exception _exception = this.ExceptionFactory("PaymentMethods", localVarResponse);
                if (_exception != null) throw _exception;
            }

            return localVarResponse;
        }

    }
}
