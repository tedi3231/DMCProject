using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.IO;
//using Microsoft.Office.Interop.Excel;
using Excel;
using Microsoft.Win32;
using System.Diagnostics;

/// <summary>
/// 数据库基类
/// 修改时间：2007-8-11
/// 修改人：王燕艳
/// </summary>
public abstract class database : IDisposable
{
    private bool isDisposed = false;
    protected SqlConnection Connection;//数据源连接字串

    /// <summary>
    /// 数据表名前缀
    /// </summary>
    public string TablePrefix;

    /// <summary>
    /// 临时表名前缀
    /// </summary>
    public string TempTablePrefix;

    /// <summary>
    /// 用于删除表的存储过程名
    /// </summary>
    protected string DelProcName = "sp_del_tables";

    protected string TableFields;
    protected string TempFields;

    public database()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //        
    }

    ~database()
    {
        // 必须以Dispose(false)方式调用,以false告诉Dispose(bool disposing)函数是从垃圾回收器在调用Finalize时调用的 
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

    /// <summary>
    /// 执行操作，以整形返回操作结果
    /// </summary>
    /// <param name="Sql">要执行的SQL语句</param>
    /// <returns>>0 ,执行成功;否则执行失败</returns>
    public int ExecuteScalarRInt(string Sql)
    {
        int result;
        SqlCommand cmd = new SqlCommand(Sql, Connection);
        try
        {
            Connection.Open();
            result = cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }

        return result;
    }

    public string ExecuteScalar(string Sql)
    {
        string result;

        SqlCommand cmd = new SqlCommand(Sql, Connection);

        try
        {
            Connection.Open();
            result = System.Convert.ToString(cmd.ExecuteScalar());
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }

        return result;
    }

    public DataSet CreateDataSet(string sql)
    {
        SqlDataAdapter ad = new SqlDataAdapter(sql, Connection);
        DataSet ds = new DataSet();
        try
        {
            Connection.Open();
            ad.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    /// <summary>
    /// 分页获取数据列表,主要针对Config数据库
    /// Author:tedi3231 added 2009.11.08
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <param name="pageSize">每页的行数</param>
    /// <param name="pageIndex">当前页面</param>
    /// <param name="orderName">排序的字段名称</param>
    /// <param name="orderType">排序类型</param>
    /// <param name="strWhere">查询条件</param>
    /// <param name="isConfig">多余条件，无特殊用途</param>
    /// <returns>返回分页的数据</returns>
    public DataSet RunProcedure(string tableName, int pageSize, int pageIndex, string orderName, string orderType, string strWhere, bool isConfig)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@doCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1500)};

        parameters[0].Value = tableName;
        parameters[1].Value = TableFields;
        //用来排序的字段名
        parameters[2].Value = orderName;

        parameters[3].Value = pageSize;
        parameters[4].Value = pageIndex;
        parameters[5].Value = 0;
        parameters[6].Value = (orderType == "Asc" ? 0 : 1);
        parameters[7].Value = strWhere;

        SqlCommand cmd = new SqlCommand("sp_pagination", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 600;
        foreach (SqlParameter parameter in parameters)
            cmd.Parameters.Add(parameter);
        ada.SelectCommand = cmd;
        try
        {
            Connection.Open();
            ada.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    /// <summary>
    /// 分页获取数据列表
    /// </summary>
    /// <param name="tableName"></param>
    /// <param name="TableType"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <param name="orderType"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public DataSet RunProcedure(string tableName, string TableType, int pageSize, int pageIndex, string orderType, string strWhere)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@doCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1500)};

        parameters[0].Value = tableName;
        parameters[1].Value = "*";
        parameters[2].Value = "id";
        parameters[3].Value = pageSize;
        parameters[4].Value = pageIndex;
        parameters[5].Value = 0;
        parameters[6].Value = (orderType == "Asc" ? 0 : 1);
        parameters[7].Value = strWhere;

        SqlCommand cmd = new SqlCommand("sp_pagination", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 600;
        foreach (SqlParameter parameter in parameters)
            cmd.Parameters.Add(parameter);
        ada.SelectCommand = cmd;
        try
        {
            Connection.Open();
            ada.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    //分页获取数据列表
    public DataSet RunProcedure(string TableType, int pageSize, int pageIndex, string orderType, string strWhere)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter ada = new SqlDataAdapter();

        SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@doCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1500)};

        if (TableType == "4") //当天的
        {
            parameters[0].Value = TablePrefix + DateTime.Today.ToString("yyyyMMdd");
            parameters[1].Value = TableFields;
            parameters[2].Value = "dCapture";
        }
        else
        {
            parameters[0].Value = TempTablePrefix + "All";
            parameters[1].Value = TempFields;
            parameters[2].Value = "vTime";
        }

        parameters[3].Value = pageSize;
        parameters[4].Value = pageIndex;
        parameters[5].Value = 0;
        parameters[6].Value = (orderType == "Asc" ? 0 : 1);
        parameters[7].Value = strWhere;

        SqlCommand cmd = new SqlCommand("sp_pagination", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 600;

        foreach (SqlParameter parameter in parameters)
        {
            cmd.Parameters.Add(parameter);
        }

        ada.SelectCommand = cmd;
        try
        {
            Connection.Open();
            ada.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    /// <summary>
    /// 查询IP归属地的详细信息
    /// </summary>
    /// <param name="ipnum">地址的大类</param>
    /// <param name="ip">要查询的IP地址</param>
    /// <returns></returns>
    public DataSet GetIpAddrInfo(string ipnum, string ip)
    {
        DataSet ds = new DataSet();
        SqlDataAdapter ada = new SqlDataAdapter();
        SqlParameter[] parms = {
                    new SqlParameter("@ip", SqlDbType.VarChar,40),                   
                    new SqlParameter("@flag", SqlDbType.TinyInt,1)
                };

        parms[0].Value = ip;
        parms[1].Value = ipnum;

        SqlCommand cmd = new SqlCommand("getIPAreaInfo", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 600;
        foreach (SqlParameter parameter in parms)
        {
            cmd.Parameters.Add(parameter);
        }

        ada.SelectCommand = cmd;
        try
        {
            Connection.Open();
            ada.Fill(ds);
            return ds;
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }

        return ds;
    }

    /// <summary>
    /// 调用存储过程进行分页
    /// </summary>
    /// <param name="connString">数据库连接字符串</param>
    /// <param name="tblName">要分页显示的表名</param>
    /// <param name="fieldKey">用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段</param>
    /// <param name="pageCurrentIndex">要显示的页码</param>
    /// <param name="pageSize">每页显示的条数</param>
    /// <param name="fieldShow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
    /// <param name="fieldOrder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC,用于指定排序顺序</param>
    /// <param name="where">查询条件</param>
    /// <param name="itemCount">总条目数</param>
    /// <param name="pageCount">总页数</param>
    /// <returns></returns>
    public DataSet GetpPageData(string connString, string tblName, string fieldKey, int pageCurrentIndex, int pageSize, string fieldShow,
        string fieldOrder, string where, out int itemCount, out int pageCount)
    {
        itemCount = 0;
        pageCount = 0;
        SqlParameter[] parms = {
                    new SqlParameter("@tbname", SqlDbType.VarChar,255),
                    new SqlParameter("@FieldKey", SqlDbType.VarChar,1000),
                    new SqlParameter("@PageCurrent", SqlDbType.Int,4),
                    new SqlParameter("@PageSize", SqlDbType.Int,4),//每页的大小(记录数)
                    new SqlParameter("@FieldShow", SqlDbType.VarChar,1000),
                    new SqlParameter("@FieldOrder", SqlDbType.VarChar,1000),
                    new SqlParameter("@Where", SqlDbType.VarChar,1000),
                    new SqlParameter("@ItemCount", SqlDbType.Int,4),
                    new SqlParameter("@PageCount", SqlDbType.Int,4)
                };



        parms[0].Value = tblName;
        parms[1].Value = fieldKey;
        parms[2].Value = pageCurrentIndex;
        parms[3].Value = pageSize; //Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["PAGESIZE"]);
        parms[4].Value = fieldShow;
        parms[5].Value = fieldOrder;
        parms[6].Value = where;
        parms[7].Value = itemCount;
        parms[8].Value = pageCount;

        parms[7].Direction = ParameterDirection.Output;
        parms[8].Direction = ParameterDirection.Output;

        // 得到查询的结果集
        DataSet dt = SqlHelper.ExecuteDataSet(connString, CommandType.StoredProcedure, "sp_PageView", parms);

        //返回总条数和页数
        if (parms[7].Value == DBNull.Value)
        {
            itemCount = 0;
        }
        else
        {
            itemCount = Convert.ToInt32(parms[7].Value);
        }
        if (parms[8].Value == DBNull.Value)
        {
            pageCount = 0;
        }
        else
        {
            pageCount = Convert.ToInt32(parms[8].Value);
        }

        return dt;
    }

    /// <summary>
    /// 调用存储过程进行分页,并根据表类型
    /// </summary>
    /// <param name="connString">数据库连接字符串</param>
    /// <param name="tblName">要分页显示的表名</param>
    /// <param name="fieldKey">用于定位记录的主键(惟一键)字段,可以是逗号分隔的多个字段</param>
    /// <param name="pageCurrentIndex">要显示的页码</param>
    /// <param name="pageSize">每页显示的条数</param>
    /// <param name="fieldShow">以逗号分隔的要显示的字段列表,如果不指定,则显示所有字段</param>
    /// <param name="fieldOrder">以逗号分隔的排序字段列表,可以指定在字段后面指定DESC/ASC,用于指定排序顺序</param>
    /// <param name="where">查询条件</param>
    /// <param name="itemCount">总条目数</param>
    /// <param name="pageCount">总页数</param>
    /// <param name="tableType">表类型,4 当天</param>
    /// <returns></returns>
    public DataSet GetpPageData(string connString, string tblName, string fieldKey, int pageCurrentIndex, int pageSize, string fieldShow,
        string fieldOrder, string where, out int itemCount, out int pageCount, string tableType)
    {
        itemCount = 0;
        pageCount = 0;
        SqlParameter[] parms = {
                    new SqlParameter("@tbname", SqlDbType.VarChar,255),
                    new SqlParameter("@FieldShow", SqlDbType.VarChar,1000),
                    new SqlParameter("@FieldOrder", SqlDbType.VarChar,1000),
                    new SqlParameter("@FieldKey", SqlDbType.VarChar,1000),                   
                    new SqlParameter("@Where", SqlDbType.VarChar,1000),
                    new SqlParameter("@PageCurrent", SqlDbType.Int,4),
                    new SqlParameter("@PageSize", SqlDbType.Int,4),//每页的大小(记录数)
                    new SqlParameter("@ItemCount", SqlDbType.Int,4),
                    new SqlParameter("@PageCount", SqlDbType.Int,4)
                };

        if (tableType == "4") //当天的
        {
            parms[0].Value = TablePrefix + DateTime.Today.ToString("yyyyMMdd");
            parms[1].Value = TableFields;
            parms[2].Value = "dCapture desc";
            parms[3].Value = "nId";
        }
        else
        {
            parms[0].Value = TempTablePrefix + "All";
            parms[1].Value = TempFields;
            parms[2].Value = "dCapture desc";
            parms[3].Value = "vTime";
        }

        //parms[3].Value = fieldKey; 
        parms[4].Value = where;
        parms[5].Value = pageCurrentIndex;
        parms[6].Value = pageSize;
        parms[7].Value = itemCount;
        parms[8].Value = pageCount;

        parms[7].Direction = ParameterDirection.Output;
        parms[8].Direction = ParameterDirection.Output;

        // 得到查询的结果集
        DataSet dt = SqlHelper.ExecuteDataSet(connString, CommandType.StoredProcedure, "sp_PageView", parms);

        //返回总条数和页数
        if (parms[7].Value == DBNull.Value)
        {
            itemCount = 0;
        }
        else
        {
            itemCount = Convert.ToInt32(parms[7].Value);
        }
        if (parms[8].Value == DBNull.Value)
        {
            pageCount = 0;
        }
        else
        {
            pageCount = Convert.ToInt32(parms[8].Value);
        }

        return dt;
    }

    /// <summary>
    /// 获取查询结果记录数
    /// </summary>
    /// <param name="TableType"></param>
    /// <param name="tableName"></param>
    /// <param name="strWhere"></param>
    /// <returns></returns>
    public int getCount(string TableType, string tableName, string strWhere)
    {
        SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@doCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1500)};

        parameters[0].Value = tableName;
        parameters[5].Value = 1;
        parameters[7].Value = strWhere;

        SqlCommand cmd = new SqlCommand("sp_pagination", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 300;
        foreach (SqlParameter parameter in parameters)
            cmd.Parameters.Add(parameter);
        try
        {
            Connection.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    //获取查询结果记录数
    public int getCount(string TableType, string strWhere)
    {
        SqlParameter[] parameters = {
            new SqlParameter("@tblName", SqlDbType.VarChar, 255),
            new SqlParameter("@strGetFields", SqlDbType.VarChar, 1000),
            new SqlParameter("@fldName", SqlDbType.VarChar, 255),
            new SqlParameter("@PageSize", SqlDbType.Int),
            new SqlParameter("@PageIndex", SqlDbType.Int),
            new SqlParameter("@doCount", SqlDbType.Bit),
            new SqlParameter("@OrderType", SqlDbType.Bit),
            new SqlParameter("@strWhere", SqlDbType.VarChar,1500)};
        if (TableType == "4")
        {
            parameters[0].Value = TablePrefix + DateTime.Today.ToString("yyyyMMdd");
        }
        else
        {
            parameters[0].Value = TempTablePrefix + "All";
        }
        parameters[5].Value = 1;
        parameters[7].Value = strWhere;

        SqlCommand cmd = new SqlCommand("sp_pagination", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandTimeout = 300;
        foreach (SqlParameter parameter in parameters)
            cmd.Parameters.Add(parameter);
        try
        {
            Connection.Open();
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    //根据时间范围删除数据表
    public void DelTables(DateTime formDate, DateTime toDate)
    {
        SqlCommand cmd = new SqlCommand(DelProcName, Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@fromDate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@toDate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@Pre", SqlDbType.VarChar, 64));
        cmd.Parameters["@fromDate"].Value = formDate;
        cmd.Parameters["@toDate"].Value = toDate;
        cmd.Parameters["@Pre"].Value = TablePrefix;
        cmd.Connection.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    //更新数据表及临时表中数据
    public void UpdateData(int isDelRec, string vTable, string vID, string vField, string vValue)
    {
        SqlCommand cmd = new SqlCommand("sp_upd_data", Connection);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@isDelRec", SqlDbType.Bit));
        cmd.Parameters.Add(new SqlParameter("@vTable", SqlDbType.VarChar, 28));
        cmd.Parameters.Add(new SqlParameter("@vID", SqlDbType.VarChar, 10));
        cmd.Parameters.Add(new SqlParameter("@vField", SqlDbType.VarChar, 20));
        cmd.Parameters.Add(new SqlParameter("@vValue", SqlDbType.VarChar, 20));
        cmd.Parameters["@isDelRec"].Value = isDelRec;
        cmd.Parameters["@vTable"].Value = vTable;
        cmd.Parameters["@vID"].Value = vID;
        cmd.Parameters["@vField"].Value = vField;
        cmd.Parameters["@vValue"].Value = vValue;
        cmd.Connection.Open();
        try
        {
            cmd.ExecuteNonQuery();
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
    }

    //判断表是否存在
    public Boolean TableIsExist(string TableName)
    {
        string sql = "select count(*) as dida from sysobjects where id = object_id(N'" + TableName + "') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
        return (ExecuteScalar(sql) != "0");
    }

    //根据类型获取数据表名
    public string GetTableName(string TableType)
    {
        string _tablename = "";
        if (TableType == "4")//当天
            _tablename = TablePrefix + DateTime.Today.ToString("yyyyMMdd");
        else
            _tablename = TempTablePrefix + "All";
        return _tablename;
    }

    //根据选择的时间段类型获取数据集
    //public DataView GetContent(string TableType, string Condition, string SortType)
    //{
    //    string tablename = GetTableName(TableType);
    //    string sql;
    //    DataView dv = new DataView();
    //    if (TableIsExist(tablename))//判断表是否存在
    //    {
    //        sql = "SELECT ";
    //        if (TableType == "4")
    //            sql += TableFields;
    //        else
    //            sql += TempFields;

    //        sql += " FROM " + tablename + " where " + Condition + " order by dCapture " + SortType;
    //        dv = CreateDataSet(sql).Tables[0].DefaultView;
    //    }
    //    return dv;
    //}



    //根据选择的时间段类型获取SQL语句
    public string GetSQL(string TableType, string Condition, string SortType)
    {
        string tablename = GetTableName(TableType);
        string sql = "";
        if (TableIsExist(tablename))//判断表是否存在
        {
            if (TableType == "4")
                sql = "SELECT " + TableFields + " FROM " + tablename + " WHERE " + Condition + " ORDER BY nId " + SortType;
            else
                sql = "SELECT " + TempFields + " FROM " + tablename + " WHERE " + Condition + " ORDER BY vTime " + SortType;
        }
        return sql;
    }

    //根据表格类型和记录编号获取数据
    public DataSet GetContentByID(string TableType, string RecordID)
    {
        string tablename = GetTableName(TableType);
        string sql = "";
        if (TableIsExist(tablename))//判断表是否存在
        {
            if (TableType == "4")//当天
                sql = "SELECT * FROM " + tablename + " where nId=" + RecordID;
            else
                sql = "SELECT * FROM " + tablename + " where vTime='" + RecordID + "'";
        }
        return CreateDataSet(sql);
    }

    //根据数据表名前缀获取数据表名集
    //public DataSet GetTablesByPre()
    //{
    //    string sql = "SELECT name FROM dbo.sysobjects WHERE xtype = 'U' AND name LIKE '" + TablePrefix + "%'";
    //    return CreateDataSet(sql);
    //}

    //设置已读标记
    public void SetReaded(string TableType, string RecordID)
    {
        string tablename = GetTableName(TableType);
        string sID = RecordID;
        if (TableType != "4")//当天
        {
            tablename = TablePrefix + Convert.ToDateTime(common.SecondToDate(Convert.ToInt64(GetContentByID(TableType, RecordID).Tables[0].Rows[0]["dCapture"]))).ToString("yyyyMMdd");
            sID = GetContentByID(TableType, RecordID).Tables[0].Rows[0]["nId"].ToString();
        }
        UpdateData(0, tablename, sID, "nState", "1");
    }

    //删除记录
    public void DeleteRecord(string TableType, string RecordID)
    {
        string tablename = GetTableName(TableType);
        string sID = RecordID;
        if (TableType != "4")//当天
        {
            tablename = TablePrefix + Convert.ToDateTime(common.SecondToDate(Convert.ToInt64(GetContentByID(TableType, RecordID).Tables[0].Rows[0]["dCapture"]))).ToString("yyyyMMdd");
            sID = GetContentByID(TableType, RecordID).Tables[0].Rows[0]["nId"].ToString();
        }
        UpdateData(1, tablename, sID, "", "");
    }

    /// <summary>
    /// 删除所有符合查询条件的记录
    /// </summary>
    /// <param name="tableName">表名名称，此处指去掉前后缀的名称</param>
    /// <param name="TableType">表格类型;4今天,其他时间的</param>
    /// <param name="condition">针对表的条件</param>
    public void DeleteAllRecord(string tableName, string TableType, string condition)
    {
        if (!string.IsNullOrEmpty(tableName))
        {
            TablePrefix = "TC_" + tableName + "_";
            TempTablePrefix = "Temp_" + tableName + "_";
        }

        string tablename = GetTableName(TableType);
        string sql = "delete from " + tablename + " where " + condition;
        ExecuteScalarRInt(sql);
    }

    public void ExportToExcel(DateTime FromDate, DateTime ToDate, string FileName)
    {
        int pageSize = 50000;//一页最多50000条记录
        DateTime dteFromDate, dteToDate;
        dteFromDate = FromDate;
        dteToDate = ToDate;
        DataSet DS;
        string TableName;
        //定义Excel对象

        ApplicationClass pApp = new ApplicationClass();
        Workbook pBook = pApp.Workbooks.Add(Type.Missing);
        Worksheet pSheet;
        //pApp.Visible = true;
        int SheetsCount = 0;
        while (dteFromDate <= dteToDate)
        {
            TableName = TablePrefix + common.DateToStr(dteFromDate);
            if (TableIsExist(TableName))
            {
                DS = CreateDataSet("select * from " + TableName);

                int rows = DS.Tables[0].Rows.Count;
                int page = rows / pageSize;//设置每页不得超过50000条记录
                int cols = DS.Tables[0].Columns.Count;
                int curRows; //当前页记录行数
                string[,] datas;

                //将数据存到数组中
                for (int rep = 0; rep <= page; rep++)
                {
                    if (rep == page)//最后一页
                        curRows = rows % pageSize;
                    else
                        curRows = pageSize;
                    datas = new string[curRows + 1, cols];
                    for (int i = 0; i < cols; i++)
                        datas[0, i] = DS.Tables[0].Columns[i].Caption;
                    for (int j = 0; j < curRows; j++)
                        for (int k = 0; k < cols; k++)
                            datas[j + 1, k] = DS.Tables[0].Rows[pageSize * rep + j][k].ToString();
                    SheetsCount++;
                    try
                    {
                        if (SheetsCount <= pBook.Sheets.Count)
                            pSheet = pBook.Sheets[SheetsCount] as Worksheet;
                        else
                            pSheet = pBook.Sheets.Add(Type.Missing, pBook.Sheets[pBook.Sheets.Count], Type.Missing, Type.Missing) as Worksheet;
                        if (rep > 0)
                            pSheet.Name = TableName + "_" + rep.ToString();
                        else
                            pSheet.Name = TableName;

                        //pSheet.Cells[1, 1] = TableName;
                        //定义表头格式
                        pSheet.get_Range(pSheet.Cells[1, 1], pSheet.Cells[1, cols]).Font.Bold = true;
                        Range rng = pSheet.get_Range("A1", pSheet.Cells[curRows + 1, cols]);
                        rng.Value2 = datas;

                        System.Runtime.InteropServices.Marshal.ReleaseComObject(pSheet);
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
                    }
                    finally
                    {
                    }
                }
            }
            dteFromDate = dteFromDate.AddDays(1);
        }
        pBook.SaveCopyAs(FileName);
        pBook.Close(false, null, null);
        pApp.Quit();

        System.Runtime.InteropServices.Marshal.ReleaseComObject(pBook);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(pApp);
        pSheet = null;
        pBook = null;
        pApp = null;
        GC.Collect();
    }

    public void ExportToExcel(string SQL, string FileName)
    {
        int pageSize = 50000;//一页最多50000条记录
        DataSet DS;
        //定义Excel对象
        ApplicationClass pApp = new ApplicationClass();
        Workbook pBook = pApp.Workbooks.Add(Type.Missing);
        Worksheet pSheet;
        //pApp.Visible = true;
        int SheetsCount = 0;
        if (SQL != "")
        {
            DS = CreateDataSet(SQL);
            int rows = DS.Tables[0].Rows.Count;
            int page = rows / pageSize;//设置每页不得超过50000条记录
            int cols = DS.Tables[0].Columns.Count;
            int curRows; //当前页记录行数
            string[,] datas;

            //将数据存到数组中
            for (int rep = 0; rep <= page; rep++)
            {
                if (rep == page)//最后一页
                    curRows = rows % pageSize;
                else
                    curRows = pageSize;
                datas = new string[curRows + 1, cols];
                for (int i = 0; i < cols; i++)
                    datas[0, i] = DS.Tables[0].Columns[i].Caption;
                for (int j = 0; j < curRows; j++)
                    for (int k = 0; k < cols; k++)
                        datas[j + 1, k] = DS.Tables[0].Rows[pageSize * rep + j][k].ToString();
                SheetsCount++;
                try
                {
                    if (SheetsCount <= pBook.Sheets.Count)
                        pSheet = pBook.Sheets[SheetsCount] as Worksheet;
                    else
                        pSheet = pBook.Sheets.Add(Type.Missing, pBook.Sheets[pBook.Sheets.Count], Type.Missing, Type.Missing) as Worksheet;
                    //定义表头格式
                    pSheet.get_Range(pSheet.Cells[1, 1], pSheet.Cells[1, cols]).Font.Bold = true;
                    Range rng = pSheet.get_Range("A1", pSheet.Cells[curRows + 1, cols]);
                    rng.Value2 = datas;

                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(rng);
                }
                finally
                {
                }
            }
        }
        pBook.SaveCopyAs(FileName);
        pBook.Close(false, null, null);
        pApp.Quit();

        System.Runtime.InteropServices.Marshal.ReleaseComObject(pBook);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(pApp);
        pSheet = null;
        pBook = null;
        pApp = null;
        GC.Collect();
    }

    public void ExportToRar(string Path, string FileName)
    {
        string rar;
        RegistryKey reg;
        object obj;
        string info = Path;
        ProcessStartInfo startInfo;
        Process rarProcess;
        if (info != "")
        {
            try
            {
                reg = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\Shell\\Open\\Command");
                obj = reg.GetValue("");
                rar = obj.ToString();
                reg.Close();
                rar = rar.Substring(1, rar.Length - 7);
                info = " m " + FileName + " " + info + " -r";
                startInfo = new ProcessStartInfo();
                startInfo.FileName = rar;
                startInfo.Arguments = info;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                rarProcess = new Process();
                rarProcess.StartInfo = startInfo;
                rarProcess.Start();
            }
            finally
            {
            }
        }
    }

    //public void ExportToRar(string SQL, string FileName)
    //{
    //    DataSet ds;
    //    string rar;
    //    RegistryKey reg;
    //    object obj;
    //    string info = "";
    //    ProcessStartInfo startInfo;
    //    Process rarProcess;
    //    if (SQL != "")
    //    {
    //        ds = CreateDataSet(SQL);
    //        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //        {
    //            if (ds.Tables[0].Rows[i]["vLocalFile"].ToString() != "")
    //                info += " " + ds.Tables[0].Rows[i]["vLocalFile"].ToString();
    //        }
    //        if (info != "")
    //        {
    //            try
    //            {
    //                reg = Registry.ClassesRoot.OpenSubKey("Applications\\WinRAR.exe\\Shell\\Open\\Command");
    //                obj = reg.GetValue("");
    //                rar = obj.ToString();
    //                reg.Close();
    //                rar = rar.Substring(1, rar.Length - 7);
    //                info = " a -as -r -EP1 " + FileName + info;
    //                startInfo = new ProcessStartInfo();
    //                startInfo.FileName = rar;
    //                startInfo.Arguments = info;
    //                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
    //                rarProcess = new Process();
    //                rarProcess.StartInfo = startInfo;
    //                rarProcess.Start();
    //            }
    //            catch
    //            { 
    //            }
    //            GC.Collect();
    //        }
    //    }
    //}

    ////通过关键字比对物理文件
    ////关键字Key
    ////文件名约束
    //public DataSet chkFiles(string sKey, string sFileName)
    //{
    //    string sql = "SELECT * FROM OpenQuery(FileSystem,'SELECT FileName, Directory FROM SCOPE(''\"/attach/\"'')";
    //    sql += " WHERE CONTAINS(Contents,''\"" + sKey + "\"'')";
    //    sql += " AND MATCHES(ShortFileName, ''" + sFileName + "'')')";
    //    return CreateDataSet(sql);
    //}

    //通过中标文件名处理记录
    public void dpsRecByFile(string sDate, string sPath, int IsGarbage)
    {
        string sql = "SELECT count(*) FROM dbo.sysobjects WHERE xtype = 'U' AND name = '" + TablePrefix + sDate + "'";
        if (ExecuteScalar(sql) != "0")
        {
            string TableName = TablePrefix + sDate;
            sql = "SELECT nId FROM " + TableName + " WHERE vLocalFile = '" + sPath + "'";
            string nId = ExecuteScalar(sql);
            if (nId != "")
                UpdateData(IsGarbage, TableName, nId, "nKey", "2");
        }
    }

    //更改物理文件文件名
    public void changeFileName()
    {
        string sql = "SELECT name FROM dbo.sysobjects WHERE xtype = 'U' AND name LIKE '" + TablePrefix + "%'";
        using (Connection)
        {
            SqlCommand cmd = new SqlCommand(sql, Connection);
            SqlCommand cmdrec = null;
            SqlCommand cmdupd = null;
            SqlDataReader rdrec = null;
            string nId = "";
            string vLocalFile = "";
            string sDirectory = "";
            string sFileName = "";
            string newPath = "";
            string newFileName = "";
            int iPosition = 0;
            Connection.Open();
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    sql = "SELECT nid, vLocalFile FROM " + reader["name"].ToString();
                    cmdrec = new SqlCommand(sql, Connection);
                    using (rdrec = cmdrec.ExecuteReader())
                    {
                        while (rdrec.Read())
                        {
                            nId = rdrec["nId"].ToString();
                            vLocalFile = rdrec["vLocalFile"].ToString();
                            iPosition = vLocalFile.IndexOf("/");
                            sDirectory = vLocalFile.Substring(0, iPosition);
                            sDirectory = sDirectory.Replace("E:", "D:");
                            sFileName = vLocalFile.Substring(iPosition + 1);
                            newFileName = TablePrefix.Substring(3, 3) + sFileName;
                            newPath = sDirectory + "\\" + newFileName;
                            if (File.Exists(vLocalFile))
                                File.Move(vLocalFile, newPath);
                            sql = "UPDATE " + reader["name"].ToString() + " SET vLocalFile = '"
                                + newPath + "' WHERE nId = " + nId;
                            cmdupd = new SqlCommand(sql, Connection);
                            cmdupd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }

    public string getUserPower(string p)
    {
        throw new Exception("The method or operation is not implemented.");
    }
}

/// <summary>
/// 从database类派生出来的dbHorse类，用于访问dmc_horse_all数据库
/// </summary>
public class dbHorse : database
{
    private bool isDisposed = false;

    public dbHorse()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HorseAllConnStr"].ConnectionString);
        TablePrefix = "TC_trojan_";
        TempTablePrefix = "Temp_trojan_";
        TableFields = "dmc_config.dbo.ConvertNumToDate(dCapture) as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vSrcPort,vSrcMac,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,vDstPort,vDnsName,vSiteName,vFlag,nState,ipnum,nId as ID,ipnum,ipset,nKey,vRate,vLocalFile";
        TempFields = "dmc_config.dbo.ConvertNumToDate(dCapture) as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vSrcPort,vSrcMac,dmc_config.dbo.[f_Int2IP](vDstAddr)as vDstAddr,vDstPort,vDnsName,vSiteName,vFlag,nState,ipnum,vTime as ID,ipnum,ipset,nKey,vRate,vLocalFile";
    }

    ~dbHorse()
    {
        Dispose(false);
    }

    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbDns类，用于访问dmc_dns_all数据库
/// </summary>
public class dbDns : database
{
    private bool isDisposed = false;


    /// <summary>
    /// 构造函数，根据表格名称确定要显示的内容及要查询的表格
    /// </summary>
    /// <param name="tableName">dns,dnsalarm</param>
    public dbDns(string tableName)
    {
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DnsAllConnStr"].ConnectionString);
        TablePrefix = "TC_" + tableName + "_";
        TempTablePrefix = "Temp_" + tableName + "_";
        TableFields = "nState,nId as ID,vStateFlag, vName, dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac, dmc_config.dbo.f_Int2IP(vSrcIp) as vSrcAddr, dmc_config.dbo.f_Int2IP(vDstIp) as vDstAddr,vDstMac,dmc_config.dbo.f_Int2IP(vAddr) vAddr,vType,nParent,ipnum,ipset,nKey";
        TempFields = "nState,vTime as ID,vStateFlag,vTime,vName, dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac, dmc_config.dbo.f_Int2IP(vSrcIp) as vSrcAddr, dmc_config.dbo.f_Int2IP(vDstIp) as vDstAddr,vDstMac,dmc_config.dbo.f_Int2IP(vAddr) as vAddr,vType,nParent,ipnum,ipset,nKey";
    }

    public dbDns()
    {
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DnsAllConnStr"].ConnectionString);
        TablePrefix = "TC_Dns_";
        TempTablePrefix = "Temp_dnsalarm_";
        TableFields = "nState,nId as ID,vStateFlag, vName, dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac,dmc_config.dbo.f_Int2IP(vSrcIp) as vSrcAddr, dmc_config.dbo.f_Int2IP(vDstIp) as vDstAddr,vDstMac,vDstMac,dmc_config.dbo.f_Int2IP(vAddr) vAddr,vType,nParent,ipnum,ipset,nKey";
        TempFields = "nState,vTime as ID,vStateFlag,vTime,vName, dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac,dmc_config.dbo.f_Int2IP(vSrcIp) as vSrcAddr, dmc_config.dbo.f_Int2IP(vDstIp) as vDstAddr,vDstMac,vDstMac,dmc_config.dbo.f_Int2IP(vAddr) vAddr,vType,nParent,ipnum,ipset,nKey";
    }

    /// <summary>
    /// 根据时间类型和ID返回一条DNS记录
    /// </summary>
    /// <param name="timeType">4 表示当天</param>
    /// <param name="id">ID编号</param>
    /// <returns></returns>
    public DataSet getDnsInfo(string timeType, string id)
    {
        string sql = string.Empty;
        if (timeType == "4")
        {
            sql = "select * from " + TablePrefix + DateTime.Now.ToString("yyyyMMdd") + " where nId=" + id;
        }
        else
        {
            sql = "select * from " + TempTablePrefix + "all" + " where vTime='" + id + "'";
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    ///Ip变化频繁域名
    /// </summary>
    /// <returns>Ip变化频繁域名DataSet</returns>
    public DataSet getIpChangeCount()
    {
        string sql = "select vName,count(vDstIp) as ipCount from temp_dns_all where vtype<>6 group by vName order by ipCount desc";
        return CreateDataSet(sql);
    }

    /// <summary>
    ///多域名对同一ip
    /// </summary>
    /// <returns>多域名对同一ip DataSet</returns>
    public DataSet getDnsChangeCount()
    {
        string sql = "select dmc_config.dbo.[f_Int2IP](vDstIp) as ip,count(vName) as dnsCount from temp_dns_all group by vDstIp order by dnsCount desc";
        return CreateDataSet(sql);
    }

    ~dbDns()
    {
        Dispose(false);
    }

    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbConfig类，用于访问dms_config数据库
/// </summary>
public class dbConfig : database
{
    /// <summary>
    /// 重点怀疑对象字段
    /// </summary>
    private const string MACIPUSER_FIELDS = "nId,nParent,vMac,dmc_config.dbo.[f_Int2IP](vIp) as vIp,nType,vMark,vUpdateUser,dUpdateDate ";

    /// <summary>
    /// 黑IP字段
    /// </summary>
    private const string TS_BWIPLIST_FIELDS = "[id],dmc_config.dbo.[f_Int2IP](vName) as vName,vType";

    private bool isDisposed = false;
    public dbConfig()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConfigConnStr"].ConnectionString);
    }

    ~dbConfig()
    {
        Dispose(false);
    }

    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }

    /// <summary>
    /// 插入数据分析的数据
    /// <param name="ip">要查询的IP地址</param>
    /// <param name="parent">要查询的节点</param>
    /// <param name="start">开始时间</param>
    /// <param name="end">结束时间 </param>
    /// </summary>
    public DataSet ExecuteClues(string ip, DateTime start, DateTime end, string parent)
    {
        SqlParameter[] parms = new SqlParameter[]{
            new SqlParameter("@ipAddr1",SqlDbType.VarChar,50 ),
            new SqlParameter("@parent",SqlDbType.VarChar,50 ),
            new SqlParameter("@beginTime",SqlDbType.DateTime ),
            new SqlParameter("@endTime",SqlDbType.DateTime )
        };

        parms[0].Value = ip;
        parms[1].Value = parent;
        parms[2].Value = start;
        parms[3].Value = end;

       int result= SqlHelper.ExecuteNonQuery(Connection, CommandType.StoredProcedure, "cluse_count", parms);

        System.Threading.Thread.Sleep(100);

        DataSet ds = new DataSet();


        ds = SqlHelper.ExecuteDataSet(ConfigurationManager.ConnectionStrings["ConfigConnStr"].ConnectionString, CommandType.Text, "SELECT TOP 1 * FROM CluesInfo");

        if (ds == null || ds.Tables == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows == null || ds.Tables[0].Rows.Count <= 0)
        {
            ds = null;
        }

        return ds;
    }

    /// <summary>
    /// 获取站点
    /// </summary>
    /// <returns></returns>
    public DataSet getHosts()
    {
        return CreateDataSet("SELECT * FROM TB_UserInfo ORDER BY nKeyId");
    }
    public DataSet getHostByID(string ID)
    {
        return CreateDataSet("SELECT * FROM TB_UserInfo WHERE nId = " + ID);
    }

    #region 重点怀疑对象

    //重点怀疑对象
    public DataSet getMacIpUsers()
    {
        return CreateDataSet("SELECT " + MACIPUSER_FIELDS + " FROM TB_MacIpUser");
    }

    //根据当前操作员获取重点怀疑对象
    public DataSet getMacIpUsers(string UserID)
    {
        string sql;
        if (getUserPower(UserID) == "-1")//当前操作员是管理员身份
            sql = "SELECT " + MACIPUSER_FIELDS + "  FROM TB_MacIpUser ORDER BY nId";
        else
            sql = "SELECT " + MACIPUSER_FIELDS + "  FROM TB_MacIpUser WHERE vUpdateUser= '" + UserID + "' ORDER BY nId";
        return CreateDataSet(sql);
    }

    public DataSet getMacIpUserByID(string ID)
    {
        return CreateDataSet("SELECT " + MACIPUSER_FIELDS + "  FROM TB_MacIpUser WHERE nId = " + ID);
    }
    public void DeleteMacIpUser(string nId)
    {
        string sql = "DELETE TB_MacIpUser WHERE nId = " + nId;
        ExecuteScalar(sql);
    }
    public string InsertMacIpUser(string nParent, string vMac, string vIp, string nType, string vMark, string vUpdateUser)
    {
        string err = "";
        string sql = "";
        if ((nType == "0") && (vMac == ""))
            err = "您选择按MAC定位，必须输入MAC地址！";
        if ((nType == "1") && (vIp == ""))
            err = "您选择按IP定位，必须输入IP地址！";
        if (err == "")
        {
            if (nType == "0")
                sql = "SELECT COUNT(*) FROM TB_MacIpUser WHERE nType = 0 AND vMac = '" + vMac + "'";
            if (nType == "1")
                sql = "SELECT COUNT(*) FROM TB_MacIpUser WHERE nType = 1 AND vIp = dmc_config.dbo.[f_IP2int]('" + vIp + "')";
            if (ExecuteScalar(sql) != "0") err = "该重点怀疑对象已存在！";
        }
        if (err == "")
        {
            if (nType == "0")//保存MAC
            {
                sql = "INSERT INTO TB_MacIpUser(nParent, vMac, nType, vMark, vUpdateUser, dUpdateDate) VALUES(";
                sql += nParent + ",";
                sql += "'" + vMac + "',";
                sql += nType + ",";
                sql += "'" + vMark + "',";
                sql += "'" + vUpdateUser + "',";
                sql += "'" + System.DateTime.Now.ToString() + "')";
            }
            if (nType == "1")//保存IP
            {
                sql = "INSERT INTO TB_MacIpUser(nParent, vIp, nType, vMark, vUpdateUser, dUpdateDate) VALUES(";
                sql += nParent + ",";
                sql += "dmc_config.dbo.[f_IP2int]('" + vIp + "'),";
                sql += nType + ",";
                sql += "'" + vMark + "',";
                sql += "'" + vUpdateUser + "',";
                sql += "'" + System.DateTime.Now.ToString() + "')";
            }
            if (sql != "") ExecuteScalar(sql);
        }
        return err;
    }
    public string UpdateMacIpUser(string nId, string nParent, string vMac, string vIp, string nType, string vMark, string vUpdateUser)
    {
        string err = "";
        string sql;
        if ((nType == "0") && (vMac == ""))
            err = "您选择按MAC定位，必须输入MAC地址！";
        if ((nType == "1") && (vIp == ""))
            err = "您选择按IP定位，必须输入IP地址！";
        if (err == "")
        {
            sql = "SELECT COUNT(*) FROM TB_MacIpUser WHERE ((nType = 0 AND vMac = '" + vMac + "')";
            sql += " OR (nType = 1 AND vIp = dmc_config.dbo.[f_IP2int]('" + vIp + "'))) AND nId <> " + nId;
            if (ExecuteScalar(sql) != "0") err = "该重点怀疑对象已存在！";
        }
        if (err == "")
        {
            sql = "UPDATE TB_MacIpUser SET ";
            sql += "nParent = " + nParent + ",";
            sql += "vMac = '" + vMac + "',";
            sql += "vIp = dmc_config.dbo.[f_IP2int]('" + vIp + "'),";
            sql += "nType =" + nType + ",";
            sql += "vMark = '" + vMark + "',";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "' ";
            sql += "WHERE nId = " + nId;
            ExecuteScalar(sql);
        }
        return err;
    }
    #endregion

    /// <summary>
    /// 根据关键字类型返回所有的敏感信息库关键字对象
    /// </summary>
    /// <param name="categoryId">关键字类型</param>
    /// <returns>关键字类型返回所有的关键字对象</returns>
    public DataSet getKeyUsers(string nCategory)
    {
        string sql = "SELECT nId, vKey, nCategory FROM TB_Keyword";
        if (nCategory != "0")
            sql += " WHERE nCategory = " + nCategory;
        return CreateDataSet(sql);
    }

    //敏感信息库
    //获得所有敏感信息关键字（包括类别名称）
    public DataSet getAllKeywords()
    {
        DataSet ds = new DataSet();
        SqlDataAdapter adKeywords = new SqlDataAdapter("SELECT * FROM TB_Keyword", Connection);
        SqlDataAdapter adCategory = new SqlDataAdapter("SELECT * FROM TB_Category WHERE nType = 2", Connection);

        try
        {
            Connection.Open();
            adKeywords.Fill(ds, "TB_Keyword");
            adCategory.Fill(ds, "TB_Category");
            System.Data.DataTable dtKeyword = ds.Tables["TB_Keyword"];
            System.Data.DataTable dtCategory = ds.Tables["TB_Category"];
            DataColumn colParent = dtCategory.Columns["nId"];
            DataColumn colChild = dtKeyword.Columns["nCategory"];
            ds.Relations.Add("Category_Keyword", colParent, colChild);
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
        return ds;
    }


    //获得所有敏感信息关键字（不包括类别名称）
    public DataSet getKeyUsers()
    {
        return CreateDataSet("SELECT nId, vKey, nCategory FROM TB_Keyword");
    }

    public DataSet getKeyUserByID(string nId)
    {
        return CreateDataSet("SELECT nId, vKey, nCategory FROM TB_Keyword WHERE nId = " + nId);
    }

    public void DeleteKeyUser(string nId)
    {
        ExecuteScalar("DELETE TB_Keyword WHERE nId = " + nId);
    }

    public string InsertKeyUser(string vKey, string nCategory, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vKey == "")
            err = "必须输入敏感信息关键字！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_Keyword WHERE vKey = '" + vKey + "'";
            if (ExecuteScalar(sql) != "0") err = "该关键字已存在！";
            //if (err == "")
            //{
            //    sql = "SELECT COUNT(*) FROM TB_GarbageKey WHERE vKey = '" + vKey + "'";
            //    if (ExecuteScalar(sql) != "0") err = "该关键字在垃圾邮件信息库中已存在！";
            //}
        }
        if (err == "")
        {
            sql = "INSERT INTO TB_Keyword(vKey, nCategory, vUpdateUser, dUpdateDate) VALUES(";
            sql += "'" + vKey + "',";
            sql += nCategory + ",";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "')";
            ExecuteScalar(sql);
        }
        return err;
    }
    public string UpdateKeyUser(string nId, string vKey, string nCategory, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vKey == "")
            err = "必须输入敏感信息关键字！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_Keyword WHERE vKey = '" + vKey + "'";
            sql += " AND nId <> " + nId;
            if (ExecuteScalar(sql) != "0") err = "该关键字已存在！";
            //if (err == "")
            //{
            //    sql = "SELECT COUNT(*) FROM TB_Keyword WHERE vKey = '" + vKey + "'";
            //    if (ExecuteScalar(sql) != "0") err = "该关键字在垃圾邮件信息库中已存在！";
            //}
        }
        if (err == "")
        {
            sql = "UPDATE TB_Keyword SET ";
            sql += "vKey = '" + vKey + "',";
            sql += "nCategory =" + nCategory + ",";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "' ";
            sql += "WHERE nId = " + nId;
            ExecuteScalar(sql);
        }
        return err;
    }

    //获得所有数据查询系统地址
    public DataSet getAppUrls()
    {
        return CreateDataSet("SELECT nId, vAppName, vUrl FROM TB_AppUrl");
    }
    public DataSet getAppUrl(string nId)
    {
        return CreateDataSet("SELECT nId, vAppName, vUrl FROM TB_AppUrl WHERE nId = " + nId);
    }
    public void DeleteAppUrl(string nId)
    {
        ExecuteScalar("DELETE TB_AppUrl WHERE nId = " + nId);
    }
    public string InsertAppUrl(string vAppName, string vUrl, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vAppName == "")
            err = "必须输入数据管理中心名称！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_AppUrl WHERE vAppName = '" + vAppName + "'";
            if (ExecuteScalar(sql) != "0") err = "该名称已存在！";
        }
        if (vUrl == "")
            err = "必须输入数据管理中心链接地址！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_AppUrl WHERE vUrl = '" + vUrl + "'";
            if (ExecuteScalar(sql) != "0") err = "该地址已存在！";
        }
        if (err == "")
        {
            sql = "INSERT INTO TB_AppUrl(vAppName, vUrl, vUpdateUser, dUpdateDate) VALUES(";
            sql += "'" + vAppName + "',";
            sql += "'" + vUrl + "',";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "')";
            ExecuteScalar(sql);
        }
        return err;
    }
    public string UpdateAppUrl(string nId, string vAppName, string vUrl, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vAppName == "")
            err = "必须输入数据管理中心名称！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_AppUrl WHERE nId <> " + nId + " AND vAppName = '" + vAppName + "'";
            if (ExecuteScalar(sql) != "0") err = "该名称已存在！";
        }
        if (vUrl == "")
            err = "必须输入数据管理中心链接地址！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_AppUrl WHERE nId <> " + nId + " AND vUrl = '" + vUrl + "'";
            if (ExecuteScalar(sql) != "0") err = "该地址已存在！";
        }
        if (err == "")
        {
            sql = "UPDATE TB_AppUrl SET ";
            sql += "vAppName = '" + vAppName + "',";
            sql += "vUrl = '" + vUrl + "',";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "' ";
            sql += "WHERE nId = " + nId;
            ExecuteScalar(sql);
        }
        return err;
    }

    //垃圾邮件关键字
    //获得所有垃圾邮件关键字（包括类别名称）
    public DataSet getAllGarbageKeys()
    {
        DataSet ds = new DataSet();
        SqlDataAdapter adGarbageKeys = new SqlDataAdapter("SELECT * FROM TB_GarbageKey", Connection);
        SqlDataAdapter adCategory = new SqlDataAdapter("SELECT * FROM TB_Category WHERE nType = 1", Connection);

        try
        {
            Connection.Open();
            adGarbageKeys.Fill(ds, "TB_GarbageKey");
            adCategory.Fill(ds, "TB_Category");
            System.Data.DataTable dtGarbageKey = ds.Tables["TB_GarbageKey"];
            System.Data.DataTable dtCategory = ds.Tables["TB_Category"];
            DataColumn colParent = dtCategory.Columns["nId"];
            DataColumn colChild = dtGarbageKey.Columns["nCategory"];
            ds.Relations.Add("Category_GarbageKey", colParent, colChild);
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
        return ds;
    }
    //获得所有垃圾邮件关键字（不包括类别名称）
    public DataSet getGarbageMails()
    {
        return CreateDataSet("SELECT nId, vKey, nCategory FROM TB_GarbageKey");
    }

    //垃圾邮件关键字
    public DataSet getGarbageMails(string nCategory)
    {
        string sql = "SELECT nId, vKey, nCategory FROM TB_GarbageKey";
        if (nCategory != "0")
            sql += " WHERE nCategory = " + nCategory;
        return CreateDataSet(sql);
    }
    public DataSet getGarbageMailByID(string nId)
    {
        return CreateDataSet("SELECT nId, vKey, nCategory FROM TB_GarbageKey WHERE nId = " + nId);
    }

    public void DeleteGarbageMail(string nId)
    {
        ExecuteScalar("DELETE TB_GarbageKey WHERE nId = " + nId);
    }
    public string InsertGarbageMail(string vKey, string nCategory, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vKey == "")
            err = "必须输入垃圾邮件关键字！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_GarbageKey WHERE vKey = '" + vKey + "'";
            if (ExecuteScalar(sql) != "0") err = "该关键字已存在！";
            //if (err == "")
            //{
            //    sql = "SELECT COUNT(*) FROM TB_Keyword WHERE vKey = '" + vKey + "'";
            //    if (ExecuteScalar(sql) != "0") err = "该关键字在敏感信息库中已存在！";
            //}
        }
        if (err == "")
        {
            sql = "INSERT INTO TB_GarbageKey(vKey, nCategory, vUpdateUser, dUpdateDate) VALUES(";
            sql += "'" + vKey + "',";
            sql += nCategory + ",";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "')";
            ExecuteScalar(sql);
        }
        return err;
    }

    public string UpdateGarbageMail(string nId, string vKey, string nCategory, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vKey == "")
            err = "必须输入垃圾邮件关键字！";
        else
        {
            sql = "SELECT COUNT(*) FROM TB_GarbageKey WHERE vKey = '" + vKey + "'";
            sql += " AND nId <> " + nId;
            if (ExecuteScalar(sql) != "0") err = "该关键字已存在！";
            //if (err == "")
            //{
            //    sql = "SELECT COUNT(*) FROM TB_Keyword WHERE vKey = '" + vKey + "'";
            //    if (ExecuteScalar(sql) != "0") err = "该关键字在敏感信息库中已存在！";
            //}
        }
        if (err == "")
        {
            sql = "UPDATE TB_GarbageKey SET ";
            sql += "vKey = '" + vKey + "',";
            sql += "nCategory =" + nCategory + ",";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "' ";
            sql += "WHERE nId = " + nId;
            ExecuteScalar(sql);
        }
        return err;
    }

    //关键字类别
    public string getCategoryID(string vCategory, string nType)
    {
        string sql = "SELECT nId FROM TB_Category WHERE vCategory = '" + vCategory + "' AND nType = " + nType;
        return ExecuteScalar(sql);
    }

    public string getCategoryID(string vCategory, string nType, string nId)
    {
        string sql = "SELECT COUNT(*) FROM TB_Category WHERE vCategory = '" + vCategory + "' AND nType = " + nType
            + " AND nId <> " + nId;
        return ExecuteScalar(sql);
    }

    public DataSet getCategories(string nType)
    {
        return CreateDataSet("SELECT nId, vCategory, vRemark FROM TB_Category WHERE nType = " + nType);
    }

    public DataSet listCategories(string nType)
    {
        return CreateDataSet("SELECT 0 AS nId, '所有' AS vCategory UNION SELECT nId, vCategory FROM TB_Category WHERE nType = " + nType);
    }

    public DataSet getCategoryByID(string nId)
    {
        return CreateDataSet("SELECT nId, vCategory, vRemark FROM TB_Category WHERE nId = " + nId);
    }

    public void DeleteCategory(string nId)
    {
        ExecuteScalar("DELETE TB_Category WHERE nId = " + nId);
    }

    public string InsertCategory(string vCategory, string vRemark, string nType, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vCategory == "")
        {
            err = "必须输入类别名称！";
        }
        else
        {
            if (getCategoryID(vCategory, nType) != "")
            {
                err = "该类别已存在！";
            }
        }
        if (err == "")
        {
            sql = "INSERT INTO TB_Category(vCategory, vRemark, nType, vUpdateUser, dUpdateDate) VALUES(";
            sql += "'" + vCategory + "',";
            sql += "'" + vRemark + "',";
            sql += nType + ",";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "')";
            ExecuteScalar(sql);
        }
        return err;
    }

    public string UpdateCategory(string nId, string vCategory, string vRemark, string nType, string vUpdateUser)
    {
        string err = "";
        string sql;
        if (vCategory == "")
            err = "必须输入类别名称！";
        else
        {
            if (getCategoryID(vCategory, nType, nId) != "") err = "该类别已存在！";
        }
        if (err == "")
        {
            sql = "UPDATE TB_Category SET ";
            sql += "vCategory = '" + vCategory + "',";
            sql += "vRemark = '" + vRemark + "',";
            sql += "nType =" + nType + ",";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "' ";
            sql += "WHERE nId = " + nId;
            ExecuteScalar(sql);
        }
        return err;
    }

    public bool CategoryIsUsed(string nId)
    {
        if (ExecuteScalar("SELECT COUNT(*) FROM TB_GarbageKey WHERE nCategory = " + nId).ToString() != "0")
            return true;
        if (ExecuteScalar("SELECT COUNT(*) FROM TB_Keyword WHERE nCategory = " + nId).ToString() != "0")
            return true;
        return false;
    }

    //用户
    public DataSet getUsers(string nId)
    {
        if (getUserPower(nId) == "-1")//管理员登录
        {
            return CreateDataSet("SELECT * FROM TS_Login");
        }
        else//一般用户登录
        {
            return CreateDataSet("SELECT * FROM TS_Login WHERE nId = " + nId);
        }
    }
    public DataSet getUsers()
    {
        return CreateDataSet("SELECT * FROM TS_Login");
    }

    public DataSet getAllUsers()
    {
        return CreateDataSet("SELECT 0 AS nid, '所有' AS vlogin UNION SELECT nId, vLogin FROM TS_Login ORDER BY nid");
    }

    //获取用户名
    public string getUserName(string nId)
    {
        return ExecuteScalar("SELECT vLogin FROM TS_Login WHERE nId = " + nId).ToString();
    }
    //获取用户权限
    public string getUserPower(string nId)
    {
        return ExecuteScalar("SELECT nPower FROM TS_Login WHERE nId = " + nId).ToString();
    }
    public DataSet getUserByID(string nId)
    {
        return CreateDataSet("SELECT * FROM TS_Login WHERE nId = " + nId);
    }
    //获取用户可操作的客户端
    public DataSet getHostsByUserID(string nId)
    {
        if (getUserPower(nId) == "-1")//管理员登录
        {
            return CreateDataSet("SELECT nId, vCorpName FROM TB_UserInfo ORDER BY nKeyId");
        }
        else//一般用户登录
        {
            return CreateDataSet("SELECT nId, vCorpName FROM TB_UserInfo WHERE (nId IN(SELECT nUserId FROM TS_LoginPower WHERE nLoginId = " + nId + ")) ORDER BY nKeyId");
        }
    }
    public string DeleteUser(string nId)
    {
        string err = "";
        //if (nId == "1")
        //    err = "admin用户是系统缺省用户，无法删除！";
        if (err == "")
        {
            if (getUserPower(nId) == "0")//一般用户时删除权限表中数据
                ExecuteScalar("DELETE TS_LoginPower WHERE nLoginId = " + nId);
            ExecuteScalar("DELETE TS_Login WHERE nId = " + nId);
        }
        return err;
    }
    public string InsertUser(string UserName, string Password, string PowerType, string Powers)
    {
        string err = "";
        string sql = "";
        if (UserName == "")
            err = "必须输入用户名！";
        if (Password == "")
            err = "必须输入密码！";
        if (ExecuteScalar("SELECT COUNT(*) FROM TS_Login WHERE vLogin = '" + UserName + "'").ToString() != "0")
            err = "该用户名已存在！";
        if (err == "")
        {
            sql = "INSERT INTO TS_Login(vLogin, vPsw, nPower) VALUES(";
            sql += "'" + UserName + "',";
            sql += "'" + common.md5(Password) + "',";
            sql += PowerType + ")";
            ExecuteScalar(sql);
            if (PowerType == "0")//一般用户时，保存权限表
            {
                char[] delimiterChars = { '|' };
                string[] aPower = Powers.Split(delimiterChars);
                string newID = ExecuteScalar("SELECT MAX(nId) AS nId FROM TS_Login");
                for (int i = 1; i < aPower.Length - 1; i++)
                {
                    sql = "INSERT INTO TS_LoginPower(nLoginId, nUserId, nPower) VALUES(";
                    sql += newID + "," + aPower[i].ToString() + ",1)";
                    ExecuteScalar(sql);
                }
            }
        }
        return err;
    }
    public string UpdateUser(string UserID, string UserName, string Password, string PowerType, string Powers)
    {
        string err = "";
        string sql = "";
        if (UserName == "")
            err = "必须输入用户名！";
        if (Password == "")
            err = "必须输入密码！";
        if (ExecuteScalar("SELECT COUNT(*) FROM TS_Login WHERE vLogin = '" + UserName + "' AND nId <> " + UserID).ToString() != "0")
            err = "该用户名已存在！";
        if (err == "")
        {
            sql = "UPDATE TS_Login SET ";
            sql += "vLogin='" + UserName + "',";
            sql += "vPsw='" + common.md5(Password) + "',";
            sql += "nPower=" + PowerType + " ";
            sql += "WHERE nId = " + UserID;
            ExecuteScalar(sql);
            ExecuteScalar("DELETE TS_LoginPower WHERE nLoginId = " + UserID);//删除旧的权限
            if (PowerType == "0")//一般用户时，保存权限表
            {
                char[] delimiterChars = { '|' };
                string[] aPower = Powers.Split(delimiterChars);
                for (int i = 1; i < aPower.Length - 1; i++)
                {
                    sql = "INSERT INTO TS_LoginPower(nLoginId, nUserId, nPower) VALUES(";
                    sql += UserID + "," + aPower[i].ToString() + ",1)";
                    ExecuteScalar(sql);
                }
            }
        }
        return err;
    }

    /// <summary>
    /// 根据当前操作员获取可操作的站点，即控制中心下面要显示的单位
    /// </summary>
    /// <param name="UserID">登陆的管理员ID</param>
    /// <returns>可操作的单位列表</returns>
    public DataSet getSites(string UserID)
    {
        string sql;
        if (getUserPower(UserID) == "-1")//当前操作员是管理员身份
            sql = @"SELECT [nKeyId]
      ,[nId]
      ,[vCorpName]
      ,[vFilePath]
      ,[vMark]
      ,[vUpdateUser]
      ,[dUpdateDate]
      ,[vFlag]
      ,[vType]
      ,[vActive]
      ,[vVersion]
      ,[vSystemType]
      ,[vAttPath]
      ,dmc_config.dbo.ConvertNumToDate([VActiveDate]) AS VActiveDate FROM TB_UserInfo ORDER BY nKeyId";
        else
            sql = @"SELECT [nKeyId]
      ,[nId]
      ,[vCorpName]
      ,[vFilePath]
      ,[vMark]
      ,[vUpdateUser]
      ,[dUpdateDate]
      ,[vFlag]
      ,[vType]
      ,[vActive]
      ,[vVersion]
      ,[vSystemType]
      ,[vAttPath]
      ,dmc_config.dbo.ConvertNumToDate([VActiveDate]) AS VActiveDate FROM TB_UserInfo WHERE nId in (SELECT nUserID FROM TS_LoginPower WHERE nLoginId = " + UserID + ") ORDER BY nKeyId";
        return CreateDataSet(sql);
    }

    /// <summary>
    /// 更新当前结点的状态
    /// </summary>
    /// <param name="nID">结点ID</param>
    /// <param name="state">结点状态</param>
    public void UpdateActiveStatus(string nID, int state)
    {
        string sql =  "UPDATE [dmc_config].[dbo].[TB_UserInfo] SET vActive = {0} WHERE nId = '{1}'";
        sql = string.Format(sql, state, nID);
        ExecuteScalarRInt(sql);
    }

    #region 白名单是否入库

    /// <summary>
    /// 获取白名单是否入库列表
    /// </summary>
    /// <returns>名单列表</returns>
    public DataSet GetWhiteInList()
    {
        return CreateDataSet("SELECT * FROM TS_WhitePower");
    }

    /// <summary>
    /// 添加一条白名单是否入库
    /// </summary>
    /// <param name="vFlag"> 标志值  </param>
    /// <param name="vMark">说明
    /// </param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddWhiteIn(int vFlag, string vMark)
    {
        string sql = "INSERT INTO TS_WhitePower(vFlag,vMark)values(" + vFlag + ",'" + vMark + "')";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条白名单是否入库
    /// </summary>
    /// <param name="vFlag"> 标志值  </param>
    /// <param name="vMark">说明
    /// </param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateWhiteIn(int id, int vFlag, string vMark)
    {
        string sql = "UPDATE TS_WhitePower set vFlag=" + vFlag + ",vMark='" + vMark + "' WHERE nID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除白名单是否入库
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelWhiteIn(int id)
    {
        string sql = "DELETE FROM TS_WhitePower WHERE nID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }
    #endregion

    #region 操作DNS黑名单
    /// <summary>
    /// 根据域名值返回DNS对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataSet GetBlackWhiteModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_DnsList WHERE vName='" + name + "'");
    }
    /// <summary>
    /// 获取白黑名单列表
    /// </summary>
    /// <param name="type">
    /// 0黑名单
    /// 1动态域名
    /// 2白名单
    /// </param>
    /// <returns>名单列表</returns>
    public DataSet GetBlackWhiteList(int type)
    {
        return CreateDataSet("SELECT * FROM TS_DnsList WHERE vLevel=" + type);
    }

    /// <summary>
    /// 查询黑白名单
    /// </summary>
    /// <param name="vName">模糊查询的域名名称</param>
    /// <param name="type">要查询的类型，0 则表示所有</param>
    /// <returns>查询到的名单列表</returns>
    public DataSet GetBlackWhiteList(string vName, int type)
    {
        string sql = "SELECT * FROM TS_DnsList WHERE 1=1 ";

        if (vName != null && vName.Trim() != "")
        {
            sql = sql + " and vName like '%" + vName + "%' ";
        }

        if (type > 0)
        {
            sql = sql + " and vLevel=" + type;
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    /// 获取白黑名单
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>名单列表</returns>
    public DataSet GetBlackWhiteModel(int id)
    {
        return CreateDataSet("SELECT * FROM TS_DnsList WHERE nid=" + id);
    }

    /// <summary>
    /// 添加一条名单(黑白或动态)
    /// </summary>
    /// <param name="vName">名单名称</param>
    /// <param name="vLevel">   
    /// 0黑名单
    /// 1动态域名
    /// 2白名单
    /// </param>
    /// <param name="bType">
    /// 如果是黑名单，则有三种值：
    /// 0普通黑名单
    /// 1重要黑名单
    /// 2紧急黑名单
    /// </param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddBlackAndWhite(string vName, int vLevel, int type, string vContent)
    {
        string sql = "INSERT INTO TS_DnsList(vName,vLevel,vType,vContent)values('" + vName + "'," + vLevel + "," + type + ",'" + vContent + "')";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条名单(黑白或动态)
    /// </summary>
    /// <param name="vName">名单名称</param>
    /// <param name="vLevel">   
    /// 0黑名单
    /// 1动态域名
    /// 2白名单
    /// </param>
    /// <param name="bType">
    /// 如果是黑名单，则有三种值：
    /// 0普通黑名单
    /// 1重要黑名单
    /// 2紧急黑名单
    /// </param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateBlackAndWhite(int id, string vName, int vLevel, int vType, string vContent)
    {
        string sql = "UPDATE TS_DnsList set vName='" + vName + "',vLevel=" + vLevel + ",vType=" + vType + ",vContent='" + vContent + "' WHERE nID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除黑白名单
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelBlackAndWhite(int id)
    {
        string sql = "DELETE FROM TS_DnsList WHERE nID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 根据类型删除黑白域名
    /// </summary>
    /// <param name="type">
    /// 0普通黑域名
    /// 1重要黑域名
    /// 2紧急黑域名
    /// 3白域名
    /// 4动态域名
    /// </param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelBlackAndWhite(string type)
    {
        string sql = "DELETE FROM TS_DnsList WHERE vLevel = " + type;
        int result = ExecuteScalarRInt(sql);
        return result > 0 ? true : false;
    }
    #endregion


    #region 操作DNS动态域名
    /// <summary>
    /// 根据域名值返回DNS动态域名对象
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public DataSet GetBlackWhiteDynamicModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_Dynamic WHERE dynamicname='" + name + "'");
    }

    /// <summary>
    /// 获取DNS动态域名
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>DNS动态域名</returns>
    public DataSet GetBlackWhiteDynamicModel(int id)
    {
        return CreateDataSet("SELECT * FROM TS_Dynamic WHERE id=" + id);
    }

    /// <summary>
    /// 获取DNS动态域名名单列表
    /// </summary>
    /// <returns>名单列表</returns>
    public DataSet GetBlackWhiteDynamicList()
    {
        return CreateDataSet("SELECT * FROM TS_Dynamic");
    }

    /// <summary>
    /// 添加一条DNS动态域名
    /// </summary>
    /// <param name="vName">名单名称</param>
    /// <param name="vContent">域名描述</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddBlackAndWhiteDynamic(string vName, string vContent)
    {
        string sql = "INSERT INTO TS_Dynamic(dynamicname,dynamicdesc)values('" + vName + "','" + vContent + "')";

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条DNS动态域名
    /// </summary>
    /// <param name="id">域名编号</param>
    /// <param name="vName">名单名称</param> 
    /// <param name="vContent">域名描述</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateBlackAndWhiteDynamic(int id, string vName, string vContent)
    {
        string sql = "UPDATE TS_Dynamic set dynamicname='" + vName + "',dynamicdesc='" + vContent + "' WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除DNS动态域名
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelBlackAndWhiteDynamic(int id)
    {
        string sql = "DELETE FROM TS_Dynamic WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }
    #endregion

    #region 操作木马特征表

    /// <summary>
    /// 返回所有木马
    /// </summary>
    /// <returns>返回所有木马</returns>
    public DataSet GetHorseList()
    {
        return CreateDataSet("SELECT * FROM TS_TrojanList");
    }

    /// <summary>
    /// 根据木马名返回木马记录
    /// </summary>
    /// <param name="name">木马名</param>
    /// <returns>木马记录</returns>
    public DataSet GetHorseModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_TrojanList WHERE vName='" + name + "'");
    }

    /// <summary>
    /// 获取木马
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>木马列表</returns>
    public DataSet GetHorseModel(int id)
    {
        return CreateDataSet("SELECT * FROM TS_TrojanList WHERE id=" + id);
    }

    /// <summary>
    /// 查询木马
    /// </summary>
    /// <param name="vName">模糊查询的木马名称</param>
    /// <returns>查询到的木马列表</returns>
    public DataSet GetHorseList(string vName)
    {
        string sql = "SELECT * FROM TS_TrojanList WHERE 1=1 ";

        if (vName != null && vName.Trim() != "")
        {
            sql = sql + " and vName like '%" + vName + "%' ";
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    /// 添加一条木马
    /// </summary>    
    /// <param name="hName">木马名称</param>
    /// <param name="hContent">木马说明</param>
    /// <param name="hKey">木马特征码</param>
    /// <param name="hPort">木马端口列表</param>
    /// <param name="hProtocl">所使用的协,0 所有，1 TCP，2 UDP，3 其它</param>
    /// <param name="vFlag">木马方向 ；0出 1进 2双向</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddHorseModel(string hName, string hKey, int hProtocl, string hPort, int vFlag, string hContent)
    {
        string sql = "INSERT INTO TS_TrojanList(vName,vKey,vProtocl,vPort,vFlag,vContent)values('" + hName + "','" + hKey + "'," + hProtocl + ",'" + hPort + "'," + vFlag + ",'" + hContent + "')";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条木马
    /// </summary>
    /// <param name="id">要更新的记录ID</param>
    /// <param name="hName">木马名称</param>
    /// <param name="hContent">木马说明</param>
    /// <param name="hKey">木马特征码</param>
    /// <param name="hPort">木马端口列表</param>
    /// <param name="hProtocl">所使用的协,0 所有，1 TCP，2 UDP，3 其它</param>
    /// <param name="vFlag">木马方向 ；0出 1进 2双向</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateHorseModel(int id, string hName, string hKey, int hProtocl, string hPort, int vFlag, string hContent)
    {
        string sql = "UPDATE TS_TrojanList set vName='" + hName + "',vKey='" + hKey + "',vProtocl=" + hProtocl + ",vFlag=" + vFlag + ",vPort='" + hPort + "',vContent='" + hContent + "' WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除木马
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelHorseModel(int id)
    {
        string sql = "DELETE FROM TS_TrojanList WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }
    #endregion

    #region 操作黑白IP表

    /// <summary>
    /// 根据黑白IP名返回黑白IP记录
    /// </summary>
    /// <param name="name">黑白IP</param>
    /// <returns>黑白IP</returns>
    public DataSet GetIPModel(string name)
    {
        return CreateDataSet("SELECT " + TS_BWIPLIST_FIELDS + " FROM TS_BWIPList WHERE vName=dmc_config.dbo.[f_IP2int]('" + name + "')");
    }

    /// <summary>
    /// 获取黑白IP
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>黑白IP列表</returns>
    public DataSet GetIPModel(int id)
    {
        return CreateDataSet("SELECT " + TS_BWIPLIST_FIELDS + " FROM TS_BWIPList WHERE id=" + id);
    }

    /// <summary>
    /// 查询黑白IP
    /// </summary>
    /// <param name="vName">模糊查询的黑白IP名称</param>
    /// <returns>查询到的黑白IP列表</returns>
    public DataSet GetIPList(string vName)
    {
        string sql = "SELECT " + TS_BWIPLIST_FIELDS + " FROM TS_BWIPList WHERE 1=1 ";

        if (vName != null && vName.Trim() != "")
        {
            sql = sql + " and dmc_config.dbo.[f_Int2IP](vName) like '%" + vName + "%' ";
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    /// 添加一条黑白IP
    /// </summary>    
    /// <param name="pName">黑白IP地址</param>
    /// <param name="pType">黑白IP类型 0,黑IP; 1,白IP</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddIPModel(string pName, int pType)
    {
        string sql = "INSERT INTO TS_BWIPList(vName,vType)values(dmc_config.dbo.[f_IP2int]('" + pName + "')," + pType + ")";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条黑白IP
    /// </summary>
    /// <param name="id">要更新的记录ID</param>
    /// <param name="pName">黑白IP地址</param>
    /// <param name="pType">黑白IP类型 0,黑IP; 1,白IP</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateIPModel(int id, string pName, int pType)
    {
        string sql = "UPDATE TS_BWIPList set vName=dmc_config.dbo.[f_IP2int]('" + pName + "'),vType=" + pType + " WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除黑白IP
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelIPModel(int id)
    {
        string sql = "DELETE FROM TS_BWIPList WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 根据类型删除黑白IP
    /// </summary>
    /// <param name="type">类型 0,黑名单;1,白名单</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelIPByType(int type)
    {
        string sql = "DELETE FROM TS_BWIPList WHERE vType = " + type;
        int result = ExecuteScalarRInt(sql);
        return result > 0 ? true : false;
    }

    /// <summary>
    /// 添加一条HTTP黑名单
    /// </summary>    
    /// <param name="pName">HTTP黑名单地址</param>
    /// <param name="pType">HTTP黑名单类型 0,黑IP; 1,白IP</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddHTTPModel(string pName, int pType)
    {
        string sql = "INSERT INTO TS_BWHttpList(vName,vType)values('" + pName + "' ," + pType + ")";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条HTTP黑名单
    /// </summary>
    /// <param name="id">要更新的记录ID</param>
    /// <param name="pName">HTTP黑名单地址</param>
    /// <param name="pType">HTTP黑名单类型 0,黑IP; 1,白IP</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateHTTPModel(int id, string pName, int pType)
    {
        string sql = "UPDATE TS_BWHttpList set vName='" + pName + "' ,vType=" + pType + " WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除黑白IP
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelHTTPModel(int id)
    {
        string sql = "DELETE FROM TS_BWHttpList WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 根据类型删除黑白IP
    /// </summary>
    /// <param name="type">类型 0,黑名单;1,白名单</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelHTTPByType(int type)
    {
        string sql = "DELETE FROM TS_BWHttpList WHERE vType = " + type;
        int result = ExecuteScalarRInt(sql);
        return result > 0 ? true : false;
    }

    /// <summary>
    /// 获取HTTP黑名单
    /// </summary>
    /// <param name="name">HTTP </param>
    /// <returns>HTTP黑名单列表</returns>
    public DataSet GetHTTPModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_BWHttpList WHERE vName='" + name+"'");
    }

    /// <summary>
    /// 获取HTTP黑名单
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>HTTP黑名单列表</returns>
    public DataSet GetHTTPModel(int id)
    {
        return CreateDataSet("SELECT  * FROM TS_BWHttpList WHERE id=" + id);
    }
    #endregion

    #region 操作黑白Mac表

    /// <summary>
    /// 根据黑白mac名返回黑白mac记录
    /// </summary>
    /// <param name="name">黑白mac</param>
    /// <returns>黑白mac</returns>
    public DataSet GetMacModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_BWMacList WHERE vName='" + name + "'");
    }

    /// <summary>
    /// 获取黑白mac
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>黑白mac列表</returns>
    public DataSet GetMacModel(int id)
    {
        return CreateDataSet("SELECT * FROM TS_BWMacList WHERE id=" + id);
    }

    /// <summary>
    /// 查询黑白mac
    /// </summary>
    /// <param name="vName">模糊查询的黑白mac名称</param>
    /// <returns>查询到的黑白mac列表</returns>
    public DataSet GetMacList(string vName)
    {
        string sql = "SELECT * FROM TS_BWMacList WHERE 1=1 ";

        if (vName != null && vName.Trim() != "")
        {
            sql = sql + " and vName like '%" + vName + "%' ";
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    /// 添加一条黑白mac
    /// </summary>    
    /// <param name="pName">黑白mac地址</param>
    /// <param name="pType">黑白mac类型 0,黑mac; 1,白mac</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddMacModel(string pName, int pType)
    {
        string sql = "INSERT INTO TS_BWMacList(vName,vType)values('" + pName + "'," + pType + ")";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条黑白mac
    /// </summary>
    /// <param name="id">要更新的记录ID</param>
    /// <param name="pName">黑白mac地址</param>
    /// <param name="pType">黑白mac类型 0,黑mac; 1,白mac</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateMacModel(int id, string pName, int pType)
    {
        string sql = "UPDATE TS_BWMacList set vName='" + pName + "',vType=" + pType + " WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除黑白mac
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelMacModel(int id)
    {
        string sql = "DELETE FROM TS_BWMacList WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 根据类型删除黑白mac
    /// </summary>
    /// <param name="type">类型 0,黑名单;1,白名单</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelMacByType(int type)
    {
        string sql = "DELETE FROM TS_BWMacList WHERE vType = " + type;
        int result = ExecuteScalarRInt(sql);
        return result > 0 ? true : false;
    }
    #endregion

    #region 操作黑白Email表

    /// <summary>
    /// 根据黑白Email名返回黑白Email记录
    /// </summary>
    /// <param name="name">黑白Email</param>
    /// <returns>黑白Email</returns>
    public DataSet GetEmailModel(string name)
    {
        return CreateDataSet("SELECT * FROM TS_BWEmailList WHERE vName='" + name + "'");
    }

    /// <summary>
    /// 获取黑白Email
    /// </summary>
    /// <param name="id">记录ID </param>
    /// <returns>黑白Email列表</returns>
    public DataSet GetEmailModel(int id)
    {
        return CreateDataSet("SELECT * FROM TS_BWEmailList WHERE id=" + id);
    }

    /// <summary>
    /// 查询黑白Email
    /// </summary>
    /// <param name="vName">模糊查询的黑白Email名称</param>
    /// <returns>查询到的黑白Email列表</returns>
    public DataSet GetEmailList(string vName)
    {
        string sql = "SELECT * FROM TS_BWEmailList WHERE 1=1 ";

        if (vName != null && vName.Trim() != "")
        {
            sql = sql + " and vName like '%" + vName + "%' ";
        }

        return CreateDataSet(sql);
    }

    /// <summary>
    /// 添加一条黑白Email
    /// </summary>    
    /// <param name="pName">黑白Email地址</param>
    /// <param name="pType">黑白Email类型 0,黑Email; 1,白Email</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool AddEmailModel(string pName, int pType)
    {
        string sql = "INSERT INTO TS_BWEmailList(vName,vType)values('" + pName + "'," + pType + ")";

        int result = ExecuteScalarRInt(sql);

        return Convert.ToInt32(result) > 0 ? true : false;
    }

    /// <summary>
    /// 更新一条黑白Email
    /// </summary>
    /// <param name="id">要更新的记录ID</param>
    /// <param name="pName">黑白Email地址</param>
    /// <param name="pType">黑白Email类型 0,黑Email; 1,白Email</param>
    /// <returns> true 成功,false 失败</returns>    
    public bool UpdateEmailModel(int id, string pName, int pType)
    {
        string sql = "UPDATE TS_BWEmailList set vName='" + pName + "',vType=" + pType + " WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 删除黑白Email
    /// </summary>
    /// <param name="id">要删除的ID</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelEmailModel(int id)
    {
        string sql = "DELETE FROM TS_BWEmailList WHERE ID=" + id;

        int result = ExecuteScalarRInt(sql);

        return result > 0 ? true : false;
    }

    /// <summary>
    /// 根据类型删除黑白Email
    /// </summary>
    /// <param name="type">类型 0,黑名单;1,白名单</param>
    /// <returns>true 成功,false 失败</returns>
    public bool DelEmailByType(int type)
    {
        string sql = "DELETE FROM TS_BWEmailList WHERE vType = " + type;
        int result = ExecuteScalarRInt(sql);
        return result > 0 ? true : false;
    }
    #endregion

    #region 管理员管理
    public DataSet getSiteByKeyID(string KeyID)
    {
        return CreateDataSet("SELECT * FROM TB_UserInfo WHERE nKeyId = " + KeyID);
    }
    public string getSiteName(string nId)
    {
        return ExecuteScalar("SELECT vCorpName FROM TB_UserInfo WHERE nId = " + nId);
    }
    public void DeleteSite(string KeyID)
    {
        ExecuteScalar("DELETE TB_UserInfo WHERE nKeyId = " + KeyID);
    }

    public string InsertSite(string vUpdateUser)
    {
        string err = "";
        string sql = "";
        string nId = "1";
        string MaxID = ExecuteScalar("SELECT MAX(nId) FROM TB_UserInfo");
        if (MaxID != "")
            nId = Convert.ToString(Convert.ToInt32(MaxID) + 1);
        string vCorpName = "site" + nId;
        string vFilePath = "\\site" + nId;
        if (err == "")
        {
            sql = "INSERT INTO TB_UserInfo(nId, vCorpName, vFilePath, vUpdateUser, dUpdateDate) VALUES(";
            sql += nId + ",";
            sql += "'" + vCorpName + "',";
            sql += "'" + vFilePath + "',";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "')";
            ExecuteScalar(sql);
        }
        return err;
    }

    public string InsertSite(string nId, string vCorpName, string vFilePath, string vUpdateUser, int flag, int vType, string vAttPath)
    {
        string err = "";
        if (!common.isInt(nId))
        {
            err = "编号必须为正整数！";
            return err;
        }
        else
        {
            if (Convert.ToInt16(ExecuteScalar("select count(*) from TB_UserInfo where nId = " + nId)) > 0)
            {
                err = "该编号已经存在！";
                return err;
            }
        }
        if (vFilePath == "")
        {
            err = "附件目录不能为空！";
            return err;
        }
        if (vCorpName == "")
        {
            err = "单位名称不能为空！";
            return err;
        }
        if (err == "")
        {
            string sql = "INSERT INTO TB_UserInfo(nId, vCorpName, vFilePath, vUpdateUser, dUpdateDate,vFlag,vType,vAttPath) VALUES(";
            sql += nId + ",";
            sql += "'" + vCorpName + "',";
            sql += "'" + vFilePath + "',";
            sql += "'" + vUpdateUser + "',";
            sql += "'" + System.DateTime.Now.ToString() + "',";
            sql += flag + ",";
            sql += vType + ",'";
            sql += vAttPath + "')";
            ExecuteScalar(sql);
        }
        return err;
    }


    public string UpdateSite(string nKeyId, string nId, string vCorpName, string vFilePath, string vUpdateUser, int flag, int vType)
    {
        string err = "";
        if (ExecuteScalar("select nId from TB_UserInfo where nKeyId = " + nKeyId) != nId)//如果nId发生变化
        {
            if (!common.isInt(nId))
            {
                err = "编号必须为正整数！";
                return err;
            }
            else
            {
                if (Convert.ToInt16(ExecuteScalar("select count(*) from TB_UserInfo where nId = " + nId)) > 0)
                {
                    err = "该编号已经存在！";
                    return err;
                }
            }
        }
        if (vFilePath == "")
        {
            err = "附件目录不能为空！";
            return err;
        }
        if (vCorpName == "")
        {
            err = "单位名称不能为空！";
            return err;
        }
        if (err == "")
        {
            string sql = "UPDATE TB_UserInfo SET ";
            sql += "nId =" + nId + ",";
            sql += "vCorpName ='" + vCorpName + "',";
            sql += "vFilePath ='" + vFilePath + "',";
            sql += "vUpdateUser = '" + vUpdateUser + "',";
            sql += "dUpdateDate = '" + System.DateTime.Now.ToString() + "', ";
            sql += "vFlag =" + flag + ",";
            sql += " vType = " + vType + " ";
            sql += "WHERE nKeyId = " + nKeyId;
            ExecuteScalar(sql);
        }
        return err;
    }
    //根据用户ID更改其在线状态
    public void changeuserstate(string UserID, string StateType, string UserIP)
    {

        string sql;
        if (StateType == "0")//退出登录
            sql = "UPDATE TS_Login SET IsLogin=0, LoginIP='" + UserIP + "' where nId=" + UserID;
        else
            sql = "UPDATE TS_Login SET IsLogin=1, LoginIP='" + UserIP + "' where nId=" + UserID;
        ExecuteScalar(sql);

    }
    #endregion

    #region 日志管理
    //新增操作日志
    public string InsertLog(string UserID, string UserName, string IP, string Content)
    {
        string err = "";
        string sql = "";
        if (err == "")
        {
            sql = "INSERT INTO TS_Log(nUserID,vUser,vIP,vContent) VALUES(";
            sql += UserID + ",";
            sql += "'" + UserName + "',";
            sql += "'" + IP + "',";
            sql += "'" + Content + "')";
            ExecuteScalar(sql);
        }
        return err;
    }
    //删除日志
    public string DeleteLog(string nId)
    {
        string err = "";
        if (err == "")
        {
            ExecuteScalar("DELETE TS_Log WHERE nId = " + nId);
        }
        return err;
    }
    //根据条件查询日志
    public DataSet getLogs(string sCondition)
    {
        string sql = "SELECT * FROM TS_Log";
        if (sCondition != "") sql += " WHERE 1=1 " + sCondition;
        return CreateDataSet(sql);
    }
    #endregion

    #region 自动维护设置参数
    //获取自动维护设置参数
    public DataSet GetPeriods(string vTable)
    {
        return CreateDataSet("SELECT * FROM TS_Periods WHERE vTable = '" + vTable + "'");

    }

    //更新自动维护设置参数
    public string UpdatePeriods(string vTable, string vUnit, string nCounter)
    {
        string err = "";
        if (err == "")
        {
            string sql = "UPDATE TS_Periods SET ";
            sql += "vUnit = '" + vUnit + "',";
            sql += "nCounter = " + nCounter;
            sql += " WHERE vTable = '" + vTable + "'";
            ExecuteScalar(sql);
        }
        return err;
    }
    #endregion

    #region 查询IP所在地址
    /// <summary>
    /// 查询IP所在地址
    /// </summary>
    /// <param name="ip">要查询的IP地址</param>
    /// <returns>得到的所在地地址</returns>
    public string GetIpArea(string ip)
    {
        string sql = "select addrinfo as area from TS_CountryCityIP where (dbo.f_IP2Int('" + ip + "') between ip1 and ip2)";
        DataSet ds = CreateDataSet(sql);
        if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 1)
            return "";
        return ds.Tables[0].Rows[0]["area"].ToString();
    }

    /// <summary>
    /// 查询IP所在地址
    /// </summary>
    /// <param name="ip">要查询的IP地址</param>
    /// <returns>得到的所在地地址</returns>
    public string GetIpArea(long ip)
    {
        string sql = "select addrinfo as area from TS_CountryCityIP where ( ip between ip1 and ip2)";
        DataSet ds = CreateDataSet(sql);
        if (ds == null || ds.Tables.Count < 0 || ds.Tables[0] == null || ds.Tables[0].Rows.Count < 1)
            return "";
        return ds.Tables[0].Rows[0]["area"].ToString();
    }
    #endregion

    //保存属性值
    public void UpdateProperty(string Property, string Value)
    {
        string sql = "UPDATE TS_Property SET ";
        sql += "vValue = '" + Value + "' ";
        sql += "WHERE vProperty = '" + Property + "'";
        ExecuteScalar(sql);
    }



    //根据关键字表比对指定所有表中数据
    public int CheckData(int IsGarbage, string sKey, string sDate, string eDate)
    {
        Int32 recCount = 0;
        SqlCommand cmd = new SqlCommand("sp_chk_all", Connection);
        cmd.CommandTimeout = 20000;
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IsGarbage", SqlDbType.Bit));
        cmd.Parameters.Add(new SqlParameter("@Key", SqlDbType.VarChar));
        cmd.Parameters.Add(new SqlParameter("@sDate", SqlDbType.DateTime));
        cmd.Parameters.Add(new SqlParameter("@eDate", SqlDbType.DateTime));
        cmd.Parameters["@IsGarbage"].Value = IsGarbage;
        if (sKey == "")
            cmd.Parameters["@Key"].Value = DBNull.Value;
        else
            cmd.Parameters["@Key"].Value = sKey;
        if (sDate == "")
            cmd.Parameters["@sDate"].Value = DBNull.Value;
        else
            cmd.Parameters["@sDate"].Value = Convert.ToDateTime(sDate);
        if (eDate == "")
            cmd.Parameters["@eDate"].Value = DBNull.Value;
        else
            cmd.Parameters["@eDate"].Value = Convert.ToDateTime(eDate);
        cmd.CommandTimeout = 300;
        cmd.Connection.Open();
        try
        {
            recCount = (Int32)cmd.ExecuteScalar();
        }
        catch
        {
            throw;
        }
        finally
        {
            Connection.Close();
        }
        return (int)recCount;
    }
}

/// <summary>
/// 从database类派生出来的dbHttpAll类，用于访问dms_http_all数据库
/// </summary>
public class dbHttpAll : database
{
    private bool isDisposed = false;
    public dbHttpAll()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["HttpAllConnStr"].ConnectionString);
        TablePrefix = "TC_Http_";
        TempTablePrefix = "Temp_Http_";
        TableFields = "nState,nId as ID,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac, dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,vSiteName,vURL,nParent,nKey";
        TempFields = "nState,vTime as ID,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,vSiteName,vURL,nParent,nKey";
    }
    ~dbHttpAll()
    {
        Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbInfoAll类，用于访问dms_info_all数据库
/// </summary>
public class dbInfoAll : database
{
    private bool isDisposed = false;
    public dbInfoAll(string InfoType)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoAllConnStr"].ConnectionString);
        TablePrefix = "TC_" + InfoType + "_";
        TempTablePrefix = "Temp_" + InfoType + "_";
        TableFields = "nState,nId as ID,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,vLogin,vPwd,vsitename,nParent,vMethod,nKey";
        TempFields = "nState,vTime as ID,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vSrcMac,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,vLogin,vPwd,vsitename,nParent,vMethod,nKey";
    }
    ~dbInfoAll()
    {
        Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbMailBox类，用于访问dms_mail_box数据库
/// </summary>
public class dbMailBox : database
{
    private bool isDisposed = false;
    public dbMailBox(string MailType)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        // MailTyp分"Pop"、"Smtp" 
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MailBoxConnStr"].ConnectionString);
        TablePrefix = "TC_" + MailType + "Mail_";
        TempTablePrefix = "Temp_" + MailType + "Mail_";
        TableFields = "nState,nId as ID,nAttach,vLpTitle,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vLpFrom,vLpTo,vLocalFile,nParent,nKey";
        TempFields = "nState,vTime as ID,nAttach,vLpTitle,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vLpFrom,vLpTo,vLocalFile,nParent,nKey";
    }
    ~dbMailBox()
    {
        Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbMailSite类，用于访问dms_mail_site数据库
/// </summary>
public class dbMailSite : database
{
    private bool isDisposed = false;
    public dbMailSite(string MailType)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MailSiteConnStr"].ConnectionString);
        TablePrefix = "TC_" + MailType + "WebMail_";
        TempTablePrefix = "Temp_" + MailType + "WebMail_";
        TableFields = "nState,nId as ID,nAttach,vLpTitle,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vLpFrom,vLpTo,vWebFile,vLocalFile,nParent,nKey";
        TempFields = "nState,vTime as ID,nAttach,vLpTitle,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,vLpFrom,vLpTo,vWebFile,vLocalFile,nParent,nKey";
    }
    ~dbMailSite()
    {
        Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}

/// <summary>
/// 从database类派生出来的dbMsgAll类，用于访问dms_msg_all数据库
/// </summary>
public class dbMsgAll : database
{
    private bool isDisposed = false;
    public dbMsgAll(string MsgType)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["MsgAllConnStr"].ConnectionString);
        TablePrefix = "TC_" + MsgType + "_";
        TempTablePrefix = "Temp_" + MsgType + "_";
        //dmc_config.dbo.[f_Int2IP](
        TableFields = "nState,vLocalFile,nId as ID,vMessage,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vMailFrom,vMailTo,nType,nParent,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,nKey";
        TempFields = "nState,vTime as ID,vMessage,dmc_config.dbo.ConvertNumToDate(dCapture)  as dCapture,vMailFrom,vMailTo,nType,nParent,dmc_config.dbo.[f_Int2IP](vSrcAddr) as vSrcAddr,dmc_config.dbo.[f_Int2IP](vDstAddr) as vDstAddr,nKey";
    }
    ~dbMsgAll()
    {
        Dispose(false);
    }
    protected override void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            // 那么这个方法是被客户直接调用的,那么托管的,和非托管的资源都可以释放 
            if (disposing)
            {
                // 释放 托管资源 
                //OtherManagedObject.Dispose();
            }
            //释放非托管资源 
            //DoUnManagedObjectDispose();

            base.Dispose(disposing);
            isDisposed = true;
        }
    }
}
