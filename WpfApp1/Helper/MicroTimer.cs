﻿using System;
using System.Runtime.InteropServices;

namespace F1Tools
{
    public class MicroTimer
    {
        #region 定时器事件类型
        const int TIME_ONESHOT = 0;
        const int TIME_PERIODIC = 1;
        #endregion

        private bool isRunning = false;
        /// <summary>
        /// 定时器的分辨率（resolution）。单位是毫秒
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        private struct TimerCaps
        {
            public int periodMin;
            public int periodMax;
        }
        #region winmm.dll API声明
        /// <summary>
        /// 设置一个媒体定时器(定时器事件类型常用的有2种，0:TIME_ONESHOT,1:TIME_PERIODIC)
        /// </summary>
        /// <param name="delay">以毫秒指定事件的周期。</param>
        /// <param name="resolution">以毫秒指定延时的精度，数值越小定时器事件分辨率越高。缺省值为1ms。</param>
        /// <param name="callback">指向一个回调函数。(委托实例)</param>
        /// <param name="user">存放用户提供的回调数据。</param>
        /// <param name="mode">指定定时器事件类型：0->TIME_ONESHOT：uDelay毫秒后只产生一次事件;1->TIME_PERIODIC ：每隔delay毫秒周期性地产生事件。</param>
        /// <returns>定时器的ID，释放资源的时候需要</returns>
        [DllImport("winmm.dll")]
        private static extern int timeSetEvent(int delay, int resolution, TimerCallback callback, int user, int mode);
        /// <summary>
        /// 结束定时器，释放资源
        /// </summary>
        /// <param name="id">设置定时器返回的ID</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int timeKillEvent(int id);
        /// <summary>
        /// 初始化结构体,TimerCaps{periodMin,periodMax}
        /// </summary>
        /// <param name="caps">TimerCaps</param>
        /// <param name="sizeOfTimerCaps">TimerCaps的长度</param>
        /// <returns></returns>
        [DllImport("winmm.dll")]
        private static extern int timeGetDevCaps(ref TimerCaps caps, int sizeOfTimerCaps);
        #endregion
        public delegate void TimerCallback(int id, int msg, int user, int param1, int param2); // timeSetEvent所对应的回调函数的签名
        public event TimerCallback OnRunningCallback;
        public event TimerCallback OnStartedCallback;
        public event TimerCallback OnStopedCallback;

        private TimerCallback m_TimerCallback;
        private int timerID;
        private TimerCaps caps = new TimerCaps();

        public MicroTimer(int dueTime, int period)
        {
            timeGetDevCaps(ref caps, Marshal.SizeOf(caps));
            caps.periodMin = period;
            caps.periodMax = dueTime;
            isRunning = false;
            m_TimerCallback = new TimerCallback(TimerEventCallback);
        }

        /// <summary>
        /// 触发事件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msg"></param>
        /// <param name="user"></param>
        /// <param name="param1"></param>
        /// <param name="param2"></param>
        private void TimerEventCallback(int id, int msg, int user, int param1, int param2)
        {
            try
            {
                OnRunningCallback?.Invoke(id, msg, user, param1, param2);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 启动定时器，回调OnStartedCallback
        /// </summary>
        public void Start()
        {
            if (!isRunning)
            {
                timerID = timeSetEvent(caps.periodMin, caps.periodMin, m_TimerCallback, 0, TIME_PERIODIC); // 间隔性地运行
                GC.KeepAlive(m_TimerCallback);
                if (timerID == 0)
                {
                    throw new Exception("无法启动计时器");
                }
                isRunning = true;
                //if (isRunning)
                //OnStartedCallback?.Invoke(this, EventArgs.Empty);
            }
        }
        /// <summary>
        ///停止定时器
        /// </summary>
        public void Stop()
        {
            if (isRunning)
            {
                timeKillEvent(timerID);
                isRunning = false;
                //OnStopedCallback?.Invoke(this, EventArgs.Empty);
                Dispose();
            }
        }
        /// <summary>
        /// 获取定时器允许状态
        /// </summary>
        /// <returns></returns>
        public bool IsRunning()
        {
            return isRunning;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            if (!timerID.Equals(0))
                timeKillEvent(timerID);
            GC.SuppressFinalize(this);
        }
    }
}
