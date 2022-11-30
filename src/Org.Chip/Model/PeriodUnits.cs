

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
    /// Defines PeriodUnits
    /// </summary>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum PeriodUnits
    {
        /// <summary>
        /// Enum Days for value: days
        /// </summary>
        [EnumMember(Value = "days")]
        Days = 1,

        /// <summary>
        /// Enum Weeks for value: weeks
        /// </summary>
        [EnumMember(Value = "weeks")]
        Weeks = 2,

        /// <summary>
        /// Enum Months for value: months
        /// </summary>
        [EnumMember(Value = "months")]
        Months = 3

    }

}
