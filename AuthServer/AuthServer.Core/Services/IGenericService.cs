using AuthServer.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Core.Services
{
    //Abstract Services
    public interface IGenericService<TEntity,TDto> where TEntity:class where TDto:class
    {
        //Shared tarafında oluşturmuş olduğumuz Response leri dönüyoruz
        Task<Response<TDto>> GetByIdAsync(int id);

        Task<Response<IEnumerable<TDto>>> GetAllAsync();

        //TEntity parametresi alan geriye bool return eden bir FuncDelege tanımlıyoruz
        //Sorguları finalde göndermek için IQueryable tanımladım
        Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate);

        //DB de oluşturduğum datayı dto ya çevirip response ile paketleyip dönüyorum
        Task<Response<TDto>> AddAsync(TEntity entity);

        //Asenkron metodu olmadığı için bu şekilde kullandık
        //Response dönebilmek için NoData adında boş bir class ekledim
        Response<NoData> Remove(TEntity entity);

        //Güncelleme işleminden sonra Response NoData dönüyorum
        //Asenkron metodu olmadığı için bu şekilde kullandık
        Response<NoData> Update(TEntity entity);

       
    }
}
