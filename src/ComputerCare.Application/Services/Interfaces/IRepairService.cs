using ComputerCare.Application.DTOs.Common;
using ComputerCare.Application.DTOs.Repair;
using ComputerCare.Domain.Enums;

namespace ComputerCare.Application.Services.Interfaces;

public interface IRepairService
{
    Task<ResponseDto<RepairRequestDto>> GetByIdAsync(Guid id);
    Task<ResponseDto<IEnumerable<RepairRequestDto>>> GetByCustomerIdAsync(Guid customerId);
    Task<ResponseDto<IEnumerable<RepairRequestDto>>> GetByStatusAsync(RepairStatus status);
    Task<ResponseDto<RepairRequestDto>> CreateAsync(CreateRepairRequestDto dto);
    Task<ResponseDto<RepairRequestDto>> UpdateStatusAsync(Guid id, RepairStatus status);
    Task<ResponseDto<RepairRequestDto>> AssignEmployeeAsync(Guid requestId, Guid employeeId);
    Task<ResponseDto<RepairRequestDto>> AddQuoteAsync(RepairQuoteDto dto);
}
