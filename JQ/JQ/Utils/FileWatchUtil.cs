using JQ.Extension;
using System;
using System.IO;
using System.Threading;

namespace JQ.Utils
{
    /// <summary>
    /// Copyright (C) 2017 yjq 版权所有。
    /// 类名：FileWatchUtil.cs
    /// 类属性：局部类（非静态）
    /// 类功能描述：本地文件监视帮助类
    /// 创建标识：yjq 2017/5/15 15:13:48
    /// </summary>
    public sealed class FileWatchUtil : JQDisposable
    {
        private Timer _timer;
        private FileSystemWatcher _fileWatcher;
        private readonly int _timeOutMillis = 500;//延迟执行时间
        private FileInfo _fileInfo;
        private Action<FileStream> _fileChangedHandle = null;//文件变动后执行方法

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="action">文件改变后的执行的方法</param>
        /// <param name="timeOutMillis">延迟时间</param>
        public FileWatchUtil(string filePath, Action<FileStream> action, int? timeOutMillis = null)
        {
            if (FileUtil.IsExistsFile(filePath))
            {
                if (timeOutMillis.HasValue)
                {
                    _timeOutMillis = timeOutMillis.Value;
                }
                if (action == null)
                {
                    throw new ArgumentNullException("file changed action");
                }
                _fileChangedHandle = action;
                _fileInfo = new FileInfo(filePath);
            }
            else
            {
                LogUtil.WarnFormat("【{0}】文件不存在", filePath);
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileInfo">文件信息</param>
        /// <param name="action">文件改变后的执行的方法</param>
        /// <param name="timeOutMillis">延迟时间</param>
        public FileWatchUtil(FileInfo fileInfo, Action<FileStream> action, int? timeOutMillis = null)
        {
            if (timeOutMillis.HasValue)
            {
                _timeOutMillis = timeOutMillis.Value;
            }
            if (action == null)
            {
                throw new ArgumentNullException("file changed action");
            }
            if (_fileInfo == null)
            {
                throw new ArgumentNullException("watching fileInfo");
            }
            _fileChangedHandle = action;
            _fileInfo = fileInfo;
        }

        public void StartWatching()
        {
            _fileWatcher = new FileSystemWatcher();
            _fileWatcher.Path = _fileInfo.DirectoryName;
            _fileWatcher.Filter = _fileInfo.Name;
            _fileWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
            _fileWatcher.Changed += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Created += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Deleted += new FileSystemEventHandler(ConfigWacherHandler_OnChanged);
            _fileWatcher.Renamed += new RenamedEventHandler(ConfigWacherHandler_OnRenamed);
            _fileWatcher.EnableRaisingEvents = true;

            _timer = new Timer(new TimerCallback(OnWatchedFileChange), null, Timeout.Infinite, Timeout.Infinite);
            _timer.Change(_timeOutMillis, Timeout.Infinite);
            LogUtil.InfoFormat("【{0}】文件监听器开启", _fileInfo.FullName);
        }

        private void ConfigWacherHandler_OnChanged(object source, FileSystemEventArgs e)
        {
            _timer.Change(_timeOutMillis, Timeout.Infinite);
        }

        private void ConfigWacherHandler_OnRenamed(object source, RenamedEventArgs e)
        {
            _timer.Change(_timeOutMillis, Timeout.Infinite);
        }

        private void OnWatchedFileChange(object state)
        {
            if (_fileInfo == null)
            {
                LogUtil.Error("【InternalConfigure】配置文件不存在");
            }
            else if (FileUtil.IsExistsFile(_fileInfo.FullName))
            {
                FileStream fs = null;
                for (int retry = 5; --retry >= 0;)
                {
                    try
                    {
                        fs = _fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
                        break;
                    }
                    catch (IOException ex)
                    {
                        if (retry == 0)
                        {
                            LogUtil.Error(string.Format("打开文件失败 [{0}],{1}", _fileInfo.FullName, ex.ToErrMsg("OnWatchedFileChange")));
                            fs = null;
                        }
                        Thread.Sleep(250);
                    }
                }
                if (fs != null)
                {
                    try
                    {
                        _fileChangedHandle?.Invoke(fs);
                        LogUtil.InfoFormat("【{0}】文件信息变更后加载完毕", _fileInfo.FullName);
                    }
                    finally
                    {
                        fs.Close();
                    }
                }
            }
            else
            {
                LogUtil.WarnFormat("【{0}】文件不存在", _fileInfo.FullName);
            }
        }

        protected override void DisposeCode()
        {
            _fileWatcher?.Dispose();
            _timer?.Dispose();
            LogUtil.InfoFormat("释放文件【{0}】监听器", _fileInfo?.FullName);
        }
    }
}