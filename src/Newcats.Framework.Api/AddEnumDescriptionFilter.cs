/***************************************************************************
 *GUID: e393baa8-2a6e-4a9a-92ea-87acf5de6a32
 *CLR Version: 4.0.30319.42000
 *DateCreated：2021-12-02 17:13:32
 *Author: NewcatsHuang
 *Email: newcats@live.com
 *Github: https://github.com/newcatshuang
 *Copyright NewcatsHuang All rights reserved.
*****************************************************************************/
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newcats.Utils.Models;
using Newcats.Utils.Extensions;
using System.Collections.Concurrent;

namespace Newcats.Framework.Api;

/// <summary>
/// 给Swagger文档的枚举添加描述
/// </summary>
public class AddEnumDescriptionFilter : ISchemaFilter
{
    /// <summary>
    /// 缓存，键为类的全名
    /// </summary>
    private static readonly ConcurrentDictionary<string, List<EnumDescription>> _cache = new ConcurrentDictionary<string, List<EnumDescription>>();

    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var list = GetAllEnumDescriptions(context.Type);
            if (list != null && list.Count > 0)
            {
                string des = string.Join("|", list.Select(s => $"{s.Value}={s.Name}({s.Description})"));
                schema.Description = schema.Description.IsNullOrWhiteSpace() ? des : $"{schema.Description}:{des}";
            }
            //else if (context.Type.IsClass && context.Type != typeof(string))
            //{
            //    UpdateSchemaDescription(schema, context);
            //}
        }
    }

    private void UpdateSchemaDescription(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Reference != null)
        {
            var s = context.SchemaRepository.Schemas[schema.Reference.Id];
            if (s != null && s.Enum != null && s.Enum.Count > 0)
            {
                if (!string.IsNullOrEmpty(s.Description))
                {
                    string description = $"【{s.Description}】";
                    if (string.IsNullOrEmpty(schema.Description) || !schema.Description.EndsWith(description))
                    {
                        schema.Description += description;
                    }
                }
            }
        }

        foreach (var key in schema.Properties.Keys)
        {
            var s = schema.Properties[key];
            UpdateSchemaDescription(s, context);
        }
    }

    private List<EnumDescription> GetAllEnumDescriptions(Type type)
    {
        if (!type.IsEnum)
            return null;
        List<EnumDescription> list = new List<EnumDescription>();
        if (_cache.TryGetValue(type.FullName, out list))
        {
            if (list != null && list.Count > 0)
                return list;
        }
        list = new List<EnumDescription>();
        foreach (Enum e in Enum.GetValues(type))
        {
            list.Add(e.GetEnumDescription());
        }

        _cache.TryAdd(type.FullName, list);
        return list;
    }
}