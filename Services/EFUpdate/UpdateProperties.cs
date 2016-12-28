using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DYV.Services.EFUpdate
{
    public static class EFUpdateProperties
    {
        public static TOrig UpdateProperties<TOrig, TDTO>(this TOrig original, TDTO dto, bool includeId = false)
        {
            var origProps = typeof(TOrig).GetProperties();
            var dtoProps = typeof(TDTO).GetProperties();

            Func<PropertyInfo, bool> idExcluder = origProp => true;

            if (!includeId)
                idExcluder = origProp => origProp.Name.Substring(origProp.Name.Length - 2).ToLower() != "id";

            foreach (PropertyInfo dtoProp in dtoProps)
            {
                origProps
                    .Where(origProp => origProp.Name == dtoProp.Name)
                    .Where(idExcluder).SingleOrDefault()
                    ?.SetMethod.Invoke(
                        original, 
                        new Object[] 
                            {
                                dtoProp.GetMethod.Invoke(dto, null)
                            });
            }

            return original;
        }
    }
}
