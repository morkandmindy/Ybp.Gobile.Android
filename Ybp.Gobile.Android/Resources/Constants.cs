﻿using System.Net;

namespace Ybp.Gobile.Android
{
    public static class Constants
    {
        public static string BASE_URL = "http://172.27.14.117:8080/hx/ajax.ashx?location=";
        public static string LOGIN_URL = BASE_URL + "login";
        public static string SEARCH_URL = BASE_URL + "isbn_search";
        public static CookieContainer CookieContainer = new CookieContainer();
        public static string JSON_CONTENT = "application/json; charset=utf-8";
        public static string FORM_DATA = "application/x-www-form-urlencoded";
        public static string BOOK_COVER_VIEW_URL ="https://contentcafe2.btol.com/ContentCafe/Jacket.aspx?UserID=YBP&Password=Yankee&Return=1&Type=L&Value=";
        public static string ADD_TO_CART_SUCCESS = "Added to Cart!";
        public static string ADD_TO_CART_ERROR = "Error encountered while adding this item to cart";
    }
}
