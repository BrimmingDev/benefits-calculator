using Api.Models;

namespace Api.ApiModels;

public class GetPaystubDTO
{
    public int Id { get; private set; }
    public decimal GrossPay { get; private set; }
    public decimal BenefitsCost { get; private set; }
    public decimal NetPay { get; private set; }

    public static GetPaystubDTO FromPaystub(PayStub payStub)
    {
        return new GetPaystubDTO
        {
            Id = payStub.Id,
            GrossPay = payStub.Gross,
            BenefitsCost = payStub.BenefitsCost,
            NetPay = payStub.Net
        };
    }
}