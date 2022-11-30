

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
    /// Payment method used to execute the transaction.  - maestro: Maestro payment card - mastercard: Mastercard payment card - sepa_credit_transfer_qr: SEPA Credit Transfer with QR code - unknown: Payment method could not be determinded - visa: Visa payment card
    /// </summary>
    /// <value>Payment method used to execute the transaction.  - maestro: Maestro payment card - mastercard: Mastercard payment card - sepa_credit_transfer_qr: SEPA Credit Transfer with QR code - unknown: Payment method could not be determinded - visa: Visa payment card</value>
    
    [JsonConverter(typeof(StringEnumConverter))]
    
    public enum PaymentMethod
    {
        /// <summary>
        /// Enum Maestro for value: maestro
        /// </summary>
        [EnumMember(Value = "maestro")]
        Maestro = 1,

        /// <summary>
        /// Enum Mastercard for value: mastercard
        /// </summary>
        [EnumMember(Value = "mastercard")]
        Mastercard = 2,

        /// <summary>
        /// Enum Sepacredittransferqr for value: sepa_credit_transfer_qr
        /// </summary>
        [EnumMember(Value = "sepa_credit_transfer_qr")]
        Sepacredittransferqr = 3,

        /// <summary>
        /// Enum Unknown for value: unknown
        /// </summary>
        [EnumMember(Value = "unknown")]
        Unknown = 4,

        /// <summary>
        /// Enum Visa for value: visa
        /// </summary>
        [EnumMember(Value = "visa")]
        Visa = 5

    }

}
