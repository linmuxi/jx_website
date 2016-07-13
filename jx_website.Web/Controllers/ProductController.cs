using jx_website.Web.BLL;
using jx_website.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace jx_website.Web.Controllers
{
    public class ProductController : Controller
    {

        ProductBLL productBLL = new ProductBLL();
        DepartmentBLL departmentBLL = new DepartmentBLL();

        // 产品首页
        public ActionResult Index()
        {
            Product product = new Product();

            // 所有产品
            ViewBag.ProductList = productBLL.queryProductList(product);

            // 热门产品
            product.isHot = "1";
            ViewBag.HotProductList = productBLL.queryProductList(product);

            return View();
        }

        // 单个产品明细
        public ActionResult Detail(Product product)
        {

            if(null == product){
                return View();
            }

            Product pro = this.productBLL.getProductById(product.id);

            if (null != pro)
            {
                ViewBag.Title = pro.name;
            }

            // 产品明细
            ViewBag.Product = pro;

            // 相关产品(暂时没有规则，目前按照最后修改日期降序查询4个产品)
            ViewBag.MoreProduct = this.productBLL.queryMoreProduct();

            return View();
        }

        // 到客户申请页面
        public ActionResult ToClientApply()
        {
            return View("ClientApply");
        }

        // 客户申请
        public JsonResult ClientApply(ClientApply clientApply)
        {
            if(null == clientApply){
                return Json(new Message(false, "操作失败"));
            }

            if (string.IsNullOrEmpty(clientApply.type) || string.IsNullOrEmpty(clientApply.username) || string.IsNullOrEmpty(clientApply.phone) || string.IsNullOrEmpty(clientApply.address) || string.IsNullOrEmpty(clientApply.sex))
            {
                return Json(new Message(false, "操作失败"));
            }


            //根据姓名和手机号查询是否已经预约过
            if (this.productBLL.getClientApplyByPhoneAndUsername(clientApply) != null)
            {
                return Json(new Message(false, "信息审核中"));
            }

            //添加预约
            object id = this.productBLL.addClientApply(clientApply);

            //开一个线程去发送申请信息到申请区域客服邮箱
            Thread thread = new Thread(new ParameterizedThreadStart(SendEmail));
            thread.Start(clientApply);

            return Json(new Message((int.Parse(id.ToString()) > 0), "操作完成"));
        }


        public ActionResult ToDetail(string productUrl)
        {
            Response.Redirect(productUrl);
            return View("");
        }

        /// <summary>
        /// 给客服发送邮件
        /// </summary>
        /// <param name="obj"></param>
        private void SendEmail(Object obj)
        {
            try
            {
                ClientApply clientApply = (ClientApply)obj;

                // 先根据申请人现居住城市获取对应城市的客服邮箱地址
                Department dept = this.departmentBLL.GetDepartmentByCity(clientApply.address);

                var appSettings = ConfigurationManager.AppSettings;
                var toMailAddress = appSettings["DefaultEmail"].ToString();
                var server = appSettings["Server"].ToString();
                var port = appSettings["Port"].ToString();
                var fromEmail = appSettings["FromEmail"].ToString();
                var username = appSettings["UserName"].ToString();
                var password = appSettings["Password"].ToString();
                var subject = "【官网客户申请】-" + clientApply.address + "-" + clientApply.username + "-" + clientApply.phone;
                var body = "来自官网的客户申请：<br/><br/> 姓名：" + clientApply.username + " <br/><br/> 性别：" + clientApply.sex + "<br/><br/> 联系电话：" + clientApply.phone + "<br/><br/> 现居住城市：" + clientApply.address;

                if (!string.IsNullOrEmpty(clientApply.productId))
                {
                    body += "<br/><br/> 申请产品名称：" + this.productBLL.getProductById(int.Parse(clientApply.productId)).name;
                }

                if (null != dept && !string.IsNullOrEmpty(dept.email))
                {
                    toMailAddress = dept.email;
                }

                string[] ccMail = null;

                var ccMailList = appSettings["ccMail"].ToString();
                if (ccMailList != null && ccMailList.Length > 0)
                {
                    ccMail = ccMailList.Split(new char[] { ';' });
                }

                EmailUtil email = new EmailUtil(server, toMailAddress, fromEmail, ccMail, subject, body, username, password, port, false, false);
                email.Send();

                // 发送完成后更新状态
                this.productBLL.UpdateEmailStatus(clientApply);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }

    public class EmailUtil
    {
        private MailMessage mMailMessage;   //主要处理发送邮件的内容（如：收发人地址、标题、主体、图片等等）
        private SmtpClient mSmtpClient; //主要处理用smtp方式发送此邮件的配置信息（如：邮件服务器、发送端口号、验证方式等等）
        private int mSenderPort;   //发送邮件所用的端口号（htmp协议默认为25）
        private string mSenderServerHost;    //发件箱的邮件服务器地址（IP形式或字符串形式均可）
        private string mSenderPassword;    //发件箱的密码
        private string mSenderUsername;   //发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）
        private bool mEnableSsl;    //是否对邮件内容进行socket层加密传输
        private bool mEnablePwdAuthentication;  //是否对发件人邮箱进行密码验证

        ///<summary>
        /// 构造函数
        ///</summary>
        ///<param name="server">发件箱的邮件服务器地址</param>
        ///<param name="toMail">收件人地址（可以是多个收件人，程序中是以“;"进行区分的）</param>
        ///<param name="fromMail">发件人地址</param>
        ///<param name="subject">邮件标题</param>
        ///<param name="emailBody">邮件内容（可以以html格式进行设计）</param>
        ///<param name="username">发件箱的用户名（即@符号前面的字符串，例如：hello@163.com，用户名为：hello）</param>
        ///<param name="password">发件人邮箱密码</param>
        ///<param name="port">发送邮件所用的端口号（htmp协议默认为25）</param>
        ///<param name="sslEnable">true表示对邮件内容进行socket层加密传输，false表示不加密</param>
        ///<param name="pwdCheckEnable">true表示对发件人邮箱进行密码验证，false表示不对发件人邮箱进行密码验证</param>
        public EmailUtil(string server, string toMail, string fromMail,string[] ccMail, string subject, string emailBody, string username, string password, string port, bool sslEnable, bool pwdCheckEnable)
        {
            try
            {
                mMailMessage = new MailMessage();
                mMailMessage.To.Add(toMail);
                mMailMessage.From = new MailAddress(fromMail);

                MailAddressCollection ccs = mMailMessage.CC;
                if(ccMail != null && ccMail.Length > 0){
                    foreach(string mail in ccMail){
                        ccs.Add(new MailAddress(mail));
                    }
                }

                mMailMessage.Subject = subject;
                mMailMessage.Body = emailBody;
                mMailMessage.IsBodyHtml = true;
                mMailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mMailMessage.Priority = MailPriority.Normal;                
                this.mSenderServerHost = server;
                this.mSenderUsername = username;
                this.mSenderPassword = password;
                this.mSenderPort = Convert.ToInt32(port);
                this.mEnableSsl = sslEnable;
                this.mEnablePwdAuthentication = pwdCheckEnable;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 添加附件
        ///</summary>
        ///<param name="attachmentsPath">附件的路径集合，以分号分隔</param>
        public void AddAttachments(string attachmentsPath)
        {
            try
            {
                string[] path = attachmentsPath.Split(';'); //以什么符号分隔可以自定义
                Attachment data;
                ContentDisposition disposition;
                for (int i = 0; i < path.Length; i++)
                {
                    data = new Attachment(path[i], MediaTypeNames.Application.Octet);
                    disposition = data.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(path[i]);
                    disposition.ModificationDate = File.GetLastWriteTime(path[i]);
                    disposition.ReadDate = File.GetLastAccessTime(path[i]);
                    mMailMessage.Attachments.Add(data);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        ///<summary>
        /// 邮件的发送
        ///</summary>
        public void Send()
        {
            try
            {
                if (mMailMessage != null)
                {
                    mSmtpClient = new SmtpClient();
                    //mSmtpClient.Host = "smtp." + mMailMessage.From.Host;
                    mSmtpClient.Host = this.mSenderServerHost;
                    mSmtpClient.Port = this.mSenderPort;
                    mSmtpClient.UseDefaultCredentials = false;
                    mSmtpClient.EnableSsl = this.mEnableSsl;
                    if (this.mEnablePwdAuthentication)
                    {
                        System.Net.NetworkCredential nc = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                        //NTLM: Secure Password Authentication in Microsoft Outlook Express
                        mSmtpClient.Credentials = nc.GetCredential(mSmtpClient.Host, mSmtpClient.Port, "NTLM");
                    }
                    else
                    {
                        mSmtpClient.Credentials = new System.Net.NetworkCredential(this.mSenderUsername, this.mSenderPassword);
                    }
                    mSmtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    mSmtpClient.Send(mMailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}