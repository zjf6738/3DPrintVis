using System;
using System.Threading;
using MVSDK;

namespace Basic
{
    public class tpvCameraObj
    {
        public Int32 m_hCamera = 0;             // 句柄
        public IntPtr       m_ImageBuffer;             // 预览通道RGB图像缓存
        public tSdkCameraCapbility tCameraCapability;  // 相机特性描述
        public int          m_iDisplayedFrames = 0;    //已经显示的总帧数
        public IntPtr       m_iCaptureCallbackCtx;     //图像回调函数的上下文参数
        public Thread       m_tCaptureThread;          //图像抓取线程
        public bool         m_bExitCaptureThread = false;//采用线程采集时，让线程退出的标志
        public tSdkFrameHead m_tFrameHead;
        public bool          m_bEraseBk = false;

        public tpvCameraObj()
        {
        }
    }
}