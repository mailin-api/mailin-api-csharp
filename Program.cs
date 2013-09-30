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

namespace mailinblue
{
	public class API
	{
		    static string ByteToString(byte[] buff)
    {
        string sbinary = "";
        for (int i = 0; i < buff.Length; i++)
            sbinary += buff[i].ToString("X2"); /* hex format */
        return sbinary;
    }    
		public void auth_call(string accessId,string secretId, string method,string content) {
			string base_url = "http://api.mailinblue.com/v1.0/";
			// Create the url
			string url = base_url + "campaign";
			// Create request
			HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
			// Set method
            request.Method = method;
            WebHeaderCollection headers = (request as HttpWebRequest).Headers;
            string httpDate = "Mon, 30 Sep 2013 23:23:55 +0530";//DateTime.UtcNow.ToString("ddd, dd MMM yyyy HH:mm:ss ") + "GMT";
            headers.Add("X-mailin-date", httpDate);
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
            //Console.WriteLine(canonicalString);
			HMACSHA1 signature = new HMACSHA1(System.Text.Encoding.ASCII.GetBytes(secretId));
			signature.Initialize();
            // Get the actual signature
            byte[] moreBytes = signature.ComputeHash(ae.GetBytes(canonicalString));
            string encodedCanonical = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(ByteToString(moreBytes).ToLower()));
            headers.Add("Authorization", accessId + ":" + encodedCanonical);
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
                System.Console.Write(responseString);
	}
	}
	class Program
	{
		public static void Main(string[] args)
		{
			API test = new API();
			test.auth_call("<Access ID>","<Secret ID>","GET","");
			Console.ReadKey(true);
		}
	}
}