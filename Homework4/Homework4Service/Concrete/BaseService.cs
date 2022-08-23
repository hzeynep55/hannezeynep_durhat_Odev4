﻿using AutoMapper;
using Homework4Base;
using Homework4Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4Service
{
    public abstract class BaseService<Dto, Entity> : IBaseService<Dto, Entity> where Entity : class
    {
        private readonly IBaseRepository<Entity> baseRepository;
        protected readonly IMapper Mapper;
        protected readonly IUnitOfWork UnitOfWork;

        public BaseService(IBaseRepository<Entity> baseRepository, IMapper mapper, IUnitOfWork unitOfWork) : base()
        {
            this.baseRepository = baseRepository;
            this.Mapper = mapper;
            this.UnitOfWork = unitOfWork;
        }

        public virtual async Task<BaseResponse<IEnumerable<Dto>>> GetAllAsync()
        {
            var tempEntity = await baseRepository.GetAllAsync();
            var result = Mapper.Map<IEnumerable<Entity>, IEnumerable<Dto>>(tempEntity);

            return new BaseResponse<IEnumerable<Dto>>(result);
        }

        public virtual async Task<BaseResponse<Dto>> GetByIdAsync(int id)
        {
            var tempEntity = await baseRepository.GetByIdAsync(id);
            var result = Mapper.Map<Entity, Dto>(tempEntity);

            return new BaseResponse<Dto>(result);
        }

        public virtual async Task<BaseResponse<Dto>> InsertAsync(Dto insertResource)
        {
            try
            {
                var tempEntity = Mapper.Map<Dto, Entity>(insertResource);

                await baseRepository.InsertAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new BaseResponse<Dto>(Mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Saving_Error", ex);
            }
        }

        public virtual async Task<BaseResponse<Dto>> RemoveAsync(int id)
        {
            try
            {
                var tempEntity = await baseRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new BaseResponse<Dto>("Id_NoData");

                baseRepository.RemoveAsync(tempEntity);
                await UnitOfWork.CompleteAsync();

                return new BaseResponse<Dto>(Mapper.Map<Entity, Dto>(tempEntity));
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Deleting_Error", ex);
            }
        }

        public virtual async Task<BaseResponse<Dto>> UpdateAsync(int id, Dto updateResource)
        {
            try
            {
                var tempEntity = await baseRepository.GetByIdAsync(id);
                if (tempEntity is null)
                    return new BaseResponse<Dto>("NoData");
                Mapper.Map(updateResource, tempEntity);

                await UnitOfWork.CompleteAsync();
                var resource = Mapper.Map<Entity, Dto>(tempEntity);

                return new BaseResponse<Dto>(resource);
            }
            catch (Exception ex)
            {
                throw new MessageResultException("Updating_Error", ex);
            }
        }
    }
}
