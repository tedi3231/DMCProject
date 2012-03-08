using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.IO;
using System.Text;

public partial class src_inputPass : System.Web.UI.Page
{

    private int type = 0;

    /// <summary>
    /// 要操作的表的字段名称
    /// </summary>
    private string field = "vName";

    /// <summary>
    /// 要操作的表的名称
    /// </summary>
    public string TableName
    { 
        set 
        {
            this.hdTbName.Value = value; 
        } 
        get
        {
            return this.hdTbName.Value;
        } 
    }

    dbConfig db = new dbConfig();

    

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(this.hdType.Value))
            {
                this.hdType.Value = Request.Params["type"];
            }

            initForm();

            this.divImport.Visible = false;
            this.passDiv.Visible = true;
        }
    }


    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btEnter_Click(object sender, EventArgs e)
    {
        string pass = TextBox1.Text.Trim();
        string name = Session["adminName"].ToString();

        pass = common.md5(pass);

        dbConfig db = new dbConfig();
        DataSet ds = db.getUsers(User.Identity.Name);

        bool isAdmin = false;

        if (ds != null && ds.Tables.Count > 0)
        {
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                if ( row[2].ToString() == pass && row[1].ToString()==name )
                {
                    isAdmin = true;
                    this.divImport.Visible = true;
                    this.passDiv.Visible = false;
                    break;
                }
            }
        }

        if (!isAdmin)
        {
            ltMsg.Text = "<script>alert('对不起，管理员密码不正确');</script>";
            return;
        }

        Session["passd"] = common.md5(TextBox1.Text.Trim());

        initForm();

        //Response.Redirect("importFileForBlack.aspx?type=" + this.hdType.Value);

        //ltMsg.Text = "<script>javascript:window.close();</script>";
    }

    /// <summary>
    /// 初始化窗体上的控件内容
    /// </summary>
    private void initForm()
    {
        #region  确认当前用户是否为管理员，并需要再次确认密码
 
        string type2 = this.hdType.Value;

        if (!string.IsNullOrEmpty(type2))
        {
            this.type = Convert.ToInt32(type2);
        }

        switch (type)
        {
            case 1:
                ltMsg.Text = "黑白IP";
                this.Title = "导入黑白IP";
                this.TableName = "TS_BWIPList";
                this.Title = "黑白IP导入导出";
                break;
            case 2:
                ltMsg.Text = "黑白MAC";
                this.Title = "导入黑白MAC";
                this.TableName = "TS_BWMacList";
                this.Title = "黑白MAC导入导出";
                break;
            case 3:
                ltMsg.Text = "黑白邮箱";
                this.Title = "导入黑白邮箱";
                this.TableName = "TS_BWEmailList";
                this.Title = "黑白邮箱导入导出";
                break;
            case 4:
            case 5:
                ltMsg.Text = "黑白域名";
                this.TableName = "TS_DnsList";
                this.Title = "黑白域名导入导出";

                this.DropDownList1.Items.Clear();
                this.DropDownList1.Items.Add(new ListItem("普通黑域名", "0"));
                this.DropDownList1.Items.Add(new ListItem("重要黑域名", "1"));
                this.DropDownList1.Items.Add(new ListItem("紧急黑域名", "2"));
                this.DropDownList1.Items.Add(new ListItem("白域名", "3"));
                this.DropDownList1.Items.Add(new ListItem("动态域名", "4"));

                break;

            default:
                break;
        }

        //string pass = Session["passd"] == null ? null : Session["passd"].ToString();

        //if (string.IsNullOrEmpty(pass))
        //{
        //    //Response.write("<script>javascript:window.close();</script>");
        //    //ltMsg.Text = "<script>alert('需要输入您的密码验证');window.open('inputPass.aspx?type=" + Request.QueryString["type"] + "');javascript:window.close();</script>";
        //    this.divImport.Visible = false;
        //    this.passDiv.Visible = true;
        //    return;
        //}

        Session["passd"] = null;
        #endregion
    }

    /// <summary>
    /// 导入方法
    /// </summary>
    private void import()
    {
        bool fileOK = false;
        string xmlPath = "";
        int count = 0;

        if (fileImport.HasFile)
        {
            string fileExtension = Path.GetExtension(fileImport.FileName).ToLower();
            if (fileExtension == ".txt")
                fileOK = true;
        }

        if (fileOK)
        {
            try
            {
                xmlPath = Server.MapPath("~/src/temp/") + this.TableName + "_" + Page.User.Identity.Name + ".txt";
                fileImport.PostedFile.SaveAs(xmlPath);
            }
            catch
            {
                xmlPath = "";
            }
        }
        else
        {
            Message.Show(this.Page, "请选择一个txt文件用于导入关键字！");
        }

        List<string> list = GetStirngListFromFile(xmlPath);

        if (list == null || list.Count <= 0)
        {
            return;
        }

        DataSet temp = null;



        string sql = string.Empty;

        foreach (string item in list)
        {
            #region 判断域名是否已经存在
            if (this.type == 4 || this.type == 5)
            {
                temp = db.CreateDataSet("SELECT * FROM TS_DnsList WHERE vName='" + item + "'");
            }
            else
            {
                temp = db.CreateDataSet("SELECT * FROM " + this.TableName + " WHERE " + this.field + "='" + item + "'");
            }
            #endregion

            if (temp != null && temp.Tables.Count > 0 && temp.Tables[0].Rows.Count > 0) //  说明此关键字已存在
            {
                continue;
            }

            if (this.type == 4 || this.type == 5) //黑白域名
            {
                sql = "insert into TS_DnsList(vName,vLevel,vType,vContent)values('" + item + "'," + Request.Form["DropDownList1"] + ",0,'" + "" + "')";
            }
            else
            {
                sql = "insert into " + this.TableName + "(vName,vType)values('" + item + "'," + Request.Form["DropDownList1"] + ")";
            }

            int result = db.ExecuteScalarRInt(sql);
            count++;

        }
        Message.Show(this.Page, "关键字导入成功，保留 " + count + " 个！");        
    }

    /// <summary>
    /// 导出方法
    /// </summary>
    private void export()
    {
        database db = new dbConfig();

        string xmlPath = Server.MapPath("~/src/temp/") + TableName + "_" + Page.User.Identity.Name + ".txt";

        DataSet ds = db.CreateDataSet("SELECT " + this.field + " FROM " + this.TableName);

        if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0) // 没有数据，直接放弃
        {
            return;
        }

        StringBuilder bul = new StringBuilder();

        foreach (DataRow row in ds.Tables[0].Rows)
        {
            if (this.type == 5)
            {
                bul.Append(row["dynamicname"].ToString());
            }
            else
            {
                bul.Append(row["vName"].ToString());
            }
            bul.Append("\n");
        }

        if (WriteStringToFile(bul, xmlPath)) //写入成功则发到客户端
        {
            SendToClient(xmlPath);
        }
    }


    /// <summary>
    /// 将字符内容写到文件中去
    /// </summary>
    /// <param name="bul">要写的内容</param>
    /// <param name="path">文件路径</param>
    /// <returns>true 写成功,false 写失败</returns>
    private bool WriteStringToFile(StringBuilder bul, string path)
    {
        StreamWriter writer = null;
        try
        {
            writer = new StreamWriter(path, false);
            writer.Write(bul.ToString());
            return true;
        }
        catch (Exception)
        {
            return false;
        }
        finally
        {
            if (writer != null)
                writer.Close();
        }
    }

    /// <summary>
    /// 将文件中的数据按行读出放入列表中
    /// </summary>
    /// <param name="path">要读取的文件</param>
    /// <returns>格式文件得到的列表</returns>
    private List<string> GetStirngListFromFile(string path)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        List<string> list = new List<string>();

        StreamReader reader = null;

        try
        {
            reader = new StreamReader(path);
            string temp = string.Empty;

            while (!reader.EndOfStream)
            {
                temp = reader.ReadLine().Trim();

                if (this.type == 1 && !common.IsValidIp(temp)) //添加IP时检查IP的合法性
                    continue;

                if (this.type == 3 && !common.IsValidEmail(temp)) //添加邮箱时检查邮箱的合法性
                    continue;

                if (!list.Contains(temp))
                {
                    list.Add(temp);
                }
            }
        }
        catch (Exception)
        {
            return null;
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
        return list;
    }

    /// <summary>
    /// 导出时将文件发送到浏览器
    /// </summary>
    /// <param name="FileName"></param>
    private void SendToClient(string FileName)
    {
        System.IO.FileInfo file = new System.IO.FileInfo(FileName);
        Response.Clear();
        Response.Charset = "GB2312";
        Response.ContentEncoding = System.Text.Encoding.UTF8;
        // 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
        Response.AddHeader("Content-Disposition", "attachment; filename=" + Server.UrlEncode(file.Name));
        // 添加头信息，指定文件大小，让浏览器能够显示下载进度 
        Response.AddHeader("Content-Length", file.Length.ToString());
        // 指定返回的是一个不能被客户端读取的流，必须被下载 
        Response.ContentType = "application/xml";
        // 把文件流发送到客户端 
        Response.WriteFile(file.FullName);
        // 停止页面的执行 
        Response.End();
    }


    /// <summary>
    /// 导出
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        export();

        this.passDiv.Visible = true;
        this.divImport.Visible = false;
    }

    protected void btSave_Click(object sender, EventArgs e)
    {
        import();
        this.passDiv.Visible = true;
        this.divImport.Visible = false;
    }
}
