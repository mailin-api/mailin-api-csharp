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

List of API calls that you can make, you can click to read more about it. Please do note that the order of parameters are important.

### Campaign calls

 * [get_account](https://apidocs.sendinblue.com/account/#1)() - Get your account information
 * [get_smtp_details](https://apidocs.sendinblue.com/account/#7)() - Get your SMTP account information
 * [create_child_account](https://apidocs.sendinblue.com/account/#2)(string email, string password, string company_org, string First_name, string Last_name, Dictionary`<string, int>` credits) - Create a Reseller child account
 * [update_child_account](https://apidocs.sendinblue.com/account/#3)(string child_authkey, string company_org, string First_name, string Last_name, string password) - Update a Reseller child account
 * [delete_child_account](https://apidocs.sendinblue.com/account/#4)(string child_authkey) - Delete a Reseller child account
 * [get_reseller_child](https://apidocs.sendinblue.com/account/#5)(Dictionary`<string, string>` child_authkey) - Get Reseller child accounts
 * [add_remove_child_credits](https://apidocs.sendinblue.com/account/#6)(String child_authkey, Dictionary`<string, int>` add_credits, Dictionary`<string, int>` remove_credits) - Add/Remove Reseller child credits
 * [get_campaigns_v2](https://apidocs.sendinblue.com/campaign/#1)(string type, string status, int page, int page_limit) - Get list of all campaigns or of specific type or status or both
 * [get_campaign_v2](https://apidocs.sendinblue.com/campaign/#1)(int id) - Get specific campaign object
 * [create_campaign](https://apidocs.sendinblue.com/campaign/#2)(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list) - Create a campaign
 * [delete_campaign](https://apidocs.sendinblue.com/campaign/#3)(int id) - Delete a campaign
 * [update_campaign](https://apidocs.sendinblue.com/campaign/#4)(string id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list) - Update campaign information
 * [campaign_report_email](https://apidocs.sendinblue.com/campaign/#5)(int id, string lang, string email_subject, List`<string>` email_to, string email_content_type, List`<string>` email_bcc, List`<string>` email_cc, string email_body) - Sending reports to specific emails
 * [campaign_recipients_export](https://apidocs.sendinblue.com/campaign/#6)(int id, string notify_url, string type) - Export recipients of a campaign
 * [send_bat_email](https://apidocs.sendinblue.com/campaign/#7)(int id, List`<string>` email_to) - Send a test Email (bat)
 * [create_trigger_campaign](https://apidocs.sendinblue.com/campaign/#8)(string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, int recurring) - Create a trigger campaign
 * [update_trigger_campaign](https://apidocs.sendinblue.com/campaign/#9)(int id, string category, string from_name, string name, string bat_sent, string html_content, string html_url, List`<int>` listid, string scheduled_date, string subject, string from_email, string reply_to, string to_field, List`<int>` exclude_list, int recurring) - Update trigger campaign information
 * [share_campaign](https://apidocs.sendinblue.com/campaign/#10)(List`<int>` campaignids) - Get campaign share link
 * [update_campaign_status](https://apidocs.sendinblue.com/campaign/#11)(int id, string status) - Modify a campaign status
 * [get_folders](https://apidocs.sendinblue.com/folder/#1)() - Get list of all the folder details.
 * [get_folder](https://apidocs.sendinblue.com/folder/#2)(int id) - Get all the folder details for folder with id `<id>`
 * [create_folder](https://apidocs.sendinblue.com/folder/#3)(string name) - Create a folder
 * [delete_folder](https://apidocs.sendinblue.com/folder/#4)(int id) - Delete folder with folder id `<id>`
 * [update_folder](https://apidocs.sendinblue.com/folder/#5)(int id, string name) - Update folder with folder id `<id>`
 * [get_lists](https://apidocs.sendinblue.com/list/#1)() - Get all the lists
 * [get_list](https://apidocs.sendinblue.com/list/#2)(int id) - Get information about a list
 * [create_list](https://apidocs.sendinblue.com/list/#3)(string list_name, int list_parent) - Create a list
 * [delete_list](https://apidocs.sendinblue.com/list/#4)(int id) - Delete a list
 * [update_list](https://apidocs.sendinblue.com/list/#5)(int id, string list_name, int list_parent) - Updating a list
 * [display_list_users](https://apidocs.sendinblue.com/list/#8)(List`<int>` listids, int page, int page_limit) - Display details of all users for the given lists
 * [add_users_list](https://apidocs.sendinblue.com/list/#6)(int id, List`<string>` users) - Add users to a list
 * [delete_users_list](https://apidocs.sendinblue.com/list/#7)(int id, List`<string>` users) - Delete users from a list
 * [get_attributes](https://apidocs.sendinblue.com/attribute/#1)() - Listing all attributes
 * [get_attribute](https://apidocs.sendinblue.com/attribute/#2)(string id) - Listing a certain type attributes
 * [create_attribute](https://apidocs.sendinblue.com/attribute/#3)(string type, Dictionary`<string, string>` data) - Creating attributes
 * [delete_attribute](https://apidocs.sendinblue.com/attribute/#4)(string id, List`<string>` data) - Deleting attributes of the given type
 * [get_user](https://apidocs.sendinblue.com/user/#2)(string email) - Get information about a user/email
 * [create_update_user](https://apidocs.sendinblue.com/user/#1)(string email, Dictionary`<string, string>` attributes, int blacklisted, List`<int>` listid, List`<int>` listid_unlink, int blacklisted_sms) - Create/Update a user information
 * [delete_user](https://apidocs.sendinblue.com/user/#3)(string email) - Deleting user from db is not permitted but this action will unlink him from all lists
 * [import_users](https://apidocs.sendinblue.com/user/#4)(string url, List`<int>` listids, string notify_url, string name) - Import users/emails
 * [export_users](https://apidocs.sendinblue.com/user/#5)(string export_attrib, string filter, string notify_url) - Export users/emails
 * [get_processes](https://apidocs.sendinblue.com/process/#1)() - Get information about all background processes
 * [get_process](https://apidocs.sendinblue.com/process/#2)(int id) - Get information about a specific process
 * [get_senders](https://apidocs.sendinblue.com/sender-management/#1)(string option) - Get information about all/specific senders
 * [create_sender](https://apidocs.sendinblue.com/sender-management/#2)(string sender_name, string sender_email, List`<string>` ip_domain) - Create a sender
 * [delete_sender](https://apidocs.sendinblue.com/sender-management/#3)(int id) - Delete a sender
 * [update_sender](https://apidocs.sendinblue.com/sender-management/#4)(int id, string sender_name, string sender_email, List`<string>` ip_domain) - Update a sender

### SMTP calls

 * [get_report](https://apidocs.sendinblue.com/report/)(int limit, string start_date, string end_date, int offset, string date, int days, string email) - Retrieve information for all report events
 * [get_statistics](https://apidocs.sendinblue.com/statistics/)(int aggregate, string tag, int days, string end_date, string start_date) - Get aggregate statistics about emails sent
 * [get_webhooks](https://apidocs.sendinblue.com/webhooks/#1)() - List registered webhooks
 * [get_webhook](https://apidocs.sendinblue.com/webhooks/#2)(int id) - Get information about a webhook
 * [create_webhook](https://apidocs.sendinblue.com/webhooks/#3)(string url, string description, List`<string>` events) - Registering a webhook
 * [delete_webhook](https://apidocs.sendinblue.com/webhooks/#5)(int id) - Deleting a webhook
 * [update_webhook](https://apidocs.sendinblue.com/webhooks/#4)(int id, string url, string description, List`<string>` events) - Editing a webhook
 * [delete_bounces](https://apidocs.sendinblue.com/bounces/)(string start_date, string end_date, string email) - Deleting bounces
 * [send_email](https://apidocs.sendinblue.com/tutorial-sending-transactional-email/)(Dictionary`<string, string>` to, string subject, List`<string>` from_name, string html, string txt, Dictionary`<string, string>` cc, Dictionary`<string, string>` bcc, List`<string>` replyto, Dictionary`<string,string>` attachment, Dictionary`<string, string>` headers) - Sending out a transactional email
 * [send_transactional_template](https://apidocs.sendinblue.com/template/)(int id, string to, string cc, string bcc, Dictionary`<string, string>` attr) - Send templates created on Sendinblue, through Sendinblue smtp.
 * [create_template](https://apidocs.sendinblue.com/template/#2)(string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status) - Create a template 
 * [update_template](https://apidocs.sendinblue.com/template/#3)(int id, string from_name, string name, string bat_sent, string html_content, string html_url, string subject, string from_email, string reply_to, string to_field, int status) - Update template information

### SMS call

 * [send_sms](https://apidocs.sendinblue.com/mailin-sms/#1)(string to, string from_name, string text, string web_url, string tag, string type) - Sending a SMS
 * [create_sms_campaign](https://apidocs.sendinblue.com/mailin-sms/#2)(string camp_name, string sender, string content, string bat_sent, List`<int>` listids, List`<int>` exclude_list, string scheduled_date) - Create a SMS campaign
 * [update_sms_campaign](https://apidocs.sendinblue.com/mailin-sms/#3)(int id, string camp_name, string sender, string content, string bat_sent, List`<int>` listids, List`<int>` exclude_list, string scheduled_date) - Update a SMS campaign
 * [send_bat_sms](https://apidocs.sendinblue.com/mailin-sms/#4)(int id, string mobilephone) - Send a test SMS campaign