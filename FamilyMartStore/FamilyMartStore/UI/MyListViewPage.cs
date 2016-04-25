using FamilyMartStore.Model;
using FamilyMartStore.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace FamilyMartStore.UI
{
    public class MyListViewPage : ContentPage
    {
        private Picker cityPicker;
        private Picker areaPicker;
        private List<FamilyStore> myStoreDataList;
        private readonly WebApiService myWebApiService;
        public MyListViewPage(string title)
        {
            myStoreDataList = new List<FamilyStore>();
            myWebApiService = new WebApiService();
            Title = title;

            SetUI();
            SetEvent();
            SetListView();
        }

        #region 初始化 UI 事件和設定資料
        /// <summary>
        /// 設定 UI 和初始資料
        /// </summary>
        private void SetUI()
        {
            cityPicker = new Picker
            {
                Title = "請選擇城市",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            var cityList = new string[]{
                "宜蘭縣","花蓮縣","台東縣","基隆市","台北市",
                "新北市","桃園市","新竹市","新竹縣","苗栗縣",
                "雲林縣","嘉義市","嘉義縣","台南市","高雄市",
                "屏東縣","澎湖縣","金門縣","台中市","彰化縣",
                "南投縣" };

            foreach (var city in cityList)
            {
                cityPicker.Items.Add(city);
            }

            areaPicker = new Picker
            {
                Title = "請選擇行政區域",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            areaPicker = new Picker
            {
                Title = "請選擇行政區域",
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
        }
        /// <summary>
        /// 設定 UI 事件
        /// </summary>
        private void SetEvent()
        {
            // 城市下拉選單
            cityPicker.SelectedIndexChanged += async (sender, e) =>
            {
                if (cityPicker.SelectedIndex != -1)
                {
                    var areaData = await myWebApiService.GetAreaDataAsync(cityPicker.Items[cityPicker.SelectedIndex]);
                    var areaList = JsonConvert.DeserializeObject<List<AreaData>>(areaData);

                    areaPicker.Items.Clear();

                    foreach (var area in areaList)
                    {
                        areaPicker.Items.Add(area.town);
                    }

                    SetListView();
                }
            };
            // 搜尋按鈕
            areaPicker.SelectedIndexChanged += async (sender, e) =>
            {
                if (areaPicker.SelectedIndex != -1)
                {
                    var city = cityPicker.Items[cityPicker.SelectedIndex];
                    var crea = areaPicker.Items[areaPicker.SelectedIndex];
                    var resultData = await myWebApiService.GetFamilyStoreDataAsync(city, crea);
                    myStoreDataList = JsonConvert.DeserializeObject<List<FamilyStore>>(resultData);

                    Debug.WriteLine(myStoreDataList.Count);
                    SetListView();
                }
            };
        }
        /// <summary>
        /// 設定 listView 資料
        /// </summary>
        private void SetListView()
        {
            var listView = new ListView
            {
                IsPullToRefreshEnabled = true,
                RowHeight = 80,
                ItemsSource = myStoreDataList.Select(X => new StoreData()
                {
                    Name = X.NAME,
                    Address = X.addr,
                    Tel = X.TEL
                }),
                ItemTemplate = new DataTemplate(typeof(MyListViewCell))
            };


            listView.ItemTapped += (sender, e) =>
            {
                var baseUrl = "https://www.google.com.tw/maps/place/";
                var storeData = e.Item as StoreData;

                if (storeData != null)
                    Device.OpenUri(new Uri($"{baseUrl}{storeData.Address}"));

                ((ListView)sender).SelectedItem = null;
            };

            Padding = new Thickness(0, 20, 0, 0);
            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    cityPicker,
                    areaPicker,
                    new Label
                    {
                        HorizontalTextAlignment= TextAlignment.Center,
                        Text = Title,
                        FontSize = 30
                    },
                    listView
                }
            };
        }
        #endregion 初始化 UI 和設定資料
    }
}
