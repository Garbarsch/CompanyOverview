using Azure;

public interface ICompanyRepository 
{

    Task<(Response,CompanyDTO)> Create(CompanyCreateDTO company);
    Task<CompanyDTO?> Read(int companyId);
    Task<IReadOnlyCollection<CompanyDTO>> Read();
    Task<Response> Update(CompanyUpdateDTO company);
    Task<Response> Delete(int companyId);
    }