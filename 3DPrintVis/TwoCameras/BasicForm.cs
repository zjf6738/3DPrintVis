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
        const int CAMERA_NUM = 4;

        // ���1��2��3��4
        private tpvCameraObj[] cameraObjs;

        //���1
        //private tpvCameraObj cameraObj1;
        //���2
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
            IntPtr uRawBuffer; //rawbuffer��SDK�ڲ����롣Ӧ�ò㲻Ҫ����delete֮����ͷź���

            while (cameraObj.m_bExitCaptureThread == false)
            {
                //500���볬ʱ,ͼ��û����ǰ���̻߳ᱻ����,�ͷ�CPU�����Ը��߳����������sleep
                eStatus = MvApi.CameraGetImageBuffer(cameraObj.m_hCamera, out FrameHead, out uRawBuffer, 500);

                if (eStatus == CameraSdkStatus.CAMERA_STATUS_SUCCESS) //����Ǵ���ģʽ�����п��ܳ�ʱ
                {
                    //ͼ������ԭʼ���ת��ΪRGB��ʽ��λͼ���ݣ�ͬʱ���Ӱ�ƽ�⡢���Ͷȡ�LUT��ISP����
                    MvApi.CameraImageProcess(cameraObj.m_hCamera, uRawBuffer, cameraObj.m_ImageBuffer, ref FrameHead);
                    //����ʮ���ߡ��Զ��عⴰ�ڡ���ƽ�ⴰ����Ϣ(����������Ϊ�ɼ�״̬��)��    
                    MvApi.CameraImageOverlay(cameraObj.m_hCamera, cameraObj.m_ImageBuffer, ref FrameHead);
                    //����SDK��װ�õĽӿڣ���ʾԤ��ͼ��
                    MvApi.CameraDisplayRGB24(cameraObj.m_hCamera, cameraObj.m_ImageBuffer, ref FrameHead);
                    //�ɹ�����CameraGetImageBuffer������ͷţ��´β��ܼ�������CameraGetImageBuffer����ͼ��
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
            // sdk״̬
            CameraSdkStatus status;
            // �豸��Ϣ
            tSdkCameraDevInfo[] tCameraDevInfoList;
            // ��ȡ���е��豸��Ϣ
            status = MvApi.CameraEnumerateDevice(out tCameraDevInfoList);
            // �豸������
            int iCameraCounts = (tCameraDevInfoList != null ? tCameraDevInfoList.Length : 0);
            
            // �������������2
            if (iCameraCounts >= 4)//��ʱiCameraCounts������ʵ�����ӵ�����������������2�����ʼ��ǰ2�����
            {
                // ��ȡ��һ�����������������ݵ�m_hCamera��ͷ
                if (!InitSingleCamera(tCameraDevInfoList, 0, CaptureThreadProc1, PreviewBox, BtnPlay, StateLabel, "Camera 1 init error", ref cameraObjs[0])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 1, CaptureThreadProc2, PreviewBox2, BtnPlay2, StateLabel2, "Camera 2 init error", ref cameraObjs[1])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 2, CaptureThreadProc3, PreviewBox3, BtnPlay3, StateLabel3, "Camera 3 init error", ref cameraObjs[2])) return false;
                if (!InitSingleCamera(tCameraDevInfoList, 3, CaptureThreadProc4, PreviewBox4, BtnPlay4, StateLabel4, "Camera 4 init error", ref cameraObjs[3])) return false;

                return true;
            }
            return false;
        }

        /// <summary>
        /// �����豸�б���ʼ���������
        /// </summary>
        /// <param name="tCameraDevInfoList">����豸�б�</param>
        /// <param name="cameraDevID">��i�����</param>
        /// <param name="PreviewBox">�����Ӧ��PictureBox</param>
        /// <param name="CaptureThreadProc1">�����Ӧ���̴߳�����</param>
        /// <param name="cameraObj">�������</param>
        /// <returns></returns>
        protected bool InitSingleCamera(tSdkCameraDevInfo[] tCameraDevInfoList, int cameraDevID,                                        
                                        ThreadStart CaptureThreadProc1,
                                        PictureBox PreviewBox, Button BtnPlay, Label StateLabel,
                                        string errMsg,
                                        ref  tpvCameraObj cameraObj)
        {


            if (MvApi.CameraInit(ref tCameraDevInfoList[cameraDevID], -1, -1, ref cameraObj.m_hCamera) == CameraSdkStatus.CAMERA_STATUS_SUCCESS)
            {
                //��������������
                MvApi.CameraGetCapability(cameraObj.m_hCamera, out cameraObj.tCameraCapability);

                cameraObj.m_ImageBuffer = Marshal.AllocHGlobal(cameraObj.tCameraCapability.sResolutionRange.iWidthMax*
                                         cameraObj.tCameraCapability.sResolutionRange.iHeightMax*3 + 1024);

                //��ʼ����ʾģ�飬ʹ��SDK�ڲ���װ�õ���ʾ�ӿ�
                MvApi.CameraDisplayInit(cameraObj.m_hCamera, PreviewBox.Handle);
                MvApi.CameraSetDisplaySize(cameraObj.m_hCamera, PreviewBox.Width, PreviewBox.Height);

                //��SDK������������ͺŶ�̬��������������ô��ڡ�
                MvApi.CameraCreateSettingPage(cameraObj.m_hCamera, this.Handle, tCameraDevInfoList[cameraDevID].acFriendlyName,
                    /*SettingPageMsgCalBack*/null, /*m_iSettingPageMsgCallbackCtx*/(IntPtr) null, 0);

                MvApi.CameraPlay(cameraObj.m_hCamera);
                BtnPlay.Text = "Pause";

                //���ַ�ʽ�����Ԥ��ͼ�����ûص���������ʹ�ö�ʱ�����߶����̵߳ķ�ʽ��
                //��������CameraGetImageBuffer�ӿ���ץͼ��
                //�����н���ʾ�����ֵķ�ʽ,ע�⣬���ַ�ʽҲ����ͬʱʹ�ã������ڻص������У�
                //��Ҫʹ��CameraGetImageBuffer������������������

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


        //1�����һ����Ƶ��Ϣ
        private void timer1_Tick(object sender, EventArgs e)
        {
            tSdkFrameStatistic tFrameStatistic;
            if (CameraObj1.m_hCamera > 0)
            {
                //���SDK��ͼ��֡ͳ����Ϣ������֡������֡�ȡ�
                MvApi.CameraGetFrameStatistic(CameraObj1.m_hCamera, out tFrameStatistic);
                //��ʾ֡����Ӧ�ó����Լ���¼��
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

        //���ڷֱ����л�ʱ��ˢ�±�����ͼ
        private void timer2_Tick(object sender, EventArgs e)
        {
            //�л��ֱ��ʺ󣬲���һ�α���
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

                //���1����ʼ��
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