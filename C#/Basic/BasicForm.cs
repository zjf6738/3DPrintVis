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
        protected CameraHandle m_hCamera = 0;             // ���
        protected IntPtr       m_ImageBuffer;             // Ԥ��ͨ��RGBͼ�񻺴�
        protected IntPtr       m_ImageBufferSnapshot;     // ץ��ͨ��RGBͼ�񻺴�
        protected tSdkCameraCapbility tCameraCapability;  // �����������
        protected int          m_iDisplayedFrames = 0;    //�Ѿ���ʾ����֡��
        protected CAMERA_SNAP_PROC m_CaptureCallback;
        protected IntPtr       m_iCaptureCallbackCtx;     //ͼ��ص������������Ĳ���
        protected Thread       m_tCaptureThread;          //ͼ��ץȡ�߳�
        protected bool         m_bExitCaptureThread = false;//�����̲߳ɼ�ʱ�����߳��˳��ı�־
        protected IntPtr       m_iSettingPageMsgCallbackCtx; //������ý�����Ϣ�ص������������Ĳ���   
        protected tSdkFrameHead m_tFrameHead;
        protected SnapshotDlg  m_DlgSnapshot = new SnapshotDlg();               //��ʾץ��ͼ��Ĵ���
        protected bool          m_bEraseBk = false;
        protected bool          m_bSaveImage = false;
        #endregion

        public BasicForm()
        {
            InitializeComponent();

            if (InitCamera() == true)
            {
                MvApi.CameraPlay(m_hCamera);
                BtnPlay.Text = "Pause";
            }

        }
       

#if USE_CALL_BACK
        public void ImageCaptureCallback(CameraHandle hCamera, IntPtr pFrameBuffer, ref tSdkFrameHead pFrameHead, IntPtr pContext)
        {
            //ͼ������ԭʼ���ת��ΪRGB��ʽ��λͼ���ݣ�ͬʱ���Ӱ�ƽ�⡢���Ͷȡ�LUT��ISP����
            MvApi.CameraImageProcess(hCamera, pFrameBuffer, m_ImageBuffer, ref pFrameHead);
            //����ʮ���ߡ��Զ��عⴰ�ڡ���ƽ�ⴰ����Ϣ(����������Ϊ�ɼ�״̬��)��   
            MvApi.CameraImageOverlay(hCamera, m_ImageBuffer, ref pFrameHead);
            //����SDK��װ�õĽӿڣ���ʾԤ��ͼ��
            MvApi.CameraDisplayRGB24(hCamera, m_ImageBuffer, ref pFrameHead);
            m_tFrameHead = pFrameHead;
            m_iDisplayedFrames++;

            if (pFrameHead.iWidth != m_tFrameHead.iWidth || pFrameHead.iHeight != m_tFrameHead.iHeight)
            {
                timer2.Enabled = true;
                timer2.Start();
                m_tFrameHead = pFrameHead;
            }
            
        }
#else
        public void CaptureThreadProc()
        {
            CameraSdkStatus eStatus;
            tSdkFrameHead FrameHead;
            IntPtr uRawBuffer;//rawbuffer��SDK�ڲ����롣Ӧ�ò㲻Ҫ����delete֮����ͷź���
  
            while(m_bExitCaptureThread == false)
            {
                //500���볬ʱ,ͼ��û����ǰ���̻߳ᱻ����,�ͷ�CPU�����Ը��߳����������sleep
                eStatus = MvApi.CameraGetImageBuffer(m_hCamera, out FrameHead, out uRawBuffer, 500);
                
                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS)//����Ǵ���ģʽ�����п��ܳ�ʱ
                {
                    //ͼ������ԭʼ���ת��ΪRGB��ʽ��λͼ���ݣ�ͬʱ���Ӱ�ƽ�⡢���Ͷȡ�LUT��ISP����
                    MvApi.CameraImageProcess(m_hCamera, uRawBuffer, m_ImageBuffer, ref FrameHead);
                    //����ʮ���ߡ��Զ��عⴰ�ڡ���ƽ�ⴰ����Ϣ(����������Ϊ�ɼ�״̬��)��    
                    MvApi.CameraImageOverlay(m_hCamera, m_ImageBuffer, ref FrameHead);
                    //����SDK��װ�õĽӿڣ���ʾԤ��ͼ��
                    MvApi.CameraDisplayRGB24(m_hCamera, m_ImageBuffer, ref FrameHead);
                    //�ɹ�����CameraGetImageBuffer������ͷţ��´β��ܼ�������CameraGetImageBuffer����ͼ��
                    MvApi.CameraReleaseImageBuffer(m_hCamera,uRawBuffer);

                    if (FrameHead.iWidth != m_tFrameHead.iWidth || FrameHead.iHeight != m_tFrameHead.iHeight)
                    {
                        m_bEraseBk = true;
                        m_tFrameHead = FrameHead;  
                    }

                    m_iDisplayedFrames++;

                    if (m_bSaveImage)
                    {
                        MvApi.CameraSaveImage(m_hCamera, "c:\\test.bmp", m_ImageBuffer, ref FrameHead, emSdkFileType.FILE_BMP, 100);
                        m_bSaveImage = false;
                    }
                }
           
            }
           
        }
#endif

        /*������ô��ڵ���Ϣ�ص�����
        hCamera:��ǰ����ľ��
        MSG:��Ϣ���ͣ�
	    SHEET_MSG_LOAD_PARAM_DEFAULT	= 0,//����Ĭ�ϲ����İ�ť�����������Ĭ�ϲ�����ɺ󴥷�����Ϣ,
	    SHEET_MSG_LOAD_PARAM_GROUP		= 1,//�л���������ɺ󴥷�����Ϣ,
	    SHEET_MSG_LOAD_PARAM_FROMFILE	= 2,//���ز�����ť��������Ѵ��ļ��м�����������󴥷�����Ϣ
	    SHEET_MSG_SAVE_PARAM_GROUP		= 3//���������ť���������������󴥷�����Ϣ
	    ����μ�CameraDefine.h��emSdkPropSheetMsg����

        uParam:��Ϣ�����Ĳ�������ͬ����Ϣ���������岻ͬ��
	    �� MSG Ϊ SHEET_MSG_LOAD_PARAM_DEFAULTʱ��uParam��ʾ�����س�Ĭ�ϲ�����������ţ���0��ʼ���ֱ��ӦA,B,C,D����
	    �� MSG Ϊ SHEET_MSG_LOAD_PARAM_GROUPʱ��uParam��ʾ�л���Ĳ�����������ţ���0��ʼ���ֱ��ӦA,B,C,D����
	    �� MSG Ϊ SHEET_MSG_LOAD_PARAM_FROMFILEʱ��uParam��ʾ���ļ��в������ǵĲ�����������ţ���0��ʼ���ֱ��ӦA,B,C,D����
	    �� MSG Ϊ SHEET_MSG_SAVE_PARAM_GROUPʱ��uParam��ʾ��ǰ����Ĳ�����������ţ���0��ʼ���ֱ��ӦA,B,C,D����
        */
        public void SettingPageMsgCalBack(CameraHandle hCamera, uint MSG, uint uParam, IntPtr pContext)
        {

        }

        private bool InitCamera()
        {
            CameraSdkStatus status;
            tSdkCameraDevInfo[] tCameraDevInfoList;
            IntPtr ptr;
            int i;
#if USE_CALL_BACK
            CAMERA_SNAP_PROC pCaptureCallOld = null;
#endif
            if (m_hCamera > 0)
            {
                //�Ѿ���ʼ������ֱ�ӷ��� true

                return true;
            }

            status = MvApi.CameraEnumerateDevice(out tCameraDevInfoList);
            if (status == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                if (tCameraDevInfoList != null)//��ʱiCameraCounts������ʵ�����ӵ�����������������1�����ʼ����һ�����
                {
                    status = MvApi.CameraInit(ref tCameraDevInfoList[0], -1,-1, ref m_hCamera);
                    if (status == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
                    {
                        //��������������
                        MvApi.CameraGetCapability(m_hCamera, out tCameraCapability);

                        m_ImageBuffer = Marshal.AllocHGlobal(tCameraCapability.sResolutionRange.iWidthMax * tCameraCapability.sResolutionRange.iHeightMax*3 + 1024);
                        m_ImageBufferSnapshot = Marshal.AllocHGlobal(tCameraCapability.sResolutionRange.iWidthMax * tCameraCapability.sResolutionRange.iHeightMax * 3 + 1024);
                        
                        //��ʼ����ʾģ�飬ʹ��SDK�ڲ���װ�õ���ʾ�ӿ�
                        MvApi.CameraDisplayInit(m_hCamera, PreviewBox.Handle);
                        MvApi.CameraSetDisplaySize(m_hCamera, PreviewBox.Width, PreviewBox.Height);

                        //����ץ��ͨ���ķֱ��ʡ�
                        tSdkImageResolution tResolution;
                        tResolution.uSkipMode = 0;
                        tResolution.uBinAverageMode = 0;
                        tResolution.uBinSumMode = 0;
                        tResolution.uResampleMask  = 0;
                        tResolution.iVOffsetFOV = 0;
                        tResolution.iHOffsetFOV = 0;
                        tResolution.iWidthFOV  = tCameraCapability.sResolutionRange.iWidthMax;
                        tResolution.iHeightFOV = tCameraCapability.sResolutionRange.iHeightMax;
                        tResolution.iWidth = tResolution.iWidthFOV;
                        tResolution.iHeight = tResolution.iHeightFOV;
                        //tResolution.iIndex = 0xff;��ʾ�Զ���ֱ���,���tResolution.iWidth��tResolution.iHeight
                        //����Ϊ0�����ʾ����Ԥ��ͨ���ķֱ��ʽ���ץ�ġ�ץ��ͨ���ķֱ��ʿ��Զ�̬���ġ�
                        //�����н�ץ�ķֱ��ʹ̶�Ϊ���ֱ��ʡ�
                        tResolution.iIndex = 0xff;
                        tResolution.acDescription = new byte[32];//������Ϣ���Բ�����
                        tResolution.iWidthZoomHd = 0;
                        tResolution.iHeightZoomHd = 0;
                        tResolution.iWidthZoomSw = 0;
                        tResolution.iHeightZoomSw = 0;
                     
                        MvApi.CameraSetResolutionForSnap(m_hCamera, ref tResolution);
                       
                        //��SDK������������ͺŶ�̬��������������ô��ڡ�
                        MvApi.CameraCreateSettingPage(m_hCamera,this.Handle,tCameraDevInfoList[0].acFriendlyName,/*SettingPageMsgCalBack*/null,/*m_iSettingPageMsgCallbackCtx*/(IntPtr)null,0);

                        //���ַ�ʽ�����Ԥ��ͼ�����ûص���������ʹ�ö�ʱ�����߶����̵߳ķ�ʽ��
                        //��������CameraGetImageBuffer�ӿ���ץͼ��
                        //�����н���ʾ�����ֵķ�ʽ,ע�⣬���ַ�ʽҲ����ͬʱʹ�ã������ڻص������У�
                        //��Ҫʹ��CameraGetImageBuffer������������������
#if USE_CALL_BACK
                        m_CaptureCallback = new CAMERA_SNAP_PROC(ImageCaptureCallback);
                        MvApi.CameraSetCallbackFunction(m_hCamera, m_CaptureCallback, m_iCaptureCallbackCtx, ref pCaptureCallOld);
#else //�����Ҫ���ö��̣߳�ʹ������ķ�ʽ
                        m_bExitCaptureThread = false;
                        m_tCaptureThread = new Thread(new ThreadStart(CaptureThreadProc));
                        m_tCaptureThread.Start();

#endif
                        //MvApi.CameraReadSN �� MvApi.CameraWriteSN ���ڴ�����ж�д�û��Զ�������кŻ����������ݣ�32���ֽ�
                        //MvApi.CameraSaveUserData �� MvApi.CameraLoadUserData���ڴ�����ж�ȡ�Զ������ݣ�512���ֽ�
                        return true;

                    }
                    else
                    {
                        m_hCamera = 0;
                        StateLabel.Text = "Camera init error";
                        String errstr = string.Format("�����ʼ�����󣬴�����{0},����ԭ����",status);
                        String errstring = MvApi.CameraGetErrorString(status);
                       // string str1 
                        MessageBox.Show(errstr + errstring, "ERROR");
                        Environment.Exit(0);
                        return false;
                    }


                }
            }
            else
            {
                MessageBox.Show("û���ҵ����������Ѿ����������������Ȩ�޲������볢��ʹ�ù���ԱȨ�����г���");
                Environment.Exit(0);
            }

            return false;
        
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (m_hCamera < 1)//��δ��ʼ�����
            {
                if (InitCamera() == true)
                {
                    MvApi.CameraPlay(m_hCamera);
                    BtnPlay.Text = "Pause";
                }
            }
            else//�Ѿ���ʼ��
            {
                if (BtnPlay.Text == "Play")
                {
                    MvApi.CameraPlay(m_hCamera);
                    BtnPlay.Text = "Pause";
                }
                else
                {
                    MvApi.CameraPause(m_hCamera);
                    BtnPlay.Text = "Play";
                }
            }
        }

        private void BasicForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_hCamera > 0)
            {
#if !USE_CALL_BACK //ʹ�ûص������ķ�ʽ����Ҫֹͣ�߳�
                m_bExitCaptureThread = true;
                while (m_tCaptureThread.IsAlive)
                {
                    Thread.Sleep(10);
                }
#endif
                MvApi.CameraUnInit(m_hCamera);
                Marshal.FreeHGlobal(m_ImageBuffer);
                Marshal.FreeHGlobal(m_ImageBufferSnapshot);
                m_hCamera = 0;
            }

        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            if (m_hCamera > 0)
            {
                MvApi.CameraShowSettingPage(m_hCamera, 1);//1 show ; 0 hide
            }
        }

        //1�����һ����Ƶ��Ϣ
        private void timer1_Tick(object sender, EventArgs e)
        {
            tSdkFrameStatistic tFrameStatistic;
            if (m_hCamera > 0)
            {
                //���SDK��ͼ��֡ͳ����Ϣ������֡������֡�ȡ�
                MvApi.CameraGetFrameStatistic(m_hCamera, out tFrameStatistic);
                //��ʾ֡����Ӧ�ó����Լ���¼��
                string sFrameInfomation = String.Format("| Resolution:{0}*{1} | Display frames{2} | Capture frames{3} |", m_tFrameHead.iWidth, m_tFrameHead.iHeight, m_iDisplayedFrames, tFrameStatistic.iCapture);
                StateLabel.Text = sFrameInfomation;
                
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

        private void BtnSnapshot_Click(object sender, EventArgs e)
        {
            tSdkFrameHead tFrameHead;
            IntPtr uRawBuffer;//��SDK�и�RAW���ݷ����ڴ棬���ͷ�
           
                          
            if (m_hCamera <= 0)
            {
                return;//�����δ��ʼ���������Ч
            }
            
            if (MvApi.CameraSnapToBuffer(m_hCamera, out tFrameHead, out uRawBuffer,500) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                //��ʱ��uRawBufferָ�������ԭʼ���ݵĻ�������ַ��Ĭ�������Ϊ8bitλ���Bayer��ʽ�����
                //����Ҫ����bayer���ݣ���ʱ�Ϳ���ֱ�Ӵ����ˣ������Ĳ�����ʾ����ν�ԭʼ����ת��ΪRGB��ʽ
                //����ʾ�ڴ����ϡ�

                //����������ԭʼ����ת��ΪRGB��ʽ���ڴ�m_ImageBufferSnapshot��
                MvApi.CameraImageProcess(m_hCamera, uRawBuffer, m_ImageBufferSnapshot, ref tFrameHead);
                //CameraSnapToBuffer�ɹ����ú������CameraReleaseImageBuffer�ͷ�SDK�з����RAW���ݻ�����
                //���򣬽������������Ԥ��ͨ����ץ��ͨ���ᱻһֱ������ֱ������CameraReleaseImageBuffer�ͷź������
                MvApi.CameraReleaseImageBuffer(m_hCamera, uRawBuffer);
                //����ץ����ʾ���ڡ�
                m_DlgSnapshot.UpdateImage(ref tFrameHead, m_ImageBufferSnapshot);
                m_DlgSnapshot.Show(); 
            }
        }

        private void BasicForm_Load(object sender, EventArgs e)
        {

        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            m_bSaveImage = true;//֪ͨԤ���̣߳�����һ��ͼƬ����Ҳ���Բο�BtnSnapshot_Click ��ץͼ��ʽ������ץһ��ͼƬ��Ȼ����� MvApi.CameraSaveImage ����ͼƬ���档      
        }

    }
}