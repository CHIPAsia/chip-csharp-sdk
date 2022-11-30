

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
    /// Flow or pathway used to initiate or execute a transaction.  - api: transaction initiated via the merchant API - direct_post: transaction executed via direct POST request - payform: transaction executed via the gateway payform - server_to_server: transaction executed via server to server API - web_office: transaction initiated via the merchant portal
    /// </summary>
    /// <value>Flow or pathway used to initiate or execute a transaction.  - api: transaction initiated via the merchant API - direct_post: transaction executed via direct POST request - payform: transaction executed via the gateway payform - server_to_server: transaction executed via server to server API - web_office: transaction initiated via the merchant portal</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum TransactionFlow
    {
        /// <summary>
        /// Enum Api for value: api
        /// </summary>
        [EnumMember(Value = "api")]
        Api = 1,

        /// <summary>
        /// Enum Directpost for value: direct_post
        /// </summary>
        [EnumMember(Value = "direct_post")]
        Directpost = 2,

        /// <summary>
        /// Enum Payform for value: payform
        /// </summary>
        [EnumMember(Value = "payform")]
        Payform = 3,

        /// <summary>
        /// Enum Servertoserver for value: server_to_server
        /// </summary>
        [EnumMember(Value = "server_to_server")]
        Servertoserver = 4,

        /// <summary>
        /// Enum Weboffice for value: web_office
        /// </summary>
        [EnumMember(Value = "web_office")]
        Weboffice = 5

    }

}
