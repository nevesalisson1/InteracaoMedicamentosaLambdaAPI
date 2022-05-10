namespace Interacao.Infrasctructure.Http
{
    public static class HttpClientDrugInteraction
    {
        private static HttpClient _client;
        private static readonly string urlBase = "https://rxnav.nlm.nih.gov/REST";

        public static string RealizarRequisiscao(string req)
        {
            if(_client == null)
            {
                _client = new HttpClient();
            }
            
            Uri urlDeRequisiscao = new Uri($"{urlBase}{req}");

            var result = _client.GetAsync(urlDeRequisiscao).Result.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}