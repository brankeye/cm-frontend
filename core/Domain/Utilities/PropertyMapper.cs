using System.Dynamic;
using System.Linq;
using System.Reflection;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Utilities
{
    public class PropertyMapper
    {
        public void Map<TModel>(TModel source, TModel destination)
            where TModel : new()
        {
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

        public void Map<TModelA, TModelB>(TModelA source, TModelB destination)
            where TModelA : new()
            where TModelB : new()
        {
            var destTypeInfo = destination.GetType().GetTypeInfo();
            var destPropInfo = destTypeInfo.DeclaredProperties.ToList();

            var sourceTypeInfo = source.GetType().GetTypeInfo();
            var sourcePropInfo = sourceTypeInfo.DeclaredProperties.ToList();

            for (var i = 0; i < destPropInfo.Count; i++)
            {
                var attrA = destPropInfo[i].GetCustomAttribute(typeof(Attributes.IgnorePropertyMapping));
                var ignoreMappingAttributeForA = attrA as Attributes.IgnorePropertyMapping;

                if (ignoreMappingAttributeForA != null) continue;

                for (var j = 0; j < sourcePropInfo.Count; j++)
                {
                    var attrB = sourcePropInfo[j].GetCustomAttribute(typeof(Attributes.IgnorePropertyMapping));
                    var ignoreMappingAttributeForB = attrB as Attributes.IgnorePropertyMapping;

                    if (ignoreMappingAttributeForB != null) continue;

                    if (destPropInfo[i].Name == sourcePropInfo[j].Name)
                    {
                        typeof(TModelB)
                        .GetTypeInfo()
                        .GetDeclaredProperty(destPropInfo[i].Name)
                        .SetValue(destination, sourcePropInfo[j].GetValue(source));
                    }
                }
            }
        }
    }
}
