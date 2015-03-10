# Sendinblue C# Library

This is the official Sendinblue C# library. It implements the various exposed APIs that you can read more about on https://apidocs.sendinblue.com.

# Requirement

 * The primary dependency is Newtonsoft's JSON .NET library. 
 * This is compatible with .NET 4.0 and above due to the usage of dynamic objects. 

## About the setup

 * The SendinBlue project is the core wrapper library.
 * The test project is a sample C# console application that references the library and gets all campaigns.
 * You can explore the source code for all the calls, or well use the IDE autocomplete to explore more.

## Available functions

List of API calls that you can make. Please do note that the order of parameters are important.

### Campaign calls

 * get_account() - Get your account information
 * get_smtp_details() - Get your SMTP account information
 * create_child_account(string email, string password, string company_org, string First_name, string Last_name, Dictionary`<string, int>` credits) - Create a Reseller child account
 * update_child_account(string child_authkey, string company_org, string First_name, string Last_name, string password) - Update a Reseller child account
 * delete_child_account(string child_authkey) - Delete a Reseller child account
 * get_child_account(Dictionary`<string, string>` child_authkey) - Get Reseller child accounts
 * add_remove_child_credits(String child_authkey, Dictionary`<string, int>` add_credits, Dictionary`<string, int>` remove_credits) - Add/Remove Reseller child credits
 * get_campaigns(string type, string status, int page, int page_limit) - Get list of all campaigns or of specific type or status or both
 * get_campaign(int id) - Get specific campaign object
 * create_campaign(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, string attachmentUrl, int inline_image) - Create a campaign
 * delete_campaign(int id) - Delete a campaign
 * update_campaign(string id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, string attachmentUrl, int inline_image) - Update campaign information
 * campaign_report_email(int id, string lang, string email_subject, List`<string>` email_to, string email_content_type, List`<string>` email_bcc, List`<string>` email_cc, string email_body) - Sending reports to specific emails
 * campaign_recipients_export(int id, string notify_url, string type) - Export recipients of a campaign
 * send_bat_email(int id, List`<string>` email_to) - Send a test Email (bat)
 * create_trigger_campaign(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, int recurring, string attachmentUrl, int inline_image) - Create a trigger campaign
 * update_trigger_campaign(int id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, int recurring, string attachmentUrl, int inline_image) - Update trigger campaign information
 * campaign_share_link(List`<int>` campaignids) - Get campaign share link
 * update_campaign_status(int id, string status) - Modify a campaign status
 * get_folders() - Get list of all the folder details.
 * get_folder(int id) - Get all the folder details for folder with id `<id>`
 * create_folder(string name) - Create a folder
 * delete_folder(int id) - Delete folder with folder id `<id>`
 * update_folder(int id, string name) - Update folder with folder id `<id>`
 * get_lists() - Get all the lists
 * get_list(int id) - Get information about a list
 * create_list(string list_name, int list_parent) - Create a list
 * delete_list(int id) - Delete a list
 * update_list(int id, string list_name, int list_parent) - Updating a list
 * display_list_users(List`<int>` listids, int page, int page_limit) - Display details of all users for the given lists
 * add_users_list(int id, List`<string>` users) - Add users to a list
 * delete_users_list(int id, List`<string>` users) - Delete users from a list
 * get_attributes() - Listing all attributes
 * get_attribute(string id) - Listing a certain type attributes
 * create_attribute(string type, Dictionary`<string, string>` data) - Creating attributes
 * delete_attribute(string id, List`<string>` data) - Deleting attributes of the given type
 * get_user(string email) - Get information about a user/email
 * create_update_user(string email, Dictionary`<string, string>` attributes, int blacklisted, List`<int>` listid, List`<int>` listid_unlink, int blacklisted_sms) - Create/Update a user information
 * delete_user(string email) - Deleting user from db is not permitted but this action will unlink him from all lists
 * import_users(string url, List`<int>` listids, string notify_url, string name, int folder_id) - Import users/emails
 * export_users(string export_attrib, string filter, string notify_url) - Export users/emails
 * get_processes() - Get information about all background processes
 * get_process(int id) - Get information about a specific process
 * get_senders(string option) - Get information about all/specific senders
 * create_sender(string sender_name, string sender_email, List`<string>` ip_domain) - Create a sender
 * delete_sender(int id) - Delete a sender
 * update_sender(int id, string sender_name, string sender_email, List`<string>` ip_domain) - Update a sender

### SMTP calls

 * get_report(int limit, string start_date, string end_date, int offset, string date, int days, string email) - Retrieve information for all report events
 * get_statistics(int aggregate, string tag, int days, string end_date, string start_date) - Get aggregate statistics about emails sent
 * get_webhooks() - List registered webhooks
 * get_webhook(int id) - Get information about a webhook
 * create_webhook(string url, string description, List`<string>` events) - Registering a webhook
 * delete_webhook(int id) - Deleting a webhook
 * update_webhook(int id, string url, string description, List`<string>` events) - Editing a webhook
 * delete_bounces(string start_date, string end_date, string email) - Deleting bounces
 * send_email(Dictionary`<string, string>` to, string subject, List`<string>` from_name, string html, string txt, Dictionary`<string, string>` cc, Dictionary`<string, string>` bcc, List`<string>` replyto, Dictionary`<string,string>` attachment, Dictionary`<string, string>` headers) - Sending out a transactional email
 * send_transactional_template(int id, string to, string cc, string bcc, Dictionary`<string, string>` attr, string attachmentUrl, Dictionary`<string,string>` attachment) - Send templates created on Sendinblue, through Sendinblue smtp.
 * create_template(string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status, int attach) - Create a template 
 * update_template(int id, string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status, int attach) - Update template information

### SMS call

 * send_sms(string to, string from_name, string text, string web_url, string tag, string type) - Sending a SMS
 * create_sms_campaign(string camp_name, string sender, string content, string bat_sent, List`<int>` listids, List`<int>` exclude_list, string scheduled_date) - Create a SMS campaign
 * update_sms_campaign(int id, string camp_name, string sender, string content, string bat_sent, List`<int>` listids, List`<int>` exclude_list, string scheduled_date) - Update a SMS campaign
 * send_bat_sms(int id, string mobilephone) - Send a test SMS campaign