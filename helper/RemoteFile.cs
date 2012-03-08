using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

/// <summary>
/// RemoteFile 的摘要说明
/// 对远程文件的下载操作
/// </summary>
public class RemoteFile
{
    private string downPath = string.Empty;

    /// <summary>
    /// 下载后保存的文件路径
    /// </summary>
    public string DownPath
    {
        get { return downPath; }
        set { downPath = value; }
    }

    private string saveDic = string.Empty;
    
    /// <summary>
    /// 保存文件的本地目录
    /// </summary>
    public string SaveDic
    {
        get { return saveDic; }
        set { saveDic = value; }
    }



    /// <summary>
    /// 构造函数
    /// </summary>
    public RemoteFile()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="saveDic">初始化本地保存目录</param>
    public RemoteFile(string saveDic)
    {
        this.saveDic = saveDic;
    }

    /// <summary>
    /// 判断远程文件是否存在 
    /// </summary>
    /// <param name="fileUrl"> 远程文件路径</param>
    /// <returns>true 存在 ，false 不存在 </returns>
    public bool RemoteFileExists(string fileUrl)
    {
        bool result = false;//下载结果
        WebResponse response = null;
        try
        {
            WebRequest req = WebRequest.Create(fileUrl);

            response = req.GetResponse();

            result = response == null ? false : true;

        }
        catch (Exception ex)
        {
            result = false;
        }
        finally
        {
            if (response != null)
                response.Close();
        }

        return result;
    }

    /// <summary>
    /// 将远程文件以byte数组的形式返回
    /// </summary>
    /// <param name="url">远程文件路径</param>
    /// <returns>byte数组; 失败返回null</returns>
    public byte[] GetRemoteFileByteContent(string url)
    {
        if (!RemoteFileExists(url))
            return null;

        WebResponse rsp = null;
        byte[] retBytes = null;

        try
        {
            Uri uri = new Uri(url);

            WebRequest req = WebRequest.Create(uri);
            rsp = req.GetResponse();
            Stream stream = rsp.GetResponseStream();

            using (MemoryStream ms = new MemoryStream())
            {
                int b;
                while ((b = stream.ReadByte()) != -1)
                {
                    ms.WriteByte((byte)b);
                }
                retBytes = ms.ToArray();
            }

            if (retBytes == null || retBytes.Length <= 0)
            {
                retBytes = null;
            }
        }
        catch (Exception ex)
        {
            retBytes = null;
        }
        finally
        {
            if (rsp != null)
            {
                rsp.Close();
            }
        }

        return retBytes;
    }

    /// <summary>
    ///  下载并保存远程文件
    /// </summary>
    /// <param name="url">远程文件访问地址</param>
    /// <returns>true 保存成功 ; false 保存失败</returns>
    public bool SaveRemoteFile(string url)
    {
        byte[] retBytes = GetRemoteFileByteContent(url);

        if (retBytes == null)
        {
            return false;
        }

        try
        {
            Uri uri = new Uri(url);                     
            FileInfo fItem = new FileInfo(uri.LocalPath);

            string filePath = saveDic + "\\" + fItem.Name;

            File.WriteAllBytes(filePath, retBytes);

            DownPath = filePath;

            return true;
        }
        catch 
        {
            return false;
        }
    }
   
}