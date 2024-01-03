using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Endpoint da API GoRest para criar um novo usuário
        string endpoint = "https://gorest.co.in/public-api/users";

        // Token de autorização
        string token = "d11bc3c344efe3e9863e5b594eb436707bda382e032b78821fcfb85c0173afd0";

        // Dados do novo usuário para serem enviados no corpo da solicitação
        var dados = new
        {
            name = "Francielly Pedroso",
            email = "franciellyfp@gmail.com",
            gender = "female",
            status = "active"
        };

        try
        {
            // Cria um cliente HTTP para enviar a solicitação
            using (HttpClient client = new HttpClient())
            {
                // Adiciona o token de autorização
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                // Converte  para formato JSON
                var jsonContent = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(dados), System.Text.Encoding.UTF8, "application/json");

                // Envia a requisição POST
                HttpResponseMessage response = await client.PostAsync(endpoint, jsonContent);

                // Printa o código de status da resposta
                Console.WriteLine($"Status Code: {response.StatusCode}");


                // Verifica se a solicitação foi sucesso
                if (response.IsSuccessStatusCode)
                {
                    // Lê a resposta como uma string
                    string responseBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Retorno da API:");
                    Console.WriteLine(responseBody);
                }
                else
                {
                    // Se não der sucesso printa o motivo do erro
                    string errorReason = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Erro na solicitação. Motivo: {errorReason}");
                }
            }
    
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}