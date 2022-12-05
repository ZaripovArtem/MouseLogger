using System.Windows;
using RestSharp;

namespace MouseLogger.Services
{
    public class SendWhattsAPP
    {
        public void Send(string mail)
        {
            try
            {
                string instanceId = "Your_Instance_Id";
                string token = "Your_Instance_Token";
                string mobile = "Number_To_Send_Message";
                var url = "https://api.ultramsg.com/" + instanceId + "/messages/chat";
                var client = new RestClient(url);
                var request = new RestRequest(url, Method.Post);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("token", token);
                request.AddParameter("to", mobile);
                request.AddParameter("body", mail);

                RestResponse response = client.Execute(request);
            }
            catch
            {
                MessageBox.Show("Ошибка с отправкой на почту. Настройте файл SendWhattsAPP");
            }
        }
    }
}
