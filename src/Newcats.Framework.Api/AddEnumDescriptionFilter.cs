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
using Newcats.Utils.Extensions;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Newcats.Framework.Api;

/// <summary>
/// 给Swagger文档的枚举添加描述
/// </summary>
public class AddEnumDescriptionFilter : ISchemaFilter
{
    /// <summary>
    /// 给Swagger文档的枚举添加描述
    /// </summary>
    /// <param name="schema">OpenApiSchema</param>
    /// <param name="context">SchemaFilterContext</param>
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
    }

    /// <summary>
    /// 获取指定路径下的xml文件
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static List<string> GetAllXmlFileFullNames(string path)
    {
        List<string> list = new();
        DirectoryInfo directory = new(path);
        var files = directory.GetFiles("*.xml");
        if (files != null && files.Length > 0)
        {
            foreach (var file in files)
            {
                list.Add(file.FullName);
            }
        }
        return list;
    }
}