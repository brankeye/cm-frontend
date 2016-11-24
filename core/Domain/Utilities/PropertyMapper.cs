using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Utilities
{
    public class PropertyMapper<TModel>
        where TModel : RealmObject, IEntity, new()
    {
        public void Map(IEntity destinationModel, IEntity sourceModel)
        {
            var destination = (TModel)destinationModel;
            var source = (TModel)sourceModel;

            var destTypeInfo = destination.GetType().GetTypeInfo();
            var destPropInfo = destTypeInfo.DeclaredProperties.ToList();

            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourcePropInfo = sourceTypeInfo.DeclaredProperties.ToList();

            for (var i = 0; i < destPropInfo.Count; i++)
            {
                var attr = destPropInfo[i].GetCustomAttribute(typeof(Attributes.IgnorePropertyMapping));
                var ignoreMappingAttribute = attr as Attributes.IgnorePropertyMapping;

                if (ignoreMappingAttribute != null) continue;

                typeof(TModel)
                    .GetTypeInfo()
                    .GetDeclaredProperty(destPropInfo[i].Name)
                    .SetValue(destination, sourcePropInfo[i].GetValue(source));
            }
        }
    }
}
