namespace KuyumcuWebApi.dto;

public class GetAllOrderRequestDto : PagedRequestDto {
    public int StatusId { get; set; }
}