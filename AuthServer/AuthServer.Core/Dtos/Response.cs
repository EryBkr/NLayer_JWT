using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AuthServer.Core.Dtos
{
    //API dönüş tipimiz olacaktır
    public class Response<T> where T : class
    {
        //private set sadece yapıcıda set edileceğini belirler

        public T Data { get; private set; }
        public int StatusCode { get; private set; }

        //Hata Sınıfımıda ekledim
        public ErrorDto Error { get; private set; }

        //Sadece Developer in kontrol edeceği bir property
        [JsonIgnore] //Serilaize olmaması için ekledik
        public bool IsSuccessful { get; private set; }

        //Başarı durumunda (Data dönülecekse)
        public static Response<T> Success(T data, int statusCode)
        {
            return new Response<T>
            {
                Data = data,
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }

        //Başarı durumunda (Data dönülmeyecekse)
        public static Response<T> Success(int statusCode)
        {
            return new Response<T>
            {
                Data = default,//default null olarakta düşünebiliriz.onun tipine göre dönüş alacaktır
                StatusCode = statusCode,
                IsSuccessful = true
            };
        }

        //Hata durumunda
        public static Response<T> Fail(ErrorDto errorDto, int statusCode)
        {

            return new Response<T>
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }

        //Hata durumunda (Tek bir hata mesajı var ise)
        public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
        {
            //Tek bir hata dönecek ErrorDto tanımı
            var errorDto = new ErrorDto(errorMessage, isShow);

            return new Response<T>
            {
                Error = errorDto,
                StatusCode = statusCode,
                IsSuccessful = false
            };
        }
    }
}
