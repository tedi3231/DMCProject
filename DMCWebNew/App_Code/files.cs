using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Data.OleDb;
using System.IO;

/// <summary>
/// files 的摘要说明
/// </summary>
public class files
{
    private bool isDisposed = false;
    protected OleDbConnection Connection;//数据源连接字串
	public files()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
        Connection = new OleDbConnection("PROVIDER=MSIDXS;DATA SOURCE=Web");
    }
    ~files()
    {
        Dispose(false);
    }

    // 无法被客户直接调用 
    // 如果 disposing 是 true, 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
    // 如果 disposing 是 false, 那么函数是从垃圾回收器在调用Finalize时调用的,此时不应当引用其他托管对象所以,只能释放非托管资源 
    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                Connection.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            // 那么这个方法是被客户直接调用的,告诉垃圾回收器从Finalization队列中清除自己,从而阻止垃圾回收器调用Finalize方法.         
            if (disposing)
                GC.SuppressFinalize(this);
            isDisposed = true;
        }
    }

    //可以被客户直接调用 
    public void Dispose()
    {
        //必须以Dispose(true)方式调用,以true告诉Dispose(bool disposing)函数是被客户直接调用的 
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    //按内容搜索文件，返回搜索到的文件个数
    public int searchFile(string content, int IsGarbage)
    {
        int fileCount = 0;
        string sDirectory = "";
        string sFileName = "";
        string sPath = "";
        string sDate = "";
        dbMailBox PopMail = new dbMailBox("Pop");
        dbMailBox SmtpMail = new dbMailBox("Smtp");
        dbMailSite GetMail = new dbMailSite("Get");
        dbMailSite SendMail = new dbMailSite("Send");
        if (content.Length > 0)
        {
            using (Connection)
            {
                Connection.Open();
                using (OleDbCommand cmd = Connection.CreateCommand())
                {
                    cmd.CommandText = string.Format(@"select Filename,Directory,Path 
                        from Scope() where CONTAINS (Contents,'{0}')
                        AND MATCHES(ShortFileName, '*.|(HTM|,EML|)')", content);
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        using (DataTable dt = new DataTable())
                        {
                            da.Fill(dt);
                            fileCount = dt.Rows.Count;
                            if (IsGarbage == 0)//对敏感信息关键字处理
                            {
                                for (int i = 0; i < fileCount; i++)
                                {
                                    sDirectory = dt.Rows[i]["Directory"].ToString();
                                    sFileName = dt.Rows[i]["FileName"].ToString();
                                    sDate = sDirectory.Substring(sDirectory.Length - 10, 8);
                                    sPath = sDirectory + "/" + sFileName;
                                    if (sDirectory.Substring(sDirectory.Length - 1) == "0")
                                    {
                                        PopMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                        SmtpMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                    }
                                    else
                                    {
                                        GetMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                        SendMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                    }
                                }
                            }
                            if (IsGarbage == 1)//对垃圾邮件关键字处理
                            {
                                for (int i = 0; i < fileCount; i++)
                                {
                                    sPath = dt.Rows[i]["Path"].ToString();
                                    File.Delete(sPath);
                                    if (sDirectory.Substring(sDirectory.Length - 1) == "0")
                                    {
                                        PopMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                        SmtpMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                    }
                                    else
                                    {
                                        GetMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                        SendMail.dpsRecByFile(sDate, sPath, IsGarbage);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        return fileCount;
    }
}
