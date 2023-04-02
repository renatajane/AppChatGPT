using ChatApp.Classes;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ChatApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {  
           Processar();
        }

        public void Processar()
        {
            button1.Text = "Aguarde...";
            button1.Enabled = false;
            Request request = new Request();
            request.model = "gpt-3.5-turbo";
            request.messages = new Classes.Message[1];
            request.messages[0] = new Classes.Message();
            request.messages[0].role = "user";
            request.messages[0].content = txtPergunta.Text;
            string json = JsonConvert.SerializeObject(request);

            var client = WebRequest.Create("https://api.openai.com/v1/chat/completions");
            client.Headers = new WebHeaderCollection();
            client.Headers["Content-Type"] = "application/json";
            client.Headers["Authorization"] = "Bearer SuaChaveAqui";
            client.Method = "POST";

            using (var streamWriter = new StreamWriter(client.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            Response response = null;
            var httpresponse = (HttpWebResponse)client.GetResponse();
            using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                response = JsonConvert.DeserializeObject<Response>(result);

            }
            StringBuilder t = new StringBuilder();
            t.AppendLine(response.choices[0].message.content);
            txtResposta.Text += t.ToString();

            txtPergunta.Text = "";
            button1.Text = "Enviar";
            button1.Enabled = true;
        }
    }
}
