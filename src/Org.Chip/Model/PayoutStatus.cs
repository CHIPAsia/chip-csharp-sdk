

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
    /// Payout status. Can have the following values:   &#x60;initialized&#x60;: Payout was created, but not executed. Initial status to new &#x60;Payout&#x60;s.  - --  &#x60;error&#x60;: An error has occurred during the execution. Execution can be attempted again.  - --  &#x60;success&#x60;: Payout was executed successfuly.
    /// </summary>
    /// <value>Payout status. Can have the following values:   &#x60;initialized&#x60;: Payout was created, but not executed. Initial status to new &#x60;Payout&#x60;s.  - --  &#x60;error&#x60;: An error has occurred during the execution. Execution can be attempted again.  - --  &#x60;success&#x60;: Payout was executed successfuly.</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum PayoutStatus
    {
        /// <summary>
        /// Enum Initialized for value: initialized
        /// </summary>
        [EnumMember(Value = "initialized")]
        Initialized = 1,

        /// <summary>
        /// Enum Error for value: error
        /// </summary>
        [EnumMember(Value = "error")]
        Error = 2,

        /// <summary>
        /// Enum Success for value: success
        /// </summary>
        [EnumMember(Value = "success")]
        Success = 3

    }

}
