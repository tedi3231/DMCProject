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
using System.IO;

/// <summary>
/// DataTableExport 的摘要说明
/// </summary>
public class DataTableExport
{
	public DataTableExport()
	{		

	}

    /// <summary>
    ///操作DataGrid DataTable導出到Excel.繁體OS,無亂碼問題.
    /// </summary>
    /// <param name="dg">DataTable</param>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <param name="strFileName">含.xls後綴</param>
    public static void DownloadAsExcel(DataGrid dg, DataSet ds, string ckID, string strFileName)
    {
        DataTable dt = GetSelectedTable(dg, ckID, ds);

        if (dt == null)
        {
            dt = new DataTable();
        }

        try
        {
            StringWriter sw = new StringWriter();
            string colstr = "";
            foreach (DataColumn col in dt.Columns)
            {
                colstr += col.ColumnName + "\t";
            }
            sw.WriteLine(colstr);

            foreach (DataRow row in dt.Rows)
            {
                colstr = "";
                foreach (DataColumn col in dt.Columns)
                {
                    colstr += row[col.ColumnName].ToString() + "\t";
                }
                sw.WriteLine(colstr);
            }
            sw.Close();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName + "");
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.Write(sw);
            System.Web.HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {
        }
    }

    /// <summary>
    ///操作GridView DataTable導出到Excel.繁體OS,無亂碼問題.
    /// </summary>
    /// <param name="dg">DataTable</param>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <param name="strFileName">含.xls後綴</param>
    public  static void DownloadAsExcel(GridView dg, DataSet ds, string ckID, string strFileName)
    {       
        DataTable dt = GetSelectedTable(dg, ckID,ds);
        
        try
        {
            StringWriter sw = new StringWriter();
            string colstr = "";
            foreach (DataColumn col in dt.Columns)
            {
                colstr += col.ColumnName + "\t";
            }
            sw.WriteLine(colstr);

            foreach (DataRow row in dt.Rows)
            {
                colstr = "";
                foreach (DataColumn col in dt.Columns)
                {
                    colstr += row[col.ColumnName].ToString() + "\t";
                }
                sw.WriteLine(colstr);
            }
            sw.Close();
            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + strFileName + "");
            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
            System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;
            System.Web.HttpContext.Current.Response.Write(sw);
            System.Web.HttpContext.Current.Response.End();
        }
        catch (Exception ex)
        {           
        }
    }


    /// <summary>
    /// 从DataGrid中选择选中的项组成表
    /// </summary>
    /// <param name="gd">GridView</param>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <returns>选中的项组成表</returns>
    public static DataTable GetSelectedTable(DataGrid gd, string ckID, DataSet ds)
    {
        DataTable dt = new DataTable();

        if (ds == null)
        {
            return dt;
        }


        string where = GetSelectedKeyList(gd, ckID);

        DataRow[] temp = ds.Tables[0].Select(gd.DataKeyField + " in(" + where + ")");

        foreach (DataColumn col in ds.Tables[0].Columns)
        {
            dt.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
        }
        //dt.Columns.AddRange(ds.Tables[0].Columns );

        foreach (DataRow row in temp)
        {
            DataRow r = dt.NewRow();
            r.ItemArray = row.ItemArray;
            dt.Rows.Add(r);
        }

        int count = dt.Rows.Count;

        return dt;
    }

    /// <summary>
    /// 从GridView中选择选中的项组成表
    /// </summary>
    /// <param name="gd">GridView</param>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <returns>选中的项组成表</returns>
    public static DataTable GetSelectedTable(GridView gd, string ckID,DataSet ds)
    {
        DataTable dt = new DataTable();

        if (ds == null)
        {
            return dt;
        }

        string where = GetSelectedKeyList(gd, ckID);

        DataRow[] temp = ds.Tables[0].Select(gd.DataKeyNames[0] + " in(" + where + ")");

        foreach (DataColumn col in ds.Tables[0].Columns)
        {
            dt.Columns.Add(new DataColumn(col.ColumnName, col.DataType));
        }
        //dt.Columns.AddRange(ds.Tables[0].Columns );

        foreach (DataRow row in temp)
        {
            DataRow r = dt.NewRow();
            r.ItemArray = row.ItemArray;
            dt.Rows.Add(r);
        }

        int count = dt.Rows.Count;

        return dt;
    }


    /// <summary>
    /// 获取DataGrid选中项的KEY
    /// </summary>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <param name="dtgData">GridView对象</param>
    /// <returns>返回KEY组合的字符串,以逗号隔开</returns>
    public static String GetSelectedKeyList(DataGrid dtgData, string ckID)
    {
        StringBuilder list = new StringBuilder("'0','");

        foreach (DataGridItem objItem in dtgData.Items)
        {
            CheckBox chk = (CheckBox)objItem.FindControl(ckID);

            if (chk != null && chk.Checked)
            {
                list.Append(dtgData.DataKeys[objItem.ItemIndex].ToString());
                list.Append("','");
            }
        }
        list.Append("0'");
        return list.ToString();
    }

    /// <summary>
    /// 获取GridView选中项的KEY
    /// </summary>
    /// <param name="ckID">CheckBox项的ID值 cbMail</param>
    /// <param name="dtgData">GridView对象</param>
    /// <returns>返回KEY组合的字符串,以逗号隔开</returns>
    public static String GetSelectedKeyList(GridView dtgData, string ckID )
    {
        StringBuilder list = new StringBuilder("'0','");

        foreach (GridViewRow objItem in dtgData.Rows)
        {
            CheckBox chk = (CheckBox)objItem.FindControl(ckID);

            if (chk != null && chk.Checked)
            {
                list.Append(dtgData.DataKeys[objItem.RowIndex].Value.ToString());
                list.Append("','");
            }
        }
        list.Append("0'");
        return list.ToString();
    }
}
