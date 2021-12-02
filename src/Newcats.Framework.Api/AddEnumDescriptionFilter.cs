/***************************************************************************
 *GUID: e393baa8-2a6e-4a9a-92ea-87acf5de6a32
 *CLR Version: 4.0.30319.42000
 *DateCreated：2021-12-02 17:13:32
 *Author: NewcatsHuang
 *Email: newcats@live.com
 *Github: https://github.com/newcatshuang
 *Copyright NewcatsHuang All rights reserved.
*****************************************************************************/
using System.Collections.Concurrent;
using Microsoft.OpenApi.Models;
using Newcats.Utils.Extensions;
using Newcats.Utils.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Newcats.Framework.Api;

/// <summary>
/// 给Swagger文档的枚举添加描述
/// </summary>
public class AddEnumDescriptionFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type.IsEnum)
        {
            var list = Utils.Helpers.EnumHelper.GetAllEnumDescriptions(context.Type);
            if (list != null && list.Count > 0)
            {
                string des = string.Join(",", list.Select(s => s.Name.Equals(s.Description, StringComparison.OrdinalIgnoreCase) ? $"{s.Value}={s.Name}" : $"{s.Value}={s.Name}({s.Description})"));
                schema.Description = schema.Description.IsNullOrWhiteSpace() ? des : $"{schema.Description}:{des}";
            }
        }
        else if (context.Type.IsClass && context.Type != typeof(string))
        {
            UpdateSchemaDescription(schema, context);
        }
    }

    private void UpdateSchemaDescription(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Reference != null)
        {
            var s = context.SchemaRepository.Schemas[schema.Reference.Id];
            if (s != null && s.Enum != null && s.Enum.Count > 0)
            {
                if (!string.IsNullOrWhiteSpace(s.Description))
                {
                    string description = $"[{s.Description}]";
                    if (string.IsNullOrWhiteSpace(schema.Description) || !schema.Description.EndsWith(description))
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
}