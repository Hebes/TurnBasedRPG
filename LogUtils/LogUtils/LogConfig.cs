using System;
using System.Collections.Generic;
using System.Text;

namespace LogUtils
{
    /// <summary>
    /// 输出日志的平台
    /// </summary>
    public enum LoggerType
    {
        /// <summary>
        /// Unity编辑器
        /// </summary>
        Unity,
        /// <summary>
        /// 服务器
        /// </summary>
        Console,
    }

    /// <summary>
    /// 日志的颜色
    /// </summary>
    public enum LogCoLor
    {
        /// <summary>
        /// 空
        /// </summary>
        None,
        /// <summary>
        /// 深红
        /// </summary>
        DarkRed,
        /// <summary>
        /// 绿色
        /// </summary>
        Green,
        /// <summary>
        /// 蓝色
        /// </summary>
        Blue,
        /// <summary>
        /// 青色
        /// </summary>
        Cyan,
        /// <summary>
        /// 紫色
        /// </summary>
        Magenta,
        /// <summary>
        /// 深黄
        /// </summary>
        DarkYellow
    }

    /// <summary>
    /// 日志配置
    /// </summary>
    public class LogConfig
    {
        /// <summary>
        /// 启用日志
        /// </summary>
        public bool enableLog = true;
        /// <summary>
        /// 日志前缀
        /// </summary>
        public string LogPrefix = "#";
        /// <summary>
        /// 启用时间
        /// </summary>
        public bool enableTime = true;
        /// <summary>
        /// 日志分离
        /// </summary>
        public string LogSeparate = ">>";
        /// <summary>
        /// 启用线程ID
        /// </summary>
        public bool enableThreadID = true;
        /// <summary>
        /// 启用跟踪
        /// </summary>
        public bool enableTrace = true;
        /// <summary>
        /// 启用保存
        /// </summary>
        public bool enableSave = true;
        /// <summary>
        /// 保存路径
        /// </summary>
        public string savePath = string.Format($"{AppDomain.CurrentDomain.BaseDirectory}Logs\\");
        /// <summary>
        /// 保存名称
        /// </summary>
        public string saveName = "ConsoLePELog.txt";
        /// <summary>
        /// 日志类型
        /// </summary>
        public LoggerType eLoggerType = LoggerType.Unity;
        /// <summary>
        /// 是否覆盖
        /// </summary>
        public bool enableCover;
    }
    interface ILogger
    {
        /// <summary>
        /// 普通信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="LogCoLor"></param>
        void Log(string msg, LogCoLor LogCoLor = LogCoLor.None);
        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        void Warn(string msg);
        /// <summary>
        /// 异常错误
        /// </summary>
        /// <param name="msg"></param>
        void Error(string msg);
    }
}
