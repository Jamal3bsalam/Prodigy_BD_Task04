using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Task02.Core.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Roles
    {
        User,
        Employee
    }
}
