using AuthServer.Core.Dtos;
using AuthServer.Core.Repositories;
using AuthServer.Core.Services;
using AuthServer.Core.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthServer.Service.Services
{
    public class GenericService<TEntity, TDto> : IGenericService<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _uOw;
        private readonly IGenericRepository<TEntity> _genericRepo;

        public GenericService(IUnitOfWork uOw, IGenericRepository<TEntity> genericRepo)
        {
            _uOw = uOw;
            _genericRepo = genericRepo;
        }

        public async Task<Response<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            await _genericRepo.AddAsync(newEntity);
            await _uOw.CommitAsync();
            var newDto = ObjectMapper.Mapper.Map<TDto>(newEntity);
            return Response<TDto>.Success(newDto, 200);
        }

        public async Task<Response<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = ObjectMapper.Mapper.Map<List<TDto>>(await _genericRepo.GetAllAsync());
            return Response<IEnumerable<TDto>>.Success(entities, 200);
        }

        public async Task<Response<TDto>> GetByIdAsync(int id)
        {
            var entity = ObjectMapper.Mapper.Map<TDto>(await _genericRepo.GetByIdAsync(id));

            if (entity == null)
                return Response<TDto>.Fail("Id Not Found", 404, true);

            var newDto = ObjectMapper.Mapper.Map<TDto>(entity);
            return Response<TDto>.Success(newDto, 200);
        }

        public Response<NoData> Remove(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepo.Remove(newEntity);
            _uOw.Commit();

            return Response<NoData>.Success(200);
        }

        public Response<NoData> Update(TDto entity)
        {
            var newEntity = ObjectMapper.Mapper.Map<TEntity>(entity);
            _genericRepo.Update(newEntity);
            _uOw.Commit();

            return Response<NoData>.Success(204);
        }

        public async Task<Response<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepo.Where(predicate);

            return Response<IEnumerable<TDto>>.Success(ObjectMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
