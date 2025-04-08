using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Basket.Infrastructure.Resolvers
{
    public class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var prop = base.CreateProperty(member, memberSerialization);

            if (member is PropertyInfo propertyInfo)
            {
                // Private setter ve constructor'ları destekle
                prop.Writable = propertyInfo.SetMethod != null;
            }

            return prop;
        }
    }
}
