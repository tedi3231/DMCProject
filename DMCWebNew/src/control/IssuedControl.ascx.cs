using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class src_control_IssuedControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 要下载的数据类型
    /// </summary>
    public string DataType
    {
        get { return this.hdType.Value; }
        set { this.hdType.Value = value; }
    }

    protected int issued(int val)
    {
        int dataType = Convert.ToInt32(DataType);

        int result = 0;

        switch (dataType)
        {
            case 1:
                result = val | 1;
                break;
            case 2:
                result = val | 2;
                break;
            case 3:
                result = val | 4;
                break;
            case 4:
                result = val | 8;
                break;
            case 5:
                result = val | 16;
                break;
            case 6:
                result = val | 32;
                break;
            case 7:
                result = val | 64;
                break;           
        }

        return result;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string dataType = DataType;

        dbConfig db = new dbConfig();
        DataSet ds = db.CreateDataSet("SELECT vState FROM TS_ListUpdate");
        int val = 0;
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            val = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
        }

        string sql = "Update TS_ListUpdate SET vState = " + issued(val);        
        db.ExecuteScalar(sql);
    }
}