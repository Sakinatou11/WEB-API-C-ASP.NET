using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebAPI.Models;

namespace ClientWPFApi.ControllersAPI
{
    public sealed class API
    {
        private static readonly HttpClient client = new HttpClient();

        private API()
        {
            client.BaseAddress = new Uri("https://localhost:7076/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static readonly object padlock = new object();
        private static API? instance = null;

        public static API Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new API();
                    }
                    return instance;
                }
            }
        }

        public async Task<ICollection<Avis>> GetAvisAsync()
        {
            ICollection<Avis> avisList = new List<Avis>();
            HttpResponseMessage response = client.GetAsync("api/avis").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                avisList = JsonSerializer.Deserialize<List<Avis>>(resp);
            }
            return avisList;
        }

        public async Task<Avis?> GetAvisAsync(Guid? id)
        {
            Avis? avis = null;
            HttpResponseMessage response = client.GetAsync("api/avis/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                avis = JsonSerializer.Deserialize<Avis>(resp);
            }
            return avis;
        }

        public async Task<Uri?> AddAvisAsync(Avis avis)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/avis", avis);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Uri?> UpdateAvisAsync(Avis avis)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/avis/" + avis.Id, avis);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteAvisAsync(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/avis/" + id);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            ICollection<Product> products = new List<Product>();
            HttpResponseMessage response = client.GetAsync("api/products").Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                products = JsonSerializer.Deserialize<List<Product>>(resp);
            }
            return products;
        }

        public async Task<Product?> GetProductAsync(Guid? id)
        {
            Product? product = null;
            HttpResponseMessage response = client.GetAsync("api/products/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                var resp = await response.Content.ReadAsStringAsync();
                product = JsonSerializer.Deserialize<Product>(resp);
            }
            return product;
        }

        public async Task<Uri?> AddProductAsync(Product product)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsJsonAsync("api/products", product);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<Uri?> UpdateProductAsync(Product product)
        {
            try
            {
                HttpResponseMessage response = await client.PutAsJsonAsync("api/products/" + product.Id, product);
                response.EnsureSuccessStatusCode();
                return response.Headers.Location;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync("api/products/" + id);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
