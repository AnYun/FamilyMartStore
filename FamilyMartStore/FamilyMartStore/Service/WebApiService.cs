using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FamilyMartStore.Service
{
    /// <summary>
    /// 存取全家 Api
    /// </summary>
    public class WebApiService
    {
        private HttpClient _httpClient;

        public WebApiService()
        {
        }
        /// <summary>
        /// 取得全家店鋪資料
        /// </summary>
        /// <param name="city">城市</param>
        /// <param name="area">區域</param>
        /// <returns></returns>
        public async Task<string> GetFamilyStoreDataAsync(string city, string area)
        {
            var url = "http://api.map.com.tw/net/familyShop.aspx?searchType=ShopList&type=&city=" + city + "&area=" + area + "&road=&fun=showStoreList&key=6F30E8BF706D653965BDE302661D1241F8BE9EBC";
            return await GetDataAsync(url);
        }
        /// <summary>
        /// 取得鄉鎮區域資料
        /// </summary>
        /// <param name="city">城市</param>
        /// <returns></returns>
        public async Task<string> GetAreaDataAsync(string city)
        {
            var url = "http://api.map.com.tw/net/familyShop.aspx?searchType=ShowTownList&type=&city=" + city + "&fun=storeTownList&key=6F30E8BF706D653965BDE302661D1241F8BE9EBC";
            return await GetDataAsync(url);
        }
        /// <summary>
        /// 存取全家 Api
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private async Task<string> GetDataAsync(string url)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("Referer", "http://www.family.com.tw/marketing/inquiry.aspx");
            _httpClient.DefaultRequestHeaders.Add("Host", "api.map.com.tw");

            var response = await _httpClient.GetAsync(url);

            var responseAsString = await response.Content.ReadAsStringAsync();


            responseAsString = responseAsString.Remove(0, 14);
            responseAsString = responseAsString.Substring(0, responseAsString.Length - 1);
            Debug.WriteLine(responseAsString);

            return responseAsString;
        }
    }
}
