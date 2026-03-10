using Employee_Offboarding.Application.DTOs.Pdf;

namespace Employee_Offboarding.Application.Abstractions.Services
{
    public interface IPdfService
    {
        byte[] GeneratePdf(PdfFormDataDto formData);
    }
}
