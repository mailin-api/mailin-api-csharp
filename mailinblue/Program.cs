/*
 * Created by SharpDevelop.
 * User: Dipankar
 * Date: 30-09-2013
 * Time: 22:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Dynamic;

namespace mailinblue
{
	public class API
	{
		public string base_url = "https://api.mailinblue.com/v1.0/";
		public string accessId = "";
		public string secretId = "";
		public API(string accessId, string secretId) {
			this.accessId = accessId;
			this.secretId = secretId;
		}
		static string ByteToString(byte[] buff) {
			string sbinary = "";
			for (int i = 0; i < buff.Length; i++)
				sbinary += buff[i].ToString("X2"); /* hex format */
			return sbinary;
		}
		private dynamic auth_call(string resource, string method,string content) {
			// Create the url
			string url = base_url + resource;
			// Create request
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			// Set method
			request.Method = method;
			WebHeaderCollection headers = (request as HttpWebRequest).Headers;
			string httpDate = DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
			Encoding ae = new UTF8Encoding();
			string content_type = "application/json";
			string content_md5 = "";
			if(!content.Equals("")) {
				// Need to verify MD5
				MD5 md5 = System.Security.Cryptography.MD5.Create();
				byte[] hash = md5.ComputeHash(ae.GetBytes(content));
				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < hash.Length; i++)
				{
					sb.Append(hash[i].ToString("X2"));
				}
				content_md5 = sb.ToString().ToLower();
			}
			string canonicalString = method+"\n" + content_md5 + "\n"+ content_type +"\n" + httpDate + "\n" + url;
			HMACSHA1 signature = new HMACSHA1(System.Text.Encoding.ASCII.GetBytes(secretId));
			signature.Initialize();
			// Get the actual signature
			byte[] moreBytes = signature.ComputeHash(ae.GetBytes(canonicalString));
			string encodedCanonical = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ByteToString(moreBytes).ToLower()));
			request.ContentType = content_type;
			request.Headers.Add("X-mailin-date", httpDate);
			request.Headers.Add("Authorization", accessId + ":" + encodedCanonical);
			//Console.Write(canonicalString);
			//Console.Write(encodedCanonical);
			HttpWebResponse response;
			response = request.GetResponse() as HttpWebResponse;
			// read the response stream and put it into a byte array
			Stream stream =  response.GetResponseStream() as Stream;
			byte[] buffer = new byte[32 * 1024];
			int nRead =0;
			MemoryStream ms = new MemoryStream();
			do
			{
				nRead = stream.Read(buffer, 0, buffer.Length);
				ms.Write(buffer, 0, nRead);
			} while (nRead > 0);
			// convert read bytes into string
			ASCIIEncoding encoding = new ASCIIEncoding();
			string responseString = encoding.GetString(ms.ToArray());
			// Return a dynamic object
			return JObject.Parse(responseString);
		}
		private dynamic get_request(string resource,string content)
		{
			return auth_call(resource,"GET","");
		}
		private dynamic post_request(string resource,string content)
		{
			return auth_call(resource,"POST",content);
		}
		private dynamic delete_request(string resource,string content)
		{
			return auth_call(resource,"DELETE","");
		}
		private dynamic put_request(string resource,string content)
		{
			return auth_call(resource,"PUT",content);
		}
		public dynamic get_account()
		{
			dynamic content = new ExpandoObject();
			return get_request("account",JsonConvert.SerializeObject(content));
		}
		public dynamic send_sms(string text,string tag,string web_url,string sms_from,string sms_to)
		{
			dynamic content = new ExpandoObject();
			content.text=text;content.tag=tag;content.web_url=web_url;content.sms_from=sms_from;content.sms_to=sms_to;
			return post_request("sms",JsonConvert.SerializeObject(content));
		}
		public dynamic get_campaigns()
		{
			dynamic content = new ExpandoObject();
			return get_request("campaign",JsonConvert.SerializeObject(content));
		}
		public dynamic get_campaign(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("campaign/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic create_campaign(string category,string from_name,string name,string bat_sent,string tags,string html_content,string html_url,List<int> listid,string scheduled_date,string subject)
		{
			dynamic content = new ExpandoObject();
			content.category=category;content.from_name=from_name;content.name=name;content.bat_sent=bat_sent;content.tags=tags;content.html_content=html_content;content.html_url=html_url;content.listid=listid;content.scheduled_date=scheduled_date;content.subject=subject;
			return post_request("campaign",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_campaign(string id)
		{
			dynamic content = new ExpandoObject();
			return delete_request("campaign/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic update_campaign(string id,string category,string from_name,string name,string bat_sent,string tags,string html_content,string html_url,List<int> listid,string scheduled_date,string subject)
		{
			dynamic content = new ExpandoObject();
			content.category=category;content.from_name=from_name;content.name=name;content.bat_sent=bat_sent;content.tags=tags;content.html_content=html_content;content.html_url=html_url;content.listid=listid;content.scheduled_date=scheduled_date;content.subject=subject;
			return put_request("campaign/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic campaign_report_email(string id,string lang,string email_subject,List<int> email_to,string email_content_type,string email_bcc,string email_cc,string email_body)
		{
			dynamic content = new ExpandoObject();
			content.lang=lang;content.email_subject=email_subject;content.email_to=email_to;content.email_content_type=email_content_type;content.email_bcc=email_bcc;content.email_cc=email_cc;content.email_body=email_body;
			return post_request("campaign/" + id + "/report",JsonConvert.SerializeObject(content));
		}
		public dynamic campaign_recipients_export(string id,string notify_url,string type)
		{
			dynamic content = new ExpandoObject();
			content.notify_url=notify_url;content.type=type;
			return post_request("campaign/" + id + "/report",JsonConvert.SerializeObject(content));
		}
		public dynamic get_processes()
		{
			dynamic content = new ExpandoObject();
			return get_request("process",JsonConvert.SerializeObject(content));
		}
		public dynamic get_process(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("process/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic get_campaignstats()
		{
			dynamic content = new ExpandoObject();
			return get_request("campaignstat",JsonConvert.SerializeObject(content));
		}
		public dynamic get_campaignstat(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("campaignstat/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic get_lists()
		{
			dynamic content = new ExpandoObject();
			return get_request("list",JsonConvert.SerializeObject(content));
		}
		public dynamic get_list(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("list/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic create_list(string list_name,string list_parent)
		{
			dynamic content = new ExpandoObject();
			content.list_name=list_name;content.list_parent=list_parent;
			return post_request("list",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_list(string id)
		{
			dynamic content = new ExpandoObject();
			return delete_request("list/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic update_list(string id,string list_name,string list_parent)
		{
			dynamic content = new ExpandoObject();
			content.list_name=list_name;content.list_parent=list_parent;
			return put_request("list/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic add_users_list(string id,List<int> users)
		{
			dynamic content = new ExpandoObject();
			content.users=users;
			return post_request("list/" + id + "/users",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_users_list(string id,List<int> users)
		{
			dynamic content = new ExpandoObject();
			content.users=users;
			return delete_request("list/" + id + "/users",JsonConvert.SerializeObject(content));
		}
		public dynamic send_email(Dictionary<string, string> cc,string text,Dictionary<string, string> bcc,Dictionary<string, string> replyto,string html,Dictionary<string, string> email_to,List<int> attachment,List<int> email_from,string subject)
		{
			dynamic content = new ExpandoObject();
			content.cc=cc;content.text=text;content.bcc=bcc;content.replyto=replyto;content.html=html;content.email_to=email_to;content.attachment=attachment;content.email_from=email_from;content.subject=subject;
			return post_request("email",JsonConvert.SerializeObject(content));
		}
		public dynamic get_webhooks()
		{
			dynamic content = new ExpandoObject();
			return get_request("webhook",JsonConvert.SerializeObject(content));
		}
		public dynamic get_webhook(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("webhook/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic create_webhook(string url,string description,List<int> events)
		{
			dynamic content = new ExpandoObject();
			content.url=url;content.description=description;content.events=events;
			return post_request("webhook",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_webhook(string id)
		{
			dynamic content = new ExpandoObject();
			return delete_request("webhook/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic update_webhook(string id,string url,string description,List<int> events)
		{
			dynamic content = new ExpandoObject();
			content.url=url;content.description=description;content.events=events;
			return put_request("webhook/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic get_statistics(int aggregate,string tag,int days,string end_date,string start_date)
		{
			dynamic content = new ExpandoObject();
			content.aggregate=aggregate;content.tag=tag;content.days=days;content.end_date=end_date;content.start_date=start_date;
			return post_request("statistics",JsonConvert.SerializeObject(content));
		}
		public dynamic get_user(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("user/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic get_user_stats(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("user/" + id + "/$type",JsonConvert.SerializeObject(content));
		}
		public dynamic create_user(Dictionary<string, string> attributes,int blacklisted,string email,List<int> listid)
		{
			dynamic content = new ExpandoObject();
			content.attributes=attributes;content.blacklisted=blacklisted;content.email=email;content.listid=listid;
			return post_request("user",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_user(string id)
		{
			dynamic content = new ExpandoObject();
			return delete_request("user/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic update_user(string id,Dictionary<string, string> attributes,int blacklisted,List<int> listid)
		{
			dynamic content = new ExpandoObject();
			content.attributes=attributes;content.blacklisted=blacklisted;content.listid=listid;
			return put_request("user/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic import_users(string url,List<int> listids,string notify_url,string name)
		{
			dynamic content = new ExpandoObject();
			content.url=url;content.listids=listids;content.notify_url=notify_url;content.name=name;
			return post_request("user/import",JsonConvert.SerializeObject(content));
		}
		public dynamic export_users(string export_attrib,string filer,string notify_url)
		{
			dynamic content = new ExpandoObject();
			content.export_attrib=export_attrib;content.filer=filer;content.notify_url=notify_url;
			return post_request("user/export",JsonConvert.SerializeObject(content));
		}
		public dynamic get_attributes()
		{
			dynamic content = new ExpandoObject();
			return get_request("attribute",JsonConvert.SerializeObject(content));
		}
		public dynamic get_attribute(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("attribute/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic create_attribute(string type,Dictionary<string, string> data)
		{
			dynamic content = new ExpandoObject();
			content.type=type;content.data=data;
			return post_request("attribute",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_attribute(string id,string data)
		{
			dynamic content = new ExpandoObject();
			content.data=data;
			return post_request("attribute/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic get_report(int limit,string start_date,string end_date,int offset,string date,int days,string email)
		{
			dynamic content = new ExpandoObject();
			content.limit=limit;content.start_date=start_date;content.end_date=end_date;content.offset=offset;content.date=date;content.days=days;content.email=email;
			return post_request("report",JsonConvert.SerializeObject(content));
		}
		public dynamic get_folders()
		{
			dynamic content = new ExpandoObject();
			return get_request("folder",JsonConvert.SerializeObject(content));
		}
		public dynamic get_folder(string id)
		{
			dynamic content = new ExpandoObject();
			return get_request("folder/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic create_folder(string name)
		{
			dynamic content = new ExpandoObject();
			content.name=name;
			return post_request("folder",JsonConvert.SerializeObject(content));
		}
		public dynamic delete_folder(string id)
		{
			dynamic content = new ExpandoObject();
			return delete_request("folder/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic update_folder(string id,string name)
		{
			dynamic content = new ExpandoObject();
			content.name=name;
			return put_request("folder/" + id,JsonConvert.SerializeObject(content));
		}
		public dynamic delete_bounces(string start_date,string end_date,string email)
		{
			dynamic content = new ExpandoObject();
			content.start_date=start_date;content.end_date=end_date;content.email=email;
			return post_request("bounces",JsonConvert.SerializeObject(content));
		}

	}
	
}