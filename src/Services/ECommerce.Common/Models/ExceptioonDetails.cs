﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Common.Models
{
    public class ExceptionDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string? Details { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }


    }
}
