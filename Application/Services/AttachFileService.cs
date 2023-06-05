using Application.IServices;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class AttachFileService : IAttachFileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttachFileService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CreateAsync(AttachFile attachFile)
        {
            await _unitOfWork.AttachFileRepo.AddAsync(attachFile);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CreateListAsync(List<AttachFile> attachFileList)
        {
            await _unitOfWork.AttachFileRepo.AddRangeAsync(attachFileList);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var itemToDelete = await _unitOfWork.AttachFileRepo.GetByIdAsync(id);
            await _unitOfWork.AttachFileRepo.RemoveAsync(itemToDelete);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AttachFile> GetByIdAsync(int id)
        {
            var item = await _unitOfWork.AttachFileRepo.GetByIdAsync(id);
            return item;
        }

        public async Task<List<AttachFile>> GetListAsync()
        {
            var item = await _unitOfWork.AttachFileRepo.GetAllAsync();
            return item;
        }

        public async Task UpdateAsync(AttachFile attachFile)
        {
            _unitOfWork.AttachFileRepo.Update(attachFile);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
