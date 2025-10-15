namespace DkGLobalPortfolio.WebApi.Models.Profile.Dto
{
    public class CreateBankInfoDto
    {
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string swift { get; set; }
        public string BinNo { get; set; }
        public string ErcNo { get; set; }
    }
}
