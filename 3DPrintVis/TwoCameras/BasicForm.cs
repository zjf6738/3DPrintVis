//#define USE_CALLBACK  //图像抓取方式宏定义，打开则采用回调函数方式，关闭，则使用多线程主动抓取方式。

//BIG5 TRANS ALLOWED

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using MVSDK;//使用SDK接口
using Snapshot; 
using CameraHandle = System.Int32;
using MvApi = MVSDK.MvApi;
using System.IO;



namespace Basic
{
    

    public partial class BasicForm : Form
    {
        

        #region variable
        const int CAMERA_NUM = 4;

        // 相机1，2，3，4
        private tpvCameraObj[] cameraObjs;

        //相机1
        //private tpvCameraObj cameraObj1;
        //相机2
        //private tpvCameraObj cameraObj2;

        #endregion

        public BasicForm()
        {
            InitializeComponent();

            //cameraObj1 = new tpvCameraObj();
            //cameraObj2 = new tpvCameraObj();
            cameraObjs = new tpvCameraObj[CAMERA_NUM];
            for (int i = 0; i < CAMERA_NUM; i++) { cameraObjs[i] = new tpvCameraObj(); }

            InitCamera();
        }

        public tpvCameraObj CameraObj1
        {
            get { return cameraObjs[0]; }
        }

        public tpvCameraObj CameraObj2
        {
            get { return cameraObjs[1]; }
        }

        public tpvCameraObj CameraObj3
        {
            get { return cameraObjs[2]; }
        }

        public tpvCameraObj CameraObj4
        {
            get { return cameraObjs[3]; }
        }

        public tpvCameraObj[] CameraObjs
        {
            get { return cameraObjs; }
            set { cameraObjs = value; }
        }

        public void CaptureThreadProc1()
        {
            CaptureThreadProc_General(CameraObj1);
        }

        public void CaptureThreadProc2()
        {
            CaptureThreadProc_General(CameraObj2);
        }

        public void CaptureThreadProc3()
        {
            CaptureThreadProc_General(CameraObj3);
        }

        public void CaptureThreadProc4()
        {
            CaptureThreadProc_General(CameraObj4);
        }

        private void CaptureThreadProc_General(tpvCameraObj cameraObj)
        {
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            IntPtr uRawBuffer; //rawbuffer由SDK内部申请。应用层不要调用delete之类的释放函数

            while (cameraObj.m_bExitCaptureThread == false)
            {
                //500毫秒超时,图像没捕获到前，线程会被挂起,释放CPU，所以该线程中无需调用sleep
                eStatus = MvApi.CameraGetImageBuffer(cameraObj.m_hCamera, out FrameHead, out uRawBuffer, 500);

                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS) //如果是触发模式，则有可能超时
                {
                    //图像处理，将原始输出转换为RGB格式的位图数据，同时叠加白平衡、饱和度、LUT等ISP处理。
                    MvApi.CameraImageProcess(cameraObj.m_hCamera, uRawBuffer, cameraObj.m_ImageBuffer, ref FrameHead);
                    //叠加十字线、自动曝光窗口、白平衡窗口信息(仅叠加设置为可见状态的)。    
                    MvApi.CameraImageOverlay(cameraObj.m_hCamera, cameraObj.m_ImageBuffer, ref FrameHead);
                    //调用SDK封装好的接口，显示预览图像
                    MvApi.CameraDisplayRGB24(cameraObj.m_hCamera, cameraObj.m_ImageBuffer, ref FrameHead);
                    //成功调用CameraGetImageBuffer后必须释放，下次才能继续调用CameraGetImageBuffer捕获图像。
                    MvApi.CameraReleaseImageBuffer(cameraObj.m_hCamera, uRawBuffer);

                    if (FrameHead.iWidth != cameraObj.m_tFrameHead.iWidth ||
                        FrameHead.iHeight != cameraObj.m_tFrameHead.iHeight)
                    {
                        cameraObj.m_bEraseBk = true;
                        cameraObj.m_tFrameHead = FrameHead;
                    }
                    cameraObj.m_iDisplayedFrames++;
                }
            }
        }

        private bool InitCamera()
        {
            // sdk状态
            CameraSdkStatus status;
            // 设备信息
            tSdkCameraDevInfo[] tCameraDevInfoList;
            // 获取所有的设备信息
            status = MvApi.CameraEnumerateDevice(out tCameraDevInfoList);
            // 设备的数量
            int iCameraCounts = (tCameraDevInfoList != null ? tCameraDevInfoList.Length : 0);
            
            // 若相机个数大于2
            if (iCameraCounts >= 4)//此时iCameraCounts返回了实际连接的相机个数。如果大于2，则初始化前2个相机
            {
                // 获取第一个相机，并将句柄传递到m_hCamera里头
                if (!InitSingleCamera(tCameraDevInfoList, 0, CaptureThreadProc1, PreviewBox, BtnPlay, StateLabel, "Camera 1 init error", ref cameraObjs[0])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 1, CaptureThreadProc2, PreviewBox2, BtnPlay2, StateLabel2, "Camera 2 init error", ref cameraObjs[1])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 2, CaptureThreadProc3, PreviewBox3, BtnPlay3, StateLabel3, "Camera 3 init error", ref cameraObjs[2])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 3, CaptureThreadProc4, PreviewBox4, BtnPlay4, StateLabel4, "Camera 4 init error", ref cameraObjs[3])) return false;

                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据设备列表，初始化单个相机
        /// </summary>
        /// <param name="tCameraDevInfoList">相机设备列表</param>
        /// <param name="cameraDevID">第i个相机</param>
        /// <param name="PreviewBox">相机对应的PictureBox</param>
        /// <param name="CaptureThreadProc1">相机对应的线程处理函数</param>
        /// <param name="cameraObj">相机对象</param>
        /// <returns></returns>
        protected bool InitSingleCamera(tSdkCameraDevInfo[] tCameraDevInfoList, int cameraDevID,                                        
                                        ThreadStart CaptureThreadProc1,
                                        PictureBox PreviewBox, Button BtnPlay, Label StateLabel,
                                        string errMsg,
                                        ref  tpvCameraObj cameraObj)
        {


            if (MvApi.CameraInit(ref tCameraDevInfoList[cameraDevID], -1, -1, ref cameraObj.m_hCamera) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                //获得相机特性描述
                MvApi.CameraGetCapability(cameraObj.m_hCamera, out cameraObj.tCameraCapability);

                cameraObj.m_ImageBuffer = Marshal.AllocHGlobal(cameraObj.tCameraCapability.sResolutionRange.iWidthMax*
                                         cameraObj.tCameraCapability.sResolutionRange.iHeightMax*3 + 1024);

                //初始化显示模块，使用SDK内部封装好的显示接口
                MvApi.CameraDisplayInit(cameraObj.m_hCamera, PreviewBox.Handle);
                MvApi.CameraSetDisplaySize(cameraObj.m_hCamera, PreviewBox.Width, PreviewBox.Height);

                //让SDK来根据相机的型号动态创建该相机的配置窗口。
                MvApi.CameraCreateSettingPage(cameraObj.m_hCamera, this.Handle, tCameraDevInfoList[cameraDevID].acFriendlyName,
                    /*SettingPageMsgCalBack*/null, /*m_iSettingPageMsgCallbackCtx*/(IntPtr) null, 0);

                MvApi.CameraPlay(cameraObj.m_hCamera);
                BtnPlay.Text = "Pause";

                //两种方式来获得预览图像，设置回调函数或者使用定时器或者独立线程的方式，
                //主动调用CameraGetImageBuffer接口来抓图。
                //本例中仅演示了两种的方式,注意，两种方式也可以同时使用，但是在回调函数中，
                //不要使用CameraGetImageBuffer，否则会造成死锁现象。

                cameraObj.m_bExitCaptureThread = false;
                cameraObj.m_tCaptureThread = new Thread(new ThreadStart(CaptureThreadProc1));
                cameraObj.m_tCaptureThread.Start();
            }
            else
            {
                cameraObj.m_hCamera = 0;
                //StateLabel.Text = "Camera 1 init error";
                StateLabel.Text =  errMsg;
                return false;
            }
            return true;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (CameraObj1.m_hCamera != 0)
            {
                if (BtnPlay.Text == "Play")
                {
                    MvApi.CameraPlay(CameraObj1.m_hCamera);
                    BtnPlay.Text = "Pause";
                }
                else
                {
                    MvApi.CameraPause(CameraObj1.m_hCamera);
                    BtnPlay.Text = "Play";
                }
            }
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            if (CameraObj1.m_hCamera > 0)
            {
                MvApi.CameraShowSettingPage(CameraObj1.m_hCamera, 1);//1 show ; 0 hide
            }
        }

        private void BtnPlay2_Click(object sender, EventArgs e)
        {
            if (CameraObj2.m_hCamera > 0)
            {
                if (BtnPlay2.Text == "Play")
                {
                    MvApi.CameraPlay(CameraObj2.m_hCamera);
                    BtnPlay2.Text = "Pause";
                }
                else
                {
                    MvApi.CameraPause(CameraObj2.m_hCamera);
                    BtnPlay2.Text = "Play";
                }
            }
        }
        private void BtnSettings2_Click(object sender, EventArgs e)
        {
            if (CameraObj2.m_hCamera > 0)
            {
                MvApi.CameraShowSettingPage(CameraObj2.m_hCamera, 1);//1 show ; 0 hide
            }
        }


        //1秒更新一次视频信息
        private void timer1_Tick(object sender, EventArgs e)
        {
            tSdkFrameStatistic tFrameStatistic;
            if (CameraObj1.m_hCamera > 0)
            {
                //获得SDK中图像帧统计信息，捕获帧、错误帧等。
                MvApi.CameraGetFrameStatistic(CameraObj1.m_hCamera, out tFrameStatistic);
                //显示帧率有应用程序自己记录。
                string sFrameInfomation = String.Format("| Resolution:{0}*{1} | Display frames{2} | Capture frames{3} |", CameraObj1.m_tFrameHead.iWidth, CameraObj1.m_tFrameHead.iHeight, CameraObj1.m_iDisplayedFrames, tFrameStatistic.iCapture);
                StateLabel.Text = sFrameInfomation;

                sFrameInfomation = String.Format("| Resolution:{0}*{1} |", CameraObj2.m_tFrameHead.iWidth, CameraObj2.m_tFrameHead.iHeight);
                StateLabel2.Text = sFrameInfomation;
                
            }
            else
            {
                StateLabel.Text = "";
            }
        }

        //用于分辨率切换时，刷新背景绘图
        private void timer2_Tick(object sender, EventArgs e)
        {
            //切换分辨率后，擦除一次背景
            if (CameraObj1.m_bEraseBk == true)
            {
                CameraObj1.m_bEraseBk = false;
                PreviewBox.Refresh();
            }
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {

        }

        private void BasicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            for (int i = 0; i < CAMERA_NUM; i++)
            {
                tpvCameraObj cameraObj = cameraObjs[i];

                //相机1反初始化
                if (cameraObj.m_tCaptureThread != null)
                {
                    cameraObj.m_bExitCaptureThread = true;
                    cameraObj.m_tCaptureThread.Join();
                    cameraObj.m_tCaptureThread = null;
                }

                if (cameraObj.m_hCamera != 0)
                {
                    MvApi.CameraUnInit(cameraObj.m_hCamera);
                    cameraObj.m_hCamera = 0;
                }

                if (cameraObj.m_ImageBuffer != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(cameraObj.m_ImageBuffer);
                    cameraObj.m_ImageBuffer = IntPtr.Zero;
                }
            }

        }



    }
}