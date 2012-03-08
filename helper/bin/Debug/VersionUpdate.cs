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
        /// 微软系统
        /// </summary>
        public const string WINDOWS = "windows";

        /// <summary>
        /// 远程连接根路径
        /// </summary>
        public static readonly string URL = System.Configuration.ConfigurationManager.AppSettings["UpdateUrl"];

        /// <summary>
        /// 保存更新的本地目录 
        /// </summary>
        public static readonly string SAVEDIC = System.Configuration.ConfigurationManager.AppSettings["SaveDic"];

        /// <summary>
        /// 根据类型获取版本号
        /// </summary>
        /// <param name="url">远程目录的根目录</param>
        /// <param name="systemType">类型 0 Fedore系统 1 Redhat系统</param>
        /// <returns>当前的版本号</returns>
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
        /// 更新本地文件
        /// </summary>
        /// <param name="url">远程基本目录</param>
        /// <param name="systemType">类型 0 Fedore系统 1 Redhat系统</param>
        /// <param name="currVersion">当前版本</param>
        /// <param name="saveDic">保存的文件目录</param>
        /// <returns>
        /// true 成功 ; 
        /// false 失败
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
