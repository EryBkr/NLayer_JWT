using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Shared.Dtos
{
    public class ErrorDto
    {
        //Bu Propertyler e sadece Yapıcı metotlarla değer atanabilir
        public List<string> Errors { get; private set; }
        //Kullanıcıya Hatayı Gösterecek miyim
        public bool IsShow { get; private set; }

        public ErrorDto()
        {
            Errors = new List<string>();
        }

        public ErrorDto(string error,bool isShow)
        {
            Errors.Add(error);
            IsShow = isShow;
        }

        public ErrorDto(List<string> errors, bool isShow)
        {
            Errors = errors;
            IsShow = isShow;
        }
    }
}
