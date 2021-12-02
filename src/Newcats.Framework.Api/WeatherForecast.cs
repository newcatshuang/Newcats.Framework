using System.ComponentModel;
using Newcats.Framework.Model;

namespace Newcats.Framework.Api
{
    /// <summary>
    /// ����Ԥ��
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ���϶�
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// ���϶�
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// �ܽ�
        /// </summary>
        public string? Summary { get; set; }

        /// <summary>
        /// ����
        /// </summary>
        public SeasonEnum Season { get; set; }

        /// <summary>
        /// �Ա�
        /// </summary>
        public GenderEnum Gender { get; set; }
    }

    /// <summary>
    /// ����
    /// </summary>
    public enum SeasonEnum
    {
        /// <summary>
        /// ����
        /// </summary>
        [Description("����")]
        Spring = 0,

        /// <summary>
        /// �ļ�
        /// </summary>
        [Description("�ļ�")]
        Summer = 1,

        /// <summary>
        /// �＾
        /// </summary>
        [Description("�＾")]
        Fall = 2,

        /// <summary>
        /// ����
        /// </summary>
        [Description("����")]
        Winter = 3
    }
}