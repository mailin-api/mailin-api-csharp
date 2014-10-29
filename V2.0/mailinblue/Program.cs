using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Web;

namespace mailinblue
{
    public class API
    {
        public string base_url = "https://api.sendinblue.com/v2.0/";
        public string accessId = "";
        
        public API(string accessId)
        {
            this.accessId = accessId;
        }
        private dynamic auth_call(string resource, string method, string content)
        {
            Stream stream = new MemoryStream();
            string url = base_url + resource;
            string content_type = "application/json";
            // Create request

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            // Set method
            request.Method = method;
            request.ContentType = content_type;
            request.Headers.Add("api-key", accessId);

            if (method == "POST" || method == "PUT") {
                using (System.IO.Stream s = request.GetRequestStream())
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(s))
                        sw.Write(content);
                }
            }
            try
            {
                HttpWebResponse response;
                response = request.GetResponse() as HttpWebResponse;
                // read the response stream and put it into a byte array
                stream = response.GetResponseStream() as Stream;
            }
            catch (System.Net.WebException ex)
            {
                // read the response stream If status code is other than 200 and put it into a byte array
                stream = ex.Response.GetResponseStream() as Stream;
            }

            byte[] buffer = new byte[32 * 1024];
            int nRead = 0;
            MemoryStream ms = new MemoryStream();
            do
            {
                nRead = stream.Read(buffer, 0, buffer.Length);
                ms.Write(buffer, 0, nRead);
            } while (nRead > 0);
            // convert read bytes into string
            ASCIIEncoding encoding = new ASCIIEncoding();
            String responseString = encoding.GetString(ms.ToArray());
            return JObject.Parse(responseString);
        }
        private dynamic get_request(string resource, string content)
        {
            return auth_call(resource, "GET", "");
        }
        private dynamic post_request(string resource, string content)
        {
            return auth_call(resource, "POST", content);
        }
        private dynamic delete_request(string resource, string content)
        {
            return auth_call(resource, "DELETE", "");
        }
        private dynamic put_request(string resource, string content)
        {
            return auth_call(resource, "PUT", content);
        }
        public dynamic get_account()
        {
            dynamic content = new ExpandoObject();
            return get_request("account", JsonConvert.SerializeObject(content));
        }
        public dynamic send_sms(string to, string from_name, string text, string web_url, string tag, string type)
        {
            dynamic content = new ExpandoObject();
            content.text = text; content.tag = tag; content.web_url = web_url; content.from = from_name; content.to = to; content.type = type;
            return post_request("sms", JsonConvert.SerializeObject(content));
        }
        public dynamic get_campaigns_v2(string type, string status, int page, int page_limit)
        {
            dynamic content = new ExpandoObject();
            content.type = type; content.status = status; content.page = page; content.page_limit = page_limit;
            String url = "type/" + type + "/status/" + status + "/page/" + page + "/page_limit/" + page_limit;
            return get_request("campaign/detailsv2/" + url ,"");
        }
        public dynamic get_campaign_v2(int id)
        {
            dynamic content = new ExpandoObject();
            return get_request("campaign/" + id + "/detailsv2/", JsonConvert.SerializeObject(content));
        }
        public dynamic create_campaign(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List<int> listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List<int> exclude_list)
        {
            dynamic content = new ExpandoObject();
            content.category = category; content.from_name = from_name; content.name = name; content.bat_sent = bat_sent; content.html_content = html_content; 
            content.html_url = html_url; content.listid = listid; content.scheduled_date = scheduled_date; content.subject = subject; content.from_email = from_email;
            content.reply_to = reply_to; content.to_field = to_field; content.exclude_list = exclude_list;
            return post_request("campaign", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_campaign(int id)
        {
            dynamic content = new ExpandoObject();
            return delete_request("campaign/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic update_campaign(string id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List<int> listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List<int> exclude_list)
        {
            dynamic content = new ExpandoObject();
            content.category = category; content.from_name = from_name; content.name = name; content.bat_sent = bat_sent; content.html_content = html_content; content.html_url = html_url; content.listid = listid; content.scheduled_date = scheduled_date; content.subject = subject; content.from_email = from_email; content.reply_to = reply_to; content.to_field = to_field; content.exclude_list = exclude_list;
            return put_request("campaign/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic campaign_report_email(int id, string lang, string email_subject, List<string> email_to, string email_content_type, List<string> email_bcc, List<string> email_cc, string email_body)
        {
            dynamic content = new ExpandoObject();
            content.lang = lang; content.email_subject = email_subject; content.email_to = email_to; content.email_content_type = email_content_type; content.email_bcc = email_bcc; content.email_cc = email_cc; content.email_body = email_body;
            return post_request("campaign/" + id + "/report", JsonConvert.SerializeObject(content));
        }
        public dynamic campaign_recipients_export(int id, string notify_url, string type)
        {
            dynamic content = new ExpandoObject();
            content.notify_url = notify_url; content.type = type;
            return post_request("campaign/" + id + "/recipients", JsonConvert.SerializeObject(content));
        }
        public dynamic get_processes()
        {
            dynamic content = new ExpandoObject();
            return get_request("process", JsonConvert.SerializeObject(content));
        }
        public dynamic get_process(int id)
        {
            dynamic content = new ExpandoObject();
            return get_request("process/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic get_lists()
        {
            dynamic content = new ExpandoObject();
            return get_request("list", JsonConvert.SerializeObject(content));
        }
        public dynamic get_list(int id)
        {
            dynamic content = new ExpandoObject();
            return get_request("list/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic create_list(string list_name, int list_parent)
        {
            dynamic content = new ExpandoObject();
            content.list_name = list_name; content.list_parent = list_parent;
            return post_request("list", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_list(int id)
        {
            dynamic content = new ExpandoObject();
            return delete_request("list/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic update_list(int id, string list_name, int list_parent)
        {
            dynamic content = new ExpandoObject();
            content.list_name = list_name; content.list_parent = list_parent;
            return put_request("list/" + id, JsonConvert.SerializeObject(content));
        }    
        public dynamic display_list_users(List<int> listids, int page, int page_limit)
        {
            dynamic content = new ExpandoObject();
            content.listids = listids; content.page = page; content.page_limit = page_limit;
            return put_request("list/display", JsonConvert.SerializeObject(content));
        }
        public dynamic add_users_list(int id, List<string> users)
        {
            dynamic content = new ExpandoObject();
            content.users = users;
            return post_request("list/" + id + "/users", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_users_list(int id, List<string> users)
        {
            dynamic content = new ExpandoObject();
            content.users = users;
            return put_request("list/" + id + "/delusers", JsonConvert.SerializeObject(content));
        }

        public dynamic send_email(Dictionary<string, string> to, string subject, List<string> from_name, string html, string txt, Dictionary<string, string> cc, Dictionary<string, string> bcc, List<string> replyto, Dictionary<string,string> attachment, Dictionary<string, string> headers)
        {
            dynamic content = new ExpandoObject();
            content.cc = cc; content.text = txt; content.bcc = bcc; content.replyto = replyto; content.html = html; content.to = to; content.attachment = attachment; content.from = from_name; content.subject = subject; content.headers = headers;
            return post_request("email", JsonConvert.SerializeObject(content));
        }
        public dynamic get_webhooks()
        {
            dynamic content = new ExpandoObject();
            return get_request("webhook", JsonConvert.SerializeObject(content));
        }
        public dynamic get_webhook(int id)
        {
            dynamic content = new ExpandoObject();
            return get_request("webhook/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic create_webhook(string url, string description, List<string> events)
        {
            dynamic content = new ExpandoObject();
            content.url = url; content.description = description; content.events = events;
            return post_request("webhook", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_webhook(int id)
        {
            dynamic content = new ExpandoObject();
            return delete_request("webhook/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic update_webhook(int id, string url, string description, List<string> events)
        {
            dynamic content = new ExpandoObject();
            content.url = url; content.description = description; content.events = events;
            return put_request("webhook/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic get_statistics(int aggregate, string tag, int days, string end_date, string start_date)
        {
            dynamic content = new ExpandoObject();
            content.aggregate = aggregate; content.tag = tag; content.days = days; content.end_date = end_date; content.start_date = start_date;
            return post_request("statistics", JsonConvert.SerializeObject(content));
        }
        public dynamic get_user(string email)
        {
            dynamic content = new ExpandoObject();
            return get_request("user/" + email.Trim(), JsonConvert.SerializeObject(content));
        }
        public dynamic create_user(Dictionary<string, string> attributes, int blacklisted, string email, List<int> listid)
        {
            dynamic content = new ExpandoObject();
            content.attributes = attributes; content.blacklisted = blacklisted; content.email = email; content.listid = listid;
            return post_request("user", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_user(string email)
        {
            dynamic content = new ExpandoObject();
            return delete_request("user/" + email, JsonConvert.SerializeObject(content));
        }
        public dynamic update_user(string id, Dictionary<string, string> attributes, int blacklisted, List<int> listid, List<int> listid_unlink)
        {
            dynamic content = new ExpandoObject();
            content.attributes = attributes; content.blacklisted = blacklisted; content.listid = listid; content.listid_unlink = listid_unlink;
            return put_request("user/" + id, JsonConvert.SerializeObject(content));
        }

        public dynamic create_update_user(string email, Dictionary<string, string> attributes, int blacklisted, List<int> listid, List<int> listid_unlink, int blacklisted_sms)
        {
            dynamic content = new ExpandoObject();
            content.email = email;
            content.attributes = attributes; 
            content.blacklisted = blacklisted; 
            content.listid = listid; 
            content.listid_unlink = listid_unlink;
            content.blacklisted_sms = blacklisted_sms;
            return put_request("user/createdituser", JsonConvert.SerializeObject(content));
        }

        public dynamic import_users(string url, List<int> listids, string notify_url, string name)
        {
            dynamic content = new ExpandoObject();
            content.url = url; content.listids = listids; content.notify_url = notify_url; content.name = name;
            return post_request("user/import", JsonConvert.SerializeObject(content));
        }
        public dynamic export_users(string export_attrib, string filter, string notify_url)
        {
            dynamic content = new ExpandoObject();
            content.export_attrib = export_attrib; content.filter = filter; content.notify_url = notify_url;
            return post_request("user/export", JsonConvert.SerializeObject(content));
        }
        public dynamic get_attributes()
        {
            dynamic content = new ExpandoObject();
            return get_request("attribute", JsonConvert.SerializeObject(content));
        }
        public dynamic get_attribute(string id)
        {
            dynamic content = new ExpandoObject();
            return get_request("attribute/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic create_attribute(string type, Dictionary<string, string> data)
        {
            dynamic content = new ExpandoObject();
            content.type = type; content.data = data;
            return post_request("attribute", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_attribute(string id, List<string> data)
        {
            dynamic content = new ExpandoObject();
            content.data = data;
            return post_request("attribute/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic get_report(int limit, string start_date, string end_date, int offset, string date, int days, string email)
        {
            dynamic content = new ExpandoObject();
            content.limit = limit; content.start_date = start_date; content.end_date = end_date; content.offset = offset; content.date = date; content.days = days; content.email = email;
            return post_request("report", JsonConvert.SerializeObject(content));
        }
        public dynamic get_folders()
        {
            dynamic content = new ExpandoObject();
            return get_request("folder", JsonConvert.SerializeObject(content));
        }
        public dynamic get_folder(int id)
        {
            dynamic content = new ExpandoObject();
            return get_request("folder/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic create_folder(string name)
        {
            dynamic content = new ExpandoObject();
            content.name = name;
            return post_request("folder", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_folder(int id)
        {
            dynamic content = new ExpandoObject();
            return delete_request("folder/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic update_folder(int id, string name)
        {
            dynamic content = new ExpandoObject();
            content.name = name;
            return put_request("folder/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic delete_bounces(string start_date, string end_date, string email)
        {
            dynamic content = new ExpandoObject();
            content.start_date = start_date; content.end_date = end_date; content.email = email;
            return post_request("bounces", JsonConvert.SerializeObject(content));
        }
        public dynamic send_transactional_template(int id, string to, string cc, string bcc, Dictionary<string, string> attr)
        {
            dynamic content = new ExpandoObject();
            content.to = to; content.cc = cc; content.bcc = bcc; content.attr = attr; 
            return put_request("template/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic share_campaign(List<int> campaignids)
        {
            dynamic content = new ExpandoObject();
            content.camp_ids = campaignids;
            return post_request("campaign/sharelinkv2", JsonConvert.SerializeObject(content));
        }
        public dynamic get_reseller_child(Dictionary<string, string> child_authkey) 
        {
            dynamic content = new ExpandoObject();
            content.auth_key = JsonConvert.SerializeObject(child_authkey);
            return post_request("account/getchildv2", JsonConvert.SerializeObject(content));
        }
        public dynamic add_remove_child_credits(String child_authkey, Dictionary<string, int> add_credits, Dictionary<string, int> remove_credits)
        {
            dynamic content = new ExpandoObject();
            content.auth_key = child_authkey;
            content.add_credit = add_credits;
            content.rmv_credit = remove_credits;
            return post_request("account/addrmvcredit", JsonConvert.SerializeObject(content));
        }
        public dynamic update_child_account(string child_authkey, string company_org, string First_name, string Last_name, string password)
        {
            dynamic content = new ExpandoObject();
            content.auth_key = child_authkey;
            content.company_org = company_org;
            content.first_name = First_name;
            content.last_name = Last_name;
            content.password = password;
            return put_request("account", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_child_account(string child_authkey)
        {
            dynamic content = new ExpandoObject();
            return delete_request("account/" + child_authkey, JsonConvert.SerializeObject(content));
        }
        public dynamic create_child_account(string email, string password, string company_org, string First_name, string Last_name, Dictionary<string, int> credits)
        {
            dynamic content = new ExpandoObject();
            content.child_email = email;
            content.password = password;
            content.company_org = company_org;
            content.first_name = First_name;
            content.last_name = Last_name;
            content.credits = credits;
            return post_request("account", JsonConvert.SerializeObject(content));
        }
        public dynamic create_sender(string sender_name, string sender_email, List<string> ip_domain)
        {
            dynamic content = new ExpandoObject();
            content.name = sender_name;
            content.email = sender_email;
            content.ip_domain = ip_domain;
            return post_request("advanced", JsonConvert.SerializeObject(content));
        }
        public dynamic delete_sender(int id)
        {
            dynamic content = new ExpandoObject();
            return delete_request("advanced/"+id, JsonConvert.SerializeObject(content));
        }
        public dynamic update_sender(int id, string sender_name, string sender_email, List<string> ip_domain)
        {
            dynamic content = new ExpandoObject();
            content.name = sender_name;
            content.email = sender_email;
            content.ip_domain = ip_domain;
            return put_request("advanced/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic get_senders(string option) 
        {
            dynamic content = new ExpandoObject();
            return get_request("advanced/index/option/"+option, JsonConvert.SerializeObject(content));
        }
        public dynamic send_bat_email(int id, List<string> email_to)
        {
            dynamic content = new ExpandoObject();
            content.emails = email_to;
            return put_request("campaign/" + id + "/test", JsonConvert.SerializeObject(content));
        }
        public dynamic send_bat_sms(int id, string mobilephone)
        {
            dynamic content = new ExpandoObject();
            content.to = mobilephone;
            String phone = HttpUtility.UrlEncode(mobilephone);
            return get_request("sms/" + id + "/" + phone, "");
            
        }
        public dynamic create_sms_campaign(string camp_name, string sender, string content, string bat_sent, List<int> listids, List<int> exclude_list, string scheduled_date)
        {
            dynamic body = new ExpandoObject();
            body.name = camp_name; body.sender = sender; body.content = content; body.bat = bat_sent; body.listid = listids; body.exclude_list = exclude_list; body.scheduled_date = scheduled_date;
            return post_request("sms", JsonConvert.SerializeObject(body));
        }
        public dynamic update_sms_campaign(int id, string camp_name, string sender, string content, string bat_sent, List<int> listids, List<int> exclude_list, string scheduled_date)
        {
            dynamic body = new ExpandoObject();
            body.name = camp_name; body.sender = sender; body.content = content; body.bat = bat_sent; body.listid = listids; body.exclude_list = exclude_list; body.scheduled_date = scheduled_date;
            return put_request("sms/" + id, JsonConvert.SerializeObject(body));
        }
        public dynamic create_trigger_campaign(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List<int> listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List<int> exclude_list, int recurring)
        {
            dynamic content = new ExpandoObject();
            content.category = category; content.from_name = from_name; content.trigger_name = name; content.bat_sent = bat_sent; content.html_content = html_content; content.html_url = html_url; content.listid = listid; content.scheduled_date = scheduled_date; content.subject = subject; content.from_email = from_email; content.reply_to = reply_to; content.to_field = to_field; content.exclude_list = exclude_list; content.recurring = recurring;
            return post_request("campaign", JsonConvert.SerializeObject(content));
        }
        public dynamic update_trigger_campaign(int id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List<int> listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List<int> exclude_list, int recurring)
        {
            dynamic content = new ExpandoObject();
            content.category = category; content.from_name = from_name; content.trigger_name = name; content.bat_sent = bat_sent; content.html_content = html_content; content.html_url = html_url; content.listid = listid; content.scheduled_date = scheduled_date; content.subject = subject; content.from_email = from_email; content.reply_to = reply_to; content.to_field = to_field; content.exclude_list = exclude_list; content.recurring = recurring;
            return put_request("campaign/" + id, JsonConvert.SerializeObject(content));
        }
        public dynamic create_template(string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status)
        {
            dynamic body = new ExpandoObject();
            body.from_name = from_name; body.template_name = name; body.bat = bat_sent; body.html_content = html_content; body.html_url = html_url; body.subject = subject; body.from_email = from_email; body.reply_to = reply_to; body.to_field = to_field; body.status = status;
            return post_request("template", JsonConvert.SerializeObject(body));
        }
        public dynamic update_template(int id, string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status)
        {
            dynamic body = new ExpandoObject();
            body.from_name = from_name; body.template_name = name; body.bat = bat_sent; body.html_content = html_content; body.html_url = html_url; body.subject = subject; body.from_email = from_email; body.reply_to = reply_to; body.to_field = to_field; body.status = status;
            return post_request("template/" + id, JsonConvert.SerializeObject(body));
        }
        public dynamic update_campaign_status(int id, string status)
        {
            dynamic content = new ExpandoObject();
            content.status = status;
            return put_request("campaign/" + id + "/updatecampstatus", JsonConvert.SerializeObject(content));
        }
        public dynamic get_smtp_details()
        {
            dynamic content = new ExpandoObject();
            return get_request("account/smtpdetail", JsonConvert.SerializeObject(content));
        }

    }

}
