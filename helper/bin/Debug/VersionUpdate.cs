using System;
using System.Collections.Generic;
using System.Text;

namespace helper
{
    public class VersionUpdate
    {
        /// <summary>
        /// redhat
        /// </summary>
        public const string REDHAT = "redhat";

        /// <summary>
        /// fedore
        /// </summary>
        public const string FEDORE = "fedora";

        /// <summary>
        /// ΢��ϵͳ
        /// </summary>
        public const string WINDOWS = "windows";

        /// <summary>
        /// Զ�����Ӹ�·��
        /// </summary>
        public static readonly string URL = System.Configuration.ConfigurationManager.AppSettings["UpdateUrl"];

        /// <summary>
        /// ������µı���Ŀ¼ 
        /// </summary>
        public static readonly string SAVEDIC = System.Configuration.ConfigurationManager.AppSettings["SaveDic"];

        /// <summary>
        /// �������ͻ�ȡ�汾��
        /// </summary>
        /// <param name="url">Զ��Ŀ¼�ĸ�Ŀ¼</param>
        /// <param name="systemType">���� 0 Fedoreϵͳ 1 Redhatϵͳ</param>
        /// <returns>��ǰ�İ汾��</returns>
        public static string GetVersion( int systemType)
        {
            string temp = string.Empty;
            if (systemType == 0)
            {
                temp = URL +  FEDORE+ "/version.txt";
            }
            else if (systemType == 1)
            {
                temp = URL + REDHAT + "/version.txt";
            }
            else if (systemType == 2)
            {
                temp = URL + WINDOWS + "/version.txt";
            }

            RemoteFile rf = new RemoteFile();


            byte[] bytes = rf.GetRemoteFileByteContent(temp);

            if (bytes == null)
                return null;

            string str = Encoding.Default.GetString(bytes);

            return str;
        }

        /// <summary>
        /// ���±����ļ�
        /// </summary>
        /// <param name="url">Զ�̻���Ŀ¼</param>
        /// <param name="systemType">���� 0 Fedoreϵͳ 1 Redhatϵͳ</param>
        /// <param name="currVersion">��ǰ�汾</param>
        /// <param name="saveDic">������ļ�Ŀ¼</param>
        /// <returns>
        /// true �ɹ� ; 
        /// false ʧ��
        /// </returns>
        public static string UpdateFile(int systemType, int currVersion)
        {

            string version = GetVersion(systemType);

            if (version == null)
                return null;

            if (Convert.ToInt32(version) <= currVersion)
                return null;


            string temp = string.Empty;
            string dic = SAVEDIC;

            if (systemType == 0)
            {
                temp = URL + FEDORE + "/update.tgz";
                dic = dic + FEDORE;
            }
            else if (systemType == 1)
            {
                temp = URL + REDHAT + "/update.tgz";
                dic = dic + REDHAT;
            }
            else if (systemType == 2)
            {
                temp = URL + WINDOWS + "/MainProc.exe";
                dic = dic + WINDOWS;
            }

            RemoteFile rf = new RemoteFile();
            rf.SaveDic = dic;

            if (rf.SaveRemoteFile(temp))
                return rf.DownPath;

            return null;
        }
    }
}
