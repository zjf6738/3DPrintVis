using System;
using System.Threading;
using MVSDK;

namespace Basic
{
    public class tpvCameraObj
    {
        public Int32 m_hCamera = 0;             // ���
        public IntPtr       m_ImageBuffer;             // Ԥ��ͨ��RGBͼ�񻺴�
        public tSdkCameraCapbility tCameraCapability;  // �����������
        public int          m_iDisplayedFrames = 0;    //�Ѿ���ʾ����֡��
        public IntPtr       m_iCaptureCallbackCtx;     //ͼ��ص������������Ĳ���
        public Thread       m_tCaptureThread;          //ͼ��ץȡ�߳�
        public bool         m_bExitCaptureThread = false;//�����̲߳ɼ�ʱ�����߳��˳��ı�־
        public tSdkFrameHead m_tFrameHead;
        public bool          m_bEraseBk = false;

        public tpvCameraObj()
        {
        }
    }
}