//#define USE_CALLBACK  //ͼ��ץȡ��ʽ�궨�壬������ûص�������ʽ���رգ���ʹ�ö��߳�����ץȡ��ʽ��

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
using MVSDK;//ʹ��SDK�ӿ�
using Snapshot; 
using CameraHandle = System.Int32;
using MvApi = MVSDK.MvApi;
using System.IO;



namespace Basic
{
    

    public partial class BasicForm : Form
    {
        

        #region variable

        //���1
        protected CameraHandle m_hCamera1 = 0;             // ���
        protected IntPtr       m_ImageBuffer1;             // Ԥ��ͨ��RGBͼ�񻺴�
        protected tSdkCameraCapbility tCameraCapability1;  // �����������
        protected int          m_iDisplayedFrames = 0;    //�Ѿ���ʾ����֡��
        protected IntPtr       m_iCaptureCallbackCtx1;     //ͼ��ص������������Ĳ���
        protected Thread       m_tCaptureThread1;          //ͼ��ץȡ�߳�
        protected bool         m_bExitCaptureThread1 = false;//�����̲߳ɼ�ʱ�����߳��˳��ı�־
        protected tSdkFrameHead m_tFrameHead1;
        protected bool          m_bEraseBk = false;

        //���2
        protected CameraHandle m_hCamera2 = 0;             // ���
        protected IntPtr m_ImageBuffer2;             // Ԥ��ͨ��RGBͼ�񻺴�
        protected tSdkCameraCapbility tCameraCapability2;  // �����������
        protected IntPtr m_iCaptureCallbackCtx2;     //ͼ��ص������������Ĳ���
        protected Thread m_tCaptureThread2;          //ͼ��ץȡ�߳�
        protected bool m_bExitCaptureThread2 = false;//�����̲߳ɼ�ʱ�����߳��˳��ı�־
        protected tSdkFrameHead m_tFrameHead2;
      
        #endregion

        public BasicForm()
        {
            InitializeComponent();

            InitCamera();
        }

        public void CaptureThreadProc1()
        {
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            IntPtr uRawBuffer;//rawbuffer��SDK�ڲ����롣Ӧ�ò㲻Ҫ����delete֮����ͷź���
  
            while(m_bExitCaptureThread1 == false)
            {
                //500���볬ʱ,ͼ��û����ǰ���̻߳ᱻ����,�ͷ�CPU�����Ը��߳����������sleep
                eStatus = MvApi.CameraGetImageBuffer(m_hCamera1, out FrameHead, out uRawBuffer, 500);
                
                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS)//����Ǵ���ģʽ�����п��ܳ�ʱ
                {
                    //ͼ������ԭʼ���ת��ΪRGB��ʽ��λͼ���ݣ�ͬʱ���Ӱ�ƽ�⡢���Ͷȡ�LUT��ISP����
                    MvApi.CameraImageProcess(m_hCamera1, uRawBuffer, m_ImageBuffer1, ref FrameHead);
                    //����ʮ���ߡ��Զ��عⴰ�ڡ���ƽ�ⴰ����Ϣ(����������Ϊ�ɼ�״̬��)��    
                    MvApi.CameraImageOverlay(m_hCamera1, m_ImageBuffer1, ref FrameHead);
                    //����SDK��װ�õĽӿڣ���ʾԤ��ͼ��
                    MvApi.CameraDisplayRGB24(m_hCamera1, m_ImageBuffer1, ref FrameHead);
                    //�ɹ�����CameraGetImageBuffer������ͷţ��´β��ܼ�������CameraGetImageBuffer����ͼ��
                    MvApi.CameraReleaseImageBuffer(m_hCamera1,uRawBuffer);

                    if (FrameHead.iWidth != m_tFrameHead1.iWidth || FrameHead.iHeight != m_tFrameHead1.iHeight)
                    {
                        m_bEraseBk = true;
                        m_tFrameHead1 = FrameHead;  
                    }
                    m_iDisplayedFrames++;
                }
           
            }
           
        }

        public void CaptureThreadProc2()
        {
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            IntPtr uRawBuffer;//rawbuffer��SDK�ڲ����롣Ӧ�ò㲻Ҫ����delete֮����ͷź���

            while (m_bExitCaptureThread2 == false)
            {
                //500���볬ʱ,ͼ��û����ǰ���̻߳ᱻ����,�ͷ�CPU�����Ը��߳����������sleep
                eStatus = MvApi.CameraGetImageBuffer(m_hCamera2, out FrameHead, out uRawBuffer, 500);

                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS)//����Ǵ���ģʽ�����п��ܳ�ʱ
                {
                    //ͼ������ԭʼ���ת��ΪRGB��ʽ��λͼ���ݣ�ͬʱ���Ӱ�ƽ�⡢���Ͷȡ�LUT��ISP����
                    MvApi.CameraImageProcess(m_hCamera2, uRawBuffer, m_ImageBuffer2, ref FrameHead);
                    //����ʮ���ߡ��Զ��عⴰ�ڡ���ƽ�ⴰ����Ϣ(����������Ϊ�ɼ�״̬��)��    
                    MvApi.CameraImageOverlay(m_hCamera2, m_ImageBuffer2, ref FrameHead);
                    //����SDK��װ�õĽӿڣ���ʾԤ��ͼ��
                    MvApi.CameraDisplayRGB24(m_hCamera2, m_ImageBuffer2, ref FrameHead);
                    //�ɹ�����CameraGetImageBuffer������ͷţ��´β��ܼ�������CameraGetImageBuffer����ͼ��
                    MvApi.CameraReleaseImageBuffer(m_hCamera2, uRawBuffer);

                    if (FrameHead.iWidth != m_tFrameHead2.iWidth || FrameHead.iHeight != m_tFrameHead2.iHeight)
                    {
                        m_bEraseBk = true;
                        m_tFrameHead2 = FrameHead;
                    }
      
                }

            }

        }

        private bool InitCamera()
        {
            CameraSdkStatus status;
            tSdkCameraDevInfo[] tCameraDevInfoList;

            status = MvApi.CameraEnumerateDevice(out tCameraDevInfoList);
            int iCameraCounts = (tCameraDevInfoList != null ? tCameraDevInfoList.Length : 0);
            if (iCameraCounts >= 2)//��ʱiCameraCounts������ʵ�����ӵ�����������������2�����ʼ��ǰ2�����
            {
                if (MvApi.CameraInit(ref tCameraDevInfoList[0], -1, -1, ref m_hCamera1) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                {
                    //��������������
                    MvApi.CameraGetCapability(m_hCamera1, out tCameraCapability1);

                    m_ImageBuffer1 = Marshal.AllocHGlobal(tCameraCapability1.sResolutionRange.iWidthMax * tCameraCapability1.sResolutionRange.iHeightMax * 3 + 1024);

                    //��ʼ����ʾģ�飬ʹ��SDK�ڲ���װ�õ���ʾ�ӿ�
                    MvApi.CameraDisplayInit(m_hCamera1, PreviewBox.Handle);
                    MvApi.CameraSetDisplaySize(m_hCamera1, PreviewBox.Width, PreviewBox.Height);

                    //��SDK������������ͺŶ�̬��������������ô��ڡ�
                    MvApi.CameraCreateSettingPage(m_hCamera1, this.Handle, tCameraDevInfoList[0].acFriendlyName,/*SettingPageMsgCalBack*/null,/*m_iSettingPageMsgCallbackCtx*/(IntPtr)null, 0);

                    MvApi.CameraPlay(m_hCamera1);
                    BtnPlay.Text = "Pause";

                    //���ַ�ʽ�����Ԥ��ͼ�����ûص���������ʹ�ö�ʱ�����߶����̵߳ķ�ʽ��
                    //��������CameraGetImageBuffer�ӿ���ץͼ��
                    //�����н���ʾ�����ֵķ�ʽ,ע�⣬���ַ�ʽҲ����ͬʱʹ�ã������ڻص������У�
                    //��Ҫʹ��CameraGetImageBuffer������������������

                    m_bExitCaptureThread1 = false;
                    m_tCaptureThread1 = new Thread(new ThreadStart(CaptureThreadProc1));
                    m_tCaptureThread1.Start();

                }
                else
                {
                    m_hCamera1 = 0;
                    StateLabel.Text = "Camera 1 init error";
                    return false;
                }

                if (MvApi.CameraInit(ref tCameraDevInfoList[1], -1, -1, ref m_hCamera2) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                {
                    //��������������
                    MvApi.CameraGetCapability(m_hCamera2, out tCameraCapability2);

                    m_ImageBuffer2 = Marshal.AllocHGlobal(tCameraCapability2.sResolutionRange.iWidthMax * tCameraCapability2.sResolutionRange.iHeightMax * 3 + 1024);

                    //��ʼ����ʾģ�飬ʹ��SDK�ڲ���װ�õ���ʾ�ӿ�
                    MvApi.CameraDisplayInit(m_hCamera2, PreviewBox2.Handle);
                    MvApi.CameraSetDisplaySize(m_hCamera2, PreviewBox2.Width, PreviewBox2.Height);

                    //��SDK������������ͺŶ�̬��������������ô��ڡ�
                    MvApi.CameraCreateSettingPage(m_hCamera2, this.Handle, tCameraDevInfoList[1].acFriendlyName,/*SettingPageMsgCalBack*/null,/*m_iSettingPageMsgCallbackCtx*/(IntPtr)null, 0);

                    MvApi.CameraPlay(m_hCamera2);
                    BtnPlay2.Text = "Pause";

                    m_bExitCaptureThread2 = false;
                    m_tCaptureThread2 = new Thread(new ThreadStart(CaptureThreadProc2));
                    m_tCaptureThread2.Start();
                }
                else
                {
                    m_hCamera2 = 0;
                    StateLabel.Text = "Camera 2 init error";
                    return false;
                }

                return true;
            }
            return false;
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (m_hCamera1 != 0)
            {
                if (BtnPlay.Text == "Play")
                {
                    MvApi.CameraPlay(m_hCamera1);
                    BtnPlay.Text = "Pause";
                }
                else
                {
                    MvApi.CameraPause(m_hCamera1);
                    BtnPlay.Text = "Play";
                }
            }
        }

        private void BasicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //���1����ʼ��
            if (m_tCaptureThread1 != null)
            {
                m_bExitCaptureThread1 = true;
                m_tCaptureThread1.Join();
                m_tCaptureThread1 = null;
            }

            if (m_hCamera1 != 0)
            {
                MvApi.CameraUnInit(m_hCamera1);
                m_hCamera1 = 0;
            }

            if (m_ImageBuffer1 != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_ImageBuffer1);
                m_ImageBuffer1 = IntPtr.Zero;
            }

            //���2����ʼ��
            if (m_tCaptureThread2 != null)
            {
                m_bExitCaptureThread2 = true;
                m_tCaptureThread2.Join();
                m_tCaptureThread2 = null;
            }

            if (m_hCamera2 != 0)
            {
                MvApi.CameraUnInit(m_hCamera2);
                m_hCamera2 = 0;
            }

            if (m_ImageBuffer2 != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(m_ImageBuffer2);
                m_ImageBuffer2 = IntPtr.Zero;
            } 
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            if (m_hCamera1 > 0)
            {
                MvApi.CameraShowSettingPage(m_hCamera1, 1);//1 show ; 0 hide
            }
        }

        //1�����һ����Ƶ��Ϣ
        private void timer1_Tick(object sender, EventArgs e)
        {
            tSdkFrameStatistic tFrameStatistic;
            if (m_hCamera1 > 0)
            {
                //���SDK��ͼ��֡ͳ����Ϣ������֡������֡�ȡ�
                MvApi.CameraGetFrameStatistic(m_hCamera1, out tFrameStatistic);
                //��ʾ֡����Ӧ�ó����Լ���¼��
                string sFrameInfomation = String.Format("| Resolution:{0}*{1} | Display frames{2} | Capture frames{3} |", m_tFrameHead1.iWidth, m_tFrameHead1.iHeight, m_iDisplayedFrames, tFrameStatistic.iCapture);
                StateLabel.Text = sFrameInfomation;

                sFrameInfomation = String.Format("| Resolution:{0}*{1} |", m_tFrameHead2.iWidth, m_tFrameHead2.iHeight);
                StateLabel2.Text = sFrameInfomation;
                
            }
            else
            {
                StateLabel.Text = "";
            }
        }

        //���ڷֱ����л�ʱ��ˢ�±�����ͼ
        private void timer2_Tick(object sender, EventArgs e)
        {
            //�л��ֱ��ʺ󣬲���һ�α���
            if (m_bEraseBk == true)
            {
                m_bEraseBk = false;
                PreviewBox.Refresh();
            }
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {

        }

        private void BtnSettings2_Click(object sender, EventArgs e)
        {
            if (m_hCamera2 > 0)
            {
                MvApi.CameraShowSettingPage(m_hCamera2, 1);//1 show ; 0 hide
            }
        }

        private void BtnPlay2_Click(object sender, EventArgs e)
        {
            if (m_hCamera2 > 0)
            {
                if (BtnPlay2.Text == "Play")
                {
                    MvApi.CameraPlay(m_hCamera2);
                    BtnPlay2.Text = "Pause";
                }
                else
                {
                    MvApi.CameraPause(m_hCamera2);
                    BtnPlay2.Text = "Play";
                }
            }
        }

    }
}