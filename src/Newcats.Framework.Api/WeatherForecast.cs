using System.ComponentModel;
using Newcats.Framework.Model;

namespace Newcats.Framework.Api
{
    /// <summary>
    /// 天气预报
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 摄氏度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 总结
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// 季节
        /// </summary>
        public SeasonEnum Season { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public GenderEnum Gender { get; set; }
    }

    /// <summary>
    /// 季节
    /// </summary>
    public enum SeasonEnum
    {
        /// <summary>
        /// 春季
        /// </summary>
        [Description("春季")]
        Spring = 0,

        /// <summary>
        /// 夏季
        /// </summary>
        [Description("夏季")]
        Summer = 1,

        /// <summary>
        /// 秋季
        /// </summary>
        [Description("秋季")]
        Fall = 2,

        /// <summary>
        /// 冬季
        /// </summary>
        [Description("冬季")]
        Winter = 3
    }
}