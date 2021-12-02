using System.ComponentModel;

namespace Newcats.Framework.Model
{
    /// <summary>
    /// 性别
    /// </summary>
    public enum GenderEnum
    {
        /// <summary>
        /// 未知
        /// </summary>
        [Description("未知")]
        None = 0,

        /// <summary>
        /// 男性
        /// </summary>
        [Description("男性")]
        Man = 1,

        /// <summary>
        /// 女性
        /// </summary>
        [Description("女性")]
        Female = 2
    }
}