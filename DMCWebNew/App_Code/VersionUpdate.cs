using System;
using System.Text;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// VersionUpdate 的摘要说明
/// 升级类
/// </summary>
public class VersionUpdate
{
    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="isAuto">是否为自动更新 </param>
    public static void update(bool isAuto)
    {
        if (isAuto)
        {
            DateTime date = DateTime.Now;

            //每周三自动更新
            if (date.DayOfWeek.ToString() == "3")
            {
                update();            
            }
            return;
        }

        update();
    }

    /// <summary>
    /// 更新
    /// </summary>
    private static void update()
    {
        database db = new dbConfig();
        DataSet ds = db.CreateDataSet("select * from TB_Update");

        if (ds == null || ds.Tables == null || ds.Tables[0] == null || ds.Tables[0].Rows.Count <= 0)
            return;

        string path = string.Empty;
        string version = string.Empty;

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (row["vSystemType"].ToString() == "2") //windows
            {
                path = helper.VersionUpdate.UpdateFile(2, Convert.ToInt32(row["vVersion"]));
                version = helper.VersionUpdate.GetVersion(2);
                if (!string.IsNullOrEmpty(path))
                    db.ExecuteScalar("update TB_Update set vVersion=" + version + ",vAppPath='" + path + "',dUpdateDate='" + DateTime.Now.ToString() + "' where vSystemType=2");
            }

            if (row["vSystemType"].ToString() == "1") //redhat
            {
                path = helper.VersionUpdate.UpdateFile(1, Convert.ToInt32(row["vVersion"]));
                version = helper.VersionUpdate.GetVersion(1);
                if (!string.IsNullOrEmpty(path))
                    db.ExecuteScalar("update TB_Update set vVersion=" + version + ",vAppPath='" + path + "',dUpdateDate='" + DateTime.Now.ToString() + "' where vSystemType=1");
            }

            if (row["vSystemType"].ToString() == "0") //fedore
            {
                path = helper.VersionUpdate.UpdateFile(0, Convert.ToInt32(row["vVersion"]));
                version = helper.VersionUpdate.GetVersion(0);
                if (!string.IsNullOrEmpty(path))
                    db.ExecuteScalar("update TB_Update set vVersion=" + version + ",vAppPath='" + path + "',dUpdateDate='" + DateTime.Now.ToString() + "' where vSystemType=0");
            }
        }
    }
}
